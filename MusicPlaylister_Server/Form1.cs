using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlaylister_Server
{


    public partial class ServerForm : Form
    {
        //subform
        static Label labelUser = new Label();
        static Label labelPassword = new Label();
        static Button create = new Button();
        static Button cancel = new Button();
        static TextBox userTB = new TextBox();
        static TextBox passwordTB = new TextBox();
        static Form newUserForm = new Form();

        //mainform

        static string sharepath = Directory.GetCurrentDirectory() + "\\WorkingDirectory\\Client";
        static string computername = Environment.MachineName.ToString();
        static string networkpath = @"\\" + computername + "\\" + sharepath.Substring(3);

        static ArrayList users = new ArrayList();
        ArrayList songs = new ArrayList();
        private delegate void SafeCallDelegate(string text);
        private static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 100;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];
        static string userpath = Directory.GetCurrentDirectory() + "\\WorkingDirectory\\Server\\users.txt";
        public ServerForm()
        {
            InitializeComponent();
            labelIPFeedback.Text = "Server IP is : " + Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(); //wants to provide ipv6 as it says this ipv4 way is obsolete 
        }

        private void buttonHostOn_Click(object sender, EventArgs e)
        {
            buttonHostOff.Enabled = true;//enable/disable buttons accordingly
            buttonHostOn.Enabled = false;
            SetupServer();
        }
        void SongToList()
        {
            songs.Clear();
            listBoxSongs.Items.Clear();
            DirectoryInfo songfolder = new DirectoryInfo(sharepath);
            FileInfo[] mp3files = songfolder.GetFiles("*.mp3");//get all mp3s
            foreach (FileInfo mp3 in mp3files)
            {
                songs.Add(mp3);
                listBoxSongs.Items.Add(mp3);
            }
        }
        private void ReceiveCallback(IAsyncResult AR)//receive/catch states, messages or orders from the server in this method
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();
                clientSockets.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            WriteFeedbackSafe(text);
            string[] split = text.Split(',');//0 is ip, 1 is username, 2 is password
            ///hashing goes here
            ///if admin users file one does not exist - prompt client to talk to admin
            ///refer to user files in server folder, load users in file to arraylist


            if (users.Count == 0) //no users exist cannot log anyone in
            {

                byte[] data = Encoding.ASCII.GetBytes("*nousers*");
                current.Send(data);
                WriteFeedbackSafe("No users created, client denied access.");
                current.Shutdown(SocketShutdown.Both);
                current.Close();
                clientSockets.Remove(current);
                return;
            }
            else if (text == "*update*") // Client authorisation
            {
                SongToList();
                clientSockets.Remove(current);
                return;
            }
            else if (split.Length == 3) // Client authorisation
            {
                byte[] data = Encoding.ASCII.GetBytes("");
                if (Authorise(split[1], split[2]))//authorise user login
                {
                    data = Encoding.ASCII.GetBytes("*allowuser*," + networkpath);//allowuser + filesharepath
                    WriteFeedbackSafe("Client authorised");
                }
                else
                {
                    data = Encoding.ASCII.GetBytes("*denyuser*");
                    WriteFeedbackSafe("Client unauthorised");
                }

                current.Send(data);
                clientSockets.Remove(current);
                return;
            }
            else
            {
                Console.WriteLine("Text is an invalid request");
                byte[] data = Encoding.ASCII.GetBytes("Invalid request");
                current.Send(data);
                Console.WriteLine("Warning Sent");
            }

            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }


        private static void CloseAllSockets()
        {
            foreach (Socket socket in clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            serverSocket.Close();
        }
        private void SetupServer()
        {
            richTextBoxFeedback.Text += "Setting up server...\n";
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
            richTextBoxFeedback.Text += "Server setup complete\n";
        }
        private void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = serverSocket.EndAccept(AR);

            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            WriteFeedbackSafe("Client connected\n");
            serverSocket.BeginAccept(AcceptCallback, null);
        }
        private void WriteFeedbackSafe(string text)
        {
            if (richTextBoxFeedback.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteFeedbackSafe);
                richTextBoxFeedback.Invoke(d, new object[] { text });
            }
            else
            {
                richTextBoxFeedback.Text += text;
            }
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {

            //if working directory doesnt exist create it
            //separate for server only access and client sharefolder
            System.IO.Directory.CreateDirectory("WorkingDirectory\\Server");
            System.IO.Directory.CreateDirectory("WorkingDirectory\\Client");
            //ensure client working directory is made as a share folder.

            DirectorySecurity sec = Directory.GetAccessControl(sharepath);
            //Using this instead of the "Everyone" string means we work on non-English systems.
            //tested this share solution over my own LAN network - it works with all firewalls settings default.
            SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Modify | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            Directory.SetAccessControl(sharepath, sec);
            //create user file if none and after, if already is one load
            if (!File.Exists(userpath))
            {
                SaveFiles("user");
            }
            else
            {
                LoadFiles();
            }
            RefreshUsers();
            SongToList();
        }

        private void ServerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseAllSockets();
        }
        private static void SaveFiles(string whichfile)
        {
            ///////////////////////////////user files only\\\\\\\\\\\\\\\\\\\\\\\\\\
            if (whichfile.ToLower().CompareTo("user") == 0) //save only user files
            {
                if (File.Exists(userpath))//delete first if exists before adding data
                {
                    using (new FileStream(userpath, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose))
                    {
                    }
                }
                FileStream stream = null;
                try
                {
                    stream = new FileStream(userpath, FileMode.Append);
                    using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8))
                    {
                        int size = users.Count;
                        sw.WriteLine(size);
                        foreach (User user in users)
                        {
                            sw.WriteLine(user.Username);
                            sw.WriteLine(user.Salt);
                            sw.WriteLine(user.Passwordhash);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                stream.Close();
            }
            ////////////////////////////////all files\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            else if (whichfile.ToLower().CompareTo("all") == 0)
            {
                if (File.Exists(userpath))//delete first if exists before adding data
                {
                    using (new FileStream(userpath, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose))
                    {
                    }
                }
                FileStream stream = null;
                try
                {
                    stream = new FileStream(userpath, FileMode.Append);
                    using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8))
                    {
                        int size = users.Count;
                        sw.WriteLine(size);
                        foreach (User user in users)
                        {
                            sw.WriteLine(user.Username);
                            sw.WriteLine(user.Salt);
                            sw.WriteLine(user.Passwordhash);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                stream.Close();
            }
            else
            {
                MessageBox.Show("Method SaveFiles, doesnt know which files you want to save.");
            }


        }
        private static void LoadFiles()
        {
            using (StreamReader reader = new StreamReader(userpath))
            {
                int size = Int32.Parse(reader.ReadLine());

                for (int i = 0; i < size; i++)
                {
                    string username = reader.ReadLine();
                    string salting = reader.ReadLine();
                    string passhash = reader.ReadLine();
                    User temp = new User(username, passhash, salting);
                    users.Add(temp);
                }
                reader.Close();

            }
        }

        private void buttonNewUser_Click(object sender, EventArgs e) //opens new form
        {

            labelUser.Text = "Username";
            labelPassword.Text = "Password";
            create.Text = "Create";
            cancel.Text = "Cancel";
            create.Location = new Point(50, 180);
            cancel.Location = new Point(160, 180);


            labelUser.Location = new Point(85, 45);
            userTB.Location = new Point(85, 70);

            labelPassword.Location = new Point(85, 95);
            passwordTB.Location = new Point(85, 120);

            newUserForm.Controls.Add(create);
            newUserForm.Controls.Add(cancel);
            newUserForm.Controls.Add(labelUser);
            newUserForm.Controls.Add(labelPassword);
            newUserForm.Controls.Add(userTB);
            newUserForm.Controls.Add(passwordTB);

            create.Click += new System.EventHandler(this.btn_Create_Click); //button eventhandlers
            cancel.Click += new System.EventHandler(this.btn_Cancel_Click);

            newUserForm.ShowDialog(this);//show vs showdialog = show will dispose from the memory after close and showdialog will not.
        }
        void btn_Create_Click(object sender, EventArgs e) //attempt connect to IPAdress
        {
            //create user, add to list and save file
            User nuuUser = new User(userTB.Text, passwordTB.Text);
            users.Add(nuuUser);
            SaveFiles("user");
            newUserForm.Close();
            RefreshUsers();
        }
        void btn_Cancel_Click(object sender, EventArgs e) //Close all forms
        {
            newUserForm.Close();
        }
        static bool Authorise(string username, string password)
        {
            foreach (User user in users)
            {
                //check every user for username
                if (username == user.Username)
                {
                    //userexists-checkpassword
                    if (user.LogIn(password))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            return false;
        }
        void RefreshUsers()
        {
            foreach (User user in users)
            {
                listBoxUsers.Items.Add(user.Username);
            }

        }

    }
}


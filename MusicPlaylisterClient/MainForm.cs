using NAudio.CoreAudioApi;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlaylisterClient
{
    public partial class FormClient : Form
    {

        ArrayList playlistitles = new ArrayList();//stringonly
        WMPLib.IWMPPlaylist currentplaylist;
        WMPLib.IWMPMedia audiofile;

        string ipaddress = "";
        string username = "";
        string password = "";

        static Label playlistLB = new Label();
        static TextBox playlistTB = new TextBox();

        static TextBox networkIP = new TextBox();
        static TextBox user = new TextBox();
        static TextBox Password = new TextBox();

        List<LinkedList<string>> allPlaylists = new List<LinkedList<string>>();
        static ArrayList songs = new ArrayList();
        static string networkpath = "";

        public static Form playlist = new Form();
        public static Form loginScreen = new Form();

        static IPAddress serverip;
        private const int PORT = 100;
        private static Socket ClientSocket = new Socket
                (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static string clientusername;//once successfully logged in the user will retrieve their username.
        public static string request = "";//this string sets the orders for sending to server
        public FormClient()
        {
            InitializeComponent();
            progressBar1.ForeColor = Color.IndianRed;
            progressBar2.ForeColor = Color.ForestGreen;
            this.treeViewPlaylists.HideSelection = false;
        }


        void SongToList()
        {
            songs.Clear();
            listBoxSongs.Items.Clear();
            DirectoryInfo songfolder = new DirectoryInfo(networkpath);
            FileInfo[] mp3files = songfolder.GetFiles("*.mp3");//get all mp3s
            foreach (FileInfo mp3 in mp3files)
            {
                songs.Add(mp3.Name);
            }
            DivideList(songs, songs.Count);//MERGE SORTING LIST
            foreach (String songname in songs)
            {
                listBoxSongs.Items.Add(songname);
            }
            //set suggestions source for audio search
            var sourcesong = new AutoCompleteStringCollection();
            sourcesong.AddRange((string[])songs.ToArray(typeof(string)));
            textBox1.AutoCompleteCustomSource = sourcesong;

            //set suggestions source for playlist search, all items that are worked with
            var sourcelist = new AutoCompleteStringCollection();
            sourcelist.AddRange((string[])songs.ToArray(typeof(string)));
            textBox2.AutoCompleteCustomSource = sourcelist;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ///on form load disable mainform and present log in screen, if log in successful enable main form and close this one.
            ///
            this.Enabled = false;
            Label labelnetworkIP = new Label();
            Label labelUser = new Label();
            Label labelPassword = new Label();

            Button connect = new Button();
            Button cancel = new Button();

            labelnetworkIP.Text = "IP Address";
            labelUser.Text = "Username";
            labelPassword.Text = "Password";

            connect.Text = "Connect";
            cancel.Text = "Cancel";
            connect.Location = new Point(50, 220);
            cancel.Location = new Point(160, 220);

            labelnetworkIP.Location = new Point(85, 35);
            networkIP.Location = new Point(85, 60);

            labelUser.Location = new Point(85, 85);
            user.Location = new Point(85, 110);

            labelPassword.Location = new Point(85, 135);
            Password.Location = new Point(85, 160);

            loginScreen.Controls.Add(connect);
            loginScreen.Controls.Add(cancel);
            loginScreen.Controls.Add(labelnetworkIP);
            loginScreen.Controls.Add(labelUser);
            loginScreen.Controls.Add(labelPassword);
            loginScreen.Controls.Add(networkIP);
            loginScreen.Controls.Add(user);
            loginScreen.Controls.Add(Password);

            loginScreen.Show(this);
            connect.Click += new System.EventHandler(this.btn_Connect_Click); //button eventhandlers
            cancel.Click += new System.EventHandler(this.btn_Cancel_Click);

        }

        void btn_Connect_Click(object sender, EventArgs e) //attempt connect to IPAdress
        {
            ipaddress = networkIP.Text;
            username = user.Text;
            clientusername = username;
            password = Password.Text;
            try
            {
                labelUserConnectedSet.Text = username;
                ConnectToServer();
            }
            catch (Exception)
            {
                Console.WriteLine("Server couldn't be found");
                return;
            }

            ReceiveResponse();//FIRST RESPONSE IS AUTHENTICATION

        }
        private static void UpdateRequest()
        {//updates server to refresh admin's view of file changes

            string request = "*update*";
            SendString(request);
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
        }
        void btn_Cancel_Click(object sender, EventArgs e) //Close all forms
        {
            Application.Exit();
        }

        private void ConnectToServer()
        {
            while (!ClientSocket.Connected)
            {
                try
                {
                    ClientSocket = new Socket
                   (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    //Please enter server IPaddress - e.g. - 10.0.0.226"
                    IPAddress ipa = IPAddress.Parse(ipaddress);
                    serverip = ipa;


                    ClientSocket.Connect(ipa, PORT);
                    //this is the bit of data to evaluate server side as first segment. and send the main menu(otherwise there is nothing differentiating occuring instances)
                    //new instances need to know where to begin regarding the callbacks to server
                    byte[] buffer = Encoding.ASCII.GetBytes(ipaddress + "," + username + "," + password);

                    ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
                }


                catch (SocketException)
                {
                    Console.Clear();
                }
            }
        }
        private static void SendString(string text)
        {
            ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ClientSocket.Connect(serverip, PORT);
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }
        /// <ReceiveResponse>
        /// Receives a response based on last sent message sent through the socket conneciton
        /// This method and the one above(sendstring) are to be constucted in a while loop together (requestloop)
        /// 
        private void ReceiveResponse()//send/receive/catch states, messages or orders from the server in this method
        {
            var buffer = new byte[2048];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
            string[] split = text.Split(',');
            if (split[0].Equals("*allowuser*"))
            {
                networkpath = split[1];//get network sharepath ~~~ \\\\servername\\WorkingDirectory\\Client
                this.Enabled = true;
                timer1.Enabled = true;
                loginScreen.Close();
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();

                SongToList();
            }
            else if (split[0].Equals("*denyuser*"))
            {
                MessageBox.Show("Username or Password Incorrect");
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
            }
            else if (split[0].Equals("*nousers*"))
            {
                MessageBox.Show("There are no users on this server, refer to server GUI to create one(admin).");
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
            }
            else
            {
                MessageBox.Show("Server not making sense, try restarting application.");
            }

        }

        private void FormClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonWDUpload_Click(object sender, EventArgs e)
        {
            //send mp3 to fileshare
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "MP3|*.mp3", Multiselect = false, ValidateNames = true })
                {
                    if (ofd.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("This action was canceled by user, no items added.");
                    }
                    else
                    {
                        string filepath = ofd.FileName;//source
                        string mp3name = filepath.Substring(filepath.LastIndexOf('\\') + 1);
                        if (!File.Exists(networkpath + "\\" + mp3name))
                        {
                            FileInfo upload = new FileInfo(filepath);//filedata
                            upload.CopyTo(networkpath + "\\" + mp3name);//destination = netshare +filename
                            SongToList();
                            UpdateRequest();
                        }
                        else
                        {
                            MessageBox.Show("That filename already exists on the server, rename your file and try again.");
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("This action was canceled by the user, no items added. \n\n\n Exception = " + exc);
            }
        }

        private void buttonWDDownload_Click(object sender, EventArgs e)
        {
            //download mp3 from fileshare
            try
            {
                using (SaveFileDialog ofd = new SaveFileDialog() { Filter = "MP3|*.mp3", ValidateNames = true })
                {
                    if (ofd.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("This action was canceled by user, no items added.");
                    }
                    else
                    {
                        string filepath = ofd.FileName;//newpath
                        string mp3name = filepath.Substring(filepath.LastIndexOf('\\') + 1);
                        FileInfo download = new FileInfo(networkpath + "\\" + listBoxSongs.SelectedItem.ToString());//filedata
                        download.CopyTo(filepath);//destination = netshare +filename

                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("This action was canceled by the user, no items added. \n\n\n Exception = " + exc);
            }
        }

        private void buttonPLNew_Click(object sender, EventArgs e)
        {

            Button create = new Button();
            Button cancelplaylist = new Button();

            playlist.StartPosition = FormStartPosition.Manual;
            playlist.Left = MousePosition.X - 110;
            playlist.Top = MousePosition.Y - 110;
            playlist.Height = 175;
            playlistLB.Text = "Playlist Name:";
            create.Text = "Create";
            cancelplaylist.Text = "Cancel";
            playlistLB.Location = new Point(85, 20);
            playlistTB.Location = new Point(85, 50);

            create.Location = new Point(50, 80);
            cancelplaylist.Location = new Point(160, 80);

            playlist.Controls.Add(create);
            playlist.Controls.Add(cancelplaylist);
            playlist.Controls.Add(playlistLB);
            playlist.Controls.Add(playlistTB);
            create.Click += new System.EventHandler(this.btn_Create_Playlist_Click); //button eventhandlers
            cancelplaylist.Click += new System.EventHandler(this.btn_Cancel_Playlist_Click);
            playlist.ShowDialog(this);
        }
        void btn_Create_Playlist_Click(object sender, EventArgs e) //create and name new playlist
        {
            //make new double link list, set the First to a name, add that to array of linklists.
            LinkedList<string> playlistDLL = new LinkedList<string>();
            playlistDLL.AddFirst(playlistTB.Text);

            playlistitles.Add(playlistTB.Text);
            allPlaylists.Add(playlistDLL);
            //clear the treeview and add all array elements to it (refresh treeview)
            RefreshTreeview();

            playlist.Close();
        }
        void RefreshTreeview()
        {
            //sort playlists in allPLaylist (no point sorting the linked lists as this defies the point of allowing insert before/after/first
            treeViewPlaylists.Nodes.Clear();
            List<string> thelist = new List<string>();
            allPlaylists = allPlaylists.OrderBy(x => x.First().ToString()).ToList(); //using Linq for my first time to sort due to object types, otherwise i would have to modify comparator

            foreach (LinkedList<string> llist in allPlaylists)
            {
                String name = llist.First();

                treeViewPlaylists.Nodes.Add(name, name);//add key + add text
                //for every song inside a linked list set sub node of that node in treeview
                // treeViewPlaylists.Nodes[treeViewPlaylists.SelectedNode.Index].Nodes.Add(listBoxSongs.SelectedItem.ToString());
                LinkedListNode<string> position = llist.Find(llist.First());

                foreach (String node in llist)//for each string in the dll
                {
                    string playlistname = llist.Find(llist.First()).Value;
                    if (node != playlistname)//if node is not the playlist name write it to treeview
                    {
                        int index = treeViewPlaylists.Nodes.IndexOfKey(playlistname);
                        treeViewPlaylists.Nodes[index].Nodes.Add(node);
                    }
                }
            }


        }

        void btn_Cancel_Playlist_Click(object sender, EventArgs e) //cancel creating playlist
        {
            playlist.Close();
        }

        private void treeViewPlaylists_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //play the linked list from the beginning
        }

        private void treeViewPlaylists_MouseDown(object sender, MouseEventArgs e)
        {
            //the right click opens a sub menu with { play from first, play from selection, play before before selection, play after selection, play last)
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        contextMenuStripPlaylist.Show(this, new Point(e.X + 25, e.Y + 315));//places the menu at the pointer position
                    }
                    break;
            }
        }

        private void listBoxSongs_MouseDown(object sender, MouseEventArgs e)
        {
            //the right click opens a sub menu with { play from first, play from selection, play before before selection, play after selection, play last)
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        contextMenuStripSongs.Show(this, new Point(e.X + 25, e.Y + 25));//places the menu at the pointer position
                    }
                    break;
            }
        }


        private void addSelectedSongToThisPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void playFirstSongToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (treeViewPlaylists.SelectedNode == null)
            {
                MessageBox.Show("No playlist selected!");
            }
            else//set a playlist into playing starting from the FIRST in the list
            {

                foreach (LinkedList<string> llist in allPlaylists)
                {
                    //get selected playlist to play
                    //check if selection is child or parent
                    if (treeViewPlaylists.SelectedNode.Level == 1) //some testing for this sshows me that 0 is parent and children increment from there
                    {
                        //this is child so compare parent to DLL

                        String find = "";//empty declaration
                        try
                        {
                            find = find + llist.Find(llist.First()).Value;
                            if (treeViewPlaylists.SelectedNode.Parent.Text.CompareTo(find) == 0)
                            {
                                currentplaylist = axWindowsMediaPlayer1.playlistCollection.newPlaylist(treeViewPlaylists.SelectedNode.Parent.Text);
                                foreach (string song in llist)//everything in linked list to current playlist
                                {
                                    audiofile = axWindowsMediaPlayer1.newMedia(networkpath + "\\" + song);
                                    currentplaylist.appendItem(audiofile);
                                }

                                axWindowsMediaPlayer1.currentPlaylist = currentplaylist;

                            }
                        }
                        catch { }
                    }
                    else //is parent
                    {

                        String find = "";//empty declaration
                        try
                        {
                            find = find + llist.Find(llist.First()).Value;
                            if (treeViewPlaylists.SelectedNode.Text.CompareTo(find) == 0)
                            {
                                currentplaylist = axWindowsMediaPlayer1.playlistCollection.newPlaylist(treeViewPlaylists.SelectedNode.Text);
                                foreach (string song in llist)
                                {
                                    audiofile = axWindowsMediaPlayer1.newMedia(networkpath + "\\" + song);
                                    currentplaylist.appendItem(audiofile);
                                }

                                axWindowsMediaPlayer1.currentPlaylist = currentplaylist;

                            }
                        }
                        catch { }
                    }

                }
            }
        }

        private void playSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxSongs.SelectedItem == null)
            {
                MessageBox.Show("No Songs Selected!");
            }
            else
            {
                string song = listBoxSongs.SelectedItem.ToString();
                currentplaylist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("Not in a playlist"); //only playing song
                audiofile = axWindowsMediaPlayer1.newMedia(networkpath + "\\" + song);
                currentplaylist.appendItem(audiofile);
                axWindowsMediaPlayer1.currentPlaylist = currentplaylist;
            }
        }

        private void playLastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewPlaylists.SelectedNode == null)
            {
                MessageBox.Show("No playlist selected!");
            }
            else//set a playlist into playing starting from the FIRST in the list
            {
                currentplaylist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("current playlist");
                foreach (LinkedList<string> llist in allPlaylists)
                {
                    //get selected playlist to play
                    //check if selection is child or parent
                    if (treeViewPlaylists.SelectedNode.Level == 1) //some testing for this sshows me that 0 is parent and children increment from there
                    {
                        //this is child so compare parent to DLL

                        String find = "";//empty declaration
                        try
                        {
                            find = find + llist.Find(llist.First()).Value;
                            if (treeViewPlaylists.SelectedNode.Parent.Text.CompareTo(find) == 0)
                            {
                                currentplaylist = axWindowsMediaPlayer1.playlistCollection.newPlaylist(treeViewPlaylists.SelectedNode.Parent.Text); //name playlist correspondingly
                                foreach (string song in llist)//everything in linked list to current playlist
                                {
                                    audiofile = axWindowsMediaPlayer1.newMedia(networkpath + "\\" + song);
                                    currentplaylist.appendItem(audiofile);
                                }

                                axWindowsMediaPlayer1.currentPlaylist = currentplaylist;
                                audiofile = axWindowsMediaPlayer1.currentPlaylist.get_Item(llist.Count - 1);
                                axWindowsMediaPlayer1.Ctlcontrols.playItem(audiofile);
                            }
                        }
                        catch { }
                    }
                    else //is parent
                    {

                        String find = "";//empty declaration
                        try
                        {
                            find = find + llist.Find(llist.First()).Value;
                            if (treeViewPlaylists.SelectedNode.Text.CompareTo(find) == 0)
                            {
                                currentplaylist = axWindowsMediaPlayer1.playlistCollection.newPlaylist(treeViewPlaylists.SelectedNode.Text); //name playlist correspondingly
                                foreach (string song in llist)
                                {
                                    audiofile = axWindowsMediaPlayer1.newMedia(networkpath + "\\" + song);
                                    currentplaylist.appendItem(audiofile);
                                }

                                axWindowsMediaPlayer1.currentPlaylist = currentplaylist;
                                audiofile = axWindowsMediaPlayer1.currentPlaylist.get_Item(llist.Count - 1);
                                axWindowsMediaPlayer1.Ctlcontrols.playItem(audiofile);
                            }
                        }
                        catch { }
                    }

                }
            }
        }

        private void playSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewPlaylists.SelectedNode == null)
            {
                MessageBox.Show("No playlist selected!");
            }
            else//set a playlist into playing starting from the FIRST in the list
            {

                foreach (LinkedList<string> llist in allPlaylists)
                {
                    //get selected playlist to play
                    //check if selection is child or parent
                    if (treeViewPlaylists.SelectedNode.Level == 1) //some testing for this sshows me that 0 is parent and children increment from there
                    {

                        //this is child so compare parent to DLL

                        String find = "";//empty declaration
                        try
                        {
                            find = find + llist.Find(llist.First()).Value;
                            if (treeViewPlaylists.SelectedNode.Parent.Text.CompareTo(find) == 0)
                            {
                                currentplaylist = axWindowsMediaPlayer1.playlistCollection.newPlaylist(treeViewPlaylists.SelectedNode.Parent.Text);

                                foreach (string song in llist)//everything in linked list to current playlist
                                {
                                    audiofile = axWindowsMediaPlayer1.newMedia(networkpath + "\\" + song);
                                    currentplaylist.appendItem(audiofile);
                                }

                                axWindowsMediaPlayer1.currentPlaylist = currentplaylist;

                                audiofile = axWindowsMediaPlayer1.currentPlaylist.get_Item(treeViewPlaylists.SelectedNode.Index + 1);
                                axWindowsMediaPlayer1.Ctlcontrols.playItem(audiofile);
                            }
                        }
                        catch { }
                    }
                    else //is parent
                    {
                        MessageBox.Show("Please open the playlist and select which song.");
                    }

                }
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDevice device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            try //extending visual threshold beyond limit
            {
                var value = device.AudioMeterInformation.MasterPeakValue * 100 * 1.5;//increasing data amplitude  by 1.5x incase volume is very low
                if (value > 100)
                {
                    progressBar1.Value = 100;
                    progressBar2.Value = 100;
                }
                progressBar1.Value = (int)value;
                progressBar2.Value = (int)value;


                // get whatever track and playlist is playing
                labelCurrentSong.Text = axWindowsMediaPlayer1.currentMedia.name;
                labelCurrentPlaylist.Text = axWindowsMediaPlayer1.currentPlaylist.name;
            }
            catch
            {

            }
            //networkpath + "\\" + mp3name
            DirectoryInfo di = new DirectoryInfo(networkpath);
            FileInfo[] fi = di.GetFiles("*.mp3", SearchOption.AllDirectories);
            int filecount = fi.Length;
            if (filecount == listBoxSongs.Items.Count)
            {
                return;//donothing if equal, sort it out if not
            }
            else
            {
                //allow all clients to keep track of SHAREMEDIA FILE updates
                SongToList();
            }


        }

        private void buttonWDSearch_Click(object sender, EventArgs e)
        {
            //search songs(sorted)
            foreach (String song in songs)
            {
                if (song.ToLower().CompareTo(textBox1.Text.ToLower()) == 0)//not case sensitive as suggestions dont help first character being lowercase
                {
                    int index = listBoxSongs.Items.IndexOf(song);
                    listBoxSongs.SetSelected(index, true);
                }
            }
        }
        void clearhighlight()
        {
            foreach (TreeNode treeViewNode in treeViewPlaylists.SelectedNode.Nodes)
            {
                treeViewNode.BackColor = Color.White;
            }
        }
        private void buttonPLSearch_Click(object sender, EventArgs e)
        {
            clearhighlight();
            //search songs in selected treeview(sorted) cannot / dont know how to enable multi selection
            string find = textBox2.Text;
            bool found = false;//state to check if was found for user prompt
            if (treeViewPlaylists.SelectedNode == null || treeViewPlaylists.SelectedNode.Level == 1)
            {
                MessageBox.Show("Please ensure that playlist (not audiofile) is selected before searching.");
            }
            else
            {
                foreach (TreeNode treeViewNode in treeViewPlaylists.SelectedNode.Nodes)
                {

                    if (treeViewNode.Text.CompareTo(find) == 0)
                    {

                        treeViewNode.BackColor = Color.Yellow;//HIGHLIGHT
                        found = true;
                    }
                }
                if (!found)
                {
                    MessageBox.Show("Item not found in playlist.");
                }
            }


        }



        public ArrayList DivideList(ArrayList arr, int size)
        {
            //merge sort implementation for normal List
            //if iteration meets only 2 items in the array return said array out of the lateral proccess of dividing
            //merging occurs on all items before returning the main array synchronously upon exiting recursive exececution of this method 
            if (size < 2)
            {
                return arr;
            }
            ArrayList lefthalf = new ArrayList();
            ArrayList righthalf = new ArrayList();

            int mid = size / 2;

            //pass through left - 0 to middle
            for (int i = 0; i < mid; i++)
            {

                lefthalf.Add(arr[i]);
            }
            //pass through right - middle to end
            for (int i = mid; i < size; i++)
            {

                righthalf.Add(arr[i]);
            }

            DivideList(lefthalf, mid);
            DivideList(righthalf, size - mid);
            MergeList(arr, lefthalf, righthalf, mid, size - mid);
            return arr;
        }
        public void MergeList(ArrayList main, ArrayList left, ArrayList right, int leftsize, int rightsize)
        {
            //i serves left and right is j, these count up until we reach the out-line of the while loop
            int i = 0, j = 0, k = 0;
            while (i < leftsize && j < rightsize)
            {
                //look for the smaller half, once its found it will follow that path for every count adding the rest in the respective lists.
                if (left[i].ToString().CompareTo(right[j].ToString()) < 0)
                {
                    main[k++] = left[i++];
                }
                else
                {
                    main[k++] = right[j++];
                }

            }

        }

        private void atTheEndOfPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //add songs to end of currently selected doubly linked list, then refresh treeview with new items
            foreach (LinkedList<string> llist in allPlaylists)
            {
                String find = "";//empty declaration
                try //need to catch if cant find
                {
                    if (treeViewPlaylists.SelectedNode.Level != 0)//for children
                    {
                        find = find + llist.Find(treeViewPlaylists.SelectedNode.Parent.Text).Value;//look for string in the list NOTE TO SELF: DO NOT ALLOW SAME NAME CREATION ON CREATE CLICK!!!
                        if (find.CompareTo(treeViewPlaylists.SelectedNode.Parent.Text) == 0)//if found the selected nodes text then add song name to dll
                        {
                            llist.AddLast(listBoxSongs.SelectedItem.ToString());//add new node to doubly linked list
                            RefreshTreeview();
                        }
                    }
                    else//for parents
                    {
                        find = find + llist.Find(treeViewPlaylists.SelectedNode.Text).Value;//look for string in the list NOTE TO SELF: DO NOT ALLOW SAME NAME CREATION ON CREATE CLICK!!!
                        if (find.CompareTo(treeViewPlaylists.Nodes[treeViewPlaylists.SelectedNode.Index].Text) == 0)//if found the selected nodes text then add song name to dll
                        {
                            llist.AddLast(listBoxSongs.SelectedItem.ToString());//add new node to doubly linked list
                            RefreshTreeview();
                        }
                    }


                }
                catch
                {
                    continue;
                }
            }
        }

        private void atTheBegginingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //add songs to currently selected doubly linked list, then refresh treeview with new items
            foreach (LinkedList<string> llist in allPlaylists)
            {
                String find = "";//empty declaration
                try
                {
                    if (treeViewPlaylists.SelectedNode.Level != 0)//for children
                    {
                        find = find + llist.Find(treeViewPlaylists.SelectedNode.Parent.Text).Value;//look for string in the list NOTE TO SELF: DO NOT ALLOW SAME NAME CREATION ON CREATE CLICK!!!
                        if (find.CompareTo(treeViewPlaylists.SelectedNode.Parent.Text) == 0)//if found the selected nodes text then add song name to dll
                        {
                            LinkedListNode<string> position = llist.Find(llist.First());
                            llist.AddAfter(position, listBoxSongs.SelectedItem.ToString());//add new node to doubly linked list
                            RefreshTreeview();
                        }
                    }
                    else//for parents
                    {
                        find = find + llist.Find(treeViewPlaylists.SelectedNode.Text).Value;//look for string in the list NOTE TO SELF: DO NOT ALLOW SAME NAME CREATION ON CREATE CLICK!!!
                        if (find.CompareTo(treeViewPlaylists.Nodes[treeViewPlaylists.SelectedNode.Index].Text) == 0)//if found the selected nodes text then add song name to dll
                        {
                            LinkedListNode<string> position = llist.Find(llist.First());
                            llist.AddAfter(position, listBoxSongs.SelectedItem.ToString());//add new node to doubly linked list
                            RefreshTreeview();
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
        }

        private void beforeSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //get selection from double linked list add before
            //refresh treeview
            if (treeViewPlaylists.SelectedNode == null)
            {
                MessageBox.Show("Nothing Selected");
            }
            bool child = false;
            foreach (LinkedList<string> llist in allPlaylists)
            {
                String find = "";//empty declaration
                try
                {
                    if (treeViewPlaylists.SelectedNode.Level != 0)//for children
                    {
                        child = true;
                        find = find + llist.Find(treeViewPlaylists.SelectedNode.Parent.Text).Value;//look for string in the list NOTE TO SELF: DO NOT ALLOW SAME NAME CREATION ON CREATE CLICK!!!
                        if (find.CompareTo(treeViewPlaylists.SelectedNode.Parent.Text) == 0)//if found the selected nodes text then add song name to dll
                        {
                            LinkedListNode<string> position = llist.Find(treeViewPlaylists.SelectedNode.Text);
                            llist.AddBefore(position, listBoxSongs.SelectedItem.ToString());//add new node to doubly linked list
                            RefreshTreeview();
                        }
                    }
                }
                catch
                {

                }
            }
            if (!child)//for parents
            {
                MessageBox.Show("Please select an audio file in a playlist and try again.");
            }
        }

        private void afterSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //get selection from double linked list add after
            //refresh treeview
            if (treeViewPlaylists.SelectedNode == null)
            {
                MessageBox.Show("Nothing Selected");
            }
            bool child = false;
            foreach (LinkedList<string> llist in allPlaylists)
            {
                String find = "";//empty declaration
                try
                {
                    if (treeViewPlaylists.SelectedNode.Level != 0)//for children
                    {
                        child = true;
                        find = find + llist.Find(treeViewPlaylists.SelectedNode.Parent.Text).Value;//look for string in the list NOTE TO SELF: DO NOT ALLOW SAME NAME CREATION ON CREATE CLICK!!!
                        if (find.CompareTo(treeViewPlaylists.SelectedNode.Parent.Text) == 0)//if found the selected nodes text then add song name to dll
                        {
                            LinkedListNode<string> position = llist.Find(treeViewPlaylists.SelectedNode.Text);
                            llist.AddAfter(position, listBoxSongs.SelectedItem.ToString());//add new node to doubly linked list
                            RefreshTreeview();
                        }
                    }
                }
                catch
                {

                }
            }
            if (!child)//for parents
            {
                MessageBox.Show("Please select an audio file in a playlist and try again.");
            }

        }

        private void buttonWDDelete_Click(object sender, EventArgs e)
        {
            //delete selected song from the folder - timer should take care of refreshing view.
            File.Delete(Path.Combine(networkpath, listBoxSongs.SelectedItem.ToString()));
        }

        private void buttonPLDelete_Click(object sender, EventArgs e)
        {
            //delete selected song from the playlist - refresh treeview.
            if (treeViewPlaylists.SelectedNode == null)
            {
                MessageBox.Show("Nothing Selected");
            }
            try
            {
                foreach (LinkedList<string> llist in allPlaylists)
                {
                    String find = "";//empty declaration
                    if (treeViewPlaylists.SelectedNode.Level != 0)//for children
                    {
                        find = find + llist.Find(treeViewPlaylists.SelectedNode.Parent.Text).Value;//look for string in the list 
                        if (find.CompareTo(treeViewPlaylists.SelectedNode.Parent.Text) == 0)
                        {
                            LinkedListNode<string> position = llist.Find(treeViewPlaylists.SelectedNode.Text);
                            llist.Remove(position.Value);//remove node from doubly linked list
                            RefreshTreeview();
                        }
                    }

                    if (treeViewPlaylists.SelectedNode.Level == 0)//for parents
                    {
                        find = find + llist.Find(treeViewPlaylists.SelectedNode.Text).Value;//look for string in the list 
                        if (find.CompareTo(treeViewPlaylists.SelectedNode.Text) == 0)
                        {
                            LinkedList<string> list = allPlaylists.Find(x => x.First() == find);
                            int index = allPlaylists.IndexOf(list);
                            allPlaylists.RemoveAt(index);//remove from list
                            RefreshTreeview();
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}

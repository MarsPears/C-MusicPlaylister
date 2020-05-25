using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylister_Server
{
    class User
    {
        string username;
        string salt;
        string passwordhash;
        public Hashing hashes = new Hashing();

        //when instanciating a new User we create their salt and password hash then store it
        //we will write all Admins/Users to array and save to file(server side)
        //On server Initiation these are loaded up in the appplication

        //For Instantiating
        public User(string name, string password)
        {
            Username = name;
            this.salt = hashes.GetSaltString();
            Passwordhash = hashes.SaltedHashGen(password, salt);
        }
        //For Loading from file
        public User(string name, string passwordhash, string salt)
        {
            this.username = name;
            this.salt = salt;
            this.passwordhash = passwordhash;
        }
        public string Username { get => username; set => username = value; }
        public string Salt { get => salt; set => salt = value; }
        public string Passwordhash { get => passwordhash; set => passwordhash = value; }

        //This validates the user password. To access we must know our users name in the first place.
        public bool LogIn(string trypassword)
        {
            //to allow log in compare key against new generation with salt
            if (Passwordhash.CompareTo(hashes.SaltedHashGen(trypassword, this.salt)) == 0)
            {
                Console.WriteLine("Password Correct.");
                return true;
            }
            else
            {
                Console.WriteLine("Incorrect Password.");
                return false;
            }

        }

    }
}

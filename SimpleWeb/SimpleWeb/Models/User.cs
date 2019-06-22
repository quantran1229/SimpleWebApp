using System;
namespace SimpleWeb.Models
{
    //A class to store information of an account: username,password,first+last name and email
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
    }
}

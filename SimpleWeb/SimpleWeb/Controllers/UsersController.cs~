using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Http;
using SimpleWeb.Models;

namespace SimpleWeb.Controllers
{
    public class UsersController : ApiController
    {
        private DatabaseController dbcon;
        public UsersController()
        {
            dbcon = new DatabaseController();
        }

        //GET API, Take a string as key name, return 0 if name is not find in database, 1 if name is exist in database
        [HttpGet]
        [Route("users/check/{name}")]
        public string CheckUsername(string name)
        {
            try
            {
                if (name == null || name == "")
                    return "0"; // if name is empty don't need to check => assume it is true
                if (dbcon.check_username(name))
                        return "0";//check if name is taken or not. Name is free => 0
                else return "1";//name is occupiced => 1
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "1";
            }
        }

        //POST API, take a user entry and add it into database
        [HttpPost]
        [Route("users/submit")]
        public string SubmitNewEntry([FromBody]User new_user)
        {
            try
            {
                dbcon.save_entry(new_user);
                SendEmail(new_user);
                return "0";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }

        //POST API, Take a username and password, return 0 if everything is correct
        //return 1 if username is incorrect
        //return 2 if password is incorrect but username is correct
        [HttpPost]
        [Route("users/login")]
        public string EntryLogin([FromBody]User new_user)
        {
            try
            {
                int result = dbcon.user_login(new_user);
                return result.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }

        //GET API, return the number of pages for all users
        [HttpGet]
        [Route("users")]
        public int getListAccounts()
        {
            try
            {
                int accounts =  dbcon.getNumberOfAccounts();
                return (accounts / 10 )+ 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        //GET API, Take int as page number, return the list of user in a page
        [HttpGet]
        [Route("users/{page}")]
        public List<User> getAccountsPage(int page)
        {
            try
            {
                return dbcon.getAllAccounts(page);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        //GET API, Take a string as key, return the number of pages for searching keyword
        [HttpGet]
        [Route("users/search/{key}")]
        public int getListSeachAccounts(string key)
        {
            try
            {
                int accounts = dbcon.searchNumberOfAccounts(key);
                return (accounts / 10) + 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        //GET API, Take a string as key and int as page number, return the list of users for searching keyword in a single page
        [HttpGet]
        [Route("users/search/{key}/{page}")]
        public List<User> getSearchPage(string key,int page)
        {
            try
            {
                return dbcon.search(key,page);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //sending an email to user's email from SimpleEmail mail account
        private void SendEmail(User user)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.Credentials = new System.Net.NetworkCredential("SimpleWebSpamEmail@gmail.com", "Iamsosimple123");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("SimpleWebSpamEmail@gmail.com");
            mail.To.Add(user.email);
            mail.Subject = "Account Creation";
            mail.Body = "Thank "+user.firstname+" "+user.lastname+" for creating an account for Simple Web, plenty of free time huh. Cheer, SimpleWeb";
            SmtpServer.Send(mail);
            Console.WriteLine("mail Send");
        }
    }
}

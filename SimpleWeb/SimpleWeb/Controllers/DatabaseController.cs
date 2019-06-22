using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using SimpleWeb.Models;

namespace SimpleWeb.Controllers
{
    public class DatabaseController
    {
        //hard code database name as right now there is no need to change database
        private const string db_name = "SimpleDatabase.sqlite";
        public Boolean check_username(string name)
        {
            if (name == null || name == "") return false;
            try
            {

                using (SqliteConnection db_con = new SqliteConnection("Data Source=" + db_name + ";Version=3;"))
                {
                    db_con.Open();
                    string str = "select username from Users WHERE username=@name";
                    SqliteCommand command = new SqliteCommand(str, db_con);
                    command.Parameters.Add(new SqliteParameter("name", name));
                    object check = command.ExecuteScalar();
                    if (check == null)
                    {
                        db_con.Close();
                        return true;
                    }
                    db_con.Close();
                }
            }
            catch (SqliteException e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        //find a user login base on user name and password
        public int user_login(User user)
        {
            if (!check_username(user.username))
            {
                try
                {

                    using (SqliteConnection db_con = new SqliteConnection("Data Source=" + db_name + ";Version=3;"))
                    {
                        db_con.Open();
                        string str = "select * from Users WHERE username=@name AND password=@pass";
                        SqliteCommand command = new SqliteCommand(str, db_con);
                        command.Parameters.Add(new SqliteParameter("name", user.username));
                        command.Parameters.Add(new SqliteParameter("pass", user.password));
                        object check = command.ExecuteScalar();
                        db_con.Close();
                        if (check != null)
                        {
                            return 0;//get the correct id/password
                        }
                        else return 2;//wrong password but username is correct
                    }
                }
                catch (SqliteException e)
                {
                    throw new Exception(e.Message);
                }
            }
            else return 1;//meaning wrong ID/username
        }

        //save entry into database
        public void save_entry(User newuser)
        {
            if (check_username(newuser.username))
            {
                try
                {
                    using (SqliteConnection db_con = new SqliteConnection("Data Source=" + db_name + ";Version=3;"))
                    {
                        db_con.Open();
                        string str = "INSERT INTO Users(username,password,firstname,lastname,email) VALUES(@name,@pass,@first,@last,@email)";
                        SqliteCommand command = new SqliteCommand(str, db_con);
                        command.Parameters.Add(new SqliteParameter("name", newuser.username));
                        command.Parameters.Add(new SqliteParameter("pass", newuser.password));
                        command.Parameters.Add(new SqliteParameter("first", newuser.firstname));
                        command.Parameters.Add(new SqliteParameter("last", newuser.lastname));
                        command.Parameters.Add(new SqliteParameter("email", newuser.email));
                        command.ExecuteNonQuery();
                        db_con.Close();
                    }
                }
                catch (SqliteException e)
                {
                    throw new Exception(e.Message);
                }
            }
            else
            {
                throw new Exception("Username already exist in database");
            }
        }

        //get total number of accounts so that can calculate number of page
        public int getNumberOfAccounts()
        {
            try
            {
                using (SqliteConnection db_con = new SqliteConnection("Data Source=" + db_name + ";Version=3;"))
                {
                    db_con.Open();
                    SqliteCommand command = new SqliteCommand("SELECT count(*) FROM Users", db_con);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error while getting list:" + e.Message);
            }
        }

        //pagination, only get 10 items at the same time, skip other
        public List<User> getAllAccounts(int page)
        {
            List <User> listAccounts= new List<User>();
            try
            {
                using (SqliteConnection db_con = new SqliteConnection("Data Source=" + db_name + ";Version=3;"))
                {
                    db_con.Open();
                    SqliteCommand command = new SqliteCommand("SELECT username,firstname,lastname,email FROM Users", db_con);
                    SqliteDataReader reader = command.ExecuteReader();
                    int i = -1;
                    while (reader.Read())
                    {
                        i++;
                        if (i < (page - 1) * 10)
                            continue;
                        if (i >= (page * 10) - 1)
                            break;
                        //create entry base on reader and add it into array
                        User entry = new User();
                        entry.username = reader["username"].ToString();
                        entry.firstname = reader["firstname"].ToString();
                        entry.lastname = reader["lastname"].ToString();
                        entry.email = reader["email"].ToString();
                        listAccounts.Add(entry);
                    }
                    reader.Close();
                    return listAccounts;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error while number list:" + e.Message);
            }
        }

        //generate all number of match search key to generate sumber of page
        public int searchNumberOfAccounts(string keyword)
        {
            List<User> listAccounts = new List<User>();
            try
            {
                using (SqliteConnection db_con = new SqliteConnection("Data Source=" + db_name + ";Version=3;"))
                {
                    db_con.Open();
                    SqliteCommand command = new SqliteCommand("SELECT count(*) FROM Users WHERE (LOWER(username) LIKE @key) OR (LOWER(firstname) LIKE @key) OR (LOWER(lastname) LIKE @key) OR (LOWER(email) LIKE @key)", db_con);
                    command.Parameters.Add(new SqliteParameter("key", '%'+keyword.ToLower()+'%'));
                    int counts = Convert.ToInt32(command.ExecuteScalar());

                    return counts;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error:" + e.Message);
            }
        }

        //searching base on keyword
        public List<User> search(string keyword,int page)
        {
            List<User> listAccounts = new List<User>();
            try
            {
                using (SqliteConnection db_con = new SqliteConnection("Data Source=" + db_name + ";Version=3;"))
                {
                    db_con.Open();
                    SqliteCommand command = new SqliteCommand("SELECT username,firstname,lastname,email FROM Users WHERE (LOWER(username) LIKE @key) OR (LOWER(firstname) LIKE @key) OR (LOWER(lastname) LIKE @key) OR (LOWER(email) LIKE @key)", db_con);
                    command.Parameters.Add(new SqliteParameter("key", '%' + keyword.ToLower() + '%'));
                    SqliteDataReader reader = command.ExecuteReader();
                    int i = -1;
                    while (reader.Read())
                    {
                        i++;
                        if (i < (page - 1) * 10)
                            continue;
                        if (i >= (page * 10) - 1)
                            break;
                        //create a new entry base on reader and add it into list
                        User entry = new User();
                        entry.username = (reader["username"]).ToString();
                        entry.firstname = (reader["firstname"]).ToString();
                        entry.lastname = (reader["lastname"]).ToString();
                        entry.email = (reader["email"]).ToString();

                        listAccounts.Add(entry);
                    }
                    reader.Close();
                    return listAccounts;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error while generate search list:" + e.Message);
            }
        }
    }
}

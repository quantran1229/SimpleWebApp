# SimpleWebApp
A Simple Web Application for creating user and login structure.
This program was created by MonoDevelop IDE https://www.monodevelop.com/ in Linux. This web application using asp.net framework and using Mono.Data.Sqlite database. Because of this, right now this program can only run on MonoDevelop IDE.

Main Function:
#1. Create an account. The account will have a unique name. SimpleWeb will check if username is exist in database or not.
#2. Send an email to user's email after account creation.
#3. Login using database.
#4. Login require for index.html by using sessionStorage.
#5. Showing list of all users accounts with first+last name,username and email as Pagination.
#6. Searching for keyword.

There are 3 main pages:
--SimpleWeb
      |
      |--- ....
      |---Views
            |
            |-----index.html   :Showing info about database
            |-----signup.html  :For user creation.
            |-----login.html   :For login page
  
  Author:Quan Tran

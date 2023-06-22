This is a blog web project. There are API and MVC projects inside this project. The MVC project calls the API project for sign in, sign up and CRUD operations. You can
do all CRUD operations on your blogs. You just need to sign up and sign in for that. An unauthenticated users can just read other users' blogs. You need to add SQL 
backup file to your Microsoft SQL Server application for using this project. I used ASP.NET Core Identity(for authentication and authorization), bcrypt(for hashing 
password), Serilog(for logging), Entity Framework Core(as ORM framework) with database first approach, Microsoft SQL Server(as database), jQuery(for client side 
validation) and so on.
# Company List Application
 Application consists of a .NET 8 Backend, following a microservices architecture and a Angular user interface.
 Backend functionality is splitted in two services, Company and UserManagement, where UserManagement has just minimal implementation for JWT Token generation with hardcoded credentials.
 For database a PostgreSQL is expected to be running on localhost.

 # Starting UP in Development Mode
Ensure a postgresql is running on local machine, if not adjust the Database connection string from Company.RestApi/Appsettings.json.
   - create a database named "dev"
   - execute /Database/CreateDatabase.sql for creating schema and tables
   - execute /Database/InsertTestData.sql for adding test data.

Server Startup:
   - build CodeChallenge.sln and run both services in IIS

UI Startup:
   - in \WebUI\ directory, run:
      - npm install
      - npm start



 

 

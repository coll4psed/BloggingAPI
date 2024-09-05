Hello this is my version of Blogging Platform API (https://roadmap.sh/projects/blogging-platform-api)

Pre requirements:
 - Installed ASP.NET Core Runtime 8.0.8
 - Installed .NET Runtime 8.0.8
 - Docker

To start the app you need:
 1. Enter the directory which contains BloggingAPI.csproj, and copy the path to your cmd
![image](https://github.com/user-attachments/assets/2fbe1424-fe7e-4f69-9197-d02bc30ea1bf)
 2. Run "docker-compose up -d" to launch database instance (PostgreSQL)
 3. Then "dotnet run"
 4. After project start, don't close cmd and go to your brwoser and open swagger panel

![image](https://github.com/user-attachments/assets/c5586962-f500-46c4-a3a4-a005a80af6f0)

To open swagger panel you need to type the address from your cmd and add /swagger/index.html to it

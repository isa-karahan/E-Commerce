# SQL Server Configuration Folder

SQL Server is used as database for .NET backend and this folder includes the necessary files to configure the docker container.
- A complete backup file
- entrypoint script
- run initialization script

When container starts entrypoint script starts the SQL server and run-initialization script. Then, second script waits 90 seconds and starts the restore process.
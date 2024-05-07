# ADHD_WEB
## Installation
### Docker
1. go to ADHD_Web.Server and find appsettings.json
2. change the connection string to your own and set the host to "db"
3. run `docker-compose up --build`
### Vue
```
# If using macos and get this problem:
# There was an error exporting HTTPS developer certificate to a file.
# error when starting dev server:
# Error: Could not create certificate.

# Run this command:
dotnet dev-certs https --clean
dotnet dev-certs https
dotnet dev-certs https --trust

cd ~/.aspnet/https
openssl genrsa -out "adhd_web.client.key" 2048
openssl req -new -x509 -key "adhd_web.client.key" -out "adhd_web.client.pem" -days 365 -subj "/TW=localhost"
```

### .NET Core
#### Model to DB
```
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet ef migrations script -o init.sql
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\local;Database=ATM;TrustServerCertificate=True"
  },
  "Authentication": {
    "SecretForKey": "hfnucsdu872iqrubvegdfh8wuicjdvgyufdhcjkvgdhjfsurwdfhjyudkevgywdfjkeugwjifuhwefjhfsi",
    "Issuer": "https://localhost",
    "Audience": "ATMApplication.Api"
  },
  "Jwt": {
    "Key": "ThisismySecretKey",
    "Issuer": "ATM"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}

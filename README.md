# FundAdministrationAPI
Fund Administration API using ASP.NET Core

# Fund Administration API 
## Features 
- CRUD Operations for Funds and Investors 
- Transaction Management 
- Repository Pattern 
- JWT Authentication 
- Swagger Documentation 
- Global Exception Handling 
- Unit Testing  
- Serilog Logging 

## Technologies 
- ASP.NET Core 8 Web API 
- Entity Framework Core 
- InMemory Database 
- xUnit 
- Moq 
- JWT Authentication 

## Run Application 
1. dotnet restore 
2. dotnet build
3. dotnet run 

## Swagger URL 
http://localhost:5166/swagger/index.html 

## Test Payload
- Create Fund 
{ 
"name": "Test Fund", 
"currency": "Dollar", 
"launchDate": "2026-05-21" 
}

- Create Investor 
{  
"fullName": "Test User", 
"email": "Test@gmail.com", 
"fundId": "YOUR_FUND_ID" 
}

- Create Transaction 
{ 
"investorId": "YOUR_INVESTOR_ID", 
"type": 1, 
"amount": 100, 
"transactionDate": "2026-05-21"
}

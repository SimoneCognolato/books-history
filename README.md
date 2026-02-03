# books-history
Demo practice with ASP.NET

## How to run the solution in a Docker Container

1. Build the image using Dockerfile
```
docker build -t books-history-api .
```

2. Run the container
```
docker run -d -p 5000:8080 -e ASPNETCORE_ENVIRONMENT=Development --name books-container books-history-api
```

3. Open the Swagger Documentation
```
http://localhost:5000/swagger/index.html
```  

## Features
This ASP.NET Core Web API allows to:
1. Create a new book
2. Get all books
3. Get a book by its guid
4. Update a book fields
5. Get a book's history, using (optionally) filters based on field, ordering and pagination (limit + offset)

## Tech Stack
- .NET 8 LTS
- ASP.NET Core Web API
- EntityFramework + InMemoryDatabase
- AutoMapper for mapping between API's DTOs and Data Models


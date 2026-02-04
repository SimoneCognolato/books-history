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

## How book's changes are tracked
The used approach is based on creating a BooksHistory table in the DB, with the following fields:

1. Id
2. BookId
3. UpdatedOn
4. UpdatedField
5. PreviousValue
6. CurrentValue

An entry in the BooksHistory table is created whenever a Book is updated using HTTP PUT

## Other tried approaches 
Another approach (probably more robust and better for proper tracking) was tried in branch: https://github.com/SimoneCognolato/books-history/tree/dev/create-history-books

That approach consisted of replicating the entire Book entry (before updating it) in the BooksHistory table, where the BooksHistory table looked almost equal to Books table.

This approach worked, but was discarded due to time constraints: showing information like "Title was changed from X to Y" required proper FE capabilities to express it (or additional BE endpoints).

# API Requirements

Create an API using .NET Core that supports the following endpoints:

## Endpoints

- `GET /api/products` - Retrieves a list of products.
- `GET /api/products/{id}` - Retrieves a specific product by ID.
- `POST /api/products` - Creates a new product.
- `PUT /api/products/{id}` - Updates an existing product.
- `DELETE /api/products/{id}` - Deletes a product.

## JWT Authentication

- Implement a simple authentication mechanism that issues a JWT token after a successful login.
- Protect the product endpoints so that only authenticated users can access them.

## Database

- Use Entity Framework Core to manage product data in an in-memory database.

## Technologies

- C#
- .NET Core
- Entity Framework Core
- JWT (JSON Web Tokens)

## External Resources

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Postman](https://www.postman.com/) for testing API endpoints


# ProductAPI Solution

This solution contains two projects:

- **ProductAPI**: ASP.NET Core Web API for managing products with JWT authentication.
- **ProductAPI.Tests**: Unit tests for the ProductAPI project using xUnit and Moq.

---

## ProductAPI

### What is it?

A RESTful API that supports CRUD operations on products, secured with JWT tokens.

### How to run

From the solution root, run:

```bash
dotnet run --project ProductAPI/ProductAPI.csproj
```

The API will start on `https://localhost:7028` (or the configured port).

Open Swagger UI at:

```
https://localhost:7028/swagger
```

### How to authenticate

1. Use the **POST /api/auth/login** endpoint with JSON:

```json
{
  "username": "taller-admin",
  "password": "123456"
}
```

2. Copy the returned JWT token.

3. Click the **Authorize** button in Swagger and enter:

```
Bearer access_token_here
```

4. You can now access protected endpoints, e.g., **GET /api/products**.

---

## ProductAPI.Tests

### How to run tests

From the solution root, run:

```bash
dotnet test
```

This will build and run all tests in `ProductAPI.Tests`.

---

## Project structure

```
/ProductAPI
  ├── Controllers
  ├── Data
  ├── Models
  ├── Repository
  ├── Helpers
  ├── Program.cs
  ├── appsettings.json
  └── ProductAPI.csproj

/ProductAPI.Tests
  ├── Controllers
  ├── Helpers
  ├── ProductAPI.Tests.csproj

/ProductAPI.sln
README.md
```

---

## Notes

- JWT secret and issuer are set in `appsettings.json`.
- Database uses in-memory provider; data resets on restart.
- Modify credentials and secrets for production use.

---

Feel free to contribute or open issues!
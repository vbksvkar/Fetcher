# Fetcher


## Table of Contents
- [Introduction](#introduction)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Endpoints](#endpoints)
- [Error Handling](#error-handling)
- [Contributing](#contributing)
- [License](#license)

## Introduction
Fetcher API is a simple RESTFul API to fetch dad jokes. It is built with ASP.NET Core. 

Currently, it has 2 features: 
- Fetch a Random 'Dad' Joke
- Search 'Dad' Jokes for a specific term


## Installation
### Prerequisites

- **Required Software**: .NET 8
- **Operating Systems**: Windows, Linux, macOS

### Steps
1. Clone the repository:
    ```sh
    git clone https://github.com/vbksvkar/Fetcher.git
    ```

2. Navigate to the folder:

    ```sh
    cd Fetcher
    ```

3. Install the dependencies:
    ```sh
    dotnet restore
    ```

4. Build the solution:
    ```sh
    dotnet build Fetcher.Api.sln
    ```

4. Run the API:
    ```sh
    dotnet run --project Fetcher.Api/Fetcher.Api.csproj
    ```

## Usage

The API is configured to expose the http endpoints on port `24999`. This ensures that the port does not conflict with the port of an already running application(s) (if any).

The base URL for the API is `http://localhost:24999`.

## Project Structure

The project is divided into several folders:
- `Fetcher.Api/` - Contains the main API project
  - `Controllers/` - API controllers. Contains the FetcherController with 2 endpoints
  - `Infra/` - Infrastructure-related code. Contains all the Dependency Injection code
  - `Program.cs` - Entry point of the application
- `Fetcher.Models/` - Contains the models used in the application
  - `Request/` - Request models
  - `Response/` - Response models
- `Fetcher.Services/` - Contains the service layer
  - `Clients/` - External clients. Contains the HttpClient for `https://icanhazdadjoke.com/`
  - `Concretes/` - Concrete implementations of services
  - `Interfaces/` - Service interfaces
- `Fetcher.Tests/` - Contains the test project
  - `FetcherServiceTests.cs` - Unit tests for the services

## Endpoints

The `FetcherController` provides the following endpoints:

- `GET http://localhost:24999/api/fetcher`
    - **Description**: Fetches a random dad joke from the service.
    - **Response**: A JSON object containing

      - id property
      - joke property

      ```json
      {
        "id": "pGe2EYozAsc",
        "joke": "What do you call a nervous javelin thrower? Shakespeare."
      }
      ```

- `GET /api/fetcher/search`
    - **Description**: Fetches all dad jokes that match the provided search term.
    - **Request**: A `SearchRequest` object containing the search term.
      - term: Search term to search the dad jokes
      - limit: Max Number of jokes to limit in the response (default 30 jokes)
      - page: The page number of the jokes to retrieve (default 1)
    - **Response**: A JSON object grouped by the length of the jokes (in words) with the search term emphasized with angular brackets (`<searchTerm>`):
        - **Short**: Jokes with words count less than 10
        - **Medium**: Jokes with words count greater than 10 and less than 20
        - **Long**: Jokes with words count 20 or more

        Response structure reduced for brevity
      ```json
        {
          "currentPage": 1,
          "limit": 30,
          "nextPage": 2,
          "previousPage": 1,
          "short": [{
              "id": "vPuzPfqOKe",
              "joke": "Camping <is> intense."
            }],
          "medium": [{
              "id": "EBAsPfiNuzd",
              "joke": "What did one plate say to the other plate? Dinner <is> on me!"
            }],
          "long": [{
              "id": "wHlOu4o41g",
              "joke": "If you think swimming with dolphins <is> expensive, you should try swimming with sharks--it cost me an arm and a leg!"
            }],
          "searchTerm": "is",
          "totalJokes": 80,
          "totalPages": 3
        }
      ```

## Error Handling

The project includes a global exception handler - an `IExceptionHandler` implementation to handle unhandled exceptions and return a standardized error response. 

The `GlobalExceptionHandler` class is used for this purpose. It implements the `IExceptionHandler` interface and logs the exception details. It returns a `ProblemDetails` object with a status code of 500 (Internal Server Error).

## Pending Things / Enhancements:
- Improved implementation of Paging: There seems to be a bug in the current implementation of Paging (page query string parameter), needs to be handled in the service and adjusted accordingly.

- Authentication and Authorization: Implement API key based authentication using the `AuthenticationHandler` and `AuthorizationHandler`

- Enhanced Unit/Functional Tests: The current unit tests are bare minimum to check only few use cases. Need to add more unit tests to improve code coverage. Also need to add functional tests to hit the endpoints and check the responses.

- Improved Response Status code handling
  

## Contributing

```
Contributions are welcome! Please follow these steps:
1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -m "Add feature"`).
4. Push to the branch (`git push origin feature-branch`).
5. Create a pull request.
```

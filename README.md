# LibraryManagement

_SOLUTION STRUCTURE_

- LibraryManagement.API: ASP .NET Core Web API project
- LibraryManagement.API.Tests: XUnit project with unit tests of API project
- LibraryManagement.Business: Class library with:
  - Interface definitions
  - Mappings for Automapper
  - Models containing Domain models and DTOs
  - Constants used in the project
- LibraryManagement.ConsoleApp: Console application project to interact with the API

_IMPLEMENTED FEATURES_

- API:
  - Concepts used:
    - Design patterns: Unit of work, Repository and Dependency injection
	- Asynchronous programming
	- Entity Framework Core with seeded SQLite in-memory database
	- General exception handling middleware
	- OverdueNotification service as BackgroundService to notify overdue books
	- Serilog logging for notification service
	- Scalar library used as a UI for API
	- Automapper used to map Domain models with DTOs
	- Domain models and DTOs
  - Models:
    - Book
	- Member
	- Transaction
  - Controllers:
    - Books, with endpoints:
	  - POST: /api/Books => Create
	  - GET: /api/Books => Get all
	    with filterOn and filterQuery query parameters to filter on Title, Author and Category
	  - GET: /api/Books/{id} => Get by id
	  - PUT: /api/Books/{id} => Update
	  - DEL: /api/Books/{id} => Remove by id
    - Members, with endpoints:
	  - POST: /api/Members => Create
	  - GET: /api/Members => Get all
	  - GET: /api/Members/{id} => Get by id
	  - PUT: /api/Members/{id} => Update
	  - DEL: /api/Members/{id} => Remove by id
    - Transactions, with endpoints:
	  - POST: /api/Transactions/Borrow => Borrow a book with bookId and memberId
      - POST: /api/Transactions/Return => Return a book with bookId and memberId
	  - GET: /api/Transactions/Overdue => Get a list of overdue books

- API Unit tests:
  - TransactionsController tests:
    - BorrowBookAsync_ShouldThrow_WhenBorrowLimitReached
  - TransactionsRepository tests:
    - BorrowBookAsync_ThrowsInvalidOperationException_WhenBookNotAvailable
	- BorrowBookAsync_ThrowsInvalidOperationException_WhenBookLimitReached
	- BorrowBookAsync_ThrowsInvalidOperationException_WhenOverdueBook
	- BorrowBookAsync_ShouldPass_WhenPossibleTransaction

- Console app:
  - Interactive app to test the API
  - Menu options:
    - Add a Book
	- Add a Member
	- Borrow a Book
	- Return a Book
	- Display Overdue Books

_INSTRUCTIONS ON RUNNING THE PROJECT_

- Get the API url by going to LibraryManagement.API/Properties/launchSettings.json/profiles/https/applicationUrl

- To run only the API:
  - Select 'LibraryManagement.API' profile to start up and run
  - If you want to use Scalar UI, open a browser and go to applicationUrl/scalar/v1

- To run the API + Console app:
  - Select 'Console App + API' profile to start up and run
  - Follow the instructions in the console window

- To run the Unit tests:
  - Open Test Explorer and run the tests

_ASSUMPTIONS AND LIMITATIONS_

  - Transactions cannot be added freely or updated, only by using Borrow and Return endpoints
  - There can be members with the same Name
  - There are no restrictions in Book properties or Member name naming conventions

# Messenger API - Lab 2

## 📖 Project Overview
This project is a RESTful Web API for a simple messenger application. It demonstrates the implementation of a Business Logic Layer, automated database management, and integration testing. 

The API allows for the creation of users, handling direct conversations, and storing/retrieving message history.

## 🛠 Technologies & Tools
* **Language:** C#
* **Framework:** .NET (ASP.NET Core Web API)
* **Database:** SQLite (In-Memory for testing / local file for development)
* **ORM:** Entity Framework Core
* **Testing:** xUnit, Moq, Postman
* **IDE:** Visual Studio / Visual Studio Code

## 🚀 How to Run the Project
1. Clone the repository to your local machine.
2. Open the solution file (`MessengerSystem.sln`) in Visual Studio.
3. Build the solution to restore NuGet packages.
4. Run the `MessengerApi` project. The server will start listening on `http://localhost:5000`.
5. The SQLite database (`messenger.db`) will be automatically created upon the first request.

## 📡 API Endpoints

| Method | Endpoint | Description | Request Body (Example) |
| :--- | :--- | :--- | :--- |
| **POST** | `/users` | Creates a new user | `{"name": "Alice"}` |
| **POST** | `/messages` | Sends a new message | `{"conversationId": "...", "senderId": "...", "text": "Hello!"}` |
| **GET** | `/conversations/{id}/messages` | Retrieves message history | *None* |

*Note: The system automatically creates a conversation entity if a message is sent to a conversation ID that does not yet exist.*

## 🧪 Testing
The project includes two levels of testing to ensure reliability:

1. **API Testing (Postman):** A pre-configured Postman collection is included in the root directory (`Messenger API Lab 2.postman_collection.json`). Import this file into Postman to test the endpoints directly.

2. **Integration Testing (xUnit):**
   The `MessengerApi.Tests` project contains an automated integration test (`FullMessageFlow_ShouldStoreAndRetrieveMessage`). It uses an in-memory SQLite database to simulate and verify the complete flow: creating users, sending a message, and retrieving the history without affecting the production database.
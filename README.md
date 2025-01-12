# KeyDataStore

Overview

The Key Data Store project is a simple application that allows you to store, retrieve, and manage key-value pairs, where each key is a unique identifier, and the associated data can be any type of information related to a person or other entities. It is built using Docker and PostgreSQL for persistent storage and Marten for document store functionality in a .NET environment.

Features

Key-Value Storage:  Store key-value pairs where each key can represent an identifier like an ID or TC number, and the value contains relevant information (e.g., name, age).
Dockerized Environment: The application is fully containerized using Docker, making it easy to deploy and run.
PostgreSQL Database: The project uses PostgreSQL as the database to store and manage the key-value pairs.
Marten for Document Storage: Marten is used to provide a document-based storage system, allowing flexibility in how data is stored and queried.


TECHNOLOGIES USED:

C#: Programming language used for backend logic.

.NET Core: Framework used for building the API.
PostgreSQL: Database system used for storing data.

Marten: Document database for handling data in a flexible format.

Docker: Containerization platform for easy deployment and management.

Docker Compose: Tool to define and manage multi-container Docker applications.






Below is an example Postman screenshot for a POST request to store data:

![getExample](https://github.com/user-attachments/assets/6bea8503-80e5-4542-864f-4843f555c12b)





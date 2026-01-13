
# ABC Retail – Azure Cloud Web Application (Phase 1)

## Overview
This repository represents **Phase 1** of the ABC Retail cloud-based e-commerce application, developed as part of an academic Portfolio of Evidence.

The focus of this phase was to design and implement a **cloud-native data storage architecture** using **Azure Storage Services**, enabling scalable and reliable management of products, customers, orders, and media assets.

---

## Technologies Used

### Application
- ASP.NET Core MVC
- C#
- Razor Pages
- HTML5, CSS3, Bootstrap

### Cloud & Storage (Microsoft Azure)
- Azure App Service
- Azure Table Storage
- Azure Blob Storage
- Azure Queue Storage
- Azure File Storage

### Tools
- Visual Studio
- Azure Portal
- Git & GitHub

---

## Architecture Overview

This phase follows a **storage-first cloud architecture**, where different Azure Storage services are used based on data type and access patterns:

- **Azure Table Storage** – Customer and product metadata
- **Azure Blob Storage** – Product images and multimedia
- **Azure Queue Storage** – Order processing messages
- **Azure File Storage** – Contracts and log files

This approach demonstrates an understanding of **scalable, cost-effective cloud storage design**.

---

## Application Features

### Product Management
- Store and retrieve product information using Azure Table Storage
- Host product images in Azure Blob Storage

### Customer Profiles
- Customer records stored in Azure Table Storage
- Input validation to ensure data integrity

### Orders & Messaging
- Orders placed by users are added to Azure Queue Storage
- Queue-based design enables asynchronous processing

### File Storage
- Documents and logs stored securely using Azure File Storage

---

## Screenshots

### Azure App Service Hosting
![Azure App Service](screenshots/azure-app-service.png)

### Azure Table Storage
![Table Storage](screenshots/table-storage.png)

### Azure Blob Storage
![Blob Storage](screenshots/blob-storage.png)

### Azure Queue Storage
![Queue Storage](screenshots/queue-storage.png)

### Azure File Storage
![File Storage](screenshots/file-storage.png)

### Application UI
![Catalog Page](screenshots/catalog-page.png)
![Cart Page](screenshots/cart-page.png)

---

## Learning Outcomes
- Cloud-native storage design using Azure Storage Services
- Asynchronous messaging with Azure Queues
- Separation of structured and unstructured data
- Practical deployment using Azure App Service
- Scalable and maintainable cloud architecture principles

---

## Project Evolution
This repository represents **Phase 1** of the project.

- Phase 2: Azure Functions & serverless automation  
- Phase 3: Azure SQL Database with geo-replication (final architecture)

---

## Author
**Denell Vandayar**  
Bachelor of Computer and Information Sciences – Application Development  

GitHub: https://github.com/DenellVandayar

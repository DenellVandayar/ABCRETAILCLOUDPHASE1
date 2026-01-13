
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
<img width="1366" height="768" alt="Screenshot 2024-08-29 114156" src="https://github.com/user-attachments/assets/2e6d9820-dc33-4eb7-baac-8d73b6400cdb" />
<img width="1366" height="768" alt="Screenshot 2024-08-29 120623" src="https://github.com/user-attachments/assets/b205c05f-4c27-4846-82ca-6e1d6f9aea58" />
<img width="1366" height="768" alt="Screenshot 2024-08-29 121534" src="https://github.com/user-attachments/assets/c562282e-0342-4b06-ac93-fb7cbaf36ab4" />
<img width="1366" height="768" alt="Screenshot 2024-08-29 123436" src="https://github.com/user-attachments/assets/880cab1d-5a3c-4ca3-a718-81b5464e82a7" />


### Azure Blob Storage
<img width="1366" height="768" alt="Screenshot 2024-08-29 121454" src="https://github.com/user-attachments/assets/9660c22d-7054-4c34-83e1-48e3752763eb" />

### Azure Queue Storage
<img width="1366" height="768" alt="Screenshot 2024-08-29 123457" src="https://github.com/user-attachments/assets/aad30721-8c7e-4974-85e0-dffc2b454d7b" />
<img width="1366" height="768" alt="Screenshot 2024-08-29 123512" src="https://github.com/user-attachments/assets/00f05779-e288-453c-8115-f6e79a1355ab" />

### Azure File Storage
<img width="1366" height="768" alt="Screenshot 2024-08-29 121418" src="https://github.com/user-attachments/assets/fd14d624-2c24-4e84-b039-dc31cde1356a" />

### Application UI
<img width="1349" height="729" alt="azure app running" src="https://github.com/user-attachments/assets/79a51094-2c57-4c2d-a122-1bd1dd0fe3dc" />

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

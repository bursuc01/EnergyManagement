# Energy Manegement System

## 1. Introduction

This documentation outlines the structure and execution of an Energy Management System (EMS), comprising a frontend application and two microservices. The system is engineered to oversee users and their linked smart energy metering devices. Users are categorized into two types: administrators (managers) and clients. Administrators possess the capability to execute CRUD operations on user accounts, smart energy metering devices, and oversee the association of users with devices.

## 2. System Overview

The Energy Management System (EMS) uses the following components:
- Frontend Application: A user interface that provides access to the EMS for both administrators and clients.
- User Microservice: A microservice responsible for managing user accounts.
- Device Microservice: A microservice dedicated to managing smart energy metering devices.

## 3. User Roles

The system defines two primary user roles:
- Administrator: has full control over user accounts, smart energy metering devices, and user-device mappings.
- Client: can access and manage their own smart energy metering devices.

## 4. Functionality

### 4.1 Administrator Functions

Administrators can perform the following CRUD operations:
- Create, Read, Update, and Delete user accounts.
- Create, Read, Update, and Delete smart energy devices.
- Manage the mapping of users to devices, where each user can own one or more devices in different locations.

### 4.2 Client Functions

Clients can:
- View their own smart energy metering devices.
- Monitor the energy consumption of their devices.

## 5. Microservice Architecture

The system employs a microservice architecture to ensure scalability, modularity, and maintainability.

### 5.1 User Microservice

This microservice manages user-related data, including user accounts and their roles.
Endpoints:
- Create User
- Read User
- Update User
- Delete User

### 5.2 Device Microservice

This microservice manages information related to smart energy metering devices.
Endpoints:
- Create Device
- Read Device
- Update Device
- Delete Device
- Link User to Device
- Unlink User from Device

## 6. Non-Functional Requirements

The EMS is designed to meet the following non-functional requirements:
- Scalability: The system must accommodate an increasing amount of user and device data as it grows.
- Fast Response Time: The application must provide quick responses to user requests.
- Security: User data and privacy must be protected through robust authentication and authorization mechanisms.
- User-Friendly Interface: The frontend should have an intuitive and user-friendly design for easy navigation.
- Maintainability: The codebase must be well-documented and modular, allowing for easy modification and updates in the future.


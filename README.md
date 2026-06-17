# 🗳️ eVote360 Pro

![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![CQRS](https://img.shields.io/badge/Architecture-CQRS-blue)
![EF Core](https://img.shields.io/badge/EF%20Core-Migrations-green)
![MailKit](https://img.shields.io/badge/Email-MailKit-orange)
![OCR](https://img.shields.io/badge/OCR-Tesseract-yellow)
![Status](https://img.shields.io/badge/Project-Academic-lightgrey)

---

## 📌 Overview

**eVote360 Pro** is a full-stack electronic voting system designed to manage a complete electoral lifecycle, including:

- Citizen validation and authentication
- OCR-based identity verification
- Secure voting workflow
- Candidate and political party management
- Electoral positions and elections configuration
- Email-based verification and voting summary delivery
- Role-based administration system

The system ensures **confidentiality, integrity, and one-vote-per-citizen enforcement per election**.

---

## ⚙️ Architecture

The project follows a **clean architecture approach with CQRS pattern**, separating responsibilities into:

📦 eVote360
┣ 📂 Core
┃ ┣ Domain (Entities, Rules)
┃ ┗ Application (CQRS: Commands & Queries)
┣ 📂 Infrastructure
┃ ┣ Persistence (EF Core, Migrations)
┃ ┣ Email Service (MailKit)
┃ ┗ OCR Integration (Tesseract)
┣ 📂 Web
┃ ┗ ASP.NET Core MVC / API Layer


### 🧠 Design Patterns Used
- CQRS (Command Query Responsibility Segregation)
- Repository Pattern
- Dependency Injection
- Unit of Work (via EF Core)
- Service Layer Architecture

---

## 🔐 Key Features

### 🧑‍💼 Citizen Voting Flow
- Identity validation via document number
- Election availability check
- Citizen status validation (active/inactive)
- One vote per election rule enforcement

### 📷 OCR Identity Verification
- Upload of ID card image
- Processing using **Tesseract OCR**
- Validation of extracted document number
- Anti-fraud verification layer

### 📧 Email Verification System
- 6-digit verification code generation
- 5-minute expiration window
- Single-use security tokens
- Delivery via **MailKit SMTP integration**

### 🗳️ Voting Process
- Step-by-step voting per electoral position
- Option to select:
  - Candidate
  - “None” option
- Vote modification allowed until final submission
- Final confirmation locks all votes

### 📊 Election Management
- CRUD for:
  - Elections
  - Electoral positions
  - Political parties
  - Citizens
  - Users
- Candidate assignment system
- Political alliances support

### 📈 Administrative Dashboard
- Election summaries by year
- Statistics:
  - Total parties
  - Real candidates (deduplicated via alliances)
  - Citizens who completed voting

---

## 🧩 Business Rules Highlights

- A citizen can vote **only once per active election**
- OCR validation is mandatory before voting
- Email verification is required after OCR success
- Voting requires all positions to be completed before finalization
- Once finalized, votes cannot be modified
- Election data integrity is enforced (no modification during active elections)
- Candidates can belong to multiple parties (alliances system)

---

## 📦 Technology Stack

### Backend
- ASP.NET Core 8
- Entity Framework Core
- SQL Server

### Patterns & Architecture
- CQRS
- Clean Architecture
- Dependency Injection

### Services
- MailKit (SMTP Email Service)
- Tesseract OCR Engine

### Database
- SQL Server with EF Core Migrations
- User Secrets for sensitive configuration

---

## 📁 Configuration

### 🔐 User Secrets (Required)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=eVote360;Trusted_Connection=True;"
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "SenderEmail": "your-email@gmail.com",
    "SenderPassword": "your-app-password"
  }
}

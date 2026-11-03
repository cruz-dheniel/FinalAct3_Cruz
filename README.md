# Activity 3 — Sign In and Sign Up System

A simple **Restaurant Menu Management System** authentication activity built with **C# Windows Forms (.NET Framework 4.7.2)** and **Microsoft Access**.

## Features

- **Sign Up:** Register with a full name, username, password, and password confirmation.
- **Input validation:** Check required fields, matching passwords, minimum password length, and duplicate usernames.
- **Password security:** Store password hashes using PBKDF2 rather than plain-text passwords.
- **Sign In:** Check registered credentials against the Microsoft Access database.
- **Show/Hide Password:** Eye-icon controls for password fields.
- **Dashboard:** Open after a successful sign-in.
- **Log Out:** Return to the existing Sign In window and clear the fields.

## Technologies Used

- C#
- Windows Forms (.NET Framework 4.7.2)
- Visual Studio
- Microsoft Access (`.accdb`)
- `System.Data.OleDb`

## Main Project Files

| File | Purpose |
| --- | --- |
| `SigninForm.cs` | Sign-in and authentication |
| `Register.cs` | Account registration |
| `DashboardForm.cs` | Dashboard and logout |
| `Database.cs` | Microsoft Access connection |
| `PasswordHelper.cs` | Password hashing and verification |
| `Program.cs` | Application entry point |

The Windows Forms Designer files and image resources are included in the project.

## How to Open the Project

1. Download or clone this repository.
2. Open `FinalAct3_Cruz.sln` in Visual Studio on **Windows**.
3. Make sure the **.NET Framework 4.7.2** development tools are available.
4. Set up the Microsoft Access database as described below.
5. Build the solution, then press **F5** to run it.

## Database Setup (Required)

> **Note:** The database file is **not included** in this repository. The application currently expects it at `C:\FinalTerm\sampledb.accdb`. This is a local path used for the school activity; it is not yet portable.

1. Create the folder `C:\FinalTerm` if it does not already exist.
2. In Microsoft Access, create a blank database named `sampledb.accdb` inside that folder.
3. Create a table named `users` with these fields:

   | Field | Data Type | Notes |
   | --- | --- | --- |
   | `UserID` | AutoNumber | Primary Key |
   | `FullName` | Short Text | Required |
   | `Username` | Short Text | Required; Indexed: Yes (No Duplicates) |
   | `PasswordHash` | Short Text | Required; Field Size: 255 |

4. Save the table. No user records need to be added manually; use the app's Register form.
5. Make sure the **Microsoft Access Database Engine (ACE OLE DB provider)** is available and its 32-bit/64-bit architecture is compatible with the application.

If the database is in a different location, update the `dbPath` value in `FinalTerm/Database.cs` before running.

## Basic Usage

1. Open the application and select **Register Here**.
2. Create an account using the registration form.
3. Return to **Sign In** and enter the registered username and password.
4. After successful authentication, the **Dashboard** opens.
5. Select **Log Out** to return to the Sign In screen.

## Notes

- The `.gitignore` excludes Visual Studio temporary/build files and Access database files (`.accdb`, `.laccdb`).
- The application is a **school activity**, not a production-ready authentication service.
- Do not commit real user information or passwords to the repository.

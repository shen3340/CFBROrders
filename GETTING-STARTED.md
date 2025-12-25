# Getting Started

## Table of Contents

- [Requirements](#requirements)
- [Software Installation](#software-installation)
- [Getting the Code](#getting-the-code)
- [Running the App](#running-the-app)

---

## Requirements

To begin, you'll need a server or computer with at least 4 GB of RAM and at least 2 GB of free space. Typical Visual Studio installations can take up **10–50 GB** (per Microsoft).

**Note:** Visual Studio is only for Windows, although Mac users can use C# development through **VSCode's C# Dev Kit**.

---

## Software Installation

1. Install [Visual Studio](https://visualstudio.microsoft.com/downloads/).  
   When the installer opens, ensure the following **Workloads** are selected:
   - **ASP.NET and web development** (under Web & Cloud)  
   - **.NET Desktop development** (under Desktop & Mobile)  
   The correct individual components should be installed by default.

2. Install [Git](https://git-scm.com/download/win).

---

## Getting the Code

1. In Visual Studio, click **Clone a Repository**.  
2. Copy the repository URL:  
   `https://github.com/CollegeFootballRisk/CFBROrders.git`  
   into the repository location and click **Clone**.  
3. Once the solution loads, Visual Studio usually restores NuGet packages automatically.  
   - If not, go to **Tools → NuGet Package Manager → Package Manager Settings**.  
   - Make sure **Allow NuGet to download missing packages** and **Automatically check for missing packages during build in Visual Studio** are checked.  
4. Run the solution again.

---

## Running the App

1. In Visual Studio, set the **startup project** to `CFBROrders.Web`.  
2. In the debug dropdown, select **IIS Express** (not HTTPS).  
3. Click the **Play** icon (or press `F5`) to build and run the solution.  
4. The app should open automatically in your default web browser.

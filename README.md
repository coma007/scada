# SCADA

SCADA-like monitoring app. (Project for course SCADA Software) 

## Authors

- [Nemanja Majstorović](https://github.com/Cone3214)
- [Milica Sladaković](https://github.com/coma007)
- [Nemanja Dutina](https://github.com/eXtremeNemanja)

## Table of Contents

1.  [Overview](#overview)
2.  [Architecture](#architecture)
3.  [Prerequisites](#prerequisites)
4.  [Configuration](#configuration)
5.  [Getting started](#getting-started)
6.  [Usage](#usage)

## Overview

**SCADA** (Supervisory Control and Data Acquisition) is a system used to monitor and control industrial processes and infrastructure. It combines software, hardware, and communication networks to collect and analyze real-time data from sensors, devices, and machines, enabling operators to make informed decisions and optimize operations.  

This project aims to emulate the fundamental behavior of a SCADA monitoring system. Our objective was to develop a lightweight solution for monitoring tags and alarms, creating an application that could be easily extended for larger projects that might incorporate hardware components. Due to limited hardware resources, we simulated the behavior of RTU (Remote Terminal Unit) drivers instead.

## Architecture

The application consists of three main layers:
*  **Client App** - This layer provides a graphical interface for the monitoring application, allowing users to log in, manipulate tags and alarms, and monitor their status. The Client App communicates with the API via an HTTP connection, utilizing a mandatory JWT token for authentication. It comprises four key components:
    * **Database Manager** - Enables users to perform login, manipulate tags and alarms, and provides a basic preview of these entities.
    * **Trending** - Allows users to monitor real-time values of tags and their graphical representation.
    * **Alarm Display** - Provides notifications to users about new alarms, sorted by priority.
    * **Report Manager** -  Enables users to filter data and generate reports across five different categories.
*  **Api Layer** - This layer serves as a connection interface to the database and logging system. It provides secure access points for different client components, with each component relying on a specific API interface. Furthermore, the API layer generates WebSocket messages when specific events occur, ensuring that subscribed clients receive relevant information.
*  **SCADA Core**


Diagram bellow shows application arhitecture. [Click here](https://github.com/coma007/scada/blob/documentation/docs/diagram.png) to download the image.

![poster](https://github.com/coma007/scada/blob/documentation/docs/diagram.png)


## Prerequisites

Before running this project, ensure that you have the following prerequisites installed:

- [.NET Core 6](https://dotnet.microsoft.com/download/dotnet/6.0)
- [React 18.2.0](https://reactjs.org/)

### Installing .NET Core 6

1. Visit the [.NET downloads page](https://dotnet.microsoft.com/download/dotnet/6.0).
2. Follow the instructions specific to your operating system to install .NET Core 6.

### Installing React 18.2.0

React can be included as a dependency in your project using a package manager like npm or yarn. To install React 18.2.0, follow these steps:

1. Open a terminal or command prompt.
2. Navigate to your project's directory.
3. Run the following command to install React:  
<br>

   ```bash
   npm install react@18.2.0
   # or
   yarn add react@18.2.0
   ```
This will install React 18.2.0 and its dependencies in your project.

## Configuration

Describe the configuration options available for the project infrastructure. Include any configuration files or environment variables that need to be set. Explain the purpose and effect of each configuration option. If there are any sensitive or confidential configurations, mention how to handle them securely.

## Getting started

Once you have the necessary requirements installed, you can proceed to run the app and start working with it.  

To get started with this project, follow the steps below:

1. Clone the repository:  
   ```bash
   git clone https://github.com/coma007/scada.git
   ```
2. Navigate to the cloned repository:
   ```bash
   cd scada
   ```

### Running the API (scada-back)

The API application ([scada-back](https://github.com/coma007/scada/tree/main/scada-back)) needs to be run first as it serves as the backend for the project. Follow these steps to run the API:
1. Navigate to the scada-back directory:
   ```bash
   cd scada-back
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Run the API:
   ```bash
   dotnet run
   ```
   The API will be available at http://localhost:7109. The web app will execute and display the logs in console.

### Running the Console App (scada-core)

The console application ([scada-core](https://github.com/coma007/scada/tree/main/scada-core)) performs core functionalities for tag processing. Follow these steps to run the console app:
1. Navigate to the scada-core directory:
   ```bash
   cd scada-core
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Run the console app:
   ```bash
   dotnet run
   ```
   The console app will execute and display the logs in console.

### Running the React App (scada-front)

The React application ([scada-front](https://github.com/coma007/scada/tree/main/scada-front) serves as the frontend for the project. Follow these steps to run the React app:
1. Navigate to the scada-core directory:
   ```bash
   cd scada-front
   ```
2. Install dependencies:
   ```bash
   npm install
   # or
   yarn install
   ```
3. Start the React app:
   ```bash
   npm start
   # or
   yarn start
   ```
   The React app will be available at http://localhost:3000.

## Usage : TODO

Explain how to use the project infrastructure. Provide examples or use cases to demonstrate its functionality. Include any command-line instructions, API endpoints, or user interface information. If applicable, provide guidelines on how to access and interact with the infrastructure.


**Happy monitoring !** 📈💣

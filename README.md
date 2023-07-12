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
4.  [Installation](#installation)
5.  [Usage](#usage)
6.  [Configuration](#configuration)

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

List the prerequisites or dependencies required to set up and run the project infrastructure. Include both software and hardware requirements, as applicable. Be specific and provide versions if necessary.

## Installation

Provide detailed steps to install and configure the project infrastructure. This section should include all the necessary commands, configurations, and setups needed to get the project up and running. If there are different installation options or environments, mention them and provide instructions accordingly.

## Usage

Explain how to use the project infrastructure. Provide examples or use cases to demonstrate its functionality. Include any command-line instructions, API endpoints, or user interface information. If applicable, provide guidelines on how to access and interact with the infrastructure.

## Configuration

Describe the configuration options available for the project infrastructure. Include any configuration files or environment variables that need to be set. Explain the purpose and effect of each configuration option. If there are any sensitive or confidential configurations, mention how to handle them securely.

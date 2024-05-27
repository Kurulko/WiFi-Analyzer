# WiFi Analyzer

# Contents

- [Installation](#installation)
- [Overview](#overview)
- [Usage](#usage)
<br/>

## Installation

To get started with the WiFi Network Analyzer, follow the steps below:

1. **Clone the repository:**
   
   ```sh
   git clone https://github.com/Kurulko/WiFi-Analyzer.git
   
2. **Open the solution file in Visual Studio:**
   
   ```sh
   cd WiFi-Analyzer
   start WiFiNetworkAnalyzer.sln

3. **Ensure you have the required .NET MAUI workload installed in Visual Studio:**
  - Go to Tools > Get Tools and Features...
  - In the Visual Studio Installer, select the .NET Multi-platform App UI development workload and install it.

4. **Build and run the project in Visual Studio:**
  - Select your target platform (Windows) from the platform selector.
  - Press F5 to build and run the application.
<br/>

## Overview

The WiFi Network Analyzer is a comprehensive .NET MAUI application designed to provide detailed information and analysis of WiFi networks. It offers insights into various parameters of your WiFi connection and available networks.

## Pages

### 1. Home

![image](https://github.com/Kurulko/WiFi-Analyzer/assets/95112563/53b4edd9-8e08-43b7-ad2d-499e8eca0858)

- **SSID:** Name of the WiFi network
- **Channel:** Channel number of the WiFi network
- **Frequency:** Frequency of the WiFi network (2.4 GHz, 5 GHz, or 6 GHz)
- **Signal Level:** Signal strength of the WiFi network (in percentage or using a scale)
- **BSSID:** MAC address of the access point
- **Distance:** Approximate distance to the access point
- **Secured:** is secured WiFi network
- **Authentication:** Type of network authentication (PSK, EAP)
  
- **Download Speed:** Data download speed from the WiFi network

### 2. Connected

![image](https://github.com/Kurulko/WiFi-Analyzer/assets/95112563/3095427c-5025-43ac-a244-bdae6fd9f5b2)

#### General Information
- Reiterates the information displayed on the main page

#### IP Address
- **Private IPv4 Address:** Local IP address assigned to your device for communication within the network
- **Public IPv4 Address:** External IP address assigned by your ISP for internet identification (Note: Not all networks have a public IP address, some use NAT)
- **Subnet Mask:** Defines the portion of the IP address used to identify the network and the host within the network

#### Security
- **Authentication**: Type of network authentication (PSK, EAP)
- **Encryption:** Type of encryption used by the WiFi network to protect data (e.g., WPA2, WPA, Open)

#### Infrastructure
- **Interfaces:** Refers to various network interfaces on your device, typically only showing WiFi interface information on this page
- **Type:** Type of WiFi network standard (e.g., 802.11n, 802.11ac)

### 3. Networks

#### Display of All Available Networks

![image](https://github.com/Kurulko/WiFi-Analyzer/assets/95112563/c4827ebd-64a2-4af3-86f3-2a0984a0c2ae)

- **Sorting:** Ability to sort data by any column in the table (e.g., SSID, Signal Level, Distance)

![image](https://github.com/Kurulko/WiFi-Analyzer/assets/95112563/a6226aa9-81e4-4660-9fe5-93fea83c995b)

- **Signal Strength Graph:** Displays the signal strength of each WiFi network in the table
  - **X-Axis:** SSID of the WiFi network
  - **Y-Axis:** Signal strength (in dBm)

![image](https://github.com/Kurulko/WiFi-Analyzer/assets/95112563/a7fbd3d8-a596-48ea-8ef6-f1ef689e0d61)

- **Distance Graph:** Displays the distance to each WiFi network in the table
  - **X-Axis:** SSID of the WiFi network
  - **Y-Axis:** Distance (in meters)

<br/>
## Usage

To get started with the WiFi Network Analyzer, launch the application and navigate through the pages to view detailed information about your current WiFi connection and other available networks. Use the sorting and graphing features to analyze the data and make informed decisions to optimize your wireless connectivity.

## Requirements

- A device with WiFi capability
- .NET MAUI (Multi-platform App UI) framework
- Visual Studio 2022 or later with .NET MAUI workload installed

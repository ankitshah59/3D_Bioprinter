# 3D Bioprinter Control Software (C#)

A closed-loop **rotary direct-ink-writing (DIW) 3D bioprinter control system** written in **C# (WinForms)**.  
This software integrates motion control, syringe pump commands, G-code interpretation, and real-time **height feedback** from a **Keyence CL-3000 confocal sensor** for **PID-controlled Z-axis compensation**.

The entire **control logic** resides in `MainForm.cs`, with the **GUI layout** defined in `MainForm.Designer.cs`.  
The **Keyence CL3 API** is included only as an interface to stream confocal sensor data.

---

## Table of Contents
- [Overview](#overview)
- [Core Features](#core-features)
- [System Architecture](#system-architecture)
- [Hardware Requirements](#hardware-requirements)
- [Software Requirements](#software-requirements)
- [Repository Structure](#repository-structure)
- [Installation & Setup](#installation--setup)
- [Configuration](#configuration)
- [Workflow](#workflow)
- [G-code Extensions](#g-code-extensions)
- [PID Height Compensation](#pid-height-compensation)
- [Data Logging](#data-logging)
- [Safety & Interlocks](#safety--interlocks)
- [Troubleshooting](#troubleshooting)
- [Future Work](#future-work)
- [License](#license)

---

## Overview
This software powers a **custom-built rotary bioprinter** for printing **strain sensors, temperature sensors, heaters, passive RF components, and chipless RFID tags** on cylindrical substrates (e.g., catheters).

Key design principles:
- **Rotary motion (θ-axis)** replaces Cartesian Y translation  
- **Confocal sensor feedback** ensures accurate nozzle–substrate standoff  
- **Closed-loop PID Z compensation** adapts to surface fluctuations in real time  
- **Extensible G-code support** with custom M-codes for pumps and PID  

---

## Core Features
- **Stage Control**
  - X-axis: Thorlabs LTS150 linear stage
  - Z-axis: Thorlabs LTS150 linear stage (nozzle height)
  - θ-axis: Thorlabs PRM1Z8 rotary stage (substrate rotation)

- **Sensor Integration**
  - Streams real-time height from Keyence CL-3000 via CL3 API
  - PID loop maintains constant nozzle–substrate gap

- **Job Execution**
  - G-code parser with rotary-aware motion planning
  - Custom M-codes for pump control, PID tuning, and compensation toggle
  - Dry-run option (no pump)

- **GUI (WinForms)**
  - Jogging controls for each axis
  - G-code loader and preview
  - Job management (start, pause, stop, resume)
  - Real-time charts: sensor height, PID error, position

- **Data Logging**
  - Run logs (CSV) with position, sensor values, PID terms
  - Job reports (JSON) with configuration snapshot

---

## System Architecture
WinForms GUI
├── Jog panel (X/Z/θ)
├── Job loader & preview
├── Live charts
└── Alarms & interlocks

Core (MainForm.cs)
├── Motion planner (X, Z, θ)
├── G-code parser (incl. custom M-codes)
├── PID controller (Z compensation)
└── Data logger

Drivers
├── Thorlabs Kinesis SDK (LTS150, PRM1Z8)
├── Keyence CL3 API (CL-3000)
└── Syringe pump (serial interface)


---

## Hardware Requirements
- **Motion**:  
  - Thorlabs LTS150 (X, Z)  
  - Thorlabs PRM1Z8 (θ rotation)  

- **Sensor**:  
  - Keyence CL-3000 confocal displacement sensor  

- **Deposition**:  
  - Syringe pump (e.g., Chemyx Fusion 4000-X, serial-controllable)  

- **Substrate**:  
  - Cylindrical catheters (3–10 mm Ø recommended)  

---

## Software Requirements
- Windows 10/11  
- Visual Studio 2022  
- .NET 6.0+  
- Thorlabs Kinesis SDK (installed, PATH configured)  
- Keyence CL3 API/DLLs  
- (Optional) NI-DAQmx if analog I/O used for sensor readout  

---

## Repository Structure
/3D_Bioprinter
├── MainForm.cs # Core control logic (motion, PID, G-code)
├── MainForm.Designer.cs # WinForms GUI layout
├── Program.cs # Application entry point
├── config/ # JSON configs (machine, PID, pump)
├── gcode_samples/ # Example jobs
└── README.md # This file


---

## Installation & Setup
1. Clone the repo:
   ```bash
   git clone https://github.com/ankitshah59/3D_Bioprinter.git
Install Kinesis SDK and verify stage control.

Install Keyence CL3 API and confirm sensor readout.

Open solution in Visual Studio, build, and run.

Configure config/appsettings.json with:

Stage serial numbers

COM ports for pump/sensor

PID parameters

## Configuration

Example appsettings.json:
{
  "Stages": {
    "X": { "Type": "LTS150", "SerialNumber": "55000123" },
    "Z": { "Type": "LTS150", "SerialNumber": "55000456" },
    "Theta": { "Type": "PRM1Z8", "SerialNumber": "83830077" }
  },
  "Sensor": {
    "Model": "KeyenceCL3000",
    "Port": "COM7",
    "Baud": 115200,
    "SamplePeriodUs": 100
  },
  "PID": {
    "SetpointMm": 5.0,
    "Kp": 0.45,
    "Ki": 0.05,
    "Kd": 0.0
  },
  "Pump": {
    "Type": "ChemyxFusion",
    "Port": "COM6",
    "DefaultFlowUlPerMin": 6.0
  }
}


## Workflow

Home axes (X, Z, θ).

Zero confocal sensor to set nozzle standoff reference.

Load G-code job and preview toolpath.

Dry-run to confirm clearance.

Start job with pump enabled; monitor live charts.

Save logs after job completes.

G-code Extensions

Supported codes:

G0/G1 X… Z… F… → linear moves (θ computed automatically)

G4 P… → dwell (ms)

M3 S… → pump on (S = flow µL/min)

M5 → pump off

M200 H… → set PID standoff height (mm)

M201 P… I… D… → set PID gains

M202 S0/1 → disable/enable Z compensation

## PID Height Compensation

Runs at 10 kHz (100 µs) sampling rate from CL-3000.

Adjusts Z-axis in real time to maintain constant nozzle–substrate gap.

Anti-windup and slew-rate limiting prevent oscillations.

## Data Logging

CSV logs: position, sensor values, PID error, flow state.

JSON job reports: config snapshot and runtime metadata.

## Safety & Interlocks

Emergency stop halts motion + pump.

Enclosure interlock (if installed) pauses job.

Soft limits prevent over-travel.

Sensor watchdog disables PID on dropout.

## Troubleshooting

Stages not moving → check Kinesis installation and serial numbers.

Sensor noise → verify rigid mount, adjust PID gains.

Uneven line width → recalibrate diameter mapping (ThetaPerMmDeg).

Pump not responding → confirm COM port and baud rate.

## Future Work

Multi-material toolhead support

Automated substrate diameter mapping

Advanced G-code preview with 3D visualization

Cloud log storage for process tracking


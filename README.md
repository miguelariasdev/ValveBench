#ValveBench

The domain problem simulated by ValveBench is a **valve pressure test**.  
Industrial valves are often tested in a controlled environment to ensure they can withstand target pressures, cycles, and temperature without leaking.

## Tech stack
ValveBench is a project designed to showcase a microservices architecture using **Avalonia Desktop UI, .NET9, gRPC, REST API.**

The application simulates a simplified valve testing bench:
- A **Desktop Avalonia Ui** lets the user configure a test profile for the valve (pressures, cycles, temperature).
- A **REST API (OperationsApi)** receives the request and orchestrates the workflow.
- A **gRPC service (CalculationsService)** handles the calculation logic and returns the result.
- Results are shown back in the Avalonia desktop app.

The backend is fully containerized with **Docker Compose**, while the Avalonia UI runs natively on the host machine.

### Prerequisites
- .NET 9 installed on your machine.
- Docker Desktop

## Usage
**- Build and start the containers (REST API + gRPC service):** docker compose up --build
**Run the Avalonia Desktop app:** dotnet run --project src/ValveBench.Desktop

## Business logic
- **Test Profile**  
  The operator configures a profile with:
  - **P1:** initial pressure  
  - **P2:** target pressure  
  - **Cycles:** number of times to go from P1 -> P2 -> P1  
  - **Temperature :** environment temperature  

- **Calculations**  
  The gRPC service simulates the outcome of applying this test:
  - **Peak Pressure:** the highest pressure reached (max of P1 and P2).  
  - **Pressure Drop:** an estimated drop in pressure after repeating the cycles.  
    - A small percentage of drop is added per cycle.  
    - Additional drop is added if the temperature is above 60 °C.  
  - **Leak Detected (true/false):**  
    If the total pressure drop is greater than 2% of the peak pressure, the system flags a leak.

## Example
In the Avalonia UI, configure a test profile:
- P1 = 100 bar
- P2 = 120 bar
- Cycles = 50
- Temperature = 60 °C
Result: Peak: 120 bar | Drop: 3 bar | Leak: True

<img width="643" height="475" alt="image" src="https://github.com/user-attachments/assets/33ea20e4-dbd7-4292-b8bc-ec1eea87afae" />

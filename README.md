# C# Payouts API Integration

This is a C# console application for making a payment request using the dLocal Payouts API. It demonstrates how to generate the required HMACSHA256 signature and send a POST request to the API endpoint.

## Prerequisites

Before running this application, make sure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download)

## Getting Started

1. Clone this repository:

```bash
git clone https://github.com/your-username/csharp-payouts.git
cd csharp-payouts
```

2. Open the Program.cs file and replace the placeholder values for login, transKey, and secretKey with your actual credentials.

3. Ensure that the System.Net.Http NuGet package is installed:
```bash
dotnet restore
```
4. Run the application:
```bash
dotnet run
```

## Configuration
* apiUrl: The dLocal Payouts API endpoint.
* login: Your API login.
* transKey: Your API transaction key.
* secretKey: Your API secret key.

## Features
* Generates a random external_id for each payment request.
* Calculates the HMACSHA256 signature for the request payload.
* Sends a POST request to the dLocal Payouts API.
* Displays the response from the API.

## Contributing
If you have improvements or suggestions, feel free to open an issue or create a pull request.

## License
This project is licensed under the MIT License - see the LICENSE file for details.


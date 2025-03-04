# Smart Attendance API

## Overview
Smart Attendance API is a backend service for managing student attendance in educational institutions using facial recognition and geofencing. The API provides features for authentication, course management, attendance tracking, and more.

## Technologies Used
- **Backend Framework**: ASP.NET Core
- **Database**: MSSQL

## Features
- User authentication (JWT-based)
- Student and lecturer management
- Course enrollment
- Attendance tracking with geofencing verification
- Facial recognition integration (upcoming)
- Error handling and logging
- Rate limiting and caching for optimized performance

## API Endpoints
📍 [**View Detailed API Endpoint Documentation**](ENDPOINTS.md)

Comprehensive documentation for all available API endpoints, including:
- Authentication routes
- Faculty and department management
- Course and enrollment endpoints
- Student and attendance tracking

## Installation
### Prerequisites
- .NET 8 SDK or later
- MSSQL Server

### Setup
1. **Clone the repository**
   ```sh
   git clone https://github.com/sim-codes/Smart-Attendance-System-API.git
   cd Smart-Attendance-System-API
   ```

2. **Run database migrations**
   ```sh
   dotnet ef database update
   ```

3. **Start the API**
   ```sh
   dotnet run
   ```

## Testing
- Use `Postman` or any API testing tool to test the endpoints.
- Run unit tests with:
  ```sh
  dotnet test
  ```

## Contributing
1. Fork the repository
2. Create a new branch (`feature-name`)
3. Commit your changes
4. Push to your branch
5. Open a pull request

## License
This project is licensed under the MIT License.

## Contact
For any inquiries, contact [segunmichael24@gmail.com].
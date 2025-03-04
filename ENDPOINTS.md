# API Endpoints Documentation

## Faculty Endpoints
| Operation | HTTP Method | Endpoint | Description |
|-----------|-------------|----------|-------------|
| Get All Faculties | GET | `/faculties` | Retrieve all faculties |
| Get Single Faculty | GET | `/faculties/{id}` | Retrieve a specific faculty by ID |
| Create Faculty | POST | `/faculties` | Create a new faculty |

## Level Endpoints
| Operation | HTTP Method | Endpoint | Description |
|-----------|-------------|----------|-------------|
| Get All Levels | GET | `/levels` | Retrieve all levels |
| Get Single Level | GET | `/levels/{id}` | Retrieve a specific level by ID |
| Create Level | POST | `/levels` | Create a new level |

## Class Schedule Endpoints
| Operation | HTTP Method | Endpoint | Description |
|-----------|-------------|----------|-------------|
| Get All Class Schedules | GET | `/class-schedules` | Retrieve all class schedules |
| Get Class Schedules by Course IDs | GET | `/class-schedules/courses` | Retrieve class schedules for specific courses |
| Get Single Class Schedule | GET | `/class-schedules/{id}` | Retrieve a specific class schedule by ID |
| Create Class Schedule | POST | `/class-schedules` | Create a new class schedule |
| Update Class Schedule | PUT | `/class-schedules/{id}` | Update an existing class schedule |
| Delete Class Schedule | DELETE | `/class-schedules/{id}` | Delete a specific class schedule |

## Department Endpoints
| Operation | HTTP Method | Endpoint | Description |
|-----------|-------------|----------|-------------|
| Get All Departments in Faculty | GET | `/faculties/{facultyId}/departments` | Retrieve all departments within a specific faculty |
| Get Single Department | GET | `/faculties/{facultyId}/departments/{id}` | Retrieve a specific department by ID within a faculty |
| Create Department | POST | `/faculties/{facultyId}/departments` | Create a new department in a specific faculty |

## Course Endpoints
| Operation | HTTP Method | Endpoint | Description |
|-----------|-------------|----------|-------------|
| Get All Courses in Department | GET | `/departments/{departmentId}/courses` | Retrieve all courses within a specific department |
| Get Single Course | GET | `/departments/{departmentId}/courses/{id}` | Retrieve a specific course by ID within a department |
| Create Course | POST | `/departments/{departmentId}/courses` | Create a new course in a specific department |

## Authentication Endpoints
| Operation | HTTP Method | Endpoint | Description |
|-----------|-------------|----------|-------------|
| Login | POST | `/authentication/login` | User login |
| Register | POST | `/authentication` | User registration |
| Reset Password | POST | `/authentication/reset-password` | Reset user password |
| Change Password | POST | `/authentication/change-password` | Change user password |
| Generate Reset Token | POST | `/authentication/generate-reset-token` | Generate a password reset token |
| Refresh Token | POST | `/token/refresh` | Refresh authentication token |

## Student Endpoints
| Operation | HTTP Method | Endpoint | Description |
|-----------|-------------|----------|-------------|
| Get All Students | GET | `/students` | Retrieve all students |
| Get Single Student | GET | `/students/{id}` | Retrieve a specific student by ID |
| Create Student | POST | `/students/{id}` | Create a new student |
| Update Student | PUT | `/students/{id}` | Update an existing student |

## Attendance Endpoints
| Operation | HTTP Method | Endpoint | Description |
|-----------|-------------|----------|-------------|
| Get All Students (for Attendance) | GET | `/students` | Retrieve all students |
| Get Single Student (for Attendance) | GET | `/students/{id}` | Retrieve a specific student by ID |
| Sign Attendance | POST | `/attendance/{studentId}` | Sign attendance for a student |
| Sign Attendance Without Location | POST | `/attendance/{studentId}/signin` | Sign attendance without location tracking |

## Enrollment Endpoints
| Operation | HTTP Method | Endpoint | Description |
|-----------|-------------|----------|-------------|
| Get All Enrollments | GET | `/enrollments/{studentId}` | Retrieve all enrollments for a student |
| Get Single Enrollment | GET | `/enrollments/{studentId}/{courseId}` | Retrieve a specific enrollment for a student and course |
| Create Enrollment | POST | `/enrollments/{studentId}` | Create a new enrollment for a student |
| Delete Enrollment | DELETE | `/enrollments/{studentId}/{courseId}` | Delete a specific enrollment for a student and course |

## Notes
- Replace `{id}`, `{facultyId}`, `{departmentId}`, `{studentId}`, and `{courseId}` with actual numeric or unique identifiers.
- Ensure proper authentication and authorization for accessing these endpoints.
- All endpoints may require specific request body formats and return corresponding response structures.

using Contracts;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private Lazy<IFacultyRepository> _facultyRepository;
        private Lazy<IDepartmentRepository> _departmentRepository;
        private Lazy<IClassroomRepository> _classroomRepository;
        private Lazy<IStudentRepository> _studentRepository;
        private Lazy<ILecturerRepository> _lecturerRepository;
        private Lazy<IAcademicSessionRepository> _academicSessionRepository;
        private Lazy<IClassScheduleRepository> _classScheduleRepository;
        private Lazy<IAttendanceRepository> _attendanceRepository;
        private Lazy<ICourseRepository> _courseRepository;
        private Lazy<ILevelRepository> _levelRepository;
        private Lazy<IEnrollmentRepository> _enrollmentRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _facultyRepository = new Lazy<IFacultyRepository>(() => new FacultyRepository(_repositoryContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_repositoryContext));
            _classroomRepository = new Lazy<IClassroomRepository>(() => new ClassroomRepository(_repositoryContext));
            _studentRepository = new Lazy<IStudentRepository>(() => new StudentRepository(_repositoryContext));
            _lecturerRepository = new Lazy<ILecturerRepository>(() => new LecturerRepository(_repositoryContext));
            _academicSessionRepository = new Lazy<IAcademicSessionRepository>(() => new AcademicSessionRepository(_repositoryContext));
            _classScheduleRepository = new Lazy<IClassScheduleRepository>(() => new ClassScheduleRepository(_repositoryContext));
            _attendanceRepository = new Lazy<IAttendanceRepository>(() => new AttendanceRepository(_repositoryContext));
            _courseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(_repositoryContext));
            _levelRepository = new Lazy<ILevelRepository>(() => new LevelRepository(_repositoryContext));
            _enrollmentRepository = new Lazy<IEnrollmentRepository>(() => new EnrollmentRepository(_repositoryContext));
        }

        public IFacultyRepository Faculty => _facultyRepository.Value;
        public ILevelRepository Level => _levelRepository.Value;
        public IAttendanceRepository Attendance => _attendanceRepository.Value;
        public IDepartmentRepository Department => _departmentRepository.Value;
        public IClassroomRepository Classroom => _classroomRepository.Value;
        public IStudentRepository Student => _studentRepository.Value;
        public ILecturerRepository Lecturer => _lecturerRepository.Value;
        public IAcademicSessionRepository AcademicSession => _academicSessionRepository.Value;
        public IClassScheduleRepository ClassSchedule => _classScheduleRepository.Value;
        public ICourseRepository Course => _courseRepository.Value;
        public IEnrollmentRepository CourseEnrollment => _enrollmentRepository.Value;

        public void Save() => _repositoryContext.SaveChanges();
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}

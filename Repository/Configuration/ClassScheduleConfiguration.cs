using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class ClassScheduleConfiguration : IEntityTypeConfiguration<ClassSchedule>
    {
        public void Configure(EntityTypeBuilder<ClassSchedule> builder)
        {
            builder.HasData(
                // Monday Schedules
                new ClassSchedule
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440001"),
                    DayOfWeek = "Monday",
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(10, 0),
                    SessionId = new Guid("154d65a8-cf33-417a-c99b-08dd282b06d7"),
                    CourseId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                    ClassroomId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    LevelId = new Guid("d4d4c053-49b6-410c-bc78-2d54a9991870"),
                    DepartmentId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                },
                new ClassSchedule
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440002"),
                    DayOfWeek = "Monday",
                    StartTime = new TimeOnly(10, 30),
                    EndTime = new TimeOnly(12, 30),
                    SessionId = new Guid("154d65a8-cf33-417a-c99b-08dd282b06d7"),
                    CourseId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52c"),
                    ClassroomId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    LevelId = new Guid("d4d4c053-49b6-410c-bc78-2d54a9991870"),
                    DepartmentId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                },
                // Tuesday Schedules
                new ClassSchedule
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440003"),
                    DayOfWeek = "Tuesday",
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(11, 0),
                    SessionId = new Guid("154d65a8-cf33-417a-c99b-08dd282b06d7"),
                    CourseId = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                    ClassroomId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    LevelId = new Guid("d4d4c053-49b6-410c-bc78-2d54a9991870"),
                    DepartmentId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                },
                // Wednesday Schedules
                new ClassSchedule
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440004"),
                    DayOfWeek = "Wednesday",
                    StartTime = new TimeOnly(8, 30),
                    EndTime = new TimeOnly(10, 30),
                    SessionId = new Guid("154d65a8-cf33-417a-c99b-08dd282b06d7"),
                    CourseId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                    ClassroomId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    LevelId = new Guid("d4d4c053-49b6-410c-bc78-2d54a9991870"),
                    DepartmentId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                },
                // Thursday Schedules
                new ClassSchedule
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440005"),
                    DayOfWeek = "Thursday",
                    StartTime = new TimeOnly(10, 0),
                    EndTime = new TimeOnly(12, 0),
                    SessionId = new Guid("154d65a8-cf33-417a-c99b-08dd282b06d7"),
                    CourseId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52c"),
                    ClassroomId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    LevelId = new Guid("d4d4c053-49b6-410c-bc78-2d54a9991870"),
                    DepartmentId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                },
                // Friday Schedules
                new ClassSchedule
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440006"),
                    DayOfWeek = "Friday",
                    StartTime = new TimeOnly(9, 30),
                    EndTime = new TimeOnly(11, 30),
                    SessionId = new Guid("154d65a8-cf33-417a-c99b-08dd282b06d7"),
                    CourseId = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                    ClassroomId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    LevelId = new Guid("d4d4c053-49b6-410c-bc78-2d54a9991870"),
                    DepartmentId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                }
            );
        }
    }
}

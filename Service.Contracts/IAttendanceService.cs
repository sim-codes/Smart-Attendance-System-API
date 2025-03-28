﻿using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IAttendanceService
    {
        (IEnumerable<AttendanceDto> attendanceRecords, MetaData metaData) GetAttendanceRecords(AttendanceParameters attendanceParameters, bool trackChanges);
        AttendanceDto GetAttendance(Guid attendanceId, bool trackChanges);
        Task<(bool isValid, string message)> ValiidateAttendance(AttendanceForCreationDto attendance);
        Task<AttendanceDto> CreateAttendance(string userId,AttendanceForCreationDto attendance);
        Task<AttendanceDto> SignAttendanceWithoutLocation(string userId, AttendanceForCreationDto attendance);
        Task AutoSignAttendanceForActiveClasses();
    }
}

using AutoMapper;
using HR.LeaveManagement.Application.features.leaveType.commands.createLeaveType;
using HR.LeaveManagement.Application.features.leaveType.commands.updateLeaveType;
using HR.LeaveManagement.Application.features.leaveType.queries.getAllLeaveTypes;
using HR.LeaveManagement.Application.features.leaveType.queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.mappingProfiles
{
    public class LeaveTypeProfile : Profile
    {
        public LeaveTypeProfile()
        {
            CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDetailsDto>();
            CreateMap<CreateLeaveTypeCommand, LeaveType>();
            CreateMap<UpdateLeaveTypeCommand, LeaveType>();
        }
    }
}

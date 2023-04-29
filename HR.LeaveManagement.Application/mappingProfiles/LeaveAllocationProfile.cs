using AutoMapper;
using HR.LeaveManagement.Application.features.leaveAllocation.commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.features.leaveAllocation.commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.features.leaveAllocation.queries.getLeaveAllocation;
using HR.LeaveManagement.Application.features.leaveAllocation.queries.getLeaveAllocationDetails;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.mappingProfiles
{
    public class LeaveAllocationProfile : Profile
    {
        public LeaveAllocationProfile()
        {
            CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
            CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
            CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
        }
    }
}

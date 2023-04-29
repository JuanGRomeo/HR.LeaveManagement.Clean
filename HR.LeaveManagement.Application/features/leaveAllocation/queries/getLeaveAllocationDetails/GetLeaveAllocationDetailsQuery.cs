using HR.LeaveManagement.Application.Features.LeaveAllocation.queries.getLeaveAllocation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.queries.getLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailsQuery : IRequest<LeaveAllocationDetailsDto>
    {
        public int Id { get; set; }
    }
}

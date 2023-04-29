using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.queries.getLeaveAllocation
{
    public class GetLeaveAllocationListQuery : IRequest<List<LeaveAllocationDto>>
    {
    }
}

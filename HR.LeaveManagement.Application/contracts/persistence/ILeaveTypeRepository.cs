using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.contracts.persistence
{
    public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
    {
        Task<bool> IsLeaveTypeUnique(string name);
    }
}

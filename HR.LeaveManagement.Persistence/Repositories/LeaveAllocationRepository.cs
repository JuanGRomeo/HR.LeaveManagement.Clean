using HR.LeaveManagement.Application.contracts.persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HRDatabaseContext context) : base(context)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _context.AddRangeAsync(allocations);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userid, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(leaveAllocation => leaveAllocation.EmployeeId == userid
                                                            && leaveAllocation.LeaveTypeId == leaveTypeId
                                                            && leaveAllocation.Period == period);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Include(leaveAllocations => leaveAllocations.LeaveType)
                .ToListAsync();

            return leaveAllocations;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Where(LeaveAllocation => LeaveAllocation.EmployeeId.Equals(userId))
                .Include(leaveAllocations => leaveAllocations.LeaveType)
                .ToListAsync();

            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var leaveAllocation = await _context.LeaveAllocations
                .Include(leaveAllocation => leaveAllocation.LeaveType)
                .FirstOrDefaultAsync(leaveAllocation => leaveAllocation.Id == id);

            return leaveAllocation;
        }

        public async Task<LeaveAllocation> GetUserAllocations(string userid, int leaveTypeid)
        {
            return await _context.LeaveAllocations.FirstOrDefaultAsync(leaveAllocation => leaveAllocation.EmployeeId == userid 
                                        && leaveAllocation.LeaveTypeId == leaveTypeid);
        }
    }
}

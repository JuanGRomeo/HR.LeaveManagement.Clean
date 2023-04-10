using AutoMapper;
using HR.LeaveManagement.Application.contracts.persistence;
using HR.LeaveManagement.Application.exceptions;
using HR.LeaveManagement.Application.features.leaveType.commands.updateLeaveType;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.features.leaveType.commands.deleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        { 
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //Validate

            //get the object from db
            var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);

            //verify that record exists
            if (leaveTypeToDelete == null)
                throw new NotFoundException(nameof(LeaveType), request.Id);

            //remove it from db
            await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

            //return record Id
            return Unit.Value;
        }
    }
}

﻿using AutoMapper;
using HR.LeaveManagement.Application.contracts.persistence;
using HR.LeaveManagement.Application.exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.features.leaveAllocation.commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveAllocationCommandHandler(
            IMapper mapper, 
            ILeaveAllocationRepository leaveAllocationRepository, 
            ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)             
                throw new BadRequestException("Invalid Leave Allocation Request", validationResult);

            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            //Get employees

            //Get period

            //Assign Allocations
            var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);
            await _leaveAllocationRepository.CreateAsync(leaveAllocation);
            return Unit.Value;
            
        }
    }
}
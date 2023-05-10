using AutoMapper;
using Carter;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.MinimalApi.Features.LeaveType
{
    public record CreateLeaveTypeCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public int DefaultDays { get; set; }
    }

    public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(createLeaveTypeCommand => createLeaveTypeCommand.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

            RuleFor(createLeaveTypeCommand => createLeaveTypeCommand.DefaultDays)
                .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1")
                .LessThan(100).WithMessage("{PropertyName} cannot exceed 100");

            RuleFor(createLeaveTypeCommand => createLeaveTypeCommand)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("Leave Type already exists");

            _leaveTypeRepository = leaveTypeRepository;
        }

        private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
        {
            return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }
    }

    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //Validate
            var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new BadRequestException("Invalid LeaveType", validationResult);

            //convert to domain entity object
            var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

            //add to db
            await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);

            //return record Id
            return leaveTypeToCreate.Id;
        }
    }

    public class CreateLeaveTypeModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/leavetypes", async ([FromBody] CreateLeaveTypeCommand leaveType, IMediator mediator) => {
                var createdLeaveTypeId = await mediator.Send(leaveType);

                return createdLeaveTypeId;
            })
            .Produces<int>(StatusCodes.Status201Created)
            .WithName("CreateLeaveType");
        }
    }
}

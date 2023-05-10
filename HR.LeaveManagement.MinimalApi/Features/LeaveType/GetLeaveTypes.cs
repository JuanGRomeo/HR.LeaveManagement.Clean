using AutoMapper;
using Carter;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.MinimalApi.Features.LeaveType
{
    public class LeaveTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DefaultDays { get; set; }
    }

    public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;

    public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

        public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<GetLeaveTypesQueryHandler> logger)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
        {
            //query db
            var leaveTypes = await _leaveTypeRepository.GetAsync();

            //convert data objects to dto
            var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            //return list of DTO
            _logger.LogInformation("Leave types were retrieved successfully");
            return data;
        }
    }

    public class GetLeaveTypesModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/leavetypes", async (IMediator mediator) => { 
                var query = new GetLeaveTypesQuery();

                var leaveTypes = await mediator.Send(query);

                return leaveTypes;
            })
            .Produces<List<LeaveTypeDto>>(StatusCodes.Status200OK)
            .WithName("GetLeaveTypes");
        }
    }
}

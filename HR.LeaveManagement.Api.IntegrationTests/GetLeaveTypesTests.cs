using HR.LeaveManagement.Api.IntegrationTests.Shared;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.ComponentModel;

namespace HR.LeaveManagement.Api.IntegrationTests
{
    public class GetLeaveTypesTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public GetLeaveTypesTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async void GetLeaveTypes() 
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var dbContext = scopedServices.GetRequiredService<HRDatabaseContext>();

                dbContext.Database.EnsureCreated();

                //dbContext.LeaveTypes.Add(new Domain.LeaveType { Id = 1, Name = "LeaveType1" });
                //dbContext.LeaveTypes.Add(new Domain.LeaveType { Id = 2, Name = "LeaveType2" });
                //dbContext.SaveChanges();
            }

            //Act 
            var response = await _httpClient.GetAsync("/api/leavetypes/");
            var responseContent = response.Content.ReadAsStringAsync().Result;

            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
using AutoFixture;
using JobApplicantsManagement.Features.Commands;
using JobApplicantsManagement.Features.Handlers;
using JobApplicantsManagement.Infrastructure.Exceptions;
using JobApplicantsManagement.Infrastructure.Presistence;
using JobApplicantsManagement.Infrastructure.Presistence.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Reflection.Metadata;

namespace JobsApplicants.Tests
{
    public class UpdateJobApplicantTests
    {
        [Fact]
        public async Task WhenUpdatingAnExistingJobApplicant_IdShouldReturn()
        {
            //Arrange
            var mediator = new Mock<IMediator>();

            var fixture = MockAutoFixture.GetFixture();

            var jobApplicants = fixture.Build<JobApplicant>()
                .With(a => a.Id, new Guid("4ee750bb-533e-4ae8-4de9-08dc72a8f555"))
                .CreateMany(1)
                .AsQueryable()
                .BuildMockDbSet();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(c => c.JobApplicants).Returns(jobApplicants.Object);


            UpdateJobApplicantCommand command = new UpdateJobApplicantCommand()
            {
                Id = new Guid("4ee750bb-533e-4ae8-4de9-08dc72a8f555"),
                AvailableFrom = 0,
                AvailableTo = 2359,
                Comment = "Test comment",
                Email = "test@gmail.com",
                FirstName = "Mud",
                LastName = "Zak",
                PhoneNumber = "+18001234567"
            };
            UpdateJobApplicantCommandHandler handler = new UpdateJobApplicantCommandHandler(mockContext.Object);

            //Act
            var cancellationToken = new CancellationToken();
            Guid id = await handler.Handle(command, cancellationToken);

            //Assert
            Assert.Equal(new Guid("4ee750bb-533e-4ae8-4de9-08dc72a8f555"), id);
        }


        [Fact]
        public async Task WhenUpdatingANonExistingJobApplicant_NotFoundExceptionShouldBeThrown()
        {
            //Arrange
            var mediator = new Mock<IMediator>();

            var fixture = MockAutoFixture.GetFixture();

            var jobApplicants = fixture.Build<JobApplicant>()
                .With(a => a.Id)
                .CreateMany(1)
                .AsQueryable()
                .BuildMockDbSet();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(c => c.JobApplicants).Returns(jobApplicants.Object);


            UpdateJobApplicantCommand command = new UpdateJobApplicantCommand()
            {
                Id = Guid.NewGuid(),
                AvailableFrom = 0,
                AvailableTo = 2359,
                Comment = "Test comment",
                Email = "test@gmail.com",
                FirstName = "Mud",
                LastName = "Zak",
                PhoneNumber = "+18001234567"
            };
            UpdateJobApplicantCommandHandler handler = new UpdateJobApplicantCommandHandler(mockContext.Object);

            //Act and Assert
            var cancellationToken = new CancellationToken();
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, cancellationToken));
        }

    }
}
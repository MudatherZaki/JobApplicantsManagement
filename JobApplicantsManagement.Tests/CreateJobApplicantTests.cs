using JobApplicantsManagement.Features.Commands;
using JobApplicantsManagement.Features.Handlers;
using JobApplicantsManagement.Infrastructure.Exceptions;
using JobApplicantsManagement.Infrastructure.Presistence;
using JobApplicantsManagement.Infrastructure.Presistence.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Reflection.Metadata;

namespace JobsApplicants.Tests
{
    public class CreateJobApplicantTests
    {
        [Fact]
        public async Task WhenCreatingAJobApplicant_IdShouldReturn()
        {
            //Arrange
            var mediator = new Mock<IMediator>();

            var mockSet = new Mock<DbSet<JobApplicant>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.JobApplicants).Returns(mockSet.Object);

            CreateJobApplicantCommand command = new CreateJobApplicantCommand()
            {
                AvailableFrom = 0,
                AvailableTo = 2359,
                Comment = "Test comment",
                Email = "test@gmail.com",
                FirstName = "Mud",
                LastName = "Zak",
                PhoneNumber = "+18001234567"
            };
            CreateJobApplicantCommandHandler handler = new CreateJobApplicantCommandHandler(mockContext.Object);

            //Act
            var cancellationToken = new CancellationToken();
            Guid id = await handler.Handle(command, cancellationToken);

            //Assert
            //Do the assertion
            mockSet.Verify(m => m.Add(It.IsAny<JobApplicant>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once());
        }

        [Fact]
        public async Task WhenCreatingAJobApplicantWithMissingName_BadRequestExceptionShouldBeThrown()
        {
            //Arrange
            var mediator = new Mock<IMediator>();

            var mockSet = new Mock<DbSet<JobApplicant>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.JobApplicants).Returns(mockSet.Object);

            CreateJobApplicantCommand command = new CreateJobApplicantCommand()
            {
                AvailableFrom = 0,
                AvailableTo = 2359,
                Comment = "Test comment",
                Email = "test@gmail.com",
                PhoneNumber = "1234567890"
            };
            CreateJobApplicantCommandHandler handler = new CreateJobApplicantCommandHandler(mockContext.Object);

            //Act and Assert
            var cancellationToken = new CancellationToken();
            await Assert.ThrowsAsync<BadRequestException>(async () => 
                await handler.Handle(command, cancellationToken));
        }

    }
}
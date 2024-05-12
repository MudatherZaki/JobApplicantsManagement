using JobApplicantsManagement.Features.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicantsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicantsController : ControllerBase
    {
        private IMediator _mediator;
        public JobApplicantsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("{id?}")]
        public async Task<IActionResult> CreateOrUpdateApplicant(
            [FromRoute] Guid? id, 
            [FromBody] JobApplicantDto inputCommand)
        {
            if(!id.HasValue)
            {

                CreateJobApplicantCommand command = 
                    new CreateJobApplicantCommand(inputCommand);
                id = await _mediator.Send(command);
            }
            else
            {
                UpdateJobApplicantCommand command = 
                    new UpdateJobApplicantCommand(id.Value, inputCommand);
                await _mediator.Send(command);
            }

            return Ok(id);
        }
    }
}

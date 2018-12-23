using Aquedata.Model;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Aquedata.Validation
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private readonly IRequestValidator _validator;
        private readonly IValidationJobFactory _jobFactory;

        public ValidationController(IRequestValidator validator, IValidationJobFactory jobFactory)
        {
            _validator = validator;
            _jobFactory = jobFactory;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400, Type = typeof(string))]
        public ActionResult ValidateData([FromBody] ValidationRequest request)
        {
            var requestValidation = _validator.Validate(request);
            if (!requestValidation.IsValid)
            {
                return BadRequest(requestValidation.InvalidReason);
            }

            var job = _jobFactory.Create(request);
            string jobId = BackgroundJob.Enqueue(() => job.Execute(request));

            return Ok(jobId);
        }
    }
}
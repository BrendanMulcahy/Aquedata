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

        public ValidationController(IRequestValidator validator)
        {
            _validator = validator;
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

            string jobId = BackgroundJob.Enqueue(() => new ValidationJob().Execute(request));

            return Ok(jobId);
        }
    }
}
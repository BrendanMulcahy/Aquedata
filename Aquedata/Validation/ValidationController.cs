using Aquedata.Model;
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult ValidateData([FromBody] ValidationRequest request)
        {
            if (!_validator.Validate(request))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
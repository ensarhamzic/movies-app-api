using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies_API.Data.Services;
using Movies_API.Data.ViewModels;

namespace Movies_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishesController : ControllerBase
    {
        private PublishService publishService;

        public PublishesController(PublishService publishService)
        {
            this.publishService = publishService;
        }

        [HttpGet, Authorize]
        public IActionResult GetPublish()
        {
            try
            {
                var publish = publishService.GetUserPublish();
                return Ok(new {publish});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost, Authorize]
        public IActionResult SetPublishName([FromBody] PublishVM request)
        {
            try
            {
                var response = publishService.SetPublishName(request);
                return Ok(new {message = response});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("publish-collections"), Authorize]
        public IActionResult PublishCollections([FromBody] PublishCollectionsVM request)
        {
            try
            {
                var response = publishService.PublishCollections(request);
                return Ok(new { message = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

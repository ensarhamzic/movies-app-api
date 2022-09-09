using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies_API.Data.Services;
using Movies_API.Data.ViewModels;

namespace Movies_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private CollectionService collectionService;

        public CollectionsController(CollectionService collectionService)
        {
            this.collectionService = collectionService;
        }

        [HttpPost, Authorize]
        public IActionResult CreateCollection([FromBody] CollectionVM request)
        {
            try
            {
                var newCollection = collectionService.CreateCollection(request.Name);
                return Created(nameof(newCollection), newCollection);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

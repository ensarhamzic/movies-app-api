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

        [HttpGet, Authorize]
        public IActionResult GetCollections()
        {
            try
            {
                var collections = collectionService.GetCollections();
                return Ok(collections);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("add-movie"), Authorize]
        public IActionResult AddMovieToCollection([FromBody] MovieVM request)
        {
            try
            {
                var response = collectionService.AddMovieToCollection(request);
                return Created(nameof(response), response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("favorize"), Authorize]
        public IActionResult AddMovieToFavorites([FromBody] FavoriteVM request)
        {
            try
            {
                var response = collectionService.AddMovieToFavorites(request);
                return Created(nameof(response), response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut, Authorize]
        public IActionResult RenameCollection([FromBody] RenameCollectionVM request)
        {
            try
            {
                var response = collectionService.RenameCollection(request);
                return Ok(new {message = response});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete, Authorize]
        public IActionResult DeleteCollection([FromBody] DeleteCollectionVM request)
        {
            try
            {
                var response = collectionService.DeleteCollection(request);
                return Ok(new { message = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

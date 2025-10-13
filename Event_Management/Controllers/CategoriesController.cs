using Event_Management.Exceptions;
using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;


namespace Event_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }


        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int? id)
        {
            if(id==null)
                return BadRequest( "Category ID is required.Please enter it");
            try
            {
                var c = _service.GetCategoryById(id.Value);
                return Ok(c);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Category>), 200)]
        public IActionResult GetAllCategories()
        {
            try
            {
                var c = _service.GetAllCategories();
                return Ok(c);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategoryDetails(int id, [FromBody] Category c)
        {
            if (id != c.CategoryID)
                return BadRequest(new { error = $"Category ID mismatch. Route ID: {id}, Body ID: {c.CategoryID}" });

            if (!ModelState.IsValid)
                return BadRequest(new { error = "Invalid model state.", details = ModelState });

            try
            {
                _service.UpdateCategoryDetails(c);
                return Ok(new { message = $"Category with ID {id} updated successfully." });
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (CategoryUpdateException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Unexpected error occurred.", details = ex.Message });
            }
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int? id)
        {
            if(id==null)
                return BadRequest( "Category ID is required.Please enter it");
            try
            {
                _service.DeleteCategory(id.Value);
                return Ok(new { message = $"Category with ID {id} deleted successfully." });
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (CategoryDeletionException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
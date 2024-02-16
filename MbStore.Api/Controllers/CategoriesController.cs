using MbStore.Application.DTOs;
using MbStore.Application.DTOs.Category;
using MbStore.Application.Interfaces;
using MbStore.Utils.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MbStore.Api.Controllers;

[Route("/api/v1/[Controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ResponseDto _response;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
        _response = new ResponseDto();
    }

    // GetAll 
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        try
        {
            var categories = await _categoryService.Get();

            if (categories == null)
            {
                _response.Message = "Categories not found";
                return BadRequest(_response);
            }

            _response.IsValid = true;
            _response.ResultObject = categories;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest(_response);
        }
    }

    // GetById 
    [HttpGet("GetById/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var category = await _categoryService.Get(id);

            if (category == null)
            {
                _response.Message = $"Category with id: {id} not found";
                return BadRequest(_response);
            }

            _response.IsValid = true;
            _response.ResultObject = category;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest(_response);
        }
    }

    // Create
    [HttpPost("Create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = $"{SharedConstants.ADMIN}, {SharedConstants.OWNER}")]
    public async Task<IActionResult> Post([FromBody] CategoryCreateDto categoryCreateDto)
    {
        try
        {
            if (categoryCreateDto == null)
            {
                _response.Message = "The sent object is null.";
                return BadRequest(_response);
            }

            var resultCreate = await _categoryService.Create(categoryCreateDto);

            if (resultCreate == null)
            {
                _response.Message = "Failed to register category";
                return BadRequest(_response);
            }

            _response.IsValid = true;
            _response.Message = "Category registered successfully";
            _response.ResultObject = resultCreate;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message;
            return BadRequest(_response);
        }
    }

    // Update
    [HttpPut("Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = $"{SharedConstants.ADMIN}, {SharedConstants.OWNER}")]
    public async Task<IActionResult> Put([FromBody] CategoryUpdateDto categoryUpdateDto)
    {
        try
        {
            if (categoryUpdateDto == null)
            {
                _response.Message = "The sent object is null.";
                return BadRequest(_response);
            }

            var resultUpdate = await _categoryService.Update(categoryUpdateDto);

            if (resultUpdate == null)
            {
                _response.Message = "Failed to update category";
                return BadRequest(_response);
            }

            _response.IsValid = true;
            _response.Message = "Category updated successfully";
            _response.ResultObject = resultUpdate;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message;
            return BadRequest(_response);
        }
    }

    // Delete
    [HttpDelete("Delete/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = $"{SharedConstants.ADMIN}, {SharedConstants.OWNER}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                _response.Message = "The ID provided is in an invalid format.";
                return BadRequest(_response);
            }

            var resultDelete = await _categoryService.Delete(id);

            _response.IsValid = true;
            _response.Message = "Category deleted successfully";
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message;
            return BadRequest(_response);
        }
    }
}

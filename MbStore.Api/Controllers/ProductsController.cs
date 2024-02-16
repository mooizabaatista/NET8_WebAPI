using MbStore.Application.DTOs;
using MbStore.Application.DTOs.Product;
using MbStore.Application.Interfaces;
using MbStore.Utils.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MbStore.Api.Controllers;

[Route("/api/v1/[Controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ResponseDto _response;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
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
            var products = await _productService.Get();

            if (products == null)
            {
                _response.Message = "Products not found";
                return BadRequest(_response);
            }

            _response.IsValid = true;
            _response.ResultObject = products;
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
            var product = await _productService.Get(id);

            if (product == null)
            {
                _response.Message = $"Product with id: {id} not found";
                return BadRequest(_response);
            }

            _response.IsValid = true;
            _response.ResultObject = product;
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
    public async Task<IActionResult> Post([FromBody] ProductCreateDto productCreateDto)
    {
        try
        {
            if (productCreateDto == null)
            {
                _response.Message = "The sent object is null.";
                return BadRequest(_response);
            }

            var resultCreate = await _productService.Create(productCreateDto);

            if (resultCreate == null)
            {
                _response.Message = "Failed to register product";
                return BadRequest(_response);
            }

            _response.IsValid = true;
            _response.Message = "Product registered successfully";
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
    public async Task<IActionResult> Put([FromBody] ProductUpdateDto productUpdateDto)
    {
        try
        {
            if (productUpdateDto == null)
            {
                _response.Message = "The sent object is null.";
                return BadRequest(_response);
            }

            var resultUpdate = await _productService.Update(productUpdateDto);

            if (resultUpdate == null)
            {
                _response.Message = "Failed to update product";
                return BadRequest(_response);
            }

            _response.IsValid = true;
            _response.Message = "Product updated successfully";
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

            var resultDelete = await _productService.Delete(id);

            _response.IsValid = true;
            _response.Message = "Product deleted successfully";
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message;
            return BadRequest(_response);
        }
    }
}

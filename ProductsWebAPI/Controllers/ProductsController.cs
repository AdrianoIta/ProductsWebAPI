using Microsoft.AspNetCore.Mvc;
using ProductsWebAPI.Business;
using ProductsWebAPI.Models;
using Swashbuckle.Swagger.Annotations;

namespace ProductsWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsBusiness ProductsBusiness;

        public ProductsController(IProductsBusiness productsBusiness)
        {
            ProductsBusiness = productsBusiness;
        }

        /// <summary>
        /// Fetch all the existent products in the products table.
        /// </summary>
        /// <returns>Return all the existent products in the table</returns>
        /// <response code="200">Returns all the products correctly</response>
        /// <response code="204">No products found</response>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public IActionResult GetProducts()
        {
            try
            {
                var products = ProductsBusiness.GetProducts();

                return Ok(products);
            }
            catch (Exception)
            {
                return NotFound("Products not found.");
            }
        }

        /// <summary>
        /// Fetch the product accordingly an id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Products
        ///     {
        ///        "productId": "a0f605b2-911c-4892-a3b1-514d2a66cd90"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Return a specific product</returns>
        /// <response code="200">Returns one single product successfully</response>
        /// <response code="500">Missing a mandatory argument or invalid format</response>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult GetProductById(string id)
        {
            try
            {
                var product = ProductsBusiness.GetProductById(id);

                return Ok(product);
            }
            catch (InvalidOperationException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Product not found.");
            }
        }

        /// <summary>
        /// Creates a product based on the input information.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Products
        ///     {
        ///          "productName": "Mouse",
        ///          "category": "Computer parts",
        ///          "unitPrice": 30
        ///     }
        ///
        /// </remarks>
        /// <returns>Returns a success message</returns>
        /// <response code="200">Successfully saved</response>
        /// <response code="400">Null arguments</response>
        /// <response code="500">Product duplicated</response>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult AddProducts(ProductsModel product)
        {
            try
            {
                ProductsBusiness.AddProducts(product);

                return Ok("Product successfully created");
            }
            catch (NullReferenceException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "One more arguments are empty or null.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates a product based on the input information.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Products
        ///     {
        ///          "productId": "a0f605b2-911c-4892-a3b1-514d2a66cd90",
        ///          "productName": "Monitor",
        ///          "category": "Computer parts",
        ///          "unitPrice": 200
        ///     }
        ///
        /// </remarks>
        /// <returns>Returns a success message</returns>
        /// <response code="200">Successfully updated</response>
        /// <response code="400">Missing a mandatory argument</response>
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProduct(ProductsModel product)
        {
            try
            {
                ProductsBusiness.UpdateProduct(product);

                return Ok("Products successfully updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a specific product by using the id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /Products
        ///     {
        ///          "productId": "a0f605b2-911c-4892-a3b1-514d2a66cd90",
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Successfully updated</response>
        /// <response code="400">Missing a mandatory argument</response>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProductById(string id)
        {
            try
            {
                ProductsBusiness.DeleteProductById(id);

                return Ok("Product successfully deleted.");
            }
            catch (ArgumentNullException)
            {
                return NotFound("Product not found.");
            }
        }
    }
}

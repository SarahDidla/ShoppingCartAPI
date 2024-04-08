using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI.Data;
using ShoppingCartAPI.Dto;
using ShoppingCartAPI.Interfaces;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductsRepository productsRepository, IMapper mapper) 
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Products>))]

        public IActionResult GetProducts() 
        {
            var products = _mapper.Map<List<ProductsDto>>(_productsRepository.GetProducts());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Products))]
        [ProducesResponseType(400)]

        public IActionResult GetProduct(int id)
        {
            if (!_productsRepository.ProductExists(id))
            {
                return NotFound();
            }
            var product = _mapper.Map<ProductsDto>(_productsRepository.GetProduct(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateProduct(ProductsDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Products>(productDto);

            if (!_productsRepository.CreateProduct(product))
            {
                ModelState.AddModelError("", "Something went wrong while creating product");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{product_id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public IActionResult DeleteProduct(int product_id)
        {
            var product = _productsRepository.GetProduct(product_id);

            if (product == null)
            {
                return NotFound();
            }

            if (!_productsRepository.DeleteProduct(product))
            {
                ModelState.AddModelError("", "Something went wrong while deleting product");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPut("{product_id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public IActionResult UpdateProduct(int product_id, [FromBody] ProductsDto updatedProduct)
        {
            if (product_id != updatedProduct.product_id)
            {
                return BadRequest(ModelState);
            }

            var productMap = _mapper.Map<Products>(updatedProduct);

            if(!_productsRepository.UpdateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating product");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

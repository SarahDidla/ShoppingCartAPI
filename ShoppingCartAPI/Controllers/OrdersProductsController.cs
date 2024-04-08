using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.Dto;
using ShoppingCartAPI.Interfaces;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class OrdersProductsController : Controller
    {
        private readonly IOrdersProductsRepository _ordersProductsRepository;
        private readonly IMapper _mapper;

        public OrdersProductsController(IOrdersProductsRepository ordersProductsRepository, IMapper mapper)
        {
            _ordersProductsRepository = ordersProductsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult GetOrdersProducts()
        {
            var ordersProducts = _mapper.Map<List<OrdersProductsDto>>(_ordersProductsRepository.GetOrdersProducts());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(ordersProducts);
        }

        [HttpGet("{order_id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult GetOrdersProductsByOrderId(int order_id)
        {
            var ordersProducts = _mapper.Map<List<OrdersProductsDto>>(_ordersProductsRepository.GetOrdersProductsByOrderId(order_id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(ordersProducts);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateOrdersProducts(OrdersProductsDto ordersProductsDto)
        {
            if (ordersProductsDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ordersProducts = _mapper.Map<OrdersProducts>(ordersProductsDto);

            if (!_ordersProductsRepository.CreateOrdersProducts(ordersProducts))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok(ordersProducts);
        }

        [HttpDelete("{order_product_id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public IActionResult DeleteOrdersProducts(int order_product_id)
        {
            if (!_ordersProductsRepository.OrdersProductsExists(order_product_id))
            {
                return NotFound();
            }

            var orderProductsToDelete = _ordersProductsRepository.GetOrdersProduct(order_product_id);

            if (!_ordersProductsRepository.DeleteOrdersProducts(orderProductsToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting ordersProduct");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}

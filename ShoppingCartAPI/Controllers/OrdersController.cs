using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.Dto;
using ShoppingCartAPI.Interfaces;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;
        private readonly IOrdersProductsRepository _ordersProductsRepository;

        public OrdersController(IOrdersRepository ordersRepository, IMapper mapper, IOrdersProductsRepository ordersProductsRepository)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
            _ordersProductsRepository = ordersProductsRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Orders>))]
        [ProducesResponseType(400)]

        public IActionResult GetOrders()
        {
            var orders = _mapper.Map<List<OrdersDto>>(_ordersRepository.GetOrders());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult GetOrder(int id)
        {
            if (!_ordersRepository.OrderExists(id))
            {
                return NotFound();
            }

            var order = _mapper.Map<OrdersDto>(_ordersRepository.GetOrderById(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(order);
        }

        /*[HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult PlaceOrder(OrdersDto ordersDto)
        {
            bool success = _ordersRepository.PlaceOrder(ordersDto.customer_id, ordersDto.total_amount, ordersDto.shipping_address, ordersDto.payment_method);

            if (success)
            {
                return Ok(ordersDto);
            }


            return BadRequest(ModelState);
        }*/

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult PlaceOrder(CartOrderRequest cartOrderRequest)
        {
            bool success = _ordersRepository.PlaceOrder(cartOrderRequest);

            if (success)
            {
                return NoContent();
            }


            return BadRequest(ModelState);
        }

        [HttpGet("getOrderDetails/{customer_id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult GetOrderDetailsByCustomerId(int customer_id)
        {
            var ordersDetails = _ordersRepository.GetOrderDetailsByCustomerId(customer_id);

            if (ordersDetails == null)
            {
                return BadRequest(ModelState);
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(ordersDetails);
        }

        [HttpDelete("{order_id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult DeleteOrder(int order_id)
        {
            if (!_ordersRepository.OrderExists(order_id))
            {
                return NotFound();
            }

            var order = _ordersRepository.GetOrderById(order_id);

            var orderProducts = _ordersProductsRepository.GetOrdersProductsByOrderId(order_id);

            foreach(var orderProduct in orderProducts)
            {
                if (!_ordersProductsRepository.DeleteOrdersProducts(orderProduct))
                {
                    return BadRequest(ModelState);
                }
            }

            if (!_ordersRepository.DeleteOrder(order))
            {
                ModelState.AddModelError("", "Something went wrong while deleting order");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

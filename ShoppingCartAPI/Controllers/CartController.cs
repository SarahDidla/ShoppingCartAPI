using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.Data;
using ShoppingCartAPI.Dto;
using ShoppingCartAPI.Interfaces;
using ShoppingCartAPI.Models;


namespace ShoppingCartAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productsRepository;
        public ICollection<Cart> cartItems = [];

        public CartController(ICartRepository cartRepository, IMapper mapper, IProductsRepository productsRepository)
        {
            _cartRepository = cartRepository;          
            _mapper = mapper;
            _productsRepository = productsRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Cart>))]
        [ProducesResponseType(400)]

        public IActionResult GetCart()
        {
            var cart = _mapper.Map<List<CartDto>>(_cartRepository.GetCart());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(cart);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult GetCart(int id)
        {
            if (!_cartRepository.CartProductExists(id))
            {
                return NotFound();
            }
            var cartProduct = _mapper.Map<CartDto>(_cartRepository.GetCart(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(cartProduct);
        }

        [HttpPost("addToCart")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            var product = _productsRepository.GetProduct(addToCartDto.productId);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            var newCartItem = new Cart
            {
                product_id = addToCartDto.productId,
                product_name = product.product_name,
                product_price = product.product_price,
                customer_id = addToCartDto.customer_id,
            };

            if (!_cartRepository.AddToCart(newCartItem))
            {
                ModelState.AddModelError("", "Something went wrong while adding to cart");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{cartId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteCart(int cartId)
        {
            if (!_cartRepository.CartProductExists(cartId))
            {
                return NotFound();
            }

            var cartItemToDelete = _cartRepository.GetCart(cartId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_cartRepository.DeleteItem(cartItemToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting cart item");
            }

            return NoContent();
        }

        [HttpPost("Checkout")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult CheckOut(ICollection<CartDto> cartItemsDto)
        {
            if (cartItemsDto != null)
            {
                this.cartItems = _mapper.Map<List<Cart>>(cartItemsDto);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(cartItems);
        }

        [HttpGet("GetCartByCustomer/{customer_id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public IActionResult GetCartByCustomerId(int customer_id)
        {
            var cart = _mapper.Map<List<CartDto>>(_cartRepository.GetCartByCustomerId(customer_id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(cart);
        }
    }
}

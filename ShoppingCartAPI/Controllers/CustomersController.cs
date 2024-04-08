using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.Dto;
using ShoppingCartAPI.Interfaces;
using ShoppingCartAPI.Models;


namespace ShoppingCartAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;
        private readonly ILoginRepository _loginRepository;

        public CustomersController(ICustomersRepository customersRepository, IMapper mapper, ILoginRepository loginRepository)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
            _loginRepository = loginRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Customers>))]
        [ProducesResponseType(400)]

        public IActionResult GetCustomers()
        {
            var customers = _mapper.Map<List<CustomersDto>>(_customersRepository.GetCustomers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(customers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Customers))]
        [ProducesResponseType(400)]

        public IActionResult GetCustomer(int id)
        {
            if (!_customersRepository.CustomerExists(id))
            {
                return NotFound();
            }
            var customer = _mapper.Map<CustomersDto>(_customersRepository.GetCustomer(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(customer);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult Register([FromBody] CustomersDto customersDto)
        {
            if (customersDto == null)
            {
                return BadRequest(ModelState);
            }

            var customer = _customersRepository.GetCustomerByUsername(customersDto.username);

            if (customer != null)
            {
                ModelState.AddModelError("", "Username already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerMap = _mapper.Map<Customers>(customersDto);

            if (!_customersRepository.Register(customerMap))
            {
                ModelState.AddModelError("", "Something went wrong while registering");
                return StatusCode(500, ModelState);
            }

            var login = new Login
            {
                username = customerMap.username,
                password = customerMap.password,
                customer_id = customerMap.customer_id
            };

            _loginRepository.CreateLogin(login);

            return Ok(_mapper.Map<CustomersDto>(customerMap));
        }

        [HttpPut("{customerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateCustomer(int customerId, [FromBody] CustomersDto updatedCustomer)
        {
            if (updatedCustomer == null)
                return BadRequest(ModelState);

            if (customerId != updatedCustomer.customer_id)
                return BadRequest(ModelState);

            if (!_customersRepository.CustomerExists(customerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var customer = _mapper.Map<Customers>(updatedCustomer);

            if (!_customersRepository.UpdateCustomer(customer))
            {
                ModelState.AddModelError("", "Something went wrong while updating customer");
                return StatusCode(500, ModelState);
            }

            _loginRepository.UpdateLoginForCustomer(customer);

            return NoContent();
        }

        [HttpDelete("{customerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public IActionResult DeleteCustomer(int customerId)
        {
            var customer = _customersRepository.GetCustomer(customerId);

            if (customer == null)
                return NotFound();

            var login = _loginRepository.GetLoginByCustomerId(customerId);

            if (login != null)
            {
                if (!_loginRepository.DeleteLogin(login))
                    return BadRequest(ModelState);
            }

            if (!_customersRepository.DeleteCustomer(customer))
            {
                ModelState.AddModelError("", "Something went wrong while deleting customer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

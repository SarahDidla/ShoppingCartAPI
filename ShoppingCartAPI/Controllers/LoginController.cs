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
    public class LoginController : Controller
    {
        private readonly DataContext _context;
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;

        public LoginController(ILoginRepository loginRepository, IMapper mapper)
        {
            _loginRepository = loginRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult GetLoginList()
        {
            var login = _mapper.Map<List<LoginDto>>(_loginRepository.GetLoginList());
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(login);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var login = _mapper.Map<LoginDto>(_loginRepository.GetLoginByUsername(loginDto.username));

            if (login == null || !_loginRepository.VerifyPassword(loginDto.username, loginDto.password))
            {
                return BadRequest(ModelState);
            }

            return Ok(login);
        }

        /*[HttpPut("{loginId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult UpdateLogin(int loginId, [FromBody] LoginDto updatedLogin)
        {
            if (updatedLogin == null)
                return BadRequest(ModelState);

            if (loginId != updatedLogin.login_id)
                return BadRequest(ModelState);

            if (!_loginRepository.LoginExists(loginId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var login = _mapper.Map<Login>(updatedLogin);

            if (!_loginRepository.UpdateLogin(login))
            {
                ModelState.AddModelError("", "Something went wrong while updating login");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }*/

        [HttpDelete("{login_id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public IActionResult DeleteLogin(int login_id)
        {
            var login = _loginRepository.GetLogin(login_id);

            if (login == null)
            {
                return NotFound();
            }

            if (!_loginRepository.DeleteLogin(login))
            {
                ModelState.AddModelError("", "Something went wrong while deleting login");
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpPut("{login_id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult UpdateLogin(int login_id, [FromBody] LoginDto updatedLogin)
        {
            if (updatedLogin == null)
            {
                return BadRequest(ModelState);
            }

            if (updatedLogin.login_id != login_id)
            {
                return BadRequest(ModelState);
            }

            if (!_loginRepository.LoginExists(login_id))
            {
                return BadRequest(ModelState);
            }
            var login = _mapper.Map<Login>(updatedLogin);

            if (!_loginRepository.UpdateLogin(login))
            {
                ModelState.AddModelError("", "Something went wrong while updating login");
                return BadRequest(ModelState);
            }

            return Ok(login);
        }
    }
}

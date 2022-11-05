using CarCollection.Repository.IRepository;
using CarCollection.ViewModels.AppUser;
using Microsoft.AspNetCore.Mvc;

namespace CarCollection.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
        {
        private readonly IAuthManager _authManager;

        public AccountController(IAuthManager authManager)
            {
            _authManager = authManager;
            }
        //POST: api/account/register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] AppUserViewModel appUserViewModel)
            {
            try
                {
                var errors = await _authManager.Register(appUserViewModel);
                if (errors.Any())
                    {
                    foreach (var error in errors)
                        {
                        ModelState.AddModelError(error.Code, error.Description);
                        }
                    return BadRequest(ModelState);
                    }
                return Ok();
                }
            catch (Exception)
                {
                throw;
                }
            }
        //POST: api/account/login
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] AppUserViewModel appUserViewModel)
            {
            var authResponse = await _authManager.Login(appUserViewModel);

            if (authResponse == null)
                return Unauthorized();

            return Ok(authResponse);
            }
        }
    }
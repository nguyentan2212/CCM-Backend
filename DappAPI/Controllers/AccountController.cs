using DappAPI.Services.Account;
using DappAPI.Services.Auth;
using DappAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace DappAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IAccountService accountService;
        public AccountController(IAuthService authService, IAccountService accountService)
        {
            this.authService = authService;
            this.accountService = accountService;
        }

        //[Authorize(Roles = "admin")]
        [HttpGet("")]
        public ActionResult<List<UserInfoViewModel>> GetUsers()
        {
            List<UserInfoViewModel> result = accountService.GetAllUsersInfo();
            return result;
        }

        [HttpGet("{publicAddress}")]
        public ActionResult<UserInfoViewModel> GetUser(string publicAddress)
        {
            UserInfoViewModel result = accountService.GetUserInfo(publicAddress);
            if (result is null)
            {
                return NotFound("User not found");
            }
            return result;
        }
      
        [AllowAnonymous]
        [HttpGet("nonce/{publicAddress}")]
        public ActionResult<GetNonceViewModel> GetNonce(string publicAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            long nonce = accountService.GetNonce(publicAddress);
            if (nonce == 0)
            {
                return NotFound("User not found");
            }
            GetNonceViewModel viewModel = new GetNonceViewModel() { publicAddress = publicAddress, Nonce = nonce };
            return Ok(viewModel);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("wrong request");
            }
            // Get the user with the given publicAddress           
            UserInfoViewModel user = accountService.GetUserInfo(request.PublicAddress);
            if (user is null)
            {
                return Unauthorized("User not found.");
            }
            // Verify digital signature
            string message = "I am signing my one-time nonce: " + accountService.GetNonce(request.PublicAddress).ToString();
            string result = authService.VerifyMessage(message, request.Signature, request.PublicAddress);
            if (String.IsNullOrEmpty(result))
            {
                return Unauthorized("Signature verification failed!");
            }
            // Verification succeeded, change nonce and return JWT
            long nonce = await accountService.ChangeNonce(user.PublicAddress);
            List<string> roles = await accountService.GetUserRoles(user.PublicAddress);
            string jwt = authService.GenerateToken(result, roles.ToList());          
            return Ok(jwt);
        }  
        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterViewModel request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            // Check if account with same public address already exits.
            UserInfoViewModel user = accountService.GetUserInfo(request.PublicAddress);
            if (user != null)
            {
                return Conflict("This account already exits");
            }
            // Verify digital signature
            string message = "I am signing my one-time nonce: " + accountService.GetNonce(request.PublicAddress).ToString();
            string result = authService.VerifyMessage(message, request.Signature, request.PublicAddress);
            if (String.IsNullOrEmpty(result))
            {
                return Unauthorized("Signature verification failed!");
            }
            // Verification succeeded, create new user, change nonce and return JWT
            user = await accountService.CreateUser(request);
            long nonce = await accountService.ChangeNonce(user.PublicAddress);
            List<string> roles = await accountService.GetUserRoles(user.PublicAddress);
            string jwt = authService.GenerateToken(user.PublicAddress, roles.ToList());
            return Ok(jwt);
        }

        [HttpPost("update")]
        public async Task<ActionResult<UserInfoViewModel>> UpdateUser([FromBody] UpdateAccountViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }           
            UserInfoViewModel user = await accountService.UpdateUser(request);  
            if (user is null)
            {
                return NotFound("User not found");
            }
            return user;
        }
    }
}

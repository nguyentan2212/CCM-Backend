using DappAPI.Services.Account;
using DappAPI.Services.Auth;
using DappAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace DappAPI.Controllers
{
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

        /// <summary>
        /// Lấy danh sách người dùng
        /// </summary>
        /// <returns>Trả về danh sách tất cả người dùng</returns>
        /// <response code="200">Trả về danh sách tất cả người dùng</response>
        [Authorize(Roles = "admin")]
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<UserDataViewModel>> GetUsers()
        {
            List<UserDataViewModel> result = accountService.GetAllUsersInfo();
            return Ok(result);
        }

        /// <summary>
        /// Lấy user bằng public address
        /// </summary>
        /// <param name="publicAddress"></param>
        /// <returns>User có public address cần lấy</returns>
        /// <response code="200">Trả về user info cần tìm</response>
        /// <response code="404">Không tìm thấy user</response>
        [HttpGet("{publicAddress}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public ActionResult<UserDataViewModel> GetUser(string publicAddress)
        {
            UserDataViewModel result = accountService.GetUserInfo(publicAddress);
            if (result is null)
            {
                return NotFound("User not found");
            }
            return Ok(result);
        }

        /// <summary>
        /// Lấy user nonce
        /// </summary>
        /// <param name="publicAddress"></param>
        /// <returns>User nonce</returns>
        /// <response code="200">Trả về user nonce</response>
        /// <response code="400">Request param sai</response>
        /// <response code="404">Không tìm thấy user có public address phù hợp</response>
        [AllowAnonymous]
        [HttpGet("nonce/{publicAddress}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
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

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Jwt token</returns>
        /// <response code="200">Login thành công, trả về jwt token</response>
        /// <response code="400">Request param sai</response>
        /// <response code="401">Xác thực không thành công</response>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<ActionResult<string>> Login([FromBody] LoginViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("wrong request");
            }
            // Get the user with the given publicAddress           
            UserDataViewModel user = accountService.GetUserInfo(request.PublicAddress);
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

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Jwt token</returns>
        /// <response code="200">Register thành công, trả về jwt token</response>
        /// <response code="400">Request param sai</response>
        /// <response code="401">Xác thực không thành công</response>
        /// <response code="409">User đã tồn tại</response>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(string))]
        public async Task<ActionResult<string>> Register([FromBody] RegisterViewModel request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            // Check if account with same public address already exits.
            UserDataViewModel user = accountService.GetUserInfo(request.PublicAddress);
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

        /// <summary>
        /// Cập nhật account
        /// </summary>
        /// <param name="request"></param>
        /// <returns>User info</returns>
        /// <response code="200">Update thành công, trả về user sau khi update</response>
        /// <response code="400">Request param sai</response>
        /// <response code="404">Không tifm thấy user</response>
        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<UserDataViewModel>> UpdateUser([FromBody] UpdateAccountViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }           
            UserDataViewModel user = await accountService.UpdateUser(request);  
            if (user is null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
    }
}

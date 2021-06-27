using DappAPI.Extensions.Exceptions;
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

        /// <summary>
        /// Lấy danh sách người dùng
        /// </summary>
        /// <returns>Trả về danh sách tất cả người dùng</returns>
        /// <response code="200">Trả về danh sách tất cả người dùng</response>
        [AllowAnonymous]
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<UserDataViewModel>> GetUsers()
        {
            List<UserDataViewModel> result = accountService.GetAllUsersInfo();
            return Ok(result);
        }

        /// <summary>
        /// Lấy user bằng userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User có id cần lấy</returns>
        /// <response code="200">Trả về user info cần tìm</response>
        /// <response code="404">Không tìm thấy user</response>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<UserDataViewModel>> GetUser(string userId)
        {
            try
            {
                UserDataViewModel result = await accountService.GetUserInfo(userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.InnerException?.Message);
            }                      
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
            try
            {
                long nonce = accountService.GetNonce(publicAddress);
                GetNonceViewModel viewModel = new GetNonceViewModel() { publicAddress = publicAddress, Nonce = nonce };
                return Ok(viewModel);
            }
            catch(NotFoundException e)
            {
                return NotFound(e.Message);
            }
            
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
        public async Task<ActionResult<LoginResultViewModel>> Login([FromBody] LoginViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("wrong request");
            }
            // Get the user with the given publicAddress           
            UserDataViewModel user = accountService.GetUserWithPublicAddress(request.PublicAddress);
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
            // Verification succeeded, check if account is lockout
            if (!user.IsActive)
            {
                return BadRequest("account is lockout");
            }

            // Verification succeeded, create new user, change nonce and return JWT
            long nonce = await accountService.ChangeNonce(user.PublicAddress);
            List<string> roles = await accountService.GetUserRoles(user.Id.ToString());
            string jwt = authService.GenerateToken(result, roles.ToList());
            LoginResultViewModel loginResult = new LoginResultViewModel() {
                Jwt = jwt,
                Id = user.Id,
                FullName = user.FullName,
                PublicAddress = user.PublicAddress,
                Role = roles.LastOrDefault()
            };
            return Ok(loginResult);
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Jwt token</returns>
        /// <response code="200">Register thành công, trả về jwt token</response>
        /// <response code="400">Request param sai</response>
        /// <response code="401">Xác thực không thành công</response>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(string))]
        public async Task<ActionResult<LoginResultViewModel>> Register([FromBody] RegisterViewModel request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            // Check if account with same public address already exits.         
            
            // Verification succeeded, create new user, change nonce and return JWT
            try
            {
                string userId = await accountService.CreateUser(request);
                List<string> roles = await accountService.GetUserRoles(userId);
                string jwt = authService.GenerateToken(userId, roles.ToList());
                LoginResultViewModel loginResult = new LoginResultViewModel()
                {
                    Jwt = jwt,
                    Id = userId,
                    FullName = request.FullName,
                    PublicAddress = request.PublicAddress,
                    Role = roles.LastOrDefault()
                };
                return Ok(loginResult);
            }
            catch( DataSaveException e)
            {
                return Unauthorized(e.Message);
            }
        }

        /// <summary>
        /// Cập nhật account
        /// </summary>
        /// <param name="request"></param>
        /// <returns>User info</returns>
        /// <response code="200">Update thành công, trả về user sau khi update</response>
        /// <response code="400">Request param sai</response>
        /// <response code="404">Không tìm thấy user</response>
        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateAccountViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }           
            try
            {
                await accountService.UpdateUser(request);
                return Ok();
            }
            catch(NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(DataSaveException e)
            {
                return BadRequest(e.Message);
            }
            
        }

        /// <summary>
        /// lock and unlocked account
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isLocked"></param>
        /// <returns>User info</returns>
        /// <response code="200">Locked, Unlocked thành công, trả về user sau khi update</response>
        /// <response code="400">Request param sai</response>
        /// <response code="404">Không tìm thấy user</response>
        [HttpPost("locked")]
        public async Task<ActionResult> Locked(string userId, bool isLocked)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            try
            {
                if (isLocked)
                {
                    await accountService.LockUser(userId);
                }
                else
                {
                    await accountService.UnlockUser(userId);
                }
                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(DataSaveException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// promote and demote account
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isPromote"></param>
        /// <returns>User info</returns>
        /// <response code="200">promote, demote thành công, trả về user sau khi update</response>
        /// <response code="400">Request param sai</response>
        /// <response code="404">Không tìm thấy user</response>
        [HttpPost("promote")]
        public async Task<ActionResult> Promote(string userId, bool isPromote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            try
            {
                if (isPromote)
                {
                    await accountService.Promote(userId);
                }
                else
                {
                    await accountService.Demote(userId);
                }
                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (DataSaveException e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}

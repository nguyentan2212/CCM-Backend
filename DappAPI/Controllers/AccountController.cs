using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DappAPI.Services.Auth;
using DappAPI.ViewModels;
using DappAPI.Repositories.UnitOfWork;
using DappAPI.Repositories;
using DappAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DappAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUnitOfWork work;
        private readonly UserManager<DappUser> userManager;
        public AccountController(IAuthService authService, IUnitOfWork work, UserManager<DappUser> userManager)
        {
            this.authService = authService;
            this.work = work;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("nonce")]
        public ActionResult<GetNonceViewModel> GetNonce([FromQuery] string publicAddress)
        {
            Repository<DappUser> userRepo = work.CreateRepository<DappUser>();
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == publicAddress);
            if (user is null)
            {
                return BadRequest("User not found");
            }
            long nonce = user.Nonce;
            GetNonceViewModel viewModel = new GetNonceViewModel() { publicAddress = publicAddress, Nonce = nonce };
            return Ok(viewModel);
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<ActionResult<string>> Auth([FromBody] LoginViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("wrong request");
            }

            // Get the user with the given publicAddress
            Repository<DappUser> userRepo = work.CreateRepository<DappUser>();
            DappUser user = userRepo.FirstOrDefault(x => x.PublicAddress == request.PublicAddress);
            if (user is null)
            {
                return Unauthorized("User not found.");
            }

            // Verify digital signature
            string message = "I am signing my one-time nonce: " + user.Nonce.ToString();
            string address = authService.RecoverPersonalSignature(message, request.Signature);
            if (address.ToLower() != request.PublicAddress.ToLower())
            {
                return Unauthorized("Signature verification failed!");
            }

            // Verification succeeded, change nonce and return JWT
            Random random = new Random();
            user.Nonce = random.Next(10000, 100000);
            userRepo.Update(user);
            await work.SaveAsync();
            var roles = await userManager.GetRolesAsync(user);
            string jwt = authService.GenerateToken(address, roles.ToList());
            return Ok(jwt);

        }     
    }
}

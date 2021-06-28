using DappAPI.Services.Statistical;
using DappAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DappAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatisticalService statisticalService;
        public StatisticalController(IStatisticalService statisticalService)
        {
            this.statisticalService = statisticalService;   
        }

        [HttpGet("")]
        public ActionResult<StatisticalViewModel> GetStatic()
        {
            StatisticalViewModel result = statisticalService.GetStastical();
            return Ok(result);
        }
    }
}

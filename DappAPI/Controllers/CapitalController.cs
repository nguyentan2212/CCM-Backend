using DappAPI.Services.CapitalServices;
using DappAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DappAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CapitalController : ControllerBase
    {
        private readonly ICapitalService capitalService;

        public CapitalController(ICapitalService capitalService)
        {
            this.capitalService = capitalService;
        }

        /// <summary>
        /// Lấy thông tin tất cả khoản vốn
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Trả về danh sách tất cả khoản vốn</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CapitalDataViewModel>> GetAll()
        {
            List<CapitalDataViewModel> capitals = capitalService.GetAllCapitals();
            return Ok(capitals);
        }

        /// <summary>
        /// Lấy thông tin khoản vốn có id trùng khớp
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Trả về khoản vốn cần tìm</response>
        /// <response code="400">Request param sai</response>
        /// <response code="404">Không tìm thấy khoản vốn có id phù hợp</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public ActionResult<CapitalDataViewModel> Get(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            CapitalDataViewModel capital = capitalService.GetCapitalsById(id);
            if (capital is null)
            {
                return NotFound("Not found");
            }
            return Ok(capital);
        }

        /// <summary>
        /// Lấy các khoản vốn mà người dùng có public address đã tạo
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Trả về danh sách khoản vốn cần tìm</response>
        /// <response code="400">Request param sai</response>
        /// <response code="404">Không tìm thấy kết quả phù hợp</response>
        [HttpGet("user/{id}")]
        public ActionResult<List<CapitalDataViewModel>> GetByCreator(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            List<CapitalDataViewModel> capitals = capitalService.GetCapitalsByCreator(id);
            if (capitals is null)
            {
                return NotFound("Not found");
            }
            return Ok(capitals);
        }

        /// <summary>
        /// Tạo một khoản vốn mới
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Trả về khoản vốn đã tạo</response>
        /// <response code="400">Request param sai</response>
        /// <response code="500">Đã xảy ra lỗi tại backend</response>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<ActionResult<CapitalDataViewModel>> Create([FromBody] CreateCapitalViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            CapitalDataViewModel capital = await capitalService.CreateCapital(request);
            if (capital is null)
            {
                return StatusCode(500, "Error occurred");
            }
            return Ok(capital);
        }

        /// <summary>
        /// Cập nhật thông tin cho khoản vốn 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Trả về khoản vốn đã cập nhật</response>
        /// <response code="400">Request param sai</response>
        /// <response code="500">Đã xảy ra lỗi tại backend</response>
        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<ActionResult<CapitalDataViewModel>> Update([FromBody] UpdateCapitalViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            CapitalDataViewModel capital = await capitalService.UpdateCapital(request);
            if (capital is null)
            {
                return StatusCode(500, "Error occurred");
            }
            return Ok(capital);
        }

        /// <summary>
        /// Hủy bỏ khoản vốn yêu cầu
        /// </summary>
        /// <returns></returns>
        [HttpPost("cancel/{id}")]
        public async Task<ActionResult<CapitalDataViewModel>> Cancel(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            CapitalDataViewModel capital = await capitalService.CancelCapital(id);
            if (capital is null)
            {
                return StatusCode(500, "Error occurred");
            }
            return Ok(capital);
        }
    }
}

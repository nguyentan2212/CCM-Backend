using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DappAPI.Services.CapitalServices;
using DappAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using DappAPI.Extensions.Enums;

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
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CapitalDataViewModel>> GetAll()
        {
            List<CapitalDataViewModel> capitals = capitalService.GetAllCapitals();
            return Ok(capitals);
        }

        /// <summary>
        /// Lấy thông tin khoản vốn có id trùng khớp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Trả về khoản vốn cần tìm</response>
        /// <response code="400">Request param sai</response>
        /// <response code="404">Không tìm thấy khoản vốn có id phù hợp</response>
        [HttpGet("id")]
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
        /// Lấy các khoản vốn trong khoản từ min đến max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [HttpGet("value")]
        public ActionResult<List<CapitalDataViewModel>> GetByValue([FromQuery] double min, double max)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            List<CapitalDataViewModel> capitals = capitalService.GetCapitalsByValue(min, max);
            return Ok(capitals);
        }

        /// <summary>
        /// Lấy các khoản vốn có chứa keyword trong tiêu đề hoặc miêu tả
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet("keyword")]
        public ActionResult<List<CapitalDataViewModel>> GetByKeyword([FromQuery] string keyword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            List<CapitalDataViewModel> capitals = capitalService.GetCapitalsByKeyword(keyword);
            return Ok(capitals);
        }

        /// <summary>
        /// Lấy khoản vốn có type như yêu cầu
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("type")]
        public ActionResult<List<CapitalDataViewModel>> GetByType(int type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
             
            List<CapitalDataViewModel> capitals = capitalService.GetCapitalsByType((CapitalType)type);
            if (capitals is null)
            {
                return NotFound("Not found");
            }
            return Ok(capitals);
        }

        /// <summary>
        /// Lấy khoản vốn có trạng thái như yêu cầu
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("status")]
        public ActionResult<List<CapitalDataViewModel>> GetByStatus(int status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            List<CapitalDataViewModel> capitals = capitalService.GetCapitalsByStatus((CapitalStatus)status);
            if (capitals is null)
            {
                return NotFound("Not found");
            }
            return Ok(capitals);
        }

        /// <summary>
        /// Tạo một khoản vốn mới
        /// </summary>
        /// <param name="request"></param>
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
        /// Phê duyệt chấp nhận cho khoản vốn được tạo ra
        /// </summary>
        /// <param name="id"></param>
        /// <param name="publicAddress"></param>
        /// <returns></returns>
        [HttpPost("confirm")]
        public async Task<ActionResult<CapitalDataViewModel>> Confirm(long id, string publicAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            CapitalDataViewModel capital = await capitalService.ConfirmCapital(id, publicAddress);
            if (capital is null)
            {
                return StatusCode(500, "Error occurred");
            }
            return Ok(capital);
        }

        /// <summary>
        /// Hủy bỏ khoản vốn yêu cầu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="publicAddress"></param>
        /// <returns></returns>
        [HttpPost("cancel")]
        public async Task<ActionResult<CapitalDataViewModel>> Cancel(long id, string publicAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong request");
            }
            CapitalDataViewModel capital = await capitalService.CancelCapital(id, publicAddress);
            if (capital is null)
            {
                return StatusCode(500, "Error occurred");
            }
            return Ok(capital);
        }
    }
}

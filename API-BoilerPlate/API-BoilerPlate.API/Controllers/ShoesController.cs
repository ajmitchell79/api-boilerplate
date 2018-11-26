using System;
using System.Net;
using System.Threading.Tasks;
using API_BoilerPlate.BRL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_BoilerPlate.API.Controllers
{
    [Route("api/shoes")]
    //[Authorize("BasicUser")]
    [AllowAnonymous]
    public class ShoesController : BaseController, IController
    {
        //************ FLUENT VALIDATION FOR MODEL INPUTS ************************
        //https://www.carlrippon.com/fluentvalidation-in-an-asp-net-core-web-api/
        //*************************************************************************

        private readonly ILogger<ShoesController> _logger;
        private readonly IShoesService _shoesService;

        public ShoesController(ILogger<ShoesController> logger, IShoesService shoesService)
        {
            _logger = logger;
            _shoesService = shoesService;
        }

        [HttpGet("")]
        public async Task<IActionResult> AllShoes()
        {
            try
            {
                var result = await _shoesService.GetAllShoes();
                return Ok(result);
            }
            catch (Exception ex)
            {
                LogError(_logger, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{shoeId}")]
        public async Task<IActionResult> GetShoes(int shoeId)
        {
            try
            {
                var result = await _shoesService.GetShoes(shoeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                LogError(_logger, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveShoes([FromBody] BRL.Command.Shoes shoes)
        {
            try
            {
                var result = await _shoesService.SaveShoes(shoes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                LogError(_logger, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
using System;
using System.Net;
using System.Threading.Tasks;
using API_BoilerPlate.BRL.Interfaces;
using API_BoilerPlate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_BoilerPlate.API.Controllers
{
    [Route("api/orders")]
    //[Authorize("BasicUser")]
    [AllowAnonymous]
    public class OrdersController : BaseController, IController
    {
        //************ FLUENT VALIDATION FOR MODEL INPUTS ************************
        //https://www.carlrippon.com/fluentvalidation-in-an-asp-net-core-web-api/
        //*************************************************************************

        private readonly ILogger<OrdersController> _logger;
        private readonly IOrdersService _ordersService;

        public OrdersController(ILogger<OrdersController> logger, IOrdersService ordersService)
        {
            _logger = logger;
            _ordersService = ordersService;
        }

        [HttpGet("")]
        public async Task<IActionResult> AllOrders()
        {
            try
            {
                var result = await _ordersService.GetAllOrders();
                return Ok(result);
            }
            catch (Exception ex)
            {
                LogError(_logger, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet("detailed")]
        public async Task<IActionResult> AllOrdersDetailed()
        {
            try
            {
                var result = await _ordersService.GetAllOrdersDetailed();
                return Ok(result);
            }
            catch (Exception ex)
            {
                LogError(_logger, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                var result = await _ordersService.GetOrder(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                LogError(_logger, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveOrder([FromBody] BRL.Command.Order order)
        {
            try
            {
                var result = await _ordersService.SaveOrder(order);
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
using GraphqlApiClientDemo.Services;
using GraphqlApiClientDemo.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GraphqlApiClientDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _transferService;

        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;
        }
        [HttpGet, Route("get-all")]
        public async Task<IActionResult> GetTransfer()
        {
            try
            {
                var transfers = await _transferService.GetTransfers();
                return Ok(transfers.OrderBy(s => s.Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

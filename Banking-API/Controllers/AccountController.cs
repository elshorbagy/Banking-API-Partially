using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Banking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Account account)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Request data is invalid");
                }

                return Ok(await _accountService.CreateAccount(account));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAsyc(int accountId)
        {
            try
            {
                if(accountId == 0)
                {
                    return BadRequest();
                }
                return Ok(await _accountService.GetAccount(accountId));
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("not found"))
                {
                    return NotFound();
                }

                return BadRequest(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Features.Bank.Commands;
using Application.Filters;

using Application.Features.Bank.Commands.CreateBank;
using Application.Features.Bank.Commands.DeleteBankById;
using Application.Features.Bank.Commands.UpdateBank;

using Application.Features.Bank.Queries.GetAllBanks;
using Application.Features.Bank.Queries.GetBankById;

using Application.Features.Bank.Requests.CreateBank;
using Application.Features.Bank.Requests.UpdateBank;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeDirect.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class BankController : BaseApiController
    {
        // GET ALL
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllBanksParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllBanksQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET BY ID
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetBankByIdQuery { Id = id }));
        }

        // CREATE
        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateBankRequest request)
        {
            var command = new CreateBankCommand(request.Name);

            return Ok(await Mediator.Send(command));
        }

        // UPDATE
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateBankRequest request)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var command = new UpdateBankCommand(id)
            {
                Name = request.Name
            };

            return Ok(await Mediator.Send(command));
        }

        // DELETE
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteBankByIdCommand { Id = id }));
        }
    }
}

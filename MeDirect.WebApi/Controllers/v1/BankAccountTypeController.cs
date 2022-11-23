using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Features.BankAccountType.Commands;
using Application.Filters;

using Application.Features.BankAccountType.Commands.CreateBankAccountType;
using Application.Features.BankAccountType.Commands.DeleteBankAccountTypeById;
using Application.Features.BankAccountType.Commands.UpdateBankAccountType;

using Application.Features.BankAccountType.Queries.GetAllBankAccountTypes;
using Application.Features.BankAccountType.Queries.GetBankAccountTypeById;

using Application.Features.BankAccountType.Requests.CreateBankAccountType;
using Application.Features.BankAccountType.Requests.UpdateBankAccountType;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeDirect.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class BankAccountTypeController : BaseApiController
    {
        // GET ALL
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllBankAccountTypesParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllBankAccountTypesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET BY ID
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetBankAccountTypeByIdQuery { Id = id }));
        }

        // CREATE
        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateBankAccountTypeRequest request)
        {
            var command = new CreateBankAccountTypeCommand(request.Name);

            return Ok(await Mediator.Send(command));
        }

        // UPDATE
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateBankAccountTypeRequest request)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var command = new UpdateBankAccountTypeCommand(id)
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
            return Ok(await Mediator.Send(new DeleteBankAccountTypeByIdCommand { Id = id }));
        }
    }
}

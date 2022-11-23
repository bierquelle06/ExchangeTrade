using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Features.BankBranch.Commands;
using Application.Filters;

using Application.Features.BankBranch.Commands.CreateBankBranch;
using Application.Features.BankBranch.Commands.DeleteBankBranchById;
using Application.Features.BankBranch.Commands.UpdateBankBranch;

using Application.Features.BankBranch.Queries.GetAllBankBranchs;
using Application.Features.BankBranch.Queries.GetBankBranchById;

using Application.Features.BankBranch.Requests.CreateBankBranch;
using Application.Features.BankBranch.Requests.UpdateBankBranch;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeDirect.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class BranchController : BaseApiController
    {
        // GET ALL
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllBankBranchsParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllBankBranchsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET BY ID
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetBankBranchByIdQuery { Id = id }));
        }

        // CREATE
        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateBankBranchRequest request)
        {
            var command = new CreateBankBranchCommand(request.Name, request.BankId);

            return Ok(await Mediator.Send(command));
        }

        // UPDATE
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateBankBranchRequest request)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var command = new UpdateBankBranchCommand(id)
            {
                Name = request.Name,
                BankId = request.BankId
            };

            return Ok(await Mediator.Send(command));
        }

        // DELETE
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteBankBranchByIdCommand { Id = id }));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Features.BankAccountActivity.Commands;
using Application.Filters;

using Application.Features.BankAccountActivity.Commands.CreateBankAccountActivity;
using Application.Features.BankAccountActivity.Commands.DeleteBankAccountActivityById;
using Application.Features.BankAccountActivity.Commands.UpdateBankAccountActivity;

using Application.Features.BankAccountActivity.Queries.GetAllBankAccountActivities;
using Application.Features.BankAccountActivity.Queries.GetBankAccountActivityById;

using Application.Features.BankAccountActivity.Requests.CreateBankAccountActivity;
using Application.Features.BankAccountActivity.Requests.UpdateBankAccountActivity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeDirect.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class BankAccountActivityController : BaseApiController
    {
        // GET ALL
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllBankAccountActivitiesParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllBankAccountActivitiesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET BY ID
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetBankAccountActivityByIdQuery { Id = id }));
        }

        // CREATE
        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateBankAccountActivityRequest request)
        {
            var command = new CreateBankAccountActivityCommand(
                bankAccountId: request.BankAccountId,
                quantity: request.Quantity,
                currencyCode: request.CurrencyCode,
                description: request.Description,
                source: request.Source,
                processID: request.ProcessID,
                processName: request.ProcessName,
                processDate: request.ProcessDate,
                balance: request.Balance,
                receiptNo: request.ReceiptNo,
                note: request.Note);

            return Ok(await Mediator.Send(command));
        }

        // UPDATE
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateBankAccountActivityRequest request)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var command = new UpdateBankAccountActivityCommand(id)
            {
                BankAccountId = request.BankAccountId,
                Quantity = request.Quantity,
                CurrencyCode = request.CurrencyCode,
                Description = request.Description,
                Source = request.Source,
                ProcessID = request.ProcessID,
                ProcessName = request.ProcessName,
                ProcessDate = request.ProcessDate,
                Balance = request.Balance,
                ReceiptNo = request.ReceiptNo,
                Note = request.Note
            };

            return Ok(await Mediator.Send(command));
        }

        // DELETE
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteBankAccountActivityByIdCommand { Id = id }));
        }
    }
}

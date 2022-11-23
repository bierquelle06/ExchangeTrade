using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Features.BankAccount.Commands;
using Application.Filters;

using Application.Features.BankAccount.Commands.CreateBankAccount;
using Application.Features.BankAccount.Commands.DeleteBankAccountById;
using Application.Features.BankAccount.Commands.UpdateBankAccount;

using Application.Features.BankAccount.Queries.GetAllBankAccounts;
using Application.Features.BankAccount.Queries.GetBankAccountById;

using Application.Features.BankAccount.Requests.CreateBankAccount;
using Application.Features.BankAccount.Requests.UpdateBankAccount;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeDirect.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class BankAccountController : BaseApiController
    {
        // GET ALL
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllBankAccountsParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllBankAccountsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET BY ID
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetBankAccountByIdQuery { Id = id }));
        }

        // CREATE
        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateBankAccountRequest request)
        {
            var command = new CreateBankAccountCommand(
                customerId: request.CustomerId,
                bankId: request.BankId,
                integratorId: request.IntegratorId,
                currencyId: request.CurrencyId,
                bankAccountTypeId: request.BankAccountTypeId,
                name: request.Name,
                code: request.Code,
                number: request.Number,
                iban: request.Iban,
                openDate: request.OpenDate,
                totalBalance: request.TotalBalance,
                totalCreditBalance: request.TotalCreditBalance,
                totalCreditCardBalance: request.TotalCreditCardBalance,
                blockBalanceLimit: request.BlockBalanceLimit,
                blockCreditLimit: request.BlockCreditLimit);

            return Ok(await Mediator.Send(command));
        }

        // UPDATE
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateBankAccountRequest request)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var command = new UpdateBankAccountCommand(id)
            {
                CustomerId = request.CustomerId.Value,
                BankAccountTypeId = request.BankAccountTypeId.Value,
                BankId = request.BankId.Value,
                CurrencyId = request.CurrencyId.Value,
                IntegratorId = request.IntegratorId.Value,

                OpenDate = request.OpenDate,
                Name = request.Name,
                Code = request.Code,
                Number = request.Number,
                Iban = request.Iban,

                TotalBalance = request.TotalBalance,
                TotalCreditBalance = request.TotalCreditBalance,
                TotalCreditCardBalance = request.TotalCreditCardBalance,
                
                BlockBalanceLimit = request.BlockBalanceLimit,
                BlockCreditLimit = request.BlockCreditLimit
            };

            return Ok(await Mediator.Send(command));
        }

        // DELETE
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteBankAccountByIdCommand { Id = id }));
        }
    }
}

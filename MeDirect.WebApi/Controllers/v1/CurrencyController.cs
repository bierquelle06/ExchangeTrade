using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Features.Currency.Commands;
using Application.Filters;

using Application.Features.Currency.Commands.CreateCurrency;
using Application.Features.Currency.Commands.DeleteCurrencyById;
using Application.Features.Currency.Commands.UpdateCurrency;

using Application.Features.Currency.Queries.GetAllCurrency;
using Application.Features.Currency.Queries.GetCurrencyById;

using Application.Features.Currency.Requests.CreateCurrency;
using Application.Features.Currency.Requests.UpdateCurrency;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeDirect.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CurrencyController : BaseApiController
    {
        // GET ALL
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCurrencyParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllCurrencyQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET BY ID
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetCurrencyByIdQuery { Id = id }));
        }

        // CREATE
        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateCurrencyRequest request)
        {
            var command = new CreateCurrencyCommand(request.Name, request.Code);

            return Ok(await Mediator.Send(command));
        }

        // UPDATE
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCurrencyRequest request)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var command = new UpdateCurrencyCommand(id)
            {
                Name = request.Name,
                Code = request.Code
            };

            return Ok(await Mediator.Send(command));
        }

        // DELETE
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCurrencyByIdCommand { Id = id }));
        }
    }
}

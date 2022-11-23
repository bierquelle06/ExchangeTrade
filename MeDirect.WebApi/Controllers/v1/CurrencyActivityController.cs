using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Features.CurrencyActivity.Commands;
using Application.Filters;

using Application.Features.CurrencyActivity.Commands.CreateCurrencyActivity;
using Application.Features.CurrencyActivity.Commands.DeleteCurrencyActivityById;
using Application.Features.CurrencyActivity.Commands.UpdateCurrencyActivity;

using Application.Features.CurrencyActivity.Queries.GetAllCurrencyActivities;
using Application.Features.CurrencyActivity.Queries.GetCurrencyActivityById;

using Application.Features.CurrencyActivity.Requests.CreateCurrencyActivity;
using Application.Features.CurrencyActivity.Requests.UpdateCurrencyActivity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeDirect.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CurrencyActivityController : BaseApiController
    {
        // GET ALL
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCurrencyActivitiesParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllCurrencyActivitiesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET BY ID
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            return Ok(await Mediator.Send(new GetCurrencyActivityByIdQuery { Id = id.Value }));
        }

        // CREATE
        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateCurrencyActivityRequest request)
        {
            var command = new CreateCurrencyActivityCommand(
                currencyId: request.CurrencyId,
                symbol: request.Symbol,
                rate: request.Rate);

            return Ok(await Mediator.Send(command));
        }

        // UPDATE
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int? id, [FromBody] UpdateCurrencyActivityRequest request)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var command = new UpdateCurrencyActivityCommand(id.Value)
            {
                CurrencyId = request.CurrencyId,
                Symbol = request.Symbol,
                Rate = request.Rate
            };

            return Ok(await Mediator.Send(command));
        }

        // DELETE
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCurrencyActivityByIdCommand { Id = id }));
        }
    }
}

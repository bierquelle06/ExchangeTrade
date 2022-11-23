using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Features.Integrator.Commands;
using Application.Filters;

using Application.Features.Integrator.Commands.CreateIntegrator;
using Application.Features.Integrator.Commands.DeleteIntegratorById;
using Application.Features.Integrator.Commands.UpdateIntegrator;

using Application.Features.Integrator.Queries.GetAllIntegrators;
using Application.Features.Integrator.Queries.GetIntegratorById;

using Application.Features.Integrator.Requests.CreateIntegrator;
using Application.Features.Integrator.Requests.UpdateIntegrator;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeDirect.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class IntegratorController : BaseApiController
    {
        // GET ALL
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllIntegratorsParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllIntegratorsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET BY ID
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetIntegratorByIdQuery { Id = id }));
        }

        // CREATE
        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateIntegratorRequest request)
        {
            var command = new CreateIntegratorCommand(name: request.Name,
                code: request.Code,
                host: request.Host,
                port: request.Port,
                url: request.Url,
                username: request.UserName,
                password: request.Password);

            return Ok(await Mediator.Send(command));
        }

        // UPDATE
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateIntegratorRequest request)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var command = new UpdateIntegratorCommand(id)
            {
                Name = request.Name,
                Code = request.Code,
                Host = request.Host,
                Port = request.Port,
                Url = request.Url,
                UserName = request.UserName,
                Password = request.Password
            };

            return Ok(await Mediator.Send(command));
        }

        // DELETE
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteIntegratorByIdCommand { Id = id }));
        }
    }
}

using Distribuidora.API.Customers.Create;
using Distribuidora.API.Customers.Update;
using Distribuidora.Application.Customers.Create;
using Distribuidora.Application.Customers.GetAll;
using Distribuidora.Application.Customers.GetById;
using Distribuidora.Application.Customers.Update;
using Distribuidora.Domain.Customers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Distribuidora.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ISender _sender;
        public CustomerController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequests request, CancellationToken cancellationToken)
        {
            var command = new CreateCustomerCommand(request.Name, request.TaxId, request.PhoneNumber, request.Address, request.ContactName, request.CreditEnabled, request.CreditDays);
            var result = await _sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    code = result.Error.Code,
                    message = result.Error.Message,
                });
            }
            return Created($"/api/customers/{result.Value}", new { id = result.Value });
        }



        [HttpGet]
        public async Task<IActionResult> GetAllCustomers([FromQuery] string? search, [FromQuery] bool? isActive, CancellationToken cancellationToken)
        {
            var query = new GetCustomerQuery(search, isActive);
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateCustomerCommand(id, request.Name, request.TaxId, request.PhoneNumber, request.Address, request.ContactName, request.CreditEnable, request.CreditDays);
            var result = await _sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                if(result.Error == CustomerErrors.NotFound)
                {
                    return NotFound(new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                    });
                }

                return BadRequest(new
                {
                    code = result.Error.Code,
                    message = result.Error.Message
                });
            }

            return NoContent();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCustomerById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetCustomerByIdQuery(id);
            var result = await _sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                if (result.Error == CustomerErrors.NotFound)
                {
                    return NotFound(new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                    });
                }
                return BadRequest(new
                {
                    code = result.Error.Code,
                    message = result.Error.Message
                });
            }
            return Ok(result.Value);
        }
    }
}

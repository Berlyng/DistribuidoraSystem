using Distribuidora.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.ChangeStatus
{
    public sealed record ActivateCustomerCommand(
        Guid CustomerId
    ) : IRequest<Result>;
    

}

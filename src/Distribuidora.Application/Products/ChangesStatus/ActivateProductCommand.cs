using Distribuidora.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Products.ChangesStatus
{
    public sealed record ActivateProductCommand(
        Guid ProductId) : IRequest<Result>;
    
}

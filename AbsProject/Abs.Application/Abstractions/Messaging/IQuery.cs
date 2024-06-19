using Abs.Domain.Abstractions;
using MediatR;

namespace Abs.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}

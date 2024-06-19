
using Abs.Domain.Abstractions;
using MediatR;

namespace Abs.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> 
        where TQuery : IQuery<TResponse>
    {
    }
}

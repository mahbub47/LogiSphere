
using MediatR;

namespace LogiSphere.Application.Core.Abstraction;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}

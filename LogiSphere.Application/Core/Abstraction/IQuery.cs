
using MediatR;

namespace LogiSphere.Application.Core.Abstraction;

public interface IQuery<TResponse> : IRequest<TResponse> { }

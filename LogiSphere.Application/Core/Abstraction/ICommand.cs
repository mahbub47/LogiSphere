
using MediatR;

namespace LogiSphere.Application.Core.Abstraction;

public interface ICommand<TResponse> : IRequest<TResponse> { }

public interface ICommand : IRequest { }

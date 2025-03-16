

using MediatR;

namespace BuildingBlocks.CQRS;

// for no return value
public interface ICommand : IRequest<Unit> { }
// for return value
public interface ICommand<out TResponse> : IRequest<TResponse> { }


using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommandHandller<in TCommand>
	: IRequestHandler<TCommand,Unit>
	where TCommand : ICommand<Unit>
{
}


public interface ICommandHandller <in TCommand, TResponse>
	: IRequestHandler<TCommand, TResponse> 
	where TCommand: ICommand<TResponse>
	where TResponse : notnull
{
}

using CrowStation.Domain.Common;

namespace CrowStation.Application.Abstractions.Messaging;

/// <summary>
/// A request that changes state.
/// </summary>
public interface ICommand;

public interface ICommand<TResponse>;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}

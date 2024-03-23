using BlockyHeroesBackend.Application.Common;
using MediatR;

namespace BlockyHeroesBackend.Application.Abstractions;

public interface IOperation : IRequest<OperationResult>
{
}

public interface IOperation<TResult> : IRequest<OperationResult<TResult>> where TResult : class
{
}

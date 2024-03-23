using BlockyHeroesBackend.Application.Common;
using MediatR;

namespace BlockyHeroesBackend.Application.Abstractions;

public interface IOperationHandler<TRequest> : IRequestHandler<TRequest, OperationResult>
    where TRequest : IOperation
{
}

public interface IOperationHandler<TRequest, TResponse> : IRequestHandler<TRequest, OperationResult<TResponse>>
    where TRequest : IOperation<TResponse>
    where TResponse : class
{
    
}

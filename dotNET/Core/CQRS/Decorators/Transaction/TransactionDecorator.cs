using Core.CQRS.Abstract;
using MediatR;
using System.Transactions;

namespace Core.CQRS.Decorators.Transaction
{
    public class TransactionDecorator<TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        private readonly IRequestHandler<TCommand, TResult> _commandHandler;

        public TransactionDecorator(IRequestHandler<TCommand, TResult> commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            using (TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var result = await _commandHandler.Handle(request, cancellationToken);
                    transactionScope.Complete();
                    return result;
                }
                catch (Exception)
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}

using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Dtos.Transactions;

namespace Application.Features.Transactions
{
    public class CreateTransactionCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateTransactionCommand, Result<TransactionDto>>
    {
        public async Task<Result<TransactionDto>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

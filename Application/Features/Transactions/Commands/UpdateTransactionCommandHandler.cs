using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Dtos.Transactions;

namespace Application.Features.Transactions
{
    public class UpdateTransactionCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateTransactionCommand, Result<TransactionDto>>
    {
        public async Task<Result<TransactionDto>> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Dtos.Transactions;

namespace Application.Features.Transactions
{
    public class GetTransactionByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTransactionByIdQuery, Result<TransactionDto>>
    {
        public async Task<Result<TransactionDto>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

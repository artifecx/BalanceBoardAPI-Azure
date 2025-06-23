using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;

namespace Application.Features.Transactions
{
    public class DeleteTransactionCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteTransactionCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

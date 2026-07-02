using System.Net;
using Campaign.Shared.Exceptions;
using Campaign.API.Commands.SellerManager.Get;
using Campaign.API.DTO.SellerManager.Response;
using Campaign.API.Repositories.SellerManager.ReadOnly;

namespace Campaign.API.Handlers.SellerManagerScore.TargetManager
{
    public class SellerManagerRevenueTargetHandler : ISellerManagerRevenueTargetHandler
    {
        private readonly ISellerManagerReadOnlyRepository _sellerManagerReadOnlyRepository;
        public SellerManagerRevenueTargetHandler(ISellerManagerReadOnlyRepository sellerManagerReadOnlyRepository)
        {
            _sellerManagerReadOnlyRepository = sellerManagerReadOnlyRepository;
        }

        public async Task<SellerManagerTargetRevenueResponse> Handle(GetTargetRevenueCommand cmd)
        {
            if (cmd.SellerManagerId <= 0)
                throw new CompaignException(HttpStatusCode.BadRequest, "Identificador do gerente é obrigatório.");

            if(!await _sellerManagerReadOnlyRepository.Exists(cmd.SellerManagerId))
                throw new CompaignException(HttpStatusCode.BadRequest, "Gerente não encontado.");

            var revenueTarget = await _sellerManagerReadOnlyRepository.GetRevenueTarget(cmd.SellerManagerId);

            return new SellerManagerTargetRevenueResponse(revenueTarget);
        }
    }
}

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

        public async Task<SellerManagerRevenueInfosResponse> Handle(GetTargetRevenueCommand cmd)
        {
            if (cmd.SellerId.HasValue && !await _sellerManagerReadOnlyRepository.Exists(cmd.SellerId.Value))
                throw new CompaignException(HttpStatusCode.BadRequest, "Gerente não encontrado.");

            return await _sellerManagerReadOnlyRepository.GetRevenueTarget(cmd.SellerId);
        }
    }
}

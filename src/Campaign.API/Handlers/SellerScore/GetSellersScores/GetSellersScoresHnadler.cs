using System.Net;
using Campaign.Shared.Mappers;
using Campaign.Shared.Exceptions;
using Campaign.API.DTO.Page.Response;
using Campaign.API.Commands.SellerScore.Get;
using Campaign.API.DTO.SellerScore.Response;
using Campaign.API.Repositories.ProductPromotion.ReadOnly;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;
using Campaign.API.Handlers.SellerScore.GetSellersScores.Mapper;
using Campaign.API.Handlers.SellerScore.GetSellersScores.Validator;

namespace Campaign.API.Handlers.SellerScore.GetSellersScores
{
    public class GetSellersScoresHnadler : IGetSellersScoresHnadler
    {
        private readonly ISellerScoreReadOnlyRepository _sellerScoreReadOnlyRepository;
        public GetSellersScoresHnadler(ISellerScoreReadOnlyRepository sellerScoreReadOnlyRepository)
        {
            _sellerScoreReadOnlyRepository = sellerScoreReadOnlyRepository;
        }

        public async Task<PageResponse<SellerScoreResponse>> Handle(GetSellersScoresByFiltersCommand cmd)
        {
            if (cmd.TeamId <= 0)
                throw new CompaignException(HttpStatusCode.BadRequest, "Identificador da equipe é obrigatória.");

            var sellersScores = await _sellerScoreReadOnlyRepository.GetByFilters(cmd.TeamId, cmd.Filter, cmd.Page, cmd.Size);

            new SellersScoresAreFindedValidator()
                .Validate(sellersScores);

            var count = await _sellerScoreReadOnlyRepository.GetByTeamIdCount(cmd.TeamId, cmd.Filter, cmd.Page, cmd.Size);

            return new(content: sellersScores,
                       totalElements: (int)count,
                       currentPage: cmd?.Page ?? 1,
                       totalPages: cmd!.Size.HasValue ? (int)(count / cmd.Size.Value) : 1);
        }

        public async Task<SellerScoreResponse> Handle(GetSellerScoreByIdCommand cmd)
        {
            var sellerScore = await _sellerScoreReadOnlyRepository.GetById(cmd.SellerScoreId) ??
                throw new CompaignException(HttpStatusCode.BadRequest, "Score de vendedor não encontrado.");

            var response = new ToDTO();
            return response.Parse(new MapperParam<Entity.SellerScore>(sellerScore));
        }
    }
}
using Campaign.API.Commands.SellerScore.Get;
using Campaign.API.DTO.SellerScore.Response;
using Campaign.API.Handlers.SellerScore.GetSellersScores.Mapper;
using Campaign.API.Repositories.ProductPromotion.ReadOnly;
using Campaign.Shared.Mappers;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;
using Campaign.API.Handlers.SellerScore.GetSellersScores.Validator;
using Campaign.API.DTO.Page.Response;
using Campaign.Shared.Exceptions;
using System.Net;

namespace Campaign.API.Handlers.SellerScore.GetSellersScores
{
    public class GetSellersScoresHnadler : IGetSellersScoresHnadler
    {
        private readonly ISellerScoreReadOnlyRepository _sellerScoreReadOnlyRepository;
        public GetSellersScoresHnadler(ISellerScoreReadOnlyRepository sellerScoreReadOnlyRepository)
        {
            _sellerScoreReadOnlyRepository = sellerScoreReadOnlyRepository;
        }

        public async Task<List<SellerScoreResponse>> Handle()
        {
            var sellersScores = await _sellerScoreReadOnlyRepository.GetAll();

            new SellersScoresAreFindedValidator()
                .Validate(sellersScores);

            var mapper = new ToDTO();
            return mapper.Parse(new MapperParam<List<Entity.SellerScore>>(sellersScores));
        }

        public async Task<PageResponse<SellerScoreResponse>> Handle(GetSellersScoresByTeamCommand cmd)
        {
            if (cmd.TeamId <= 0)
                throw new CompaignException(HttpStatusCode.BadRequest, "Identificador da equipe é obrigatória.");

            var sellersScores = await _sellerScoreReadOnlyRepository.GetByTeamId(cmd.TeamId, cmd.Filter, cmd.Page, cmd.Size);

            new SellersScoresAreFindedValidator()
                .Validate(sellersScores);

            var count = await _sellerScoreReadOnlyRepository.GetByTeamIdCount(cmd.TeamId, cmd.Filter, cmd.Page, cmd.Size);

            var mapper = new ToDTO();
            var result = mapper.Parse(new MapperParam<List<Entity.SellerScore>>(sellersScores));

            return new(currentPage: cmd.Page, totalElements: (int)count, totalPages: (int)(count / cmd.Size), content: result);
        }
    }
}

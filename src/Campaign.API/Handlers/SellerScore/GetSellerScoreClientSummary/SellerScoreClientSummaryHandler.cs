using Campaign.Shared.Mappers;
using Campaign.API.DTO.Page.Response;
using Campaign.API.Commands.SellerScore.Get;
using Campaign.API.Repositories.Customer.ReadOnly;
using Campaign.Shared.DataBaseContext.Entities.Customer;
using Campaign.API.DTO.SellerScoreProductSummary.Response;
using Campaign.API.Repositories.SellerScoreClientSummary.ReadOnly;
using Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;
using Campaign.API.Handlers.SellerScore.GetSellerScoreClientSummary.Mapper;
using Campaign.API.Handlers.SellerScore.GetSellerScoreClientSummary.Validator;

namespace Campaign.API.Handlers.SellerScore.GetSellerScoreClientSummary
{
    public class SellerScoreClientSummaryHandler : ISellerScoreClientSummaryHandler
    {
        private readonly ICustomerReadOnlyRepository _customerReadOnlyRepository;
        private readonly ISellerScoreClientSummaryReadOnlyRepository _sellerScoreClientSummaryReadOnlyRepository;

        public SellerScoreClientSummaryHandler(ICustomerReadOnlyRepository customerReadOnlyRepository,
                                               ISellerScoreClientSummaryReadOnlyRepository sellerScoreClientSummaryReadOnlyRepository)
        {
            _customerReadOnlyRepository = customerReadOnlyRepository;
            _sellerScoreClientSummaryReadOnlyRepository = sellerScoreClientSummaryReadOnlyRepository;
        }

        public async Task<PageResponse<SellerScoreClientSummaryResponse.Customer, SellerScoreClientSummaryResponse.Resume>> Handle(GetSellerScoreClientSummaryCommand cmd)
        {
            new GetSellerScoreClientSummaryDataValidator()
                .Validate(cmd);

            var summary = await _sellerScoreClientSummaryReadOnlyRepository.GetByFilters(size: cmd.Size,
                                                                                         page: cmd.Page,
                                                                                         sellerId: cmd.SellerId,
                                                                                         customerId: cmd.CustomerId,
                                                                                         promotionCode: cmd.PromotionCode,
                                                                                         customerSalesEventType: cmd.CustomerSalesEventType);
            var customersIds = summary.Select(x => x.CustomerId);
            var customers = await _customerReadOnlyRepository.GetByIds([.. customersIds]);

            var count = await _sellerScoreClientSummaryReadOnlyRepository.GetByFiltersCount(sellerId: cmd.SellerId,
                                                                                            promotionCode: cmd.PromotionCode,
                                                                                            customerSalesEventType: cmd.CustomerSalesEventType);

            var mapper = new ToDTO();
            var result = mapper.Parse(new MapperParam<List<SellerScoreClientsSummary>>(summary), new MapperParam<List<Customer>>(customers));

            return new(currentPage: cmd.Page,
                       resume: result.Resumes,
                       content: result.Customers,
                       totalElements: (int)count,
                       totalPages: (int)(count / cmd!.Size));
        }
    }
}

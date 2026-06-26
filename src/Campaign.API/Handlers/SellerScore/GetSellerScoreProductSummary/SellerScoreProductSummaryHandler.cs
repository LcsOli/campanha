using Campaign.API.Commands.SellerScore.Get;
using Campaign.API.Handlers.SellerScore.GetSellerScoreProductSummary.Mapper;
using Campaign.API.Handlers.SellerScore.GetSellerScoreProductSummary.Validator;
using Campaign.API.Repositories.Customer.ReadOnly;
using Campaign.API.Repositories.Product.ReadOnly;
using Campaign.API.Repositories.SellerScoreProductsSummary.ReadOnly;
using Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;
using Campaign.Shared.Mappers;
using Campaign.Shared.DataBaseContext.Entities.Customer;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.API.Handlers.SellerScore.GetSellerScoreProductSummary
{
    public class SellerScoreProductSummaryHandler : ISellerScoreProductSummary
    {
        private readonly IProductReadOnlyRepository _productReadOnlyRepository;
        private readonly ICustomerReadOnlyRepository _customerReadOnlyRepository;
        private readonly ISellerScoreProductSummaryReadOnlyRepository _sellerScoreProductSummaryReadOnlyRepository;

        public SellerScoreProductSummaryHandler(IProductReadOnlyRepository productReadOnlyRepository,
                                                ICustomerReadOnlyRepository customerReadOnlyRepository,
                                                ISellerScoreProductSummaryReadOnlyRepository sellerScoreProductSummaryReadOnlyRepository)
        {
            _productReadOnlyRepository = productReadOnlyRepository;
            _customerReadOnlyRepository = customerReadOnlyRepository;
            _sellerScoreProductSummaryReadOnlyRepository = sellerScoreProductSummaryReadOnlyRepository;
        }

        public async Task Handle(GetSellerScoreProductSummaryCommand cmd)
        {
            new SellerScoreProductSummaryDataValidator()
                .Validate(cmd);

            var summary = await _sellerScoreProductSummaryReadOnlyRepository.GetByFilters(cmd.Size,
                                                                                          cmd.Page,
                                                                                          cmd.SellerId,
                                                                                          cmd.Customer,
                                                                                          cmd.ProductId,
                                                                                          cmd.PromotionCode);
            var customersIds = summary.Select(x => x.CustomerId);
            var customers = await _customerReadOnlyRepository.GetByIds([.. customersIds]);

            var productsIds = summary.Select(x => x.ProductId);
            var products = await _productReadOnlyRepository.GetByIds([.. productsIds]);

            var mapper = new ToDTO();

            var customerParam = new MapperParam<List<Customer>>(customers);
            var productParam = new MapperParam<List<Entity.Product>>(products);
            var summaryParam = new MapperParam<List<SellerScoreProductsSummary>>(summary);


            var result = mapper.Parse(productParam, customerParam, summaryParam);



        }
    }
}

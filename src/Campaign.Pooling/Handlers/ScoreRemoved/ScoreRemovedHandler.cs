using Campaign.Processor.API.Commands.ScoreRemoved.Create;
using Campaign.Pooling.Repositories.ProductPromotion.ReadOnly;
using Campaign.Processor.API.Handlers.ScoreCanceled.Validator;
using Campaign.Pooling.Repositories.OrderProductRemoved.ReadOnly;

\namespace Campaign.Processor.API.Handlers.ScoreRemoved
{
    public class ScoreRemovedHandler : IScoreRemovedHandler
    {
        private readonly IProductPromotionReadOnlyRepository _productPromotionReadOnlyRepository;
        private readonly IOrderProductRemovedReadOnlyRepository _orderProductRemovedReadOnlyRepository;
        public ScoreRemovedHandler(IProductPromotionReadOnlyRepository productPromotionReadOnlyRepository,
                                   IOrderProductRemovedReadOnlyRepository orderProductRemovedReadOnlyRepository)
        {
            _productPromotionReadOnlyRepository = productPromotionReadOnlyRepository;
            _orderProductRemovedReadOnlyRepository = orderProductRemovedReadOnlyRepository;
        }

        /*
            Motivo da criação do handler

                - Alguns pedidos podem ter produtos removidos. 
                    - Preciso pegar os produtos removidos por pedido;
                    - Preciso saber qual foi o RCA que fez este pedido;
                    - Preciso calcular a quantidade de pontos que o RCA perdeu por conta do produto removido;
         */

        public async Task Handle(RegisterScoreRemovedCommand cmd)
        {
            new RegisterScoreRemovedDataValidator()
                .Validate(cmd);

            var ordersIds = cmd.OrdersDetails.Select(x => x.OrderId).Distinct();

            var productsRemoveds = await _orderProductRemovedReadOnlyRepository.GetByOrdersIds(cmd.promotionCode, [.. ordersIds]);

            if (productsRemoveds.Count <= 0)
                return;

            var productsIds = productsRemoveds.Select(x => x.ProductId).Distinct();

            var productsPromotion = await _productPromotionReadOnlyRepository.GetByProductsIdsAndPromotionCode(cmd.promotionCode, [.. productsIds]);

            /*
             
                 Problema: Para cada produto removido do, preciso buscar o RCA 

             */

        }
    }
}
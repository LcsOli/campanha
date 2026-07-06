using Campaign.Processor.API.Commands.ScoreRemoved.Create;
using Campaign.Pooling.Repositories.ProductPromotion.ReadOnly;
using Campaign.Pooling.Repositories.OrderProductRemoved.ReadOnly;
using Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderItemRemoved.Validator;

namespace Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderItemRemoved
{
    public class ScoreByOrderRemovedHandler : IScoreByOrderRemovedHandler
    {
        private readonly IProductPromotionReadOnlyRepository _productPromotionReadOnlyRepository;
        private readonly IOrderProductRemovedReadOnlyRepository _orderProductRemovedReadOnlyRepository;
        public ScoreByOrderRemovedHandler(IProductPromotionReadOnlyRepository productPromotionReadOnlyRepository,
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

            var productsRemoveds = await _orderProductRemovedReadOnlyRepository.GetByOrdersIds(cmd.PromotionCode, [.. ordersIds]);

            if (productsRemoveds.Count <= 0)
                return;

            var productsIds = productsRemoveds.Select(x => x.ProductId).Distinct();

            var productsPromotion = await _productPromotionReadOnlyRepository.GetByProductsIdsAndPromotionCode(cmd.PromotionCode, [.. productsIds]);

            /*
             
                 Problema: Para cada produto removido do, preciso buscar o RCA 

             */

        }
    }
}
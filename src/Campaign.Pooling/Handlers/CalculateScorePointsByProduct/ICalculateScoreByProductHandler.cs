using Campaign.Pooling.Commands.CalculateScoreByProduct;

namespace Campaign.Pooling.Handlers.CalculateScoreByProduct
{
    public interface ICalculateScoreByProductHandler
    {
        Task Handle(CalculateScoreByProductCommand cmd);
    }
}

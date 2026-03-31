using Campaign.Pooling.Commands.CalculateScoreByProduct;

namespace Campaign.Pooling.Handlers.CalculateScoreByProduct
{
    public interface ICalculateScorePointsHandler
    {
        void Handle(CalculateScoreByProductCommand cmd);
    }
}

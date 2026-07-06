using System.ComponentModel.DataAnnotations.Schema;

namespace Campaign.Shared.DataBaseContext.Entities.Seller
{
    public class SellerScore
    {
        public int Id { get; private set; }
        public int SellerId { get; private set; }
        public decimal Score { get; private set; }
        public string Name { get; private set; } = default!;
        public string ManagerName { get; private set; } = default!;
        public int TeamId { get; private set; }
        public Team.Team? Team { get; private set; }
        public short Coupons { get; private set; }
        public decimal RevenueTarget { get; private set; }
        public decimal CurrentRevenue { get; private set; }
        public decimal RevenueMonth1 { get; private set; }
        public decimal RevenueMonth2 { get; private set; }
        public decimal RevenueMonth3 { get; private set; }
        public decimal RevenueMonth4 { get; private set; }
        public decimal RevenueMonth5 { get; private set; }
        public short QtyConsumersReactivateds { get; private set; }
        public short QtyConsumersRegistereds { get; private set; }
        public DateTime? LastScoreByAccess { get; private set; }
        public int SellerManagerId { get; private set; }
        public decimal ScoreProductsCanceledsOrders { get; private set; }
        public decimal ScoreProductRemovedFromOrders { get; private set; }
        public bool GetPointsByTraining { get; private set; }

        public SellerScore(int teamId,
                           string name,
                           int sellerId,
                           string managerName,
                           int sellerManagerId)
        {
            Name = name;
            TeamId = teamId;
            SellerId = sellerId;
            ManagerName = managerName;
            SellerManagerId = sellerManagerId;
        }

        public SellerScore(string name,
                           short coupons,
                           int sellerId,
                           decimal score,
                           Team.Team team,
                           string managerName,
                           decimal revenueTarget,
                           decimal currentRevenue,
                           decimal revenueMonth1,
                           decimal revenueMonth2,
                           decimal revenueMonth3,
                           decimal revenueMonth4,
                           decimal revenueMonth5,
                           bool getPointsByTraining,
                           DateTime? lastScoreByAccess,
                           short qtyConsumersRegistereds,
                           short qtyConsumersReactivateds)
        {
            Name = name;
            Team = team;
            Score = score;
            Coupons = coupons;
            SellerId = sellerId;
            ManagerName = managerName;
            RevenueTarget = revenueTarget;
            CurrentRevenue = currentRevenue;
            RevenueMonth1 = revenueMonth1;
            RevenueMonth2 = revenueMonth2;
            RevenueMonth3 = revenueMonth3;
            RevenueMonth4 = revenueMonth4;
            RevenueMonth5 = revenueMonth5;
            LastScoreByAccess = lastScoreByAccess;
            GetPointsByTraining = getPointsByTraining;
            QtyConsumersRegistereds = qtyConsumersRegistereds;
            QtyConsumersReactivateds = qtyConsumersReactivateds;
        }

        public void UpdateScore(decimal score)
        {
            Score += score;
        }

        public void UpdateCoupons(short coupons)
        {
            Coupons = coupons;
        }

        public void UpdateCurrentRevenue(decimal revenue)
        {
            CurrentRevenue = revenue;
        }

        public void ClearCurrentRevenue()
        {
            CurrentRevenue = 0;
        }

        public void UpdateQtyReactivateds(short qtyReactivateds)
        {
            QtyConsumersReactivateds += qtyReactivateds;
        }

        public void UpdateQtyRegistereds(short qtyRegistereds)
        {
            QtyConsumersRegistereds += qtyRegistereds;
        }

        public void UpdateRevenueByMonth(decimal revenue)
        {
            if (RevenueMonth1 == 0)
            {
                RevenueMonth1 = revenue;
                return;
            }

            if (RevenueMonth2 == 0)
            {
                RevenueMonth2 = revenue;
                return;
            }

            if (RevenueMonth3 == 0)
            {
                RevenueMonth3 = revenue;
                return;
            }

            if (RevenueMonth4 == 0)
            {
                RevenueMonth4 = revenue;
                return;
            }

            RevenueMonth5 = revenue;

            //TODO - Verificar a possibilidade de criar uma tabela de receita mensal para evitar a necessidade de criar uma coluna para cada mês, visto que isso pode gerar problemas de manutenção no futuro.
        }

        public void UpdateLastScoreByAccess()
        {
            LastScoreByAccess = DateTime.Now;
        }

        public void ClearPoints()
        {
            Score = 0;
            Coupons = 0;
            RevenueTarget = 0;
            CurrentRevenue = 0;
            RevenueMonth1 = 0;
            RevenueMonth2 = 0;
            RevenueMonth3 = 0;
            RevenueMonth4 = 0;
            RevenueMonth5 = 0;
            QtyConsumersRegistereds = 0;
            QtyConsumersReactivateds = 0;
        }

        public void SetScoreProductsCanceledsOrders(decimal score)
        {
            ScoreProductsCanceledsOrders += score;
        }

        public void SetScoreProductRemovedFromOrders(decimal score)
        {
            ScoreProductRemovedFromOrders += score;
        }
    }
}

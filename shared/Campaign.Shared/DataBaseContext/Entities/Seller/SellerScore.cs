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

        [NotMapped]
        public decimal CouponsByRevenue { get; private set; }

        [NotMapped]
        public decimal CouponsByScore { get; private set; }

        public SellerScore(string name,
                           int sellerId,
                           string managerName)
        {
            Name = name;
            SellerId = sellerId;
            ManagerName = managerName;
        }

        public void UpdateScore(decimal score)
        {
            Score += score;
        }

        public void UpdateCoupons(short coupons)
        {
            Coupons += coupons;
        }

        public void UpdateCouponsByRevenue()
        {
            CouponsByRevenue++;
        }

        public void UpdateCouponsByScore()
        {
            CouponsByScore++;
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

        public void UpdateRevenueByMonth(decimal revenue, int month)
        {
            //TODO - Verificar a possibilidade de decidir qual coluna preencher de forma dinamica sem depender de switch case.

            switch (month)
            {
                case 6:
                    RevenueMonth1 = revenue;
                    break;
                case 7:
                    RevenueMonth2 = revenue;
                    break;
                case 8:
                    RevenueMonth3 = revenue;
                    break;
                case 9:
                    RevenueMonth4 = revenue;
                    break;
                case 10:
                    RevenueMonth5 = revenue;
                    break;
            }
            //TODO - Verificar a possibilidade de criar uma tabela de receita mensal para evitar a necessidade de criar uma coluna para cada mês, visto que isso pode gerar problemas de manutenção no futuro.
        }

        public void UpdateLastScoreByAccess()
        {
            LastScoreByAccess = DateTime.Now;
        }
    }
}

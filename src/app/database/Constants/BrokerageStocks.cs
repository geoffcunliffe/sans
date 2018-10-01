using Sans.CreditUnion.Database.Models;
using System.Collections.Generic;

namespace Sans.CreditUnion.Database.Constants
{
    public class BrokerageStocks
    {
        public static BrokerageStock SansCreditUnion => new BrokerageStock
        {
            Id = 1,
            CompanyName = "SANS Credit Union",
            PreviousDayClose = 100,
            Ticker = "SANS"
        };

        public static BrokerageStock BurgerHut => new BrokerageStock
        {
            Id = 2,
            CompanyName = "Burger Hut",
            PreviousDayClose = 10,
            Ticker = "BURG"
        };

        public static BrokerageStock FurnitureRUs => new BrokerageStock
        {
            Id = 3,
            CompanyName = "Furniture 'R Us",
            PreviousDayClose = 5,
            Ticker = "FURN"
        };

        public static List<BrokerageStock> All => new List<BrokerageStock>
        {
            SansCreditUnion,
            BurgerHut,
            FurnitureRUs
        };
    }
}

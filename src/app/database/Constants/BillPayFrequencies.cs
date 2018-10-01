using System.Collections.Generic;

namespace Sans.CreditUnion.Database.Constants
{
    public static class BillPayFrequencies
    {
        public const string Daily = "Daily";
        public const string Monthly = "Monthly";
        public const string Weekly = "Weekly";
        public const string Yearly = "Yearly";

        // Hardcoding vs. Reflection for simplicity/this shouldn't change often (or possibly ever).
        public static string[] All => new[]
        {
            Daily,
            Monthly,
            Weekly,
            Yearly
        };
    }
}

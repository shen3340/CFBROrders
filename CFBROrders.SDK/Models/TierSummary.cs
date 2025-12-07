namespace CFBROrders.SDK.Models
{
    public partial class TierSummary
    {
        public int Tier { get; set; }
        public int Players { get; set; }
        public int Quota { get; set; }
        public int AssignedStars { get; set; }
        public double QuotaPercent { get; set; }
        public int TotalTerritories { get; set; }
        public int CompletedTerritories { get; set; }
        public double CompletedPercent { get; set; }
    }
}

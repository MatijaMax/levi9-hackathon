namespace FibaApi
{
    public class PlayerStatsResponse
    {
        public string PlayerName { get; set; }
        public int GamesPlayed { get; set; }
        public TraditionalStats Traditional { get; set; }
        public AdvancedStats Advanced { get; set; }
    }

    public class TraditionalStats
    {
        public FreeThrowsStats FreeThrows { get; set; }
        public TwoPointsStats TwoPoints { get; set; }
        public ThreePointsStats ThreePoints { get; set; }
        public double? Points { get; set; } = 0;
        public double? Rebounds { get; set; } = 0;
        public double? Blocks { get; set; } = 0;
        public double? Assists { get; set; } = 0;
        public double? Steals { get; set; } = 0;
        public double? Turnovers { get; set; } = 0;
    }

    public class FreeThrowsStats
    {
        public double? Attempts { get; set; } = 0;
        public double? Made { get; set; } = 0;
        public double? ShootingPercentage { get; set; } = 0;
    }

    public class TwoPointsStats
    {
        public double? Attempts { get; set; } = 0;
        public double? Made { get; set; } = 0;
        public double? ShootingPercentage { get; set; } = 0;
    }

    public class ThreePointsStats
    {
        public double? Attempts { get; set; } = 0;
        public double? Made { get; set; } = 0;
        public double? ShootingPercentage { get; set; } = 0;
    }

    public class AdvancedStats
    {
        public double? Valorization { get; set; } = 0;
        public double? EffectiveFieldGoalPercentage { get; set; } = 0;
        public double? TrueShootingPercentage { get; set; } = 0;
        public double? HollingerAssistRatio { get; set; } = 0;
    }
}

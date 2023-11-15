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
        public double Points { get; set; }
        public double Rebounds { get; set; }
        public double Blocks { get; set; }
        public double Assists { get; set; }
        public double Steals { get; set; }
        public int Turnovers { get; set; }
    }

    public class FreeThrowsStats
    {
        public double Attempts { get; set; }
        public double Made { get; set; }
        public double ShootingPercentage { get; set; }
    }

    public class TwoPointsStats
    {
        public double Attempts { get; set; }
        public double Made { get; set; }
        public double ShootingPercentage { get; set; }
    }

    public class ThreePointsStats
    {
        public double Attempts { get; set; }
        public double Made { get; set; }
        public double ShootingPercentage { get; set; }
    }

    public class AdvancedStats
    {
        public double Valorization { get; set; }
        public double EffectiveFieldGoalPercentage { get; set; }
        public double TrueShootingPercentage { get; set; }
        public double HollingerAssistRatio { get; set; }
    }
}

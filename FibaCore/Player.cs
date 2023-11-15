using FibaCore.Enums;

namespace FibaCore
{
    public class Player
    {
        public string? Name { get; set; }
        public Position Position { get; set; }

        public int? FTM { get; set; }
        public int? FTA { get; set; }
        public int? TwoPM { get; set; }
        public int? TwoPA { get; set; }
        public int? ThreePM { get; set; }
        public int? ThreePA { get; set; }
        public int? REB { get; set; }
        public int? BLK { get; set; }
        public int? AST { get; set; }
        public int? STL { get; set; }
        public int? TOV { get; set; }
    }
}

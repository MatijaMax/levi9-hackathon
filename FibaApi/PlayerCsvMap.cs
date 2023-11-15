using CsvHelper.Configuration;
using FibaCore;

namespace FibaApi
{
    public sealed class PlayerCsvMap : ClassMap<PlayerCSV>
    {
        public PlayerCsvMap()
        {
            Map(m => m.Name).Name("PLAYER");
            Map(m => m.Position).Name("POSITION");
            Map(m => m.FTM).Name("FTM");
            Map(m => m.FTA).Name("FTA");
            Map(m => m.TwoPM).Name("2PM");
            Map(m => m.TwoPA).Name("2PA");
            Map(m => m.ThreePM).Name("3PM");
            Map(m => m.ThreePA).Name("3PA");
            Map(m => m.REB).Name("REB");
            Map(m => m.BLK).Name("BLK");
            Map(m => m.AST).Name("AST");
            Map(m => m.STL).Name("STL");
            Map(m => m.TOV).Name("TOV");
        }
    }
}

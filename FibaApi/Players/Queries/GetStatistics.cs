using FibaCore;
using FibaInfrastructure.Repositories;
using MediatR;

namespace FibaApi.Players.Queries
{
    public class GetStatistics
    {
        public class Query : IRequest<PlayerStatsResponse>
        {
            public string PlayerFullName;
        }

        public class RequestHandler : IRequestHandler<Query, PlayerStatsResponse>
        {
            private readonly IRepository<Player> _repository;

            public RequestHandler(IRepository<Player> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<PlayerStatsResponse?> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                List<Player> players = _repository.GetAll().Where(p => p.Name == request.PlayerFullName).ToList();
                if (players.Count == 0)
                {
                    return Task.FromResult<PlayerStatsResponse?>(null);
                }
                var response = new PlayerStatsResponse();
                response.PlayerName = request.PlayerFullName;
                response.GamesPlayed = players.Count();
                response.Traditional = countTraditional(players);
                response.Advanced = countAdvanced(players);



                return Task.FromResult(response);
            }

            public TraditionalStats countTraditional(List<Player> players)
            {
                TraditionalStats traditionalStats = new TraditionalStats();
                foreach (var player in players)
                {
                    traditionalStats.Assists += player.AST;
                    traditionalStats.Rebounds += player.REB;
                    traditionalStats.Blocks += player.BLK;
                    traditionalStats.Points += (player.FTM + 2 * player.TwoPM + 3 * player.ThreePM);
                    traditionalStats.Steals += player.STL;
                    traditionalStats.Turnovers += player.TOV;


                    traditionalStats.FreeThrows = new FreeThrowsStats();
                    traditionalStats.FreeThrows.Attempts += player.FTA;
                    traditionalStats.FreeThrows.Made += player.FTM;
                    traditionalStats.TwoPoints = new TwoPointsStats();
                    traditionalStats.TwoPoints.Attempts += player.TwoPA;
                    traditionalStats.TwoPoints.Made += player.TwoPM;
                    traditionalStats.ThreePoints = new ThreePointsStats();
                    traditionalStats.ThreePoints.Attempts += player.ThreePA;
                    traditionalStats.ThreePoints.Made += player.ThreePM;

                }

                //ROUNDING
                int playerCount = players.Count();

                traditionalStats.Assists = Math.Round((traditionalStats.Assists / playerCount) ?? 0, 1);
                traditionalStats.Rebounds = Math.Round((traditionalStats.Rebounds / playerCount) ?? 0, 1);
                traditionalStats.Blocks = Math.Round((traditionalStats.Blocks / playerCount) ?? 0, 1);
                traditionalStats.Points = Math.Round((traditionalStats.Points / playerCount) ?? 0, 1);
                traditionalStats.Steals = Math.Round((traditionalStats.Steals / playerCount) ?? 0, 1);
                traditionalStats.Turnovers = Math.Round((traditionalStats.Turnovers / playerCount) ?? 0, 1);

                traditionalStats.FreeThrows.ShootingPercentage = Math.Round(((traditionalStats.FreeThrows.Made / traditionalStats.FreeThrows.Attempts) * 100) ?? 0, 1);
                traditionalStats.TwoPoints.ShootingPercentage = Math.Round(((traditionalStats.TwoPoints.Made / traditionalStats.TwoPoints.Attempts) * 100) ?? 0, 1);
                traditionalStats.ThreePoints.ShootingPercentage = Math.Round(((traditionalStats.ThreePoints.Made / traditionalStats.ThreePoints.Attempts) * 100) ?? 0, 1);

                traditionalStats.FreeThrows.Attempts = Math.Round((traditionalStats.FreeThrows.Attempts / playerCount) ?? 0, 1);
                traditionalStats.FreeThrows.Made = Math.Round((traditionalStats.FreeThrows.Made / playerCount) ?? 0, 1);
                traditionalStats.TwoPoints.Attempts = Math.Round((traditionalStats.TwoPoints.Attempts / playerCount) ?? 0, 1);
                traditionalStats.TwoPoints.Made = Math.Round((traditionalStats.TwoPoints.Made / playerCount) ?? 0, 1);
                traditionalStats.ThreePoints.Attempts = Math.Round((traditionalStats.ThreePoints.Attempts / playerCount) ?? 0, 1);
                traditionalStats.ThreePoints.Made = Math.Round((traditionalStats.ThreePoints.Made / playerCount) ?? 0, 1);

                return traditionalStats;
            }


            public AdvancedStats countAdvanced(List<Player> players)
            {
                AdvancedStats advancedStats = new AdvancedStats();
                foreach (var player in players)
                {
                    advancedStats.EffectiveFieldGoalPercentage += (player.TwoPM + player.ThreePM + 0.5 * player.ThreePM) / (player.TwoPA + player.ThreePA) * 100;
                    advancedStats.Valorization += (player.FTM + 2 * player.TwoPM + 3 * player.ThreePM + player.REB + player.BLK + player.AST + player.STL) - (player.FTA - player.FTM + player.TwoPA - player.TwoPM + player.ThreePA - player.ThreePM + player.TOV);
                    advancedStats.TrueShootingPercentage += (player.FTM + 2 * player.TwoPM + 3 * player.ThreePM) / (2 * (player.TwoPA + player.ThreePA + player.FTA * 0.475)) * 100;
                    advancedStats.HollingerAssistRatio += (player.TwoPM + player.ThreePM + 0.5 * player.ThreePM) / (player.TwoPA + player.ThreePA) * 100;
                }

                //ROUNDING
                int playerCount = players.Count();
                advancedStats.EffectiveFieldGoalPercentage = Math.Round((advancedStats.EffectiveFieldGoalPercentage / playerCount) ?? 0, 1);
                advancedStats.Valorization = Math.Round((advancedStats.Valorization / playerCount) ?? 0, 1);
                advancedStats.TrueShootingPercentage = Math.Round((advancedStats.TrueShootingPercentage / playerCount) ?? 0, 1);
                advancedStats.HollingerAssistRatio = Math.Round((advancedStats.HollingerAssistRatio / playerCount) ?? 0, 1);

                return advancedStats;
            }






        }
    }
}

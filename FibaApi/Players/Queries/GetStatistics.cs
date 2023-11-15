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
                //var response = players;

                /*     var response = new StatsResponse
                     {
                         PlayerName = playerStats.PlayerName,
                         GamesPlayed = playerStats.GamesPlayed,
                         Traditional = playerStats.Traditional,
                         Advanced = playerStats.Advanced
                     };  */

                return Task.FromResult(response);
            }
        }
    }
}

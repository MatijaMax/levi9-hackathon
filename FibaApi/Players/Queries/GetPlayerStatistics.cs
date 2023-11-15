using FibaCore;
using FibaInfrastructure.Repositories;
using MediatR;
namespace FibaApi.Players.Queries
{
    public static class GetPlayerStatistics
    {
        public class Query : IRequest<List<Player>>
        {

        }

        public class RequestHandler : IRequestHandler<Query, List<Player>>
        {
            private readonly IRepository<Player> _repository;

            public RequestHandler(IRepository<Player> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<List<Player>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                List<Player> players = _repository.GetAll().ToList();

                return Task.FromResult(players);
            }
        }
    }
}

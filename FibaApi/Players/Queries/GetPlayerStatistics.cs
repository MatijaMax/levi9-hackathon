using MediatR;
namespace FibaApi.Players.Queries
{
    public static class GetPlayerStatistics
    {
        public class Query : IRequest<List<Customer>>
        {

        }

        public class RequestHandler : IRequestHandler<Query, List<Customer>>
        {
            private readonly IRepository<Customer> _repository;

            public RequestHandler(IRepository<Customer> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<List<Customer>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                List<Customer> customers = _repository.GetAll().ToList();

                return Task.FromResult(customers);
            }
        }
    }
}

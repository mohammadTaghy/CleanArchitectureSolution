using MediatR;

namespace Application.UseCases
{
    public class BaseLoadItemQuery<TResponse> : IRequest<TResponse>
        where TResponse : class,new()
    {
        public int Id { get; set; }
    }
}

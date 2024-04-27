namespace Aira.Application.Features.Continent.Queries;

public record GetContinentByIdQuery(int Id) : IRequest<ContinentDto>;

internal class GetContinentByIdQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetContinentByIdQuery, ContinentDto>
{
    public async Task<ContinentDto> Handle(GetContinentByIdQuery request, CancellationToken cancellationToken)
    {
        var continent = await dbContext.Continent.Where(r => r.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return continent?.Adapt<ContinentDto>();
    }
}
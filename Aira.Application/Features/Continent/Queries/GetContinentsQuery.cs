namespace Aira.Application.Features.Continent.Queries;

public record GetContinentsQuery : IRequest<IList<ContinentListDto>>, ICacheableMediatrQuery
{
    public bool BypassCache { get; set; }
    public string CacheKey { get; set; }
    public TimeSpan SlidingExpiration { get; set; }
}

internal class GetContinentsQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetContinentsQuery, IList<ContinentListDto>>
{
    public async Task<IList<ContinentListDto>> Handle(GetContinentsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Continent.ProjectToType<ContinentListDto>()
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
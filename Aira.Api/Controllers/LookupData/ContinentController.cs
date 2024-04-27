namespace Aira.Api.Controllers.LookupData;

public class ContinentController : BaseApiController
{
    [HttpGet("GetContinents")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ContinentListDto>))]
    public async Task<ApiResponse<IList<ContinentListDto>>> GetContinents()
    {
        var data = await Mediator.Send(new GetContinentsQuery
        {
            BypassCache = false,
            CacheKey = CacheKeys.ContinentListKey,
            SlidingExpiration = TimeSpan.FromDays(365)
        });

        return new ApiResponse<IList<ContinentListDto>>
        {
            StatusCode = 200,
            Message = "Data retrieved successfully",
            Data = data
        };

    }

    [HttpGet("{continentId:int}", Name = "GetContinentById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContinentDto))]
    public async Task<ApiResponse<ContinentDto>> GetContinentById(int continentId)
    {
        var data = await Mediator.Send(new GetContinentByIdQuery(continentId));

        return new ApiResponse<ContinentDto>
        {
            StatusCode = 200,
            Message = "Data retrieved successfully",
            Data = data
        };
    }
}
using ETrade.Application.Wrappers.Abstract;
using ETrade.Application.Wrappers.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETrade.Application.Features.Addresses.Queries.GetStreetListQuery;

public class GetStreetListQueryResponse
{
    public DataResult<List<SelectListItem>> Result { get; set; }
}
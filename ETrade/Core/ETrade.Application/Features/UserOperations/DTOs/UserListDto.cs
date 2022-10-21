using ETrade.Application.Features.UserOperations.DTOs;

public class UserListDto
{
    public string Draw { get; set; }
    public int RecordsFiltered { get; set; }
    public int RecordsTotal { get; set; }
    public List<UserSummaryDto> UserSummaryDtos { get; set; }
    public bool IsSuccess { get; set; }
}
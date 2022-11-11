namespace ETrade.Application.Repositories;

public interface IUnitOfWork:IAsyncDisposable
{
    IAddressRepository AddressRepository { get; }
    IUserImageRepository UserImageRepository { get; }
    IMenuRepository MenuRepository { get; }
    IActionRepository ActionRepository { get; }
    IRequestInfoLogRepository RequestInfoLogRepository { get; }
    Task<int> SaveAsync();
}
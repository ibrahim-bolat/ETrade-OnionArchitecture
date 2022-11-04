
using ETrade.Application.Repositories;
using ETrade.Persistence.Contexts;

namespace ETrade.Persistence.Repositories;

public class UnitOfWork:IUnitOfWork
{
    private readonly DataContext _dataContext;
    public IAddressRepository AddressRepository { get; }
    public IUserImageRepository UserImageRepository { get; }
    public IMenuRepository MenuRepository { get; }
    public IActionRepository ActionRepository { get; }

    public UnitOfWork(DataContext dataContext, IAddressRepository addressRepository, IUserImageRepository userImageRepository, IMenuRepository menuRepository, IActionRepository actionRepository)
    {
        _dataContext = dataContext;
        AddressRepository = addressRepository;
        UserImageRepository = userImageRepository;
        MenuRepository = menuRepository;
        ActionRepository = actionRepository;
    }

    public async ValueTask DisposeAsync()
    {
        await _dataContext.DisposeAsync();
    }
    
    public async Task<int> SaveAsync()
    {
        return await _dataContext.SaveChangesAsync();
    }
}
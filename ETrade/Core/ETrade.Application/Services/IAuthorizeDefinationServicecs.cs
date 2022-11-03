using ETrade.Application.Model;

namespace ETrade.Application.Services;

public interface IAuthorizeDefinationService
{
    List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
}
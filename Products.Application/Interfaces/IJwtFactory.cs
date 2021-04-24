using Products.Domain;

namespace Products.Application.Interfaces
{
    public interface IJwtFactory
    {
        string Create(User user);
    }
}

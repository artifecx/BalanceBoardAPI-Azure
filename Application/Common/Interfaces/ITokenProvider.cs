using Domain.Entities;

namespace Application.Interfaces;

public interface ITokenProvider
{
    string Create(User user);
}

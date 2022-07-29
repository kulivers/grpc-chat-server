using System.Security.Principal;

namespace MockDataProviders;

public class UsersProvider
{
    public IEnumerable<User> GetUsers()
    {
        yield return new User("egor", "111");
        yield return new User("admin", "111");
        yield return new User("Egor", "111");
        yield return new User("Edmin", "111");
    }
}
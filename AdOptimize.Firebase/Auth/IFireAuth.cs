using System.Threading.Tasks;

public interface IAuthService
{
    Task<string> RegisterUser(string email, string password);
}

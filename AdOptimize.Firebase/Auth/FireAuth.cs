using FirebaseAdmin.Auth;
using System.Threading.Tasks;

public class AuthService : IAuthService
{
    public async Task<string> RegisterUser(string email, string password)
    {
        var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs()
        {
            Email = email,
            Password = password,
        });

        return userRecord.Uid;
    }
}

using System.Threading.Tasks;
using CostumeCraze.Auth.Models;

namespace CostumeCraze.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}
using System.Threading.Tasks;

namespace LightningPT.Core.User
{
    public interface IUserKeyChecker
    {
        Task<bool> PassKeyIsValidAsync(string passKey);
    }
}
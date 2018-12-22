using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using LightningPT.Core.Entities;

namespace LightningPT.Core.User
{
    public class UserKeyChecker : IUserKeyChecker,ITransientDependency
    {
        private readonly IRepository<PtUser,long> _userRepository;

        public UserKeyChecker(IRepository<PtUser, long> userRepository)
        {
            _userRepository = userRepository;
        }

        [UnitOfWork(isTransactional: false)]
        public Task<bool> PassKeyIsValidAsync(string passKey)
        {
            return Task.FromResult(_userRepository.GetAll().Any(z=>z.PassKey == passKey));
        }
    }
}
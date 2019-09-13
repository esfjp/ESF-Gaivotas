using ESFGaivotas.Model.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESFGaivotas.Model.Repository.Database
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        Task<UserModel> GetUserByLogin(string login, string password);
        void CreateUser(UserModel user);
    }

    public class UserRepository : MockBaseRepository<UserModel>, IUserRepository
    {
        public UserRepository(ESFContext context)
            : base(context) { }

        public ESFContext ESFContext
        {
            get { return Context as ESFContext; }
        }

        #region Implementation

        public Task<UserModel> GetUserByLogin(string login, string password)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class MockUserRepository : MockBaseRepository<UserModel>, IUserRepository
    {
        public MockUserRepository(ESFContext context)
            : base(context) { }

        public ESFContext ESFContext
        {
            get { return Context as ESFContext; }
        }

        #region Implementation

        public async Task<UserModel> GetUserByLogin(string login, string password)
        {
            return await ESFContext.User
                .FirstOrDefaultAsync(l => l.Login == login && l.PasswordHash == password);
        }

        public void CreateUser(UserModel user)
        {
            try
            {
                ESFContext.User.Add(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}

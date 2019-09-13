using ESFGaivotas.Model.Core;
using ESFGaivotas.Model.Repository;
using ESFGaivotas.Model.Repository.Database;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ESFGaivotas.Services
{
    public interface IUserService
    {
        UserModel User { get; set; }
        Task<bool> LoginAttempt(string login, string password);
        Task<List<ReportModel>> GetUserReports();
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        #region Implementation

        public UserModel User { get; set; }

        public async Task<bool> LoginAttempt(string login, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ReportModel>> GetUserReports()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class MockUserService : IUserService
    {
        public MockUserService() { }

        #region Implementation

        public UserModel User { get; set; }

        public async Task<bool> LoginAttempt(string login, string password)
        {
            await Task.Delay(3000);

            using (var unitOfWork = new UnitOfWork(new ESFContext()))
            {
                var user = await unitOfWork.User.GetUserByLogin(login, password);

                if (user != null)
                {
                    User = new UserModel()
                    {
                        Id = user.Id,
                        Login = login,
                        PasswordHash = password,
                        ProfilePicture = "Logo.png",
                        FirstName = "Sem Fronteiras",
                    };

                    return true;
                }
                else
                    return false;
            }

        }

        public async Task<List<ReportModel>> GetUserReports()
        {
            await Task.Delay(3000);

            using (var unitOfWork = new UnitOfWork(new ESFContext()))
                return await unitOfWork.Reports.GetReportsByUser(User);
        }

        #endregion
    }
}

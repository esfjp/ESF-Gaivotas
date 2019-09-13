using ESFGaivotas.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESFGaivotas.Model.Repository
{
    public class UserModel
    {
        // User data
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public ERole Role { get; set; }
        public string ProfilePicture { get; set; }

        // Reports data
        public ICollection<ReportModel> Reports { get; set; }
    }
}

using _7_Refactoring.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _7_Refactoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Database _database = new Database();
        private readonly MessageBus _messageBus = new MessageBus();

        public void ChangeEmail(int userId, string newEmail)
        {
            object[] data = _database.GetUserById(userId);
            var user = UserFactory.Create(data);

            object[] companyData = _database.GetCompany();
            var company = CompanyFactory.Create(companyData);

            user.ChangeEmail(newEmail, company);

            _database.SaveCompany(company);
            _database.SaveUser(user);
            _messageBus.SendEmailChangedMessage(userId, newEmail);
        }
    }
}

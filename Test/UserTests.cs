using _7_Refactoring.Domain;
using System;
using Xunit;

namespace Test
{
    public class UserTests
    {
        [Fact]
        public void Changing_email_from_non_corporate_to_corporate()
        {
            var company = new Company("mycorp.com", 1);
            var sut = new User(1, "user@gmail.com", UserType.Customer, false);

            sut.ChangeEmail("new@mycorp.com", company);

            Assert.Equal(2, company.NumberOfEmployees);
            Assert.Equal("new@mycorp.com", sut.Email);
            Assert.Equal(UserType.Employee, sut.Type);
        }

        [InlineData("new@gmail.com", 0, UserType.Customer)]
        [InlineData("newuser@mycorp.com", 1, UserType.Employee)]
        [InlineData("user@mycorp.com", 1, UserType.Employee)]
        [Theory]
        public void Change_employee_email(string newEmail, int expectedNumOfEmployees, UserType expectedUserType)
        {
            var company = new Company("mycorp.com", 1);
            var sut = new User(1, "user@mycorp.com", UserType.Employee, false);

            sut.ChangeEmail(newEmail, company);

            Assert.Equal(expectedNumOfEmployees, company.NumberOfEmployees);
            Assert.Equal(newEmail, sut.Email);
            Assert.Equal(expectedUserType, sut.Type);
        }

        [Fact]
        public void Changing_email_in_empty_company()
        {
            var company = new Company("mycorp.com", 0);
            var sut = new User(1, "user@mycorp.com", UserType.Employee, false);

            Assert.Throws<ApplicationException>(() => sut.ChangeEmail("new@gmail.com", company));
        }

        [Fact]
        public void Changing_confirmed_email()
        {
            var company = new Company("mycorp.com", 1);
            var sut = new User(1, "user@mycorp.com", UserType.Employee, true);

            Assert.Throws<ApplicationException>(() => sut.ChangeEmail("new@gmail.com", company));
        }
    }
}

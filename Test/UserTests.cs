using _7_Refactoring.Domain;
using System;
using Xunit;
using FluentAssertions;

namespace Test
{
    public class UserTests
    {
        [Fact]
        public void Changing_email_from_non_corporate_to_corporate()
        {
            var company = new Company("mycorp.com", 1);
            var sut = new User(1, "user@gmail.com", UserType.Customer, false);

            const string newEmail = "new@mycorp.com";
            sut.ChangeEmail(newEmail, company);

            company.NumberOfEmployees.Should().Be(2);
            sut.Email.Should().Be(newEmail);
            sut.Type.Should().Be(UserType.Employee);
            sut.EmailChangedEvents.Should().Equal(new EmailChangedEvent(1, newEmail));
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

using _7_Refactoring.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _7_Refactoring.Domain
{
    public class Company
    {
        public string DomainName { get; private set; }
        public int NumberOfEmployees { get; private set; }

        public Company(string domainName, int numberOfEmployees)
        {
            DomainName = domainName;
            NumberOfEmployees = numberOfEmployees;
        }

        public bool IsEmailCorporate(string email)
        {
            string emailDomain = email.Split('@')[1];
            return emailDomain == DomainName;
        }

        public void ChangeNumberOfEmployees(int delta)
        {
            Precondition.Requires(NumberOfEmployees + delta >= 0);
            NumberOfEmployees += delta;
        }
    }
}

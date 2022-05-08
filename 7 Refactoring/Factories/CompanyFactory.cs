using _7_Refactoring.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _7_Refactoring.Factories
{
    public class CompanyFactory
    {
        public static Company Create(object[] data)
        {
            Precondition.Requires(data.Length >= 2);

            string domainName = (string)data[0];
            int numberOfEmployees = (int)data[1];

            return new Company(domainName, numberOfEmployees);
        }
    }
}

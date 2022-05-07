﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _7_Refactoring
{
    public enum UserType
    {
        Customer = 1,
        Employee = 2
    }

    public class User
    {
        private Database Database;
        private MessageBus MessageBus;

        public int UserId { get; private set; }
        public string Email { get; private set; }
        public UserType Type { get; private set; }

        public void ChangeEmail(int userId, string newEmail)
        {
            object[] data = Database.GetUserById(userId);
            UserId = userId;
            Email = (string)data[1];
            Type = (UserType)data[2];

            if (Email == newEmail)
                return;

            object[] companyData = Database.GetCompany();
            string companyDomainName = (string)companyData[0];
            int numberOfEmployees = (int)companyData[1];
            string emailDomain = newEmail.Split('@')[1];
            bool isEmailCorporate = emailDomain == companyDomainName;
            UserType newType = isEmailCorporate
                ? UserType.Employee
                : UserType.Customer;
            
            if (Type != newType)
            {
                int delta = newType == UserType.Employee ? 1 : -1;
                int newNumber = numberOfEmployees + delta;
                Database.SaveCompany(newNumber);
            }

            Email = newEmail;
            Type = newType;
            Database.SaveUser(this);
            MessageBus.SendEmailChangedMessage(UserId, newEmail);
        }
    }
}
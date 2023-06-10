using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _7_Refactoring.Domain
{
    public class EmailChangedEvent
    {
        public int UserId { get; }
        public string NewEmail { get; }

        public EmailChangedEvent(int userId, string newEmail)
        {
            UserId = userId;
            NewEmail = newEmail;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            EmailChangedEvent temp = (EmailChangedEvent)obj;
            if (temp.UserId == this.UserId && temp.NewEmail == this.NewEmail)
                return true;
            else
                return false;
        }

        //public override int GetHashCode()
        //{
        //    throw new NotImplementedException();
        //}
    }
}

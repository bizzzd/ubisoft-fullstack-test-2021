using System;
using System.Collections.Generic;

namespace AcmeGames.Domain
{
    public class User
    {
        public string UserAccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
 
        public DateTime DateOfBirth { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public virtual ICollection<Ownership> Ownership { get; set; } = new List<Ownership>();
    }
}

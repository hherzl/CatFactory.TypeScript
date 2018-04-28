using System;

namespace CatFactory.TypeScript.Tests
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName
            => FirstName + (string.IsNullOrEmpty(MiddleName) ? string.Empty : " " + MiddleName) + LastName;

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }
    }
}

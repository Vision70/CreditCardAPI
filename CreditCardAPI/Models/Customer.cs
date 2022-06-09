﻿namespace CreditCardAPI.Models
{
    public class Customer : BaseModel
    {
        public Customer()
        {
        }

        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }
    }
}

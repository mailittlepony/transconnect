using System;

namespace Maili
{
    public class Driver : Employee
    {
        protected bool Availability {get; set;}
        public int Order_nb {get; set;}


        public Driver(string firstName, string lastName, DateTime birth, string address, string email, string phone, int NSS, DateTime HiringDate, float Salary, bool Availability, int Order_nb) 
        : base(firstName, lastName, birth, address, email, phone, NSS, HiringDate, "Chauffeur", Salary)
        {
            this.Availability = Availability;
            this.Order_nb = Order_nb;
        }
    }
}
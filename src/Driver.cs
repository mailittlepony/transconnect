using System;

namespace Maili
{
    public class Driver : Employee
    {
        public float HonoraryByKm { get; set; }
        private bool availability;
        public bool Availability 
        { 
            get 
            { 
                if (availability) return true;
                else 
                {
                    availability = DateTime.Now.Subtract(OrderTaken).TotalHours >= 24;
                    return availability;
                }
            } 
            set { availability = value; }
        }
        public DateTime OrderTaken { get; set; } = DateTime.Now;
        public int Order_nb {get; set;}


        public Driver(string firstName, string lastName, DateTime birth, string address, string email, string phone, int NSS, DateTime HiringDate, float Salary, bool Availability, float honoraryByKm, int Order_nb = 0) 
        : base(firstName, lastName, birth, address, email, phone, NSS, HiringDate, "Chauffeur", Salary)
        {
            HonoraryByKm = honoraryByKm;
            this.Availability = Availability;
            this.Order_nb = Order_nb;
        }
    }
}
using System;

namespace Maili
{
    public class Client : Person
    {
        private float purchaseAmount = 0;

        public float PurchaseAmount
        {
            get { return purchaseAmount; }
            set { purchaseAmount = value; }
        }
        public Client() : base()
        {

        }

        public Client(Client copy)
        {
            firstName = copy.firstName;
            lastName = copy.lastName;
            birth = copy.birth;
            address = copy.address;
            email = copy.email;
            phone = copy.phone;
        }

        public void Copy(Client copy)
        {
            firstName = copy.firstName;
            lastName = copy.lastName;
            birth = copy.birth;
            address = copy.address;
            email = copy.email;
            phone = copy.phone;
        }

        public Client(string firstName, string lastName, DateTime birth, string address, string email, string phone, float purchaseAmount) 
        : base(firstName, lastName, birth, address, email, phone)
        {
            this.purchaseAmount = purchaseAmount;
        }
    }
}
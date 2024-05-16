
namespace Maili
{
    public class Person
    {
        protected string lastName = "";
        protected string firstName = "";
        protected DateTime birth = DateTime.Now;
        protected string address = "";
        protected string email = "";
        protected string phone = "";

        public string FirstName
        {
            get { return firstName; }   
            set { firstName = value; } 
        }

        public string LastName
        {
            get { return lastName; }   
            set { lastName = value; } 
        }

        public string Address
        {
            get { return address; }   
            set { address = value; } 
        }

        public string Email
        {
            get { return email; }   
            set { email = value; } 
        }

        public string Phone
        {
            get { return phone; }   
            set { phone = value; } 
        }

        public DateTime Birth
        {
            get { return birth; }
            set { birth = value; }
        }

        public Person()
        {

        }

        public Person(string firstName, string lastName, DateTime birth, string address, string email, string phone)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.birth = birth;
            this.address = address;
            this.email = email;
            this.phone = phone;
        }

        public override string ToString()
        {
            return firstName + " " + lastName + " | " + birth.Date + " | " + address + " | " + email + " | " + phone;
        }
    }
}

namespace Maili 
{
    public class Employee : Person
    {
        public int NSS { get; set; } = 0;
        public DateTime HiringDate { get; set; } = DateTime.Now;
        public string Job { get; set; } = "";
        public float salary { get; set;} = 0;

        public List<Employee> SubEmployees { get; set; } 
        public int Id { get; }

        private static int count = 0;
        public Employee(Employee copy)
        {
            NSS = copy.NSS;
            HiringDate = copy.HiringDate;
            firstName = copy.firstName;
            lastName = copy.lastName;
            birth = copy.birth;
            address = copy.address;
            email = copy.email;
            phone = copy.phone;
            Job = copy.Job;
            salary = copy.salary;
            SubEmployees = copy.SubEmployees;
        }

        public Employee(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            SubEmployees = new List<Employee>();
        }

        public void Copy(Employee copy)
        {
            NSS = copy.NSS;
            HiringDate = copy.HiringDate;
            firstName = copy.firstName;
            lastName = copy.lastName;
            birth = copy.birth;
            address = copy.address;
            email = copy.email;
            phone = copy.phone;
            Job = copy.Job;
            salary = copy.salary;
            SubEmployees = copy.SubEmployees;
        }

        public Employee(string firstName, string lastName, DateTime birth, string address, string email, string phone, int NSS, DateTime HiringDate, string Job, float Salary) 
        : base(firstName, lastName, birth, address, email, phone)
        {
            this.NSS = NSS;
            this.HiringDate = HiringDate;
            this.Job = Job;
            salary = Salary;
            Id = count;
            count++;
            SubEmployees = new List<Employee>();
        }
 
        public static string GetOrganigramTree(Employee employee)
        { 
            return GetOrganigramBranch(employee, true, 0);
        }

        private static string GetOrganigramBranch(Employee employee, bool isLast, int padding)
        {
            int spacing = 8;
            string str = employee.FirstName + " " + employee.LastName + "\n";

            foreach (Employee sub in employee.SubEmployees)
            {
                for (int j = 0; j < 2; ++j)
                {
                for (int i = 0; i < padding; i += spacing)
                {
                    if (isLast || (i == padding - spacing && i != 0))
                    str += String.Concat(Enumerable.Repeat(" ", spacing));
                    else
                    str += "\u2502" + String.Concat(Enumerable.Repeat(" ", spacing - 1));
                }
                if (j == 0) str += "\u2502\n";
                }

                if (sub.Id == employee.SubEmployees.Last().Id) str += "\u2514";
                else str += "\u251C";

                for (int i = 0; i < spacing - 1; i++) str += "\u2500";
                str += " " + GetOrganigramBranch(sub, sub.Id == employee.SubEmployees.Last().Id, padding + spacing);
            }

            return str;
        }

        public override string ToString()
        {
            return NSS + " | " + HiringDate + " | " + base.ToString() + " | " + Job + " | " + salary;
        }
    }
}

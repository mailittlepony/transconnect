
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

        // This constructor used by pair with the function Copy a bit below is used in the panel EditEmployee in order to clone an Employee
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
            // Return the organizational tree starting from the given employee
            return GetOrganigramBranch(employee, true, 0);
        }

        private static string GetOrganigramBranch(Employee employee, bool isLast, int padding)
        {
            int spacing = 8; // Define the spacing between nodes
            string str = employee.FirstName + " " + employee.LastName + " (" + employee.Job + ")" + "\n"; // Start building the branch with employee information

            // Traverse through subordinates of the employee
            foreach (Employee sub in employee.SubEmployees)
            {
                // Add appropriate spacing and lines to represent the hierarchy
                for (int j = 0; j < 2; ++j)
                {
                    for (int i = 0; i < padding; i += spacing)
                    {
                        if (isLast && i == padding - spacing && i != 0)
                            str += String.Concat(Enumerable.Repeat(" ", spacing)); // Add spaces for last node in the branch
                        else
                            str += "\u2502" + String.Concat(Enumerable.Repeat(" ", spacing - 1)); // Add vertical line with spacing
                    }
                    if (j == 0) str += "\u2502\n"; // Add vertical line at the end of each spacing iteration
                }

                // Add appropriate line character based on position in the hierarchy
                if (sub.Id == employee.SubEmployees.Last().Id) str += "\u2514"; // Last node in the branch
                else str += "\u251C"; // Intermediate node

                // Add horizontal line and recursively call for subordinates
                for (int i = 0; i < spacing - 1; i++) str += "\u2500"; // Add horizontal line
                str += " " + GetOrganigramBranch(sub, sub.Id == employee.SubEmployees.Last().Id, padding + spacing); // Recursively call for subordinates
            }

            return str; // Return the constructed branch
        }

        public override string ToString()
        {
            // Return employee information as a string
            return NSS + " | " + HiringDate + " | " + base.ToString() + " | " + Job + " | " + salary;
        }
    }
}


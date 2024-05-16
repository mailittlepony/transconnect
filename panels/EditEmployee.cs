
namespace Maili
{
    namespace Panels
    {
        public class EditEmployee : Panel, IActionListener
        {
            private Employee temp_employee;
            private Employee edited_employee;
            private Employee? superior = new Employee("","");
            private bool isNew;

            public EditEmployee(Employee? employee = null) : base(employee == null ? "Ajouter un nouveau salarié" : "Modifier un salarié")
            {
                if (employee != null) 
                {
                    temp_employee = new Employee(employee);
                    edited_employee = employee;
                    isNew = false;
                }
                else
                {
                    temp_employee = new Employee("", "");
                    edited_employee = new Employee("","");
                    isNew = true;
                }

                if (!isNew)
                {
                    Add(new Button("Supprimer le salarié", this, "Supprimer"));
                    Add(new Label(""));
                }
                Add(new TextInput("NSS : ", edited_employee.NSS.ToString(), this, "NSS"));
                Add(new TextInput("Date d'entrée : ", edited_employee.HiringDate.ToString(), this, "HD"));
                Add(new TextInput("Prénom : ", edited_employee.FirstName, this, "Prénom"));
                Add(new TextInput("Nom : ", edited_employee.LastName, this, "Nom"));
                Add(new TextInput("Supérieur (format : NOM prénom): ", "", this, "Sup"));
                if (!isNew) Add(new TextInput("Date de naissance : ", edited_employee.Birth.ToString(), this, "Date"));
                Add(new TextInput("Adresse (N° voie, nom de rue, ville, CP) : ", edited_employee.Address, this, "Adresse"));
                Add(new TextInput("Email : ", edited_employee.Email, this, "Email"));
                Add(new TextInput("Numéro de téléphone : ", edited_employee.Phone, this, "Numéro"));
                Add(new TextInput("Emploi : ", edited_employee.Job, this, "Job"));
                Add(new TextInput("Salaire : ", edited_employee.salary.ToString(), this, "Salary"));
                Add(new Label(""));
                Add(new Button("Annuler", this));
                Add(new Button("OK", this));
            }

            public void ActionPerformed(Button button, int key)
            {
                if (button.Id == "NSS")
                {
                    temp_employee.NSS = int.Parse(((TextInput)button).Output);
                }
                else if (button.Id == "HD")
                {
                    temp_employee.HiringDate = DateTime.Parse(((TextInput)button).Output);
                }
                else if (button.Id == "Prénom")
                {
                    temp_employee.FirstName = ((TextInput)button).Output;
                }
                else if (button.Id == "Nom")
                {
                    temp_employee.LastName = ((TextInput)button).Output;
                }
                else if (button.Id == "Sup")
                {
                    string[] sub = ((TextInput)button).Output.Split(' ');
                    if (sub.Length == 2) superior = TransConnect.employees.Find(x => x.LastName == sub[1] && x.FirstName == sub[0]);
                }
                else if (button.Id == "Date")
                {
                    temp_employee.Birth = DateTime.Parse(((TextInput)button).Output);
                }
                else if (button.Id == "Adresse")
                {
                    temp_employee.Address = ((TextInput)button).Output;
                }
                else if (button.Id == "Email")
                {
                    temp_employee.Email = ((TextInput)button).Output;
                }
                else if (button.Id == "Numéro")
                {
                    temp_employee.Phone = ((TextInput)button).Output;
                }
                else if (button.Id == "Job")
                {
                    temp_employee.Job = ((TextInput)button).Output;
                }
                else if (button.Id == "Salary")
                {
                    temp_employee.salary = float.Parse(((TextInput)button).Output);
                }
                else if (button.Id == "Annuler")
                {
                    Employees employeePanel = new Employees(TransConnect.employees);
                    Panel.Display(employeePanel);
                }
                else if (button.Id == "OK")
                {
                    if (isNew)
                    {
                        TransConnect.employees.Add(temp_employee);
                        if ( superior != null) superior.SubEmployees.Add(temp_employee);
                    }
                    else
                    {
                        edited_employee.Copy(temp_employee);
                    }
                    Employees employeePanel = new Employees(TransConnect.employees);
                    Panel.Display(employeePanel);
                }
                else if (button.Id == "Supprimer")
                {
                    TransConnect.employees.Remove(edited_employee);
                    Employees employeePanel = new Employees(TransConnect.employees);
                    Panel.Display(employeePanel);
                }
            }
        }
 
    }
}
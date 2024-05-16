namespace Maili
{
    namespace Panels
    {
        public class Employees : Panel, IActionListener
        {
            private List<Employee> employees {get; set;}
            public Employees(List<Employee> employees) : base()
            {
                this.employees = employees;
            }

            public override void Update()
            {
                components.Clear();

                Add(new Button("Retour", this, "retourHome"));
                Add(new Button("Ajouter un nouveau salarié", this, "Ajouter"));
                Add(new Button("Imprimer l'Arbre", this, "Print"));
                Add(new Label(""));
                Add(new Label("Liste des salariés enregistrés :"));
                Add(new Label(""));
                
                for (int i = 0; i < employees.Count; i++)
                {
                    Employee employee = employees[i];
                    Add(new Button(employee.ToString(), this, "Edit employee:" + i.ToString()));
                }

                if (employees.Count == 0)
                {
                    Add(new Label("Aucun salarié trouvé"));
                }

                Display();

                base.Update();
            }
        
            public void ActionPerformed(Button button, int key)
            {
                if (button.Id == "retourHome")
                {
                    Home HomePanel = new Home();
                    Panel.Display(HomePanel);
                }
                else if (button.Id == "Ajouter")
                {
                    EditEmployee addEmployee_panel = new EditEmployee();
                    Panel.Display(addEmployee_panel);
                }
                else if (button.Id.Split(':')[0] == "Edit employee")
                {
                    Employee employee = employees[int.Parse(button.Id.Split(':')[1])];
                    EditEmployee editemployee_panel = new EditEmployee(employee);
                    Panel.Display(editemployee_panel);
                }
                else if (button.Id == "Print")
                {
                    Panel Arbre = new Panel();
                    Arbre.Add(new Button("Retour",this, "retourEmployee"));
                    Arbre.Add(new Label(Employee.GetOrganigramTree(TransConnect.chef)));
                    Panel.Display(Arbre);
                }
                else if (button.Id == "retourEmployee")
                {
                    Employees EmployeePanel = new Employees(TransConnect.employees);
                    Panel.Display(EmployeePanel);
                }
            }
        }
    }
}
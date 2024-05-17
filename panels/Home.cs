using System.Buffers;
using Maili;
using Maili.Panels;

public class Home : Panel, IActionListener
{
    public Home() : base("Home")
    {
        Add(new Button("Clients", this));
        Add(new Button("Salariés", this));
        Add(new Button("Statistiques", this));
        Add(new Button("Commandes", this));
        Add(new Button("Autre", this));
    }

    public void ActionPerformed(Button button, int key)
    {
        if (button.Id == "Clients")
        {
            Clients clients_panel = new Clients(TransConnect.clients);
            Panel.Display(clients_panel);
        }
        else if (button.Id == "Salariés")
        {
            Employees employees_panel = new Employees(TransConnect.employees);
            Panel.Display(employees_panel);
        }
        else if (button.Id == "Statistiques")
        {
            Statistics statistics_panel = new Statistics();
            Panel.Display(statistics_panel);
        }
        else if (button.Id == "Commandes")
        {
            Orders orders_panel = new Orders(TransConnect.orders);
            Panel.Display(orders_panel);
        }
        else if (button.Id == "Autre")
        {
            Panel PasLeTemps = new Panel("Je n'ai pas eu le temps de finir");
            Panel.Display(PasLeTemps);
        }
    }
}
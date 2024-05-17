namespace Maili
{
    namespace Panels
    {
        public class Statistics : Panel, IActionListener
        {
            public Statistics() : base("Statistiques")
            {
                Add(new Button("Retour", this));
                Add(new Label("Moyenne des prix des commandes : " + Order.MeanValue(TransConnect.orders).ToString()));
                Add(new Button("Nombre de livraison effectués par chauffeurs", this, "Delivery"));
                Add(new Button("Commandes par période de temps", this, "Order"));
                Add(new Button("Moyenne des comptes clients", this, "Mean value Clients"));
                Add(new Button("Liste des commandes par client", this, "List Orders"));
            }

            public void ActionPerformed(Button button, int key)
            {
                if (button.Id == "Delivery")
                {
                    List<Driver> drivers = new List<Driver>();
                    foreach (Employee employeee in TransConnect.employees.FindAll(c => c.Job == "Chauffeur")) drivers.Add((Driver)employeee);
                    DeliveryPerDriver Delivery_per_driver = new DeliveryPerDriver(drivers);
                    Panel.Display(Delivery_per_driver);
                }
                else if (button.Id == "Order")
                {
                    
                }
                else if (button.Id == "Mean value Clients")
                {
                    Panel PasCompris = new Panel("Je n'ai pas compris la question");
                    Add(new Button("Retour", new ActionListener((button, key) => 
                    { 
                        Statistics statPanel = new Statistics();
                        Panel.Display(statPanel);
                    })));
                    Panel.Display(PasCompris);
                }
                else if (button.Id == "List Order")
                {
                    
                }
                else if (button.Id == "Retour")
                {
                    Home homePanel = new Home();
                    Panel.Display(homePanel);
                }
            }
        }
    }
}
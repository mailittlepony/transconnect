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
                    Panel orderByMonthPanel = new Panel("Liste des commandes par mois");
                    string[] months = { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", 
                    "Juillet", "Aout", "Septembre", "Octobre", "Novembre", "Décembre" };

                    orderByMonthPanel.Add(new Button("Retour", new ActionListener((button, key) => 
                    { 
                        Statistics statPanel = new Statistics();
                        Panel.Display(statPanel);
                    })));

                    foreach (string month in months)
                    {
                        orderByMonthPanel.Add(new Label(month + " : "));
                        List<Order> orders = TransConnect.orders.FindAll(o => months[o.Date.Month - 1] == month);
                        foreach (Order order in orders)
                        {
                            orderByMonthPanel.Add(new Label(order.ToString()));
                        }
                        orderByMonthPanel.Add(new Label(""));
                    }

                    Panel.Display(orderByMonthPanel);
                }
                else if (button.Id == "Mean value Clients")
                {
                    Panel PasCompris = new Panel("Je n'ai pas compris la question");
                    PasCompris.Add(new Button("Retour", new ActionListener((button, key) => 
                    { 
                        Statistics statPanel = new Statistics();
                        Panel.Display(statPanel);
                    })));
                    Panel.Display(PasCompris);
                }
                else if (button.Id == "List Orders")
                {
                    Panel listOrderPanel = new Panel("Liste des commandes par clients");

                    listOrderPanel.Add(new Button("Retour", new ActionListener((button, key) => 
                    { 
                        Statistics statPanel = new Statistics();
                        Panel.Display(statPanel);
                    })));

                    foreach (Client client in TransConnect.clients)
                    {
                        listOrderPanel.Add(new Label(client.FirstName + " " + client.LastName + " : "));
                        List<Order> orders = TransConnect.orders.FindAll(o => o.Client.FirstName + " " + o.Client.LastName == client.FirstName + " " + client.LastName);
                        foreach (Order order in orders)
                        {
                            listOrderPanel.Add(new Label(order.ToString()));
                        }
                        listOrderPanel.Add(new Label(""));
                    }

                    Panel.Display(listOrderPanel);
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
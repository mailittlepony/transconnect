namespace Maili
{
    namespace Panels
    {
        public class EditOrder : Panel, IActionListener
        {
            Order temp_order;
            Order edited_order;
            private bool isNew;

            public EditOrder(Order? order = null) : base(order == null ? "Créer une nouvelle commande" : "Modifier une commande")
            {
                if (order != null)
                {
                    temp_order = new Order(order);
                    edited_order = order;
                    isNew = false;
                }
                else
                {
                    temp_order = new Order();
                    edited_order = new Order();
                    isNew = true;
                }

                Add(new TextInput("Client (nom prénom) : ", temp_order.Client == null ? "" : temp_order.Client.ToString(), this, "Client"));
                Add(new TextInput("Ville de départ : ", temp_order.Road.Departure, this, "Depart"));
                Add(new TextInput("Ville d'arrivée : ", temp_order.Road.Arrival, this, "Arrival"));
                Add(new TextInput("Chauffeur : ", temp_order.Road.Driver == null ? "" : temp_order.Road.Driver.ToString(), this, "Driver"));
                Add(new TextInput("Véhicule : ", temp_order.Road.Vehicule == null ? "" : temp_order.Road.Vehicule.ToString(), this, "Car"));
                Add(new Label(""));
                Add(new Button("Calculer les frais de transport: ", this, "Calculate fees"));
                Add(new Label("", "Fees"));
                Add(new Label("", "Itinary"));
                Add(new Label(""));
                Add(new Button("Annuler", this));
                Add(new Button("OK", this));
            }

            public void ActionPerformed(Button button, int key)
            {
                if (button.Id == "Client")
                {
                    temp_order.Client = TransConnect.clients.Find(c => { return c.LastName.ToUpper() + " " + c.FirstName.ToUpper() == ((TextInput)button).Output.ToUpper(); });
                    if (temp_order.Client == null) ((TextInput)button).Text = "Client inconnu. Veuillez d'abord le créer.";
                }
                else if (button.Id == "Depart")
                {
                    temp_order.Road.Departure = ((TextInput)button).Output.ToUpper();
                }
                else if (button.Id == "Arrival")
                {
                    temp_order.Road.Arrival = ((TextInput)button).Output.ToUpper();
                }
                else if (button.Id == "Driver")
                {
                    temp_order.Road.Driver = (Driver?)TransConnect.employees.Find(d => d.LastName.ToUpper() + " " + d.FirstName.ToUpper() == ((TextInput)button).Output.ToUpper());
                    

                }
                else if (button.Id == "Calculate fees")
                {

                    int[,] matrix = Road.GetAdjencyMatrix(Road.GetRoadsFromCSV("res/distances.csv"), r => r.Distance);
                    int src = Road.CityToIntMapping[temp_order.Road.Departure];
                    int dest = Road.CityToIntMapping[temp_order.Road.Arrival];
                    Road road = Dijkstra.GetShortestPath(matrix, src, dest);
                    Label? fees_label = (Label?)components.Find(c => c.Id == "Fees");
                    if (fees_label != null) fees_label.Text = "Shipping fees : ";
                    //temp_order.Price =;
                    //temp_order.Client.PurchaseAmount += temp_order.Price;
                    Label? itinary_label = (Label?)components.Find(c => c.Id == "Itinary");
                    if (itinary_label != null)
                    {
                        itinary_label.Text = "Itinary : ";
                        foreach (int v in road.Vertices)
                        {
                            itinary_label.Text += Road.IntToCityMapping[v] + " -> ";
                        }
                        itinary_label.Text += road.Distance + "km";
                    }
                }
                else if (button.Id == "OK")
                {
                    temp_order.Road.Driver.Order_nb ++; 
                }
                else if (button.Id == "Annuler")
                {
                    Home return_panel = new Home();
                    Panel.Display(return_panel);
                }
            }
        }
    }
}
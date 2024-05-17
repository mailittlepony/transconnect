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
                if (!isNew)
                {
                    Add(new Button("Supprimer la commande", this, "Supprimer"));
                    Add(new Label(""));
                }

                Add(new TextInput("Client (prénom nom) : ", temp_order.Client == null ? "" : temp_order.Client.ToString(), this, "Client"));
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
                    temp_order.Client = TransConnect.clients.Find(c => c.FirstName.ToUpper() + " " + c.LastName.ToUpper() == ((TextInput)button).Output.ToUpper());
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
                    temp_order.Road.Driver = (Driver?)TransConnect.employees.Find(d => d.FirstName.ToUpper() + " " + d.LastName.ToUpper() == ((TextInput)button).Output.ToUpper());
                    if (temp_order.Road.Driver == null) button.Text = ((TextInput)button).Name +  "Le chauffeur n'a pas été trouvé";
                    else if (!temp_order.Road.Driver.Availability) button.Text = ((TextInput)button).Name + "Ce chauffeur n'est pas disponible veuillez en choisir un autre";

                }
                else if (button.Id == "Car")
                {
                    temp_order.Road.Vehicule = TransConnect.vehicules.Find(v => v.Type.ToUpper() == ((TextInput)button).Output.ToUpper());
                    if (temp_order.Road.Vehicule == null) button.Text = ((TextInput)button).Name +  "Le véhicule n'a pas été trouvé";
                }
                else if (button.Id == "Calculate fees")
                {
                    Label? fees_label = (Label?)components.Find(c => c.Id == "Fees");
                    Label? itinary_label = (Label?)components.Find(c => c.Id == "Itinary");

                    int[,] matrix = Road.GetAdjencyMatrix(Road.GetRoadsFromCSV("res/distances.csv"), r => r.Distance);

                    if (!Road.CityToIntMapping.ContainsKey(temp_order.Road.Departure) || !Road.CityToIntMapping.ContainsKey(temp_order.Road.Arrival))
                    {
                        if (fees_label != null) fees_label.Text = "Merci de rentrer une ville valide.";
                        if (itinary_label != null) itinary_label.Text = "";
                        return;
                    }

                    int src = Road.CityToIntMapping[temp_order.Road.Departure];
                    int dest = Road.CityToIntMapping[temp_order.Road.Arrival];

                    Road road = Dijkstra.GetShortestPath(matrix, src, dest);
                    temp_order.Road.Distance = road.Distance;
                    temp_order.Road.Vertices = road.Vertices;
                    temp_order.Price = road.Distance * (temp_order.Road.Vehicule == null ? 0 : temp_order.Road.Vehicule.PricePerKm);
                    if (fees_label != null) fees_label.Text = "Shipping fees : " + temp_order.Price.ToString() + "€";
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
                    if (isNew)
                    {
                        TransConnect.orders.Add(temp_order);
                        temp_order.Road.Driver.Order_nb ++;
                        temp_order.Road.Driver.Availability = false;
                        temp_order.Road.Driver.OrderTaken = DateTime.Now;
                        temp_order.Client.PurchaseAmount += temp_order.Price;
                    }
                    else
                    {
                        edited_order.Copy(temp_order);
                    }
                    Orders orders_panel = new Orders(TransConnect.orders);
                    Panel.Display(orders_panel);
                }
                else if (button.Id == "Annuler")
                {
                    Orders orders_panel = new Orders(TransConnect.orders);
                    Panel.Display(orders_panel);
                }
                else if (button.Id == "Supprimer")
                {
                    TransConnect.orders.Remove(edited_order);
                    Orders orderPanel = new Orders(TransConnect.orders);
                    Panel.Display(orderPanel);
                }
            }
        }
    }
}

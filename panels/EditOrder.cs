namespace Maili
{
    namespace Panels
    {
        // Panel for editing or creating orders
        public class EditOrder : Panel, IActionListener
        {
            // Fields to manage temporary and edited orders
            Order temp_order;
            Order edited_order;
            private bool isNew;

            // Constructor for creating EditOrder panel
            public EditOrder(Order? order = null) : base(order == null ? "Passer une commande" : "Modifier une commande")
            {
                // Initialize temporary and edited orders based on the provided order
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

                // Add components to the panel for user input
                if (!isNew)
                {
                    Add(new Button("Supprimer la commande", this, "Delete")); // Option to delete the order
                    Add(new Label("")); // Blank line for spacing
                }

                // Add text inputs for order details
                Add(new TextInput("Client (prenom nom): ", temp_order.Client == null ? "" : temp_order.Client.ToString(), this, "Client"));
                Add(new TextInput("Ville de départ: ", temp_order.Road.Departure, this, "Depart"));
                Add(new TextInput("Ville d'arrivée: ", temp_order.Road.Arrival, this, "Arrival"));
                Add(new TextInput("Chauffeur: ", temp_order.Road.Driver == null ? "" : temp_order.Road.Driver.ToString(), this, "Driver"));
                Add(new TextInput("Véhicule: ", temp_order.Road.Vehicule == null ? "" : temp_order.Road.Vehicule.ToString(), this, "Car"));
                Add(new Label("")); // Blank line for spacing

                // Add buttons for actions
                Add(new Button("Calculer les frais de port ", this, "Calculate fees"));
                Add(new Label("", "Fees"));
                Add(new Label("", "Itinary"));
                Add(new Label("")); // Blank line for spacing
                Add(new Button("Annuler", this));
                Add(new Button("OK", this));
            }

            // Method to handle button actions
            public void ActionPerformed(Button button, int key)
            {
                // Handle actions based on button id
                if (button.Id == "Client")
                {
                    // Update client based on user input
                    temp_order.Client = TransConnect.clients.Find(c => c.FirstName.ToUpper() + " " + c.LastName.ToUpper() == ((TextInput)button).Output.ToUpper());
                    if (temp_order.Client == null) ((TextInput)button).Text = ((TextInput)button).Name + "client inconnu. Veuillez d'abord le créer.";
                }
                else if (button.Id == "Depart")
                {
                    // Update departure city based on user input
                    temp_order.Road.Departure = ((TextInput)button).Output.ToUpper();
                }
                else if (button.Id == "Arrival")
                {
                    // Update arrival city based on user input
                    temp_order.Road.Arrival = ((TextInput)button).Output.ToUpper();
                }
                else if (button.Id == "Driver")
                {
                    // Update driver information based on user input
                    temp_order.Road.Driver = (Driver?)TransConnect.employees.Find(d => d.FirstName.ToUpper() + " " + d.LastName.ToUpper() == ((TextInput)button).Output.ToUpper());
                    if (temp_order.Road.Driver == null) button.Text = ((TextInput)button).Name + "chauffeur inconnu. Veuillez d'abord le créer.";
                    else if (!temp_order.Road.Driver.Availability) button.Text = ((TextInput)button).Name + "ce chauffeur est indisponible.";
                }
                else if (button.Id == "Car")
                {
                    // Update vehicle information based on user input
                    temp_order.Road.Vehicule = TransConnect.vehicules.Find(v => v.Type.ToUpper() == ((TextInput)button).Output.ToUpper());
                    if (temp_order.Road.Vehicule == null) button.Text = ((TextInput)button).Name + "type de véhicule innexistant";
                }
                else if (button.Id == "Calculate fees")
                {
                    // Calculate shipping fees and display itinerary
                    Label? fees_label = (Label?)components.Find(c => c.Id == "Fees");
                    Label? itinary_label = (Label?)components.Find(c => c.Id == "Itinary");

                    // Get adjacency matrix for road distances
                    int[,] matrix = Road.GetAdjencyMatrix(Road.GetRoadsFromCSV("res/distances.csv"), r => r.Distance);

                    // Check if departure and arrival cities are valid
                    if (!Road.CityToIntMapping.ContainsKey(temp_order.Road.Departure) || !Road.CityToIntMapping.ContainsKey(temp_order.Road.Arrival))
                    {
                        if (fees_label != null) fees_label.Text = "Merci de rentrer des villes valides";
                        if (itinary_label != null) itinary_label.Text = "";
                        return;
                    }

                    // Find shortest path using Dijkstra's algorithm
                    int src = Road.CityToIntMapping[temp_order.Road.Departure];
                    int dest = Road.CityToIntMapping[temp_order.Road.Arrival];
                    Road road = Dijkstra.GetShortestPath(matrix, src, dest);

                    // Update order details with calculated values
                    temp_order.Road.Distance = road.Distance;
                    temp_order.Road.Vertices = road.Vertices;
                    temp_order.Price = road.Distance * (temp_order.Road.Vehicule == null ? 0 : temp_order.Road.Vehicule.PricePerKm +
                        (temp_order.Road.Driver == null ? 0 : temp_order.Road.Driver.HonoraryByKm));

                    // Update UI labels with calculated values
                    if (fees_label != null) fees_label.Text = "Frais de port: " + temp_order.Price.ToString() + "€";
                    if (itinary_label != null)
                    {
                        itinary_label.Text = "Itinéraire: ";
                        foreach (int v in road.Vertices)
                        {
                            itinary_label.Text += Road.IntToCityMapping[v] + " -> ";
                        }
                        itinary_label.Text += road.Distance + "km";
                    }
                }
                else if (button.Id == "OK")
                {
                    // Save or update the order
                    if (isNew)
                    {
                        TransConnect.orders.Add(temp_order);
                        if (temp_order.Road.Driver != null)
                        {
                            temp_order.Road.Driver.Order_nb++;
                            temp_order.Road.Driver.Availability = false;
                            temp_order.Road.Driver.OrderTaken = DateTime.Now;
                        }
                        temp_order.Client.PurchaseAmount += temp_order.Price;
                    }
                    else
                    {
                        edited_order.Copy(temp_order);
                    }

                    // Display orders panel
                    Orders orders_panel = new Orders(TransConnect.orders);
                    Panel.Display(orders_panel);
                }
                else if (button.Id == "Annuler")
                {
                    // Cancel editing and display orders panel
                    Orders orders_panel = new Orders(TransConnect.orders);
                    Panel.Display(orders_panel);
                }
                else if (button.Id == "Supprimer")
                {
                    // Delete the order and display orders panel
                    TransConnect.orders.Remove(edited_order);
                    Orders orderPanel = new Orders(TransConnect.orders);
                    Panel.Display(orderPanel);
                }
            }
        }
    }
}

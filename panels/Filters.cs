
namespace Maili
{
    namespace Panels
    {
        public class Filters : Panel, IActionListener
        {
            private string city = "";
            private bool alphaOrder = false;
            private bool purchaseOrder = false;
            private List<Client> sortedClients = new List<Client>();

            public string City
            {
                get { return city; }
            }

            public bool AlphaOrder
            {
                get { return alphaOrder; }
            }

            public bool PurchaseOrder
            {
                get { return purchaseOrder; }
            }

            public List<Client> SortedClients
            {
                get { return sortedClients; }
            }

            public Filters() : base("Filtres")
            {
                Add(new TextInput("Ville : ", city, this, "City"));
                Add(new Button("Ordre Alphabétique", this, "AlphaOrder"));
                Add(new Button("Ordre Par montant des achats cumulés ", this, "PurchaseOrder"));
            }

            public void ActionPerformed(Button button, int key)
            {
                if (button.Id == "City")
                {
                    city = ((TextInput)button).Output;
                    sortedClients = TransConnect.clients.FindAll(client => {
                        string[] splitedAddress = client.Address.Split(", ");
                        if (splitedAddress.Length < 3) return false;
                        return splitedAddress[2].ToUpper() == city.ToUpper(); 
                    });
                    if (city == "") sortedClients = TransConnect.clients;
                    Clients clientPanel = new Clients(sortedClients);
                    Panel.Display(clientPanel);
                }
                else if (button.Id == "AlphaOrder") 
                {
                    sortedClients = new List<Client>(TransConnect.clients.OrderBy(c => c.LastName + c.FirstName)); 
                    Clients clientPanel = new Clients(sortedClients);
                    Panel.Display(clientPanel);
                }
                else if (button.Id == "PurchaseOrder")
                {
                    sortedClients = new List<Client>(TransConnect.clients.OrderBy(d => d.PurchaseAmount));
                    Clients clientPanel = new Clients(sortedClients);
                    Panel.Display(clientPanel);
                }
            }
        }
    }
}

namespace Maili
{
    namespace Panels
    {
        public class Clients : Panel, IActionListener
        {
            private List<Client> clients;

            public Clients(List<Client> clients) : base("Clients")
            {
                this.clients = clients;
            }

            public override void Update()
            {
                components.Clear();

                Add(new Button("Retour", this));
                Add(new Button("Ajouter un nouveau client", this, "Ajouter"));
                Add(new Button("Filtres", this));
                Add(new Label(""));
                Add(new Label("Liste des clients enregistrés :"));
                Add(new Label(""));
                
                for (int i = 0; i < clients.Count; i++)
                {
                    Client client = clients[i];
                    Add(new Button(client.ToString(), this, "Edit client:" + i.ToString()));
                }

                if (clients.Count == 0)
                {
                    Add(new Label("Aucun client trouvé"));
                }

                Display();

                base.Update();
            }

            public void ActionPerformed(Button button, int key)
            {
                if (button.Id == "Retour")
                {
                    Home homePanel = new Home();
                    Panel.Display(homePanel);
                }
                else if (button.Id == "Ajouter")
                {
                    EditClient addclient_panel = new EditClient();
                    Panel.Display(addclient_panel);
                }
                else if (button.Id.Split(':')[0] == "Edit client")
                {
                    Client client = clients[int.Parse(button.Id.Split(':')[1])];
                    EditClient editclient_panel = new EditClient(client);
                    Panel.Display(editclient_panel);
                }
                else if (button.Id == "Filtres")
                {
                    Filters filtersPanel = new Filters();
                    Panel.Display(filtersPanel);
                }
            }
        }
    }
}

namespace Maili
{
    namespace Panels
    {
        public class EditClient : Panel, IActionListener
        {
            private Client temp_client;
            private Client edited_client;
            private bool isNew;

            public EditClient(Client? client = null) : base(client == null ? "Ajouter un nouveau client" : "Modifier un client")
            {
                if (client != null) 
                {
                    temp_client = new Client(client);
                    edited_client = client;
                    isNew = false;
                }
                else
                {
                    temp_client = new Client();
                    edited_client = new Client();
                    isNew = true;
                }

                if (!isNew)
                {
                    Add(new Button("Supprimer le client", this, "Supprimer"));
                    Add(new Label(""));
                }

                Add(new TextInput("Prénom : ", edited_client.FirstName, this, "Prénom"));
                Add(new TextInput("Nom : ", edited_client.LastName, this, "Nom"));
                if (!isNew) Add(new TextInput("Date de naissance : ", edited_client.Birth.ToString(), this, "Date"));
                Add(new TextInput("Adresse (N° voie, nom de rue, ville, CP) : ", edited_client.Address, this, "Adresse"));
                Add(new TextInput("Email : ", edited_client.Email, this, "Email"));
                Add(new TextInput("Numéro de téléphone : ", edited_client.Phone, this, "Numéro"));
                Add(new Label(""));
                Add(new Button("Annuler", this));
                Add(new Button("OK", this));
            }

            public void ActionPerformed(Button button, int key)
            {
                if (button.Id == "Prénom")
                {
                    temp_client.FirstName = ((TextInput)button).Output;
                }
                else if (button.Id == "Nom")
                {
                    temp_client.LastName = ((TextInput)button).Output;
                }
                else if (button.Id == "Date")
                {
                    temp_client.Birth = DateTime.Parse(((TextInput)button).Output);
                }
                else if (button.Id == "Adresse")
                {
                    temp_client.Address = ((TextInput)button).Output;
                }
                else if (button.Id == "Email")
                {
                    temp_client.Email = ((TextInput)button).Output;
                }
                else if (button.Id == "Numéro")
                {
                    temp_client.Phone = ((TextInput)button).Output;
                }
                else if (button.Id == "Annuler")
                {
                    Clients clientPanel = new Clients(TransConnect.clients);
                    Panel.Display(clientPanel);
                }
                else if (button.Id == "OK")
                {
                    if (isNew)
                    {
                        TransConnect.clients.Add(temp_client);
                    }
                    else
                    {
                        edited_client.Copy(temp_client);
                    }
                    Clients clientPanel = new Clients(TransConnect.clients);
                    Panel.Display(clientPanel);
                }
                else if (button.Id == "Supprimer")
                {
                    TransConnect.clients.Remove(edited_client);
                    Clients clientPanel = new Clients(TransConnect.clients);
                    Panel.Display(clientPanel);
                }
            }
        }
 
    }
}
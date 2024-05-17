
namespace Maili
{
    namespace Panels
    {
        public class Orders : Panel, IActionListener
        {            
            private List<Order> orders;
            public Orders(List<Order> orders) : base("Commandes")
            {
                this.orders = orders;
            }
            public override void Update()
            {
                components.Clear();

                Add(new Button("Retour", this, "home"));
                Add(new Button("Passer une nouvelle commande", this, "Add"));
                Add(new Label(""));
                Add(new Label("Liste des commandes enregistrées :"));
                Add(new Label(""));
                
                for (int i = 0; i < orders.Count; i++)
                {
                    Order order = orders[i];
                    Add(new Button(order.ToString(), this, "Edit order:" + i.ToString()));
                }

                if (orders.Count == 0)
                {
                    Add(new Label("Aucune commande trouvé"));
                }

                Display();

                base.Update();
            }

            public void ActionPerformed(Button button, int key)
            {
                if (button.Id == "Add")
                {
                    EditOrder editOrderPanel = new EditOrder();
                    Panel.Display(editOrderPanel);
                }
                else if (button.Id == "home")
                {
                    Home return_panel = new Home();
                    Panel.Display(return_panel);
                }
                else if (button.Id.Split(':')[0] == "Edit order")
                {
                    Order order = orders[int.Parse(button.Id.Split(':')[1])];
                    EditOrder editOrder_panel = new EditOrder(order);
                    Panel.Display(editOrder_panel);
                }
            }
        }
    }
}
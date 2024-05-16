
namespace Maili
{
    namespace Panels
    {
        public class Orders : Panel, IActionListener
        {            
            protected List<Order> orders { get; set; }
            public Orders() : base("Orders")
            {
                Add(new Button("Passer une nouvelle commande", this, "Add"));
            }
            public Orders(List<Order> orders) : base()
            {
                this.orders = orders;
            }

            public void ActionPerformed(Button button, int key)
            {
                if (button.Id == "Add")
                {
                    EditOrder editOrderPanel = new EditOrder();
                    Panel.Display(editOrderPanel);
                }
            }
        }
    }
}
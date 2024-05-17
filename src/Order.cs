using System;
using Maili.Panels;

namespace Maili
{
    public class Order
    {
        public Client? Client { get; set; }
        public Road Road { get; set; }
        public float Price { get; }
        public DateTime Date { get; set; } = DateTime.Now;

        public Order()
        {
            Client = new Client();
            Road = new Road();
        }

        public Order(Client client, Road road)
        {
            Client = client;
            Road = road;
        }

        public Order(Order order)
        {
            Client = order.Client;
            Road = order.Road;
            Price = order.Price;
            Date = order.Date;
        }

        public static float MeanValue(List<Order> orders)
            {
                float s = 0;
                foreach (Order order in orders)
                {
                    s += order.Price;
                }
                return s/orders.Count();
            }

        //A completer: Une livraison effectuée suppose le paiement du client dans notre application 
        
    }
}

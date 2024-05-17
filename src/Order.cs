using System;
using Maili.Panels;

namespace Maili
{
    public class Order
    {
        public Client Client { get; set; }
        public Road Road { get; set; }
        public float Price { get; set; }
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

        public void Copy(Order copy)
        {
            Client = copy.Client;
            Road = copy.Road;
            Price = copy.Price;
            Date = copy.Date;
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

        public override string ToString()
        {
            return Date + " | " + Client.FirstName + " | " + Client.LastName + " | " + Road.Departure + " | " + Road.Arrival + " | " + Price ;
        }
        
    }
}

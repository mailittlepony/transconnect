namespace Maili
{
    namespace Panels
    {
        public class DeliveryPerDriver : Panel
        {
            private List<Driver>? Drivers {get; set;} 
            public DeliveryPerDriver(List<Driver> drivers) : base("Nombre de livraison effectués par chauffeurs")
            {
                Drivers = drivers;

                Add(new Button("Retour", new ActionListener((button, key) => 
                { 
                    Statistics statPanel = new Statistics();
                    Panel.Display(statPanel);
                })));

                if (Drivers != null)
                {
                    foreach (Driver driver in Drivers) 
                    {
                        Add(new Label(driver.FirstName + driver.LastName + " :" + driver.Order_nb));
                    }
                }
                else
                {
                    Console.WriteLine("Aucun Chauffeur trouvé");
                }
            }
        }
    }
}
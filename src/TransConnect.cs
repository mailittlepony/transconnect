
using System.Text.Json;
using Maili.Panels;

namespace Maili
{
    class TransConnect
    {
        static public string ClientFilePath { get; } = "data/clients.csv";
        public static List<Client> clients = new List<Client>();

        public static List<Vehicule> vehicules = new List<Vehicule>
        {
            new Vehicule("Camion frigorifique", 1.0f),
            new Vehicule("Camion citerne", 0.50f),
            new Vehicule("Camion benne", 0.2f),
            new Vehicule("Voiture", 0.10f),
        };
        
        public static List<Order> orders = new List<Order>(); 
        public static Employee chef = new Employee("Mr","Dupond",DateTime.Now,"","","", 0, DateTime.Now, "Directeur général", 0);
        public static Employee DirCom = new Employee("Mme","Fiesta",DateTime.Now,"","","",0, DateTime.Now, "Directrice commerciale", 0);
        public static Employee DirOp = new Employee("Mr","Fetard",DateTime.Now,"","","",0, DateTime.Now, "Directeur des Opérations", 0);
        public static Employee DirRH = new Employee("Mme","Joyeuse",DateTime.Now,"","","",0, DateTime.Now, "Directrice des RH", 0);
        public static Employee DirFin = new Employee("Mr","GripSous",DateTime.Now,"","","",0, DateTime.Now, "Directeur financier", 0);
        public static Employee Com1 = new Employee("Mr","Forge",DateTime.Now,"","","",0, DateTime.Now, "Commercial", 0);
        public static Employee Com2 = new Employee("Mme","Fermi",DateTime.Now,"","","",0, DateTime.Now, "Commerciale", 0);
        public static Employee ChefEq1 = new Employee("Mr","Royal",DateTime.Now,"","","",0, DateTime.Now, "Chef d'équipe", 0);
        public static Employee ChefEq2 = new Employee("Mme","Prince",DateTime.Now,"","","",0, DateTime.Now, "Chef d'équipe", 0);
        public static Driver Chauff1 = new Driver("Mr","Romu",DateTime.Now,"","","",0, DateTime.Now, 0, true, 0.2f, 0);
        public static Driver Chauff2 = new Driver("Mr","Romi",DateTime.Now,"","","",0, DateTime.Now, 0, true, 0.3f, 0);
        public static Driver Chauff3 = new Driver("Mme","Roma",DateTime.Now,"","","",0, DateTime.Now, 0, true, 0.4f, 4);
        public static Driver Chauff4 = new Driver("Mr","Rome",DateTime.Now,"","","",0, DateTime.Now, 0, false, 0.2f, 8);
        public static Driver Chauff5 = new Driver("Mr","Rimou",DateTime.Now,"","","",0, DateTime.Now, 0, true, 0.5f, 9);
        public static Employee Form = new Employee("Mme","Couleur",DateTime.Now,"","","",0, DateTime.Now, "Formation", 0);
        public static Employee Contrats = new Employee("Mme","ToutLeMonde",DateTime.Now,"","","",0, DateTime.Now, "Contrats", 0);
        public static Employee DirCompt = new Employee("Mr","Picsou",DateTime.Now,"","","",0, DateTime.Now, "Direction comptable", 0);
        public static Employee Gestion = new Employee("Mr","GrosSous",DateTime.Now,"","","",0, DateTime.Now, "Controleur de Gestion", 0);
        public static Employee Compt1 = new Employee("Mr","Fournier",DateTime.Now,"","","",0, DateTime.Now, "Comptable", 0);
        public static Employee Compt2 = new Employee("Mme","Gautier",DateTime.Now,"","","",0, DateTime.Now, "Comptable", 0);

        public static List<Employee> employees = new List<Employee>
        {
            chef,
            DirCom,
            DirOp,
            DirRH,
            DirFin,
            Com1,
            Com2,
            ChefEq1,
            ChefEq2,
            Chauff1,
            Chauff2,
            Chauff3,
            Chauff4,
            Chauff5,
            Form,
            Contrats,
            DirCompt,
            Gestion,
            Compt1,
            Compt2,
        };

        static void Main(string[] args)
        {
            // Load data file
            string clientFileStr = File.ReadAllText(ClientFilePath);
            List<Client>? loadedClients = clientFileStr == "" ? new List<Client>() : JsonSerializer.Deserialize<List<Client>>(clientFileStr);
            clients = loadedClients == null ? new List<Client>() : loadedClients;

            chef.SubEmployees = new List<Employee>
            {
                DirCom,
                DirOp,
                DirRH,
                DirFin,
            };

            DirCom.SubEmployees = new List<Employee>
            {
                Com1,
                Com2,
            };

            DirOp.SubEmployees = new List<Employee>
            {
                ChefEq1,
                ChefEq2,
            };

            ChefEq1.SubEmployees = new List<Employee> 
            {
                Chauff1,
                Chauff2,
                Chauff3,
            };

            ChefEq2.SubEmployees = new List<Employee> 
            {
                Chauff4,
                Chauff5,
            };

            DirRH.SubEmployees = new List<Employee> 
            {
                Form,
                Contrats,
            };

            DirFin.SubEmployees = new List<Employee>
            {
                DirCompt,
                Gestion,
            };

            DirCompt.SubEmployees = new List<Employee> 
            {
                Compt1,
                Compt2,
            };

            Home home_panel = new Home();
            Panel.Display(home_panel);
        }
    };
}
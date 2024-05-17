
using Maili.Panels;

namespace Maili
{
  public class Road
  {
    public string Departure { get; set; }
    public string Arrival { get; set; }
    public int Distance { get; set; }
    public int Time { get; set; }
    public Vehicule? Vehicule { get; set; } = null;
    public Driver? Driver { get; set; } = null;

    public List<int> Vertices { get; set; } = new List<int>();

    // Static dictionaries for mapping between city names and indices
    public static Dictionary<string, int> CityToIntMapping { get; } = new Dictionary<string, int>();
    public static Dictionary<int, string> IntToCityMapping { get; set; } = new Dictionary<int, string>();

    // Default constructor
    public Road()
    {
      Arrival = "";
      Departure = "";
      Distance = 0;
      Time = 0;
    }

    // Constructor to initialize a road with vertices and distance
    public Road(int[] vertices, int distance)
    {
      Distance = distance;
      Vertices = new List<int>(vertices);
      Departure = IntToCityMapping[Vertices[0]]; // Map the first vertex to the departure city
      Arrival = IntToCityMapping[Vertices.Last()]; // Map the last vertex to the arrival city
    }
    // Constructor to initialize a road with specific details
    public Road(string departure, string arrival, int distance, int time, Driver? driver = null, Vehicule? vehicule = null)
    {
      Arrival = arrival;
      Departure = departure;
      Distance = distance;
      Time = time;
      Driver = driver;
      Vehicule = vehicule;
    }

    // Method to read roads from a CSV file
    public static List<Road> GetRoadsFromCSV(string filePath)
    {
      List<Road> roads = new List<Road>(); // Initialize an empty list to store roads

      StreamReader sr = new StreamReader(filePath); // Open the CSV file for reading
      if (sr == null) return new List<Road>(); // If the file cannot be opened, return an empty list

      string line = sr.ReadLine(); // Read the first line from the file
      while (line != null) // Loop until the end of the file
      {
        string[] splitedLine = line.Split(';'); // Split the line by semicolon
        if (splitedLine.Length == 5 && splitedLine[0][0] != '#') // Check for valid data and ignore comments
        {
          int km = 0; // Initialize distance
          int duration = 0; // Initialize duration
          try
          {
            km = int.Parse(splitedLine[2]); // Parse the distance
            duration = int.Parse(splitedLine[3].Split('h')[0]) * 3600 + int.Parse(splitedLine[3].Split('h')[1]) * 60; // Parse the time
          }
          catch
          {
            // Handle any parsing errors
          }

          // Add a new Road object to the list
          roads.Add(new Road(splitedLine[0].ToUpper(), splitedLine[1].ToUpper(), km, duration));
        }

        line = sr.ReadLine(); // Read the next line
      }
      sr.Close(); // Close the StreamReader

      return roads; // Return the list of roads
    }

    // Delegate to determine the weight
    public delegate int GraphWeight(Road road);

    // Method to generate adjacency matrix from roads
    public static int[,] GetAdjencyMatrix(List<Road> roads, GraphWeight graphWeight)
    {
      HashSet<string> cities = new HashSet<string>(); // Create a HashSet to store unique city names
      foreach (Road road in roads) // Iterate over each road in the list
      {
        cities.Add(road.Arrival); // Add the arrival city to the HashSet
        cities.Add(road.Departure); // Add the departure city to the HashSet
      }
      List<string> citiesList = cities.ToList(); // Convert the HashSet to a list

      // Map city names to indices and vice versa
      for (int i = 0; i < citiesList.Count; i++)
      {
        CityToIntMapping[citiesList[i]] = i; // Map city name to index
        IntToCityMapping[i] = citiesList[i]; // Map index to city name
      }

      int[,] matrix = new int[citiesList.Count, citiesList.Count]; // Initialize a 2D array for the adjacency matrix
      foreach (Road road in roads) // Iterate over each road in the list
      {
        // Update the adjacency matrix with weights calculated by the provided graphWeight delegate
        matrix[CityToIntMapping[road.Departure], CityToIntMapping[road.Arrival]] = graphWeight(road);
      }

      return matrix; // Return the adjacency matrix
    }

  }

  // Static class implementing Dijkstra's algorithm
  public static class Dijkstra
  {
    // Method to find the shortest path using Dijkstra's algorithm
    public static Road GetShortestPath(int[,] graph, int src, int dest)
    {
      PriorityQueue<(int, int), int> pq = new PriorityQueue<(int, int), int>(); // Priority queue to store vertices
      int[] distances = Enumerable.Repeat(Int32.MaxValue, graph.GetLength(0)).ToArray(); // Array to store distances from source
      int[] parents = Enumerable.Repeat(-1, graph.GetLength(0)).ToArray(); // Array to store parent nodes

      pq.Enqueue((0, src), 0); // Enqueue the source vertex with distance 0
      distances[src] = 0; // Set the distance of the source vertex to 0

      while (pq.Count > 0) // While the priority queue is not empty
      {
        (int dist_u, int u) = pq.Dequeue(); // Dequeue the vertex with the shortest distance

        if (u == dest) break; // If the destination vertex is reached, exit the loop
        if (dist_u > distances[u]) continue; // Skip if the distance is greater than the current distance

        for (int v = 0; v < graph.GetLength(0); ++v) // Iterate over neighbors of the current vertex
        {
          if (graph[u, v] > 0) // If there is an edge between u and v
          {
            int weight = graph[u, v]; // Get the weight of the edge

            if (distances[v] > distances[u] + weight) // If a shorter path to v is found through u
            {
              distances[v] = distances[u] + weight; // Update the distance to v
              parents[v] = u; // Set u as the parent of v
              pq.Enqueue((distances[v], v), distances[v]); // Enqueue v with its updated distance
            }
          }
        }
      }

      Road path = new Road(); // Initialize a new road object to represent the shortest path
      int current = dest; // Start from the destination vertex
      while (current != src) // Traverse back from destination to source to construct the path
      {
        path.Vertices.Add(current); // Add the current vertex to the path
        current = parents[current]; // Move to the parent of the current vertex
      }
      path.Vertices.Add(src); // Add the source vertex to the path
      path.Vertices.Reverse(); // Reverse the order of vertices to get the correct path
      path.Distance = distances[dest]; // Set the distance of the path

      return path; // Return the shortest path
    }
  }

}


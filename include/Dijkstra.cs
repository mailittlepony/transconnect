
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

    public static Dictionary<string, int> CityToIntMapping { get; } = new Dictionary<string, int>();
    public static Dictionary<int, string> IntToCityMapping { get; set; } = new Dictionary<int, string>();

    public Road()
    {
      Arrival = "";
      Departure = "";
      Distance = 0;
      Time = 0;
    }

    public Road(int[] vertices, int distance)
    {
      Distance = distance;
      Vertices = new List<int>(vertices);
      Departure = IntToCityMapping[Vertices[0]];
      Arrival = IntToCityMapping[Vertices.Last()];
    }

    public Road(string departure, string arrival, int distance, int time, Driver? driver = null, Vehicule? vehicule = null)
    {
      Arrival = arrival;
      Departure = departure;
      Distance = distance;
      Time = time;
      Driver = driver;
      Vehicule = vehicule;
    }

    public static List<Road> GetRoadsFromCSV(string filePath)
    {
      List<Road> roads = new List<Road>();

      StreamReader sr = new StreamReader(filePath);
      if (sr == null) return new List<Road>();

      string line = sr.ReadLine();
      while (line != null)
      {
        string[] splitedLine = line.Split(';');
        if (splitedLine.Length == 5 && splitedLine[0][0] != '#')
        {
          int km = 0;
          int duration = 0;
          try
          {
            km = int.Parse(splitedLine[2]);
            duration = int.Parse(splitedLine[3].Split('h')[0]) * 3600 + int.Parse(splitedLine[3].Split('h')[1]) * 60;
          }
          catch { }

          roads.Add(new Road(splitedLine[0].ToUpper(), splitedLine[1].ToUpper(), km, duration));
        }

        line = sr.ReadLine();
      }
      sr.Close();

      return roads;
    }

    public delegate int GraphWeight(Road road);
    public static int[,] GetAdjencyMatrix(List<Road> roads, GraphWeight graphWeight)
    {
      HashSet<string> cities = new HashSet<string>();
      foreach (Road road in roads)
      {
        cities.Add(road.Arrival);
        cities.Add(road.Departure);
      }
      List<string> citiesList = cities.ToList();

      for (int i = 0; i < citiesList.Count; i++)
      {
        CityToIntMapping[citiesList[i]] = i;
        IntToCityMapping[i] = citiesList[i];
      }

      int[,] matrix = new int[citiesList.Count, citiesList.Count];
      foreach (Road road in roads)
      {
        matrix[CityToIntMapping[road.Departure], CityToIntMapping[road.Arrival]] = graphWeight(road);
      }

      return matrix;
    }
  }

  public static class Dijkstra
  {
    public static Road GetShortestPath(int[,] graph, int src, int dest)
    {
      PriorityQueue<(int, int), int> pq = new PriorityQueue<(int, int), int>();
      int[] distances = Enumerable.Repeat(Int32.MaxValue, graph.GetLength(0)).ToArray();
      int[] parents = Enumerable.Repeat(-1, graph.GetLength(0)).ToArray();

      pq.Enqueue((0, src), 0);
      distances[src] = 0;

      while (pq.Count > 0)
      {
        (int dist_u, int u) = pq.Dequeue();

        if (u == dest) break;
        if (dist_u > distances[u]) continue;

        for (int v = 0; v < graph.GetLength(0); ++v)
        {
          if (graph[u, v] > 0)
          {
            int weight = graph[u, v];

            if (distances[v] > distances[u] + weight)
            {
              distances[v] = distances[u] + weight;
              parents[v] = u;
              pq.Enqueue((distances[v], v), distances[v]);
            }
          }

        }
      }

      Road path = new Road();
      int current = dest;
      while (current != src)
      {
        path.Vertices.Add(current);
        current = parents[current];
      }
      path.Vertices.Add(src);
      path.Vertices.Reverse();
      path.Distance = distances[dest];

      return path;
    }
  }
}

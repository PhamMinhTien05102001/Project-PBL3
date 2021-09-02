using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Đồ_án_3_lớp.BLL
{
    public class Graph
    {
        private HashSet<Node> nodes;
        private bool directed;
        public string ShortestPath;
        public List<Node> pathNode;
        //Khoi tao do thi
        //Neu directed = true => do thi co huong
        public Graph(bool directed)
        {
            this.directed = directed;
            nodes = new HashSet<Node>();
            pathNode = new List<Node>();
        }
        //Them nut vao do thi
        public void addNode(Node[] n)
        {
            foreach (var i in n)
            {
                nodes.Add(i);
            }
        }
        //In ra cac diem trong graph
        public void display()
        {
            foreach (Node i in nodes)
            {
                i.show();
            }
        }
        //Them canh vao do thi
        public void addEdge(Node Source, Node destination, double weight)
        {
            nodes.Add(Source);
            nodes.Add(destination);
            addEdgeHelper(Source, destination, weight);
            if (!directed && Source != destination)
            {
                addEdgeHelper(destination, Source, weight);
            }
        }
        //Kiem tra xem canh da duoc them vao chua
        //Neu da them nhung co canh khac thi cap nhat
        //Neu chua thi them vao
        private void addEdgeHelper(Node a, Node b, double weight)
        {

            foreach (Edge edge in a.edges)
            {
                if (edge.source == a && edge.destination == b)
                {
                    edge.weight = weight;
                    return;
                }
            }
            a.edges.Add(new Edge(a, b, weight));
        }
       
       
        //In ra cac canh
        public void printEdges()
        {
            foreach (Node node in nodes)
            {
                List<Edge> edges = node.edges;

                if (edges.Count() == 0)
                {
                    Console.WriteLine("Node " + node.name + " has no edges.");
                    continue;
                }
                Console.WriteLine("Node " + node.name + " has edges to: ");

                foreach (Edge edge in edges)
                {
                    Console.WriteLine(edge.destination.name + "(" + edge.weight + ") ");
                }
                Console.WriteLine();
            }
        }
        //Kiem tra giua 2 nut co canh noi hay khong
        public bool hasEdge(Node Source, Node destination)
        {
            int check = 0;
            List<Edge> edges = Source.edges;
            foreach (Edge edge in edges)
            {
                if (edge.destination.name == destination.name && edge.weight != Double.PositiveInfinity)
                {
                    check = 1;
                    return true;
                }
              
            }
            if (check == 0) { return false; }
            return true;  
        }
        
        public void resetNodesVisited()
        {
            foreach (Node node in nodes)
            {
                node.unvisit();
            }
        }
        public void DijkstraShortestPath(Node start, Node end)
        {
            pathNode.Clear();
            Dictionary<Node, Node> changedAt = new Dictionary<Node, Node>();
            changedAt.Add(start, null);
            //luu con duong ngan nhat den hien tai
            Dictionary<Node, double> shortestPathMap = new Dictionary<Node, double>();
            //Dat moi do dai cua cac duong di bang +vc
            foreach (Node node in nodes)
            {
                if (node == start)
                    shortestPathMap.Add(start, 0);
                else shortestPathMap.Add(node, Double.PositiveInfinity);
            }

            //Di qua moi nut co the toi duoc tu nut start
            foreach (Edge edge in start.edges)
            {
                if (!shortestPathMap.ContainsKey(edge.destination))
                    shortestPathMap.Add(edge.destination, edge.weight);
                else shortestPathMap[edge.destination] = edge.weight;
                if (!changedAt.ContainsKey(edge.destination))
                    changedAt.Add(edge.destination, start);
                else changedAt[edge.destination] = start;
            }

            start.visit();
            //chay cho den khi van con nut truy cap duoc
            while (true)
            {
                Node currentNode = closestReachableUnvisited(shortestPathMap);
                //Neu k co duong di toi nut khac
                if (currentNode == null)
                {
                    ShortestPath = "Không có đường đi giữa 2 Node " + start.name + " và " + end.name;
                    //Console.WriteLine("There isn't a path between " + start.name + " and " + end.name);
                    return;
                }
                //Neu toi duoc end
                if (currentNode == end)
                {

                    pathNode.Add(end);
                    ShortestPath = "Đường đi ngắn nhất giữa 2 Node "
                                           + start.name + " và " + end.name + " là : ";

                    //Console.WriteLine("The path with the smallest weight between "
                    //                       + start.name + " and " + end.name + " is:");

                    Node child = end;

                    String path = end.name;
                    while (true)
                    {
                        Node parent = changedAt[child];
                        if (parent == null)
                        {
                            break;
                        }

                        path = parent.name + " -> " + path;
                        pathNode.Add(parent);
                        child = parent;
                    }
                    ShortestPath += path;

                    //Console.WriteLine("The path costs: " + shortestPathMap[end]);
                    return;
                }
                currentNode.visit();


                foreach (Edge edge in currentNode.edges)
                {
                    if (edge.destination.isVisited())
                        continue;

                    if (Convert.ToDouble(shortestPathMap[currentNode]) + edge.weight < Convert.ToDouble(shortestPathMap[edge.destination]))
                    {
                        if (!shortestPathMap.ContainsKey(edge.destination))
                            shortestPathMap.Add(edge.destination, Convert.ToDouble(shortestPathMap[currentNode]) + edge.weight);
                        shortestPathMap[edge.destination] = Convert.ToDouble(shortestPathMap[currentNode]) + edge.weight;
                        if (!changedAt.ContainsKey(edge.destination))
                            changedAt.Add(edge.destination, currentNode);
                        else changedAt[edge.destination] = currentNode;
                    }
                }
            }
        }
        //Tim nut gan nhat chua di qua truoc do
        private Node closestReachableUnvisited(Dictionary<Node, double> shortestPathMap)
        {

            double shortestDistance = Double.PositiveInfinity;
            Node closestReachableNode = null;
            foreach (Node node in nodes)
            {
                if (node.isVisited() == true)
                    continue;

                double currentDistance = Convert.ToDouble(shortestPathMap[node]);

                if (currentDistance == Double.PositiveInfinity)
                    continue;

                if (currentDistance < shortestDistance)
                {
                    shortestDistance = currentDistance;
                    closestReachableNode = node;
                }
            }
            return closestReachableNode;
        }
        public static List<Node> getListGraph(string name, List<Node> nodes)
        {
            List<Node> newNodes = new List<Node>();
            foreach (Node node in nodes)
            {
                if (name.Length == 1)
                {

                    if (node.name[0].ToString() == name)
                    {
                        newNodes.Add(node);
                    }
                }
                if (name.Length == 2)
                {
                    // Console.WriteLine("Yes");
                    if (node.STT != -1)
                    {
                        string ans = (node.name[0].ToString() + node.name[1].ToString());
                        if(ans == name)
                        newNodes.Add(node);
                    }
                }
            }
            return newNodes;
        }
        public void createGraph(List<Node> nodes, string[] name, int length)
        {
            for (int i = 0; i < length; i++)
            {
                List<Node> getListNodes = getListGraph(name[i], nodes);
                //Console.WriteLine("New list node {0}",name[i]);

                double height = 0;
                foreach (Node node in getListNodes)
                {
                    if (node.z / 10 > height) height = node.z / 10;
                }
                int floorRoom = 0;
                List<Node> floorNode = new List<Node>();
                List<Node> ctNode = new List<Node>();
                for (int j = 0; j < Convert.ToInt32(height) + 1; j++)
                {
                    if (j > 0) break;
                    foreach (Node node in getListNodes)
                    {
                        if (node.z == 0 && node.STT != -1)
                        {
                            floorNode.Add(node);
                            if (node.name.Contains("CT"))
                                ctNode.Add(node);
                            if (j == 0) floorRoom++;
                        }
                    }
                }
                // Console.WriteLine(floorRoom);

                for (int u = 0; u < Convert.ToInt32(height) + 1; u++)
                {
                    for (int j = 1; j < floorRoom; j++)
                    {

                        Node source = new Node(0, -1, "0", 0, 0, 0), destination = new Node(0, -1, "0", 0, 0, 0);
                        foreach (Node node in getListNodes)
                        {
                            if (node.STT == j + u * floorRoom) source = node;
                            if (node.STT == j + u * floorRoom + 1) destination = node;
                        }
                        //   Console.WriteLine("There is a edge betwwen {0} and {1}", source.name, destination.name);
                        this.addEdge(source, destination, source.getDis(destination));
                    }
                }
                foreach (Node node in ctNode)
                {
                    int stt = node.STT;
                    for (int j = 1; j < Convert.ToInt32(height) + 1; j++)
                    {
                        Node source = new Node(0, -1, "0", 0, 0, 0), destination = new Node(0, -1, "0", 0, 0, 0);
                        foreach (Node nodee in getListNodes)
                        {
                            if (nodee.STT == stt + (j - 1) * floorRoom) source = nodee;
                            if (nodee.STT == stt + j * floorRoom) destination = nodee;
                        }
                        //   Console.WriteLine("There is a edge floor betwwen {0} and {1}", source.name, destination.name);
                        this.addEdge(source, destination, source.getDis(destination));
                    }
                }
            }
        }
    }
}

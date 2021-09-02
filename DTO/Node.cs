using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Đồ_án_3_lớp.BLL
{
    public class Node
    {
        public int IdNode;
        public string IdKhu;
        public int STT;
        public string name; // name = E110 , E201 
        public double x;
        public double y;
        public double z;
        public bool visited;
        public List<Edge> edges;

        //Khoi tao nut voi so thu tu n, ten dia diem name, toa do x,y,z
        //visited = true khi canh da duoc di qua
        // edges la list chua cac canh noi voi nut nay
        public Node() { }
        public Node(int n, int STT, string name, double x, double y, double z)
        {
            this.IdNode = n;
            this.STT = STT;
            this.name = name;
            this.x = x;
            this.y = y;
            this.z = z;
            visited = false;
            edges = new List<Edge>();
        }
        //Khoi tao nut voi so thu tu n, ten dia diem name, toa do x,y,z
        public double getDis(Node temp)
        {
            return Math.Sqrt((this.y - temp.y) * (this.y - temp.y) + (this.z - temp.z) * (this.z - temp.z) + (this.x - temp.x) * (this.x - temp.x));
        }

        public bool isVisited()
        {
            return visited;
        }

        public void visit()
        {
            visited = true;
        }

        public void unvisit()
        {
            visited = false;
        }
        //In ra toa do x,y,z
        public void show()
        {
            Console.WriteLine("{0} , {1} , {2}", x, y, z);
        }

    }
}

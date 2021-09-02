using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Đồ_án_3_lớp.BLL
{
    public class Edge
    {
        public Node source;
        public Node destination;
        public double weight;

        //Khoi tao Edge voi diem dau la s, diem cuoi la d, trong so la w
        public Edge(Node s, Node d, double w)
        {
            source = s;
            destination = d;
            weight = w;
        }
        //In ra thong tin cua canh
        public String toString()
        {
            return (source.name + destination.name + weight).ToString();
        }
        //So sanh 2 canh
        public int CompareTo(Edge otherEdge)
        {
            return this.weight.CompareTo(otherEdge.weight);
        }
    }
}

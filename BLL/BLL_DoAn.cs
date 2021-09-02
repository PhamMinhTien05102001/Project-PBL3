using Đồ_án_3_lớp.DAL;
using Đồ_án_3_lớp.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Đồ_án_3_lớp.BLL
{
    class BLL_DoAn
    {
        public int AddNode;
        public Graph g = new Graph(false);
        public List<string> firstDel = new List<string>(), secondDel = new List<string>();
        public List<string> firstAdd = new List<string>(), secondAdd = new List<string>();
        public List<Node> AllNodeInCSDL;
        public string[] text, NameKhu;
        private static BLL_DoAn _Instance;
        public static BLL_DoAn Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_DoAn();
                }
                return _Instance;
            }
            private set { }
        }
        private BLL_DoAn()
        {

        }
        public void SetUp()    // Đặt ở View
        {
            AddNode = 1;
            g = new Graph(false);
            text = BLL_DoAn.Instance.GetNameKhu().ToArray();
            NameKhu = BLL_DoAn.Instance.GetNameKhu().ToArray();
        }
        public Image ZoomPicture(Image img, Size size)
        {
            Bitmap bm = new Bitmap(img, Convert.ToInt32(img.Width * size.Width), Convert.ToInt32(img.Height * size.Height));
            Graphics gpu = Graphics.FromImage(bm);
            gpu.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bm;
        }
        public void Find(Node source, Node destination)
        {
            g.resetNodesVisited();
            InitGraph();
            g.DijkstraShortestPath(source, destination);
            g.pathNode.Reverse();
        }
        public Bitmap CreateDynamicPoint_BLL(int Value, Bitmap Image)
        {
            Point p1, p2;
            List<ADDNODE> listOFPoint = GetAllAddNode_BLL();
            for (int i = 0; i < listOFPoint.Count; i++)
            {
                if (Value == 0)
                {
                    p1 = new Point(Convert.ToInt32((listOFPoint[i].X - 3) / 2), Convert.ToInt32(listOFPoint[i].Y / 2));
                    p2 = new Point(Convert.ToInt32((listOFPoint[i].X + 3) / 2), Convert.ToInt32((listOFPoint[i].Y) / 2));
                    //MessageBox.Show(p1.ToString() + "    " + p2.ToString());
                }
                else
                {
                    p1 = new Point(Convert.ToInt32((listOFPoint[i].X - 3) / 2 * Value), Convert.ToInt32(listOFPoint[i].Y / 2) * Value);
                    p2 = new Point(Convert.ToInt32((listOFPoint[i].X + 3) / 2 * Value), Convert.ToInt32((listOFPoint[i].Y) * Value / 2) * Value);
                    //MessageBox.Show(p1.ToString() + "    " + p2.ToString());
                }
                DrawLineBrushes((Bitmap)Image, p1, p2, Value);
            }
            Image img = Image;
            return (Bitmap)img;
        }
        public void DrawLineBrushes(Bitmap bmp, Point p1, Point p2, int Value)
        {
            Brush p = new SolidBrush(Color.Yellow);
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.FillEllipse(p, p1.X - 4, p1.Y - 4, 8, 8);
            }
        }
        public void resetGraph()
        {
            g = new Graph(false);
        }
        public void InitGraph()
        {
            BLL_DoAn.Instance.g.createGraph(AllNodeInCSDL, NameKhu, NameKhu.Length);
            BLL_DoAn.Instance.connectListEdge(firstAdd, secondAdd);
            BLL_DoAn.Instance.connectExtraNode();
            BLL_DoAn.Instance.removeListEdge(firstDel, secondDel);
        }
        public void removeListEdge(List<String> first, List<String> second)
        {
            for (int i = 0; i < first.Count(); i++)
            {
                Node x = getNodeByName(first[i]);
                Node y = getNodeByName(second[i]);
                //Console.WriteLine("List Remove : {0} and {1}", x.name, y.name);
                BLL_DoAn.Instance.g.addEdge(x, y, Double.PositiveInfinity);
            }
        }
        public void connectListEdge(List<String> first, List<String> second)
        {
            for (int i = 0; i < first.Count(); i++)
            {
                Node x = getNodeByName(first[i]);
                Node y = getNodeByName(second[i]);
                //Console.WriteLine("List Add : {0} and {1}", x.name, y.name);
                BLL_DoAn.Instance.g.addEdge(x, y, x.getDis(y));
            }
        }
        public Bitmap Draw(int Value, Bitmap Image)
        {
            Point p1, p2;
            for (int i = 0; i < BLL_DoAn.Instance.g.pathNode.Count - 1; i++)
            {
                if (Value == 0)
                {
                    p1 = new Point(Convert.ToInt32(BLL_DoAn.Instance.g.pathNode[i].x / 2), Convert.ToInt32(BLL_DoAn.Instance.g.pathNode[i].y / 2));
                    p2 = new Point(Convert.ToInt32(BLL_DoAn.Instance.g.pathNode[i + 1].x / 2), Convert.ToInt32(BLL_DoAn.Instance.g.pathNode[i + 1].y / 2));
                    //MessageBox.Show(p1.ToString() + "    " + p2.ToString());
                }
                else
                {
                    p1 = new Point(Convert.ToInt32(BLL_DoAn.Instance.g.pathNode[i].x * Value / 2), Convert.ToInt32(BLL_DoAn.Instance.g.pathNode[i].y * Value / 2));
                    p2 = new Point(Convert.ToInt32(BLL_DoAn.Instance.g.pathNode[i + 1].x * Value / 2), Convert.ToInt32(BLL_DoAn.Instance.g.pathNode[i + 1].y * Value / 2));
                    //MessageBox.Show(p1.ToString() + "    " + p2.ToString());
                }
                DrawLineInt((Bitmap)Image, p1, p2, Value);
            }
            Image img = Image;
            return (Bitmap)img;
        }
        public Bitmap Draw_Form2(List<Node> pathNode, Model temp, Bitmap Image)
        {
            if (pathNode.Count == 1)
            {
                Point p1 = new Point(temp.startPossition.X + (pathNode[0].STT - 1) % temp.numberOfRoom * temp.width, temp.startPossition.Y - ((pathNode[0].STT - 1) / temp.numberOfRoom) * temp.height);
                DrawLineInt((Bitmap)Image, p1, new Point(p1.X + 15, p1.Y + 15), 1);
                DrawLineInt((Bitmap)Image, new Point(p1.X, p1.Y + 15), new Point(p1.X + 15, p1.Y), 1);
            }
            for (int i = 0; i < pathNode.Count - 1; i++)
            {
                if (Math.Abs(pathNode[i].STT - pathNode[i + 1].STT) == 1 || Math.Abs(pathNode[i].z - pathNode[i + 1].z) == 10)
                {
                    //MessageBox.Show(pathNode[i].name + ' ' + pathNode[i].STT + ' ' + temp.numberOfRoom);
                    Point p1 = new Point(temp.startPossition.X + (pathNode[i].STT - 1) % temp.numberOfRoom * temp.width, temp.startPossition.Y - ((pathNode[i].STT - 1) / temp.numberOfRoom) * temp.height);
                    Point p2 = new Point(temp.startPossition.X + (pathNode[i + 1].STT - 1) % temp.numberOfRoom * temp.width, temp.startPossition.Y - ((pathNode[i + 1].STT - 1) / temp.numberOfRoom) * temp.height);
                    //MessageBox.Show(p1.ToString()+"       " + p2.ToString());
                    DrawLineInt((Bitmap)Image, p1, p2, 1);
                }
            }
            Image img = Image;
            return (Bitmap)img;
        }
        public void DrawLineInt(Bitmap bmp, Point p1, Point p2, int Value)
        {
            Pen redPen = new Pen(Color.Red, Math.Max(3, 3 * Value));
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.DrawLine(redPen, p1, p2);
            }
        }


        public void Delete(string txt1, string txt2, Node source, Node destination)
        {
            firstDel.Add(txt1);
            secondDel.Add(txt2);
            BLL_DoAn.Instance.g.addEdge(source, destination, Double.PositiveInfinity);
            //Console.WriteLine("Delete {0} and {1}", txt1, txt2);
            for (int i = 0; i < firstDel.Count(); i++)
            {
                if (firstAdd[i] == txt1)
                {
                    if (secondAdd[i] == txt2)
                    {
                        firstAdd.RemoveAt(i);
                        secondAdd.RemoveAt(i);
                        break;
                    }
                }
                if (firstAdd[i] == txt2)
                {
                    if (secondAdd[i] == txt1)
                    {
                        firstAdd.RemoveAt(i);
                        secondAdd.RemoveAt(i);
                        break;
                    }
                }
            }

        }
        public void Add(string txt1, string txt2, Node source, Node destination)
        {
            firstAdd.Add(txt1);
            secondAdd.Add(txt2);
            // Console.WriteLine("{0} and {1}", txt1, txt2);
            BLL_DoAn.Instance.g.addEdge(source, destination, source.getDis(destination));
            for (int i = 0; i < firstDel.Count(); i++)
            {
                if (firstDel[i] == txt1)
                {
                    if (secondDel[i] == txt2)
                    {
                        firstDel.RemoveAt(i);
                        secondDel.RemoveAt(i);
                        break;
                    }
                }
                if (firstDel[i] == txt2)
                {
                    if (secondDel[i] == txt1)
                    {

                        firstDel.RemoveAt(i);
                        secondDel.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        public Graph getGraph()
        {
            return BLL_DoAn.Instance.g;
        }
        public Node getNodeByName(string txt1)
        {
            foreach (Node node in AllNodeInCSDL)
            {
                if (node.name == txt1)
                {
                    return node;
                }
            }
            return null;
        }
        public void Connect()
        {
            // DP1 : DP_CM0
            // DP2 : DP1
            // DP3 : DP2
            // DP4 : DP_CC0
            // DP5 : DP_EM0
            // HTF : HT3
            Add("EM_CT21", "EC110B", getNodeByName("EM_CT21"), getNodeByName("EC110B"));
            Add("EC112", "CM128", getNodeByName("EC112"), getNodeByName("CM128"));
            Add("CM125", "CC101", getNodeByName("CM125"), getNodeByName("CC101"));
            Add("BT_CT21", "BS108", getNodeByName("BT_CT21"), getNodeByName("BS108"));
            Add("BT_CT31", "BS107", getNodeByName("BT_CT31"), getNodeByName("BS107"));
            Add("BS109", "BT_CT11", getNodeByName("BS109"), getNodeByName("BT_CT11"));
            Add("BS209", "BT_CT12", getNodeByName("BS209"), getNodeByName("BT_CT12"));
            Add("BS208", "BT_CT22", getNodeByName("BS208"), getNodeByName("BT_CT22"));
            Add("BS207", "BT_CT32", getNodeByName("BS207"), getNodeByName("BT_CT32"));
            Add("F_CT11", "BT_CT11", getNodeByName("F_CT11"), getNodeByName("BT_CT11"));
            Add("DP_CM0", "DP1", getNodeByName("DP_CM0"), getNodeByName("DP1"));
            Add("DP2", "DP1", getNodeByName("DP2"), getNodeByName("DP1"));
            Add("DP2", "DP_HT", getNodeByName("DP2"), getNodeByName("DP_HT"));
            Add("DP_CM0", "DP_CC0", getNodeByName("DP_CM0"), getNodeByName("DP_CC0"));
            Add("DP_CM0", "DP_EM0", getNodeByName("DP_CM0"), getNodeByName("DP_EM0"));
            Add("DP_CM0", "EC112", getNodeByName("DP_CM0"), getNodeByName("EC112"));
            Add("DP_EM0", "EM_CT11", getNodeByName("DP_EM0"), getNodeByName("EM_CT11"));
            Add("DP_CM0", "CM128", getNodeByName("DP_CM0"), getNodeByName("CM128"));
            Add("F110", "DP1", getNodeByName("F110"), getNodeByName("DP1"));
            Add("F108", "DP2", getNodeByName("F108"), getNodeByName("DP2"));
            Add("F107", "DP2", getNodeByName("F107"), getNodeByName("DP2"));
            Add("F_CT21", "DP2", getNodeByName("F_CT21"), getNodeByName("DP2"));
            Add("CC107", "DP_CC0", getNodeByName("CC107"), getNodeByName("DP_CC0"));
            Add("BT_CT31", "DP_CC0", getNodeByName("BT_CT31"), getNodeByName("DP_CC0"));
            Add("CC105", "DP_HT", getNodeByName("CC105"), getNodeByName("DP_HT"));
            Add("CC106", "DP_HT", getNodeByName("CC106"), getNodeByName("DP_HT"));
            Add("CC107", "DP_HT", getNodeByName("CC107"), getNodeByName("DP_HT"));
            Add("BS107", "DP_HT", getNodeByName("BS107"), getNodeByName("DP_HT"));
            Add("BS108", "DP_HT", getNodeByName("BS108"), getNodeByName("DP_HT"));
            Add("BS109", "DP_HT", getNodeByName("BS109"), getNodeByName("DP_HT"));
            Add("F_CT21", "DP_HT", getNodeByName("F_CT21"), getNodeByName("DP_HT"));
        }
        public bool isHasEdge(string start, string end)
        {
            int check = 0;
            Node source = getNodeByName(start);
            Node destination = getNodeByName(end);
            if (g.hasEdge(source, destination) == true)
            {
                check = 1;
            }
            if (check == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddNodeInGraph()
        {
            AllNodeInCSDL = new List<Node>();
            foreach (PHONGHOC i in BLL_DoAn.Instance.GetAllPhongHoc_BLL())
            {
                AllNodeInCSDL.Add(BLL_DoAn.Instance.ConverNodeFromPhongHoc(AddNode++, i));
            }
            foreach (CAUTHANG i in BLL_DoAn.Instance.GetAllCauThang_BLL())
            {
                AllNodeInCSDL.Add(BLL_DoAn.Instance.ConverNodeFromCauThang(AddNode++, i));
            }
            foreach (ADDNODE i in BLL_DoAn.Instance.GetAllAddNode_BLL())
            {
                AllNodeInCSDL.Add(BLL_DoAn.Instance.ConverNodeFromAddNode(AddNode++, i));
            }
        }
        public Node ConverNodeFromPhongHoc(int Id, PHONGHOC i)
        {
            return new Node()
            {
                IdNode = Id,
                IdKhu = i.IdKhu,
                STT = i.STTPH,
                name = i.IdPhong,
                x = i.X,
                y = i.Y,
                z = i.Z,
                visited = false,
                edges = new List<Edge>()
            };
        }   // Convert 1 node từ PhongHoc -> Node
        public Node ConverNodeFromCauThang(int Id, CAUTHANG i)
        {
            return new Node()
            {
                IdNode = Id,
                IdKhu = i.IdKhu,
                STT = i.STTCT,
                name = i.IdCauThang,
                x = i.X,
                y = i.Y,
                z = i.Z,
                visited = false,
                edges = new List<Edge>()
            };
        }   // Convert 1 Node từ CauThang -> Node
        public Node ConverNodeFromAddNode(int Id, ADDNODE i)
        {
            return new Node
            {
                IdNode = Id,
                IdKhu = i.IdKhu,
                name = i.IdAddNode,
                STT = -1,
                x = i.X,
                y = i.Y,
                z = 0,
                visited = false,
                edges = new List<Edge>()
            };
        }
        public List<string> GetNameKhu()
        {
            List<string> data = new List<string>();
            foreach (KHU i in DAL_DoAn.Instance.GetAllKhu_DAL())
                data.Add(i.TenKhu);
            return data;
        }   // Trả về danh sách tên khu
        public List<PHONGHOC> GetAllPhongHoc_BLL()
        {
            return DAL_DoAn.Instance.GetAllPhongHoc_DAL();
        }
        public List<CAUTHANG> GetAllCauThang_BLL()
        {
            return DAL_DoAn.Instance.GetAllCauThang_DAL();
        }
        public List<ADDNODE> GetAllAddNode_BLL()
        {
            return DAL_DoAn.Instance.GetAllAddNode_DAL();
        }
        public List<Node> getExtraNode(string Id)
        {
            string IdKhu = getNameKhuFromIdKhu(Id);
            List<Node> nodes = new List<Node>();
            foreach (Node node in AllNodeInCSDL)
            {
                if (node.STT == -1 && node.IdKhu == IdKhu && node.IdKhu != "8")
                {
                    nodes.Add(node);
                }
            }
            return nodes;
        }
        public List<Node> getNumOfRoomFromKhu(string Id)
        {
            string IdKhu = getNameKhuFromIdKhu(Id);
            List<Node> nodes = new List<Node>();
            foreach (Node node in AllNodeInCSDL)
            {
                if (node.STT != -1 && node.z == 0 && node.IdKhu == IdKhu)
                {
                    nodes.Add(node);
                }
            }
            return nodes;
        }
        public void connectExtraNode()
        {
            for (int i = 0; i < NameKhu.Length; i++)
            {
                List<Node> NodeMain = getNumOfRoomFromKhu(NameKhu[i]);
                List<Node> NodeExtra = getExtraNode(NameKhu[i]);
                if (NodeExtra == null)
                {
                    continue;
                }
                else
                {
                    foreach (Node node in NodeExtra)
                    {
                        foreach (Node node1 in NodeMain)
                        {
                            g.addEdge(node, node1, node.getDis(node1));
                        }
                    }
                }
            }
        }
        public void insertExtraNode(Node node)
        {
            DAL_DoAn.Instance.InsertAddNode_DAL(node);
        }
        public void updateExtraNode(Node node)
        {
            DAL_DoAn.Instance.updateAddNode_DAL(node);
        }
        public bool checkNameNode(string name)
        {
            foreach (Node node in AllNodeInCSDL)
            {
                if (node.name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool checkIsExistNode(Node i)
        {
            foreach (Node node in AllNodeInCSDL)
            {
                if (i.x == node.x && i.y == node.y && i.z == node.z && node.z == 0 && node.name != i.name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool checkIfHasNode(Node i)
        {
            foreach (Node node in AllNodeInCSDL)
            {
                if (i == node)
                {
                    return true;
                }
            }
            return false;
        }
        public Node GetNodeByXYLocation(double x, double y, double z)
        {
            foreach (Node node in AllNodeInCSDL)
            {
                if (node.x == x && node.y == y && node.z == z)
                    return node;
            }
            return null;
        }
        public string getNameKhuFromIdKhu(string node)
        {
            string[] arr = { "EC", "EM", "CM", "CC", "BT", "F", "BS" };
            for (int i = 0; i < arr.Length; i++)
            {
                if (node.Contains(arr[i]))
                {
                    int val = i + 1;
                    return val.ToString();
                }
            }
            return "8";
        }
    }
}


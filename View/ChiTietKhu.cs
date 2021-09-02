using Đồ_án_3_lớp.BLL;
using Đồ_án_3_lớp.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Đồ_án_3_lớp.View
{
    public partial class ChiTietKhu : Form
    {
        
        public string name { get; set; }
        List<Node> pathNode, nodes;
        Dictionary<string, Model> position = new Dictionary<string, Model>()
        {
            {"EM", new Model(new Point(93,226),6,169,144) },
            {"EC", new Model(new Point(110,562),5,210,155) },
            {"CC", new Model(new Point(66,54),7,126,100) },
            {"CM", new Model(new Point(80,201),6,150,110) },
            {"F", new Model(new Point(57,462),13,108,134) },
            {"BT", new Model(new Point(65,303),9,125,124) },
            {"BS", new Model(new Point(136,158),3,266,110) },
        };
        public ChiTietKhu(string m, List<Node> nodes)
        {
            InitializeComponent();
            name = m;
            //MessageBox.Show(name);
            this.nodes = nodes;
            pathNode = Graph.getListGraph(m, this.nodes);
            SetGUI();
            if (position.ContainsKey(name))
            {
                pictureBox1.Image = BLL_DoAn.Instance.Draw_Form2(pathNode, position[name], (Bitmap)pictureBox1.Image);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = e.Location.X + ":" + e.Location.Y;
        }

        public void SetGUI()
        {

            if (System.IO.File.Exists("img\\" + name + "_Model.jpg"))
            {
                Image myimage = new Bitmap("img\\" + name + "_Model.jpg");
                pictureBox1.Image = myimage;
            }
        }
    }
}

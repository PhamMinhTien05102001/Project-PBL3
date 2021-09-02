using Đồ_án_3_lớp.BLL;
using Đồ_án_3_lớp.DAL;
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
    public partial class AddNode : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        public AddNode(String s)
        {
            BLL_DoAn.Instance.SetUp();
            BLL_DoAn.Instance.AddNodeInGraph();
            BLL_DoAn.Instance.InitGraph();
            InitializeComponent();
            groupBox1.Text = s;
            SetComboBox();
        }
        private void SetComboBox()
        {
            foreach (string i in BLL_DoAn.Instance.GetNameKhu())
            {
                if (i == "AddNode") cmbKhu.Items.Add("<Không thuộc khu nào>");
                else cmbKhu.Items.Add(i);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool checkKhuValid(string text)
        {
            foreach (string s in cmbKhu.Items)
            {
                if (text == s)
                {
                    return true;
                }
            }
            return false;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
      
            string text = txtNameNode.Text;
            string textKhu = cmbKhu.Text;
            double X, Y;
            bool isDoubleX  =Double.TryParse(txtX.Text,out X);
            bool isDoubleY = Double.TryParse(txtY.Text, out Y);
            if (text == "")
            {
                MessageBox.Show("Hãy nhập tên Node!"); 
            }
            else if (BLL_DoAn.Instance.checkNameNode(text) == true)
            {
                MessageBox.Show("Node " + text + " đã tồn tại!");
            }
            else if (cmbKhu.Text == "")
            {
                MessageBox.Show("Hãy nhập khu");    
            }
            else if (checkKhuValid(cmbKhu.Text) == false)
            {
                MessageBox.Show("Tên khu không tồn tại");
            }
            else if (isDoubleX == false || isDoubleY == false)
            {
                if (txtX.Text == "" && txtY.Text == "")
                {
                    MessageBox.Show("Hãy nhập X và Y");
                }
                else if (txtX.Text == "" && txtY.Text != "")
                {
                    MessageBox.Show("Hãy nhập X");
                }
                else if (txtY.Text == "" && txtX.Text != "")
                {
                    MessageBox.Show("Hãy nhập Y");
                }
                else
                {
                    MessageBox.Show("X,Y không hợp lệ");
                }
            }
            else if (X < 0)
            {
                MessageBox.Show("X phải là số dương ");
            }
            else if (Y < 0)
            {
                MessageBox.Show("Y phải là số dương ");
            }
            else
            {
                Node node = new Node
                {
                    IdKhu = BLL_DoAn.Instance.getNameKhuFromIdKhu(textKhu),
                    IdNode = BLL_DoAn.Instance.AddNode++,
                    name = text,
                    STT = -1,
                    x = Convert.ToDouble(X),
                    y = Convert.ToDouble(Y),
                    z = 0,
                    visited = false,
                    edges = new List<Edge>()
                };
                if (BLL_DoAn.Instance.checkIsExistNode(node) == true)
                {
                    MessageBox.Show("Đã có Node '" + BLL_DoAn.Instance.GetNodeByXYLocation(X, Y, 0).name + "' ở tọa độ này!");
                }
                else if (BLL_DoAn.Instance.checkIfHasNode(node) == true)
                {
                    MessageBox.Show("Node '" + text + "' đã tồn tại!");
                }
                else
                {
                    MessageBox.Show("Thêm node thành công");
                    BLL_DoAn.Instance.insertExtraNode(node);
                    BLL_DoAn.Instance.AddNodeInGraph();
                }
                d();
            }
        }
    }
}

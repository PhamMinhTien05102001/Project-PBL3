using Đồ_án_3_lớp.BLL;
using Đồ_án_3_lớp.DAL;
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
    public partial class EditNode : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        public EditNode(String s)
        {
            InitializeComponent();
            groupBox1.Text = s;
            foreach (string i in BLL_DoAn.Instance.GetNameKhu())
            {
                if (i == "AddNode") cmbKhu.Items.Add("<Không thuộc khu nào>");
                else cmbKhu.Items.Add(i);
            }
            SetUp();
        }
        private void SetUp()
        {
            dgvEditNode.DataSource = BLL_DoAn.Instance.GetAllAddNode_BLL();
 
            ADDNODE n1 = BLL_DoAn.Instance.GetAllAddNode_BLL()[0];
            txtNameNode.Text = n1.IdAddNode;
            cmbKhu.SelectedIndex = Convert.ToInt32(n1.IdKhu) - 1;
            txtX.Text = n1.X.ToString();
            txtY.Text = n1.Y.ToString();
        }
        private void dgvEditNode_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewSelectedRowCollection r = dgvEditNode.SelectedRows;
            if(dgvEditNode.SelectedRows.Count == 1)
            {
                txtNameNode.Text = r[0].Cells["IdAddNode"].Value.ToString();
                cmbKhu.SelectedIndex = Convert.ToInt32(r[0].Cells["IdKhu"].Value.ToString()) - 1;
                txtX.Text = r[0].Cells["X"].Value.ToString();
                txtY.Text = r[0].Cells["Y"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Mỗi lần edit chỉ chọn 1 Node");
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
            bool isDoubleX = Double.TryParse(txtX.Text, out X);
            bool isDoubleY = Double.TryParse(txtY.Text, out Y);
            if (cmbKhu.Text == "")
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
                    x = X,
                    y = Y,
                    z = 0,
                    visited = false,
                    edges = new List<Edge>()
                };
                if (BLL_DoAn.Instance.checkIsExistNode(node) == true)
                {
                    MessageBox.Show("Đã có Node '" + BLL_DoAn.Instance.GetNodeByXYLocation(X, Y, 0).name + "' ở tọa độ này!");
                }
                else
                {
                    MessageBox.Show("Chỉnh sửa node thành công");
                    BLL_DoAn.Instance.updateExtraNode(node);
                    BLL_DoAn.Instance.AddNodeInGraph();
                    SetUp();
                }
                d();
            }
        }
    }
}

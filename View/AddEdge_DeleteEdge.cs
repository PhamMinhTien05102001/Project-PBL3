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
    public partial class AddEdge_DeleteEdge : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        int checkAddorDelete = 0;
        public AddEdge_DeleteEdge(String s)
        {
            InitializeComponent();
            BLL_DoAn.Instance.SetUp();
            BLL_DoAn.Instance.AddNodeInGraph();
            BLL_DoAn.Instance.InitGraph();
            groupBox1.Text = s;
            SetComboBox();        
            if (s == "Delete Edge")
            {
                checkAddorDelete = 1;
            }
           
        }
        private void SetComboBox()
        {
            foreach (PHONGHOC i in BLL_DoAn.Instance.GetAllPhongHoc_BLL())
            {
                cmbStart.Items.Add(i.IdPhong);
                cmbEnd.Items.Add(i.IdPhong);
            }
            foreach (CAUTHANG i in BLL_DoAn.Instance.GetAllCauThang_BLL())
            {
                cmbStart.Items.Add(i.IdCauThang);
                cmbEnd.Items.Add(i.IdCauThang);
            }
            foreach (ADDNODE i in BLL_DoAn.Instance.GetAllAddNode_BLL())
            {
                cmbStart.Items.Add(i.IdAddNode);
                cmbEnd.Items.Add(i.IdAddNode);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (checkAddorDelete == 0)
            {
                if (cmbEnd.Text != "" && cmbStart.Text != "")
                {
                    Node source = BLL_DoAn.Instance.getNodeByName(cmbStart.Text);
                    Node destination = BLL_DoAn.Instance.getNodeByName(cmbEnd.Text);
                    if (source == null && destination != null)
                    {
                        MessageBox.Show("Tên Node '" + cmbStart.Text + "' không tồn tại");
                    }
                    else if (source != null && destination == null)
                    {
                        MessageBox.Show("Tên Node '" + cmbEnd.Text + "' không tồn tại");
                    }
                    else if (source == null && destination == null)
                    {
                        MessageBox.Show("Tên Node '" + cmbStart.Text + "' và '" + cmbEnd.Text + "' không tồn tại");
                    }
                    else if (source != null && destination != null)
                    {
                        if (BLL_DoAn.Instance.isHasEdge(cmbStart.Text, cmbEnd.Text) == false)
                        {
                            MessageBox.Show("Kết nối 2 Node : '" + cmbStart.Text + "' và '" + cmbEnd.Text + "' thành công!");
                            BLL_DoAn.Instance.Add(cmbStart.Text, cmbEnd.Text, source, destination);
                        }
                        else
                        {
                            MessageBox.Show("Đường đi giữa 2 Node : '" + cmbStart.Text + "' và '" + cmbEnd.Text + "' đã tồn tại!");
                        }
                    }   
                }
                else
                {
                    MessageBox.Show("Hãy nhập đầy đủ cả 2 Node!");     
                }
            }
            else
            {
                if (cmbEnd.Text != "" && cmbStart.Text != "" )
                {
                    Node source = BLL_DoAn.Instance.getNodeByName(cmbStart.Text);
                    Node destination = BLL_DoAn.Instance.getNodeByName(cmbEnd.Text);
                    if (source == null && destination != null)
                    {
                        MessageBox.Show("Tên Node '"+cmbStart.Text+ "' không tồn tại");
                    }
                    else if (source != null && destination == null)
                    {
                        MessageBox.Show("Tên Node '"+cmbEnd.Text+ "' không tồn tại");
                    }
                    else if (source == null && destination == null)
                    {
                        MessageBox.Show("Tên Node '"+cmbStart.Text+"' và '"+cmbEnd.Text+ "' không tồn tại");
                    }
                    else if (source!=null && destination !=null)
                    {  
                        if (BLL_DoAn.Instance.isHasEdge(cmbStart.Text, cmbEnd.Text) == false)
                        {
                            MessageBox.Show("Không có đường đi nào giữa 2 Node : '" + cmbStart.Text + "' và '" + cmbEnd.Text + "'");
                        }
                        else
                        {
                            MessageBox.Show("Xóa thành công đường đi giữa 2 Node : '" + cmbStart.Text + "' và '" + cmbEnd.Text + "'!");
                            BLL_DoAn.Instance.Delete(cmbStart.Text, cmbEnd.Text, source, destination);
                        }
                    }     
                }
                else
                {
                    MessageBox.Show("Hãy nhập đầy đủ cả 2 Node!");     
                }
            }
            d();
        }
    }
}

using Đồ_án_3_lớp.BLL;
using Đồ_án_3_lớp.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Đồ_án_3_lớp.View
{
    public partial class GiaoDienChinh : Form
    {
        public int[] x, y, size;
        PictureBox org;
        Image myimage, copyMyImage;
        public GiaoDienChinh()
        {
            InitializeComponent();
            BLL_DoAn.Instance.SetUp();
            BLL_DoAn.Instance.AddNodeInGraph();
            BLL_DoAn.Instance.Connect();
            InitPictureBoxImage();
            CreateDynamicButton();
            SetComboBox();
            CreateDynamicPoint();

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
        private void InitPictureBoxImage()  // Đặt ở View
        {
            //OpenFileDialog open = new OpenFileDialog();
            myimage = new Bitmap("img\\all.jpg");
            copyMyImage = new Bitmap("img\\all.jpg");
            pictureBox1.Image = copyMyImage;
        }
        private void CreateDynamicButton()  // Đặt ở View
        {
            x = new int[] { 896, 882, 890, 905, 695, 588, 695 };
            y = new int[] { 202, 32, 300, 390, 510, 347, 470 };
            size = new int[] { 20, 15, 15, 20, 15, 20, 15 };
            pictureBox1.Controls.Clear();

            for (int i = 0; i < x.Length; i++)
            {
                RoundButton dynamicButton = new RoundButton();
                if (trackBar1.Value == 0)
                {
                    dynamicButton.Height = size[i];
                    dynamicButton.Width = size[i];
                    dynamicButton.Location = new Point(x[i] - dynamicButton.Width / 2, y[i] - dynamicButton.Width / 2);
                }
                else
                {
                    dynamicButton.Location = new Point((x[i] - size[i] / 2 - dynamicButton.Width / 2) * trackBar1.Value, (y[i] - size[i] / 2 - dynamicButton.Height / 2) * trackBar1.Value);
                    dynamicButton.Height = size[i] * trackBar1.Value;
                    dynamicButton.Width = size[i] * trackBar1.Value;
                }
                dynamicButton.BackColor = Color.Aqua;
                dynamicButton.Text = BLL_DoAn.Instance.text[i];
                dynamicButton.Click += new EventHandler(roundButton1_Click);
                pictureBox1.Controls.Add(dynamicButton);
            }
        }
        public void CreateDynamicPoint()
        {
            pictureBox1.Image = BLL_DoAn.Instance.CreateDynamicPoint_BLL(trackBar1.Value, (Bitmap)pictureBox1.Image);
        }
        private void roundButton1_Click(object sender, EventArgs e)    // Đặt ở View
        {
            RoundButton btn = sender as RoundButton;
            string s = btn.Text;
            ChiTietKhu f = new ChiTietKhu(s, BLL_DoAn.Instance.g.pathNode);
            f.Text = "Chi tiết khu " + s;
            f.Show();
        }

        private void Form1_Load(object sender, EventArgs e)     // Đặt ở View
        {
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 6;
            trackBar1.SmallChange = 1;
            trackBar1.LargeChange = 1;
            trackBar1.UseWaitCursor = false;
            this.DoubleBuffered = true;
            org = new PictureBox();
            org.Image = pictureBox1.Image;
        }
        private void trackBar1_Scroll(object sender, EventArgs e)   // Đặt ở View
        {
            if (trackBar1.Value != 0)
            {
                ChangePictureBoxImage();
                CreateDynamicButton();
                CreateDynamicPoint();
                FindAndDraw(false);
            }
        }
        public void ChangePictureBoxImage()
        {
            if (trackBar1.Value != 0)
            {
                pictureBox1.Image = null;
                pictureBox1.Image = BLL_DoAn.Instance.ZoomPicture(org.Image, new Size(trackBar1.Value, trackBar1.Value));
                CreateDynamicButton();
                CreateDynamicPoint();
            }
        }

        public void FindAndDraw(bool flag)
        {
            int checkText1 = 0, checkText2 = 0;
            richTextBox1.Text = "";
            if (cmbStart.Text != "" && cmbEnd.Text != "")
            {
                string start = cmbStart.Text;
                string end = cmbEnd.Text;
                Node source = new Node(0, -1, "0", 0, 0, 0), destination = new Node(0, -1, "0", 0, 0, 0);
                foreach (Node node in BLL_DoAn.Instance.AllNodeInCSDL)
                {
                    if (node.name == start)
                    {
                        source = node;
                        checkText1 = 1;
                    }
                    if (node.name == end)
                    {
                        destination = node;
                        checkText2 = 1;
                    }
                }
                if (checkText1 == 1 && checkText2 == 1)
                {
                    BLL_DoAn.Instance.Find(source, destination);
                    richTextBox1.Text += BLL_DoAn.Instance.g.ShortestPath + "\n";
                    pictureBox1.Image = BLL_DoAn.Instance.Draw(trackBar1.Value, (Bitmap)pictureBox1.Image);
                }
                else
                {
                    if (checkText1 == 0 && checkText2 == 1) MessageBox.Show("Điểm xuất phát không tồn tại!");
                    if (checkText1 == 1 && checkText2 == 0) MessageBox.Show("Điểm đích không tồn tại!");
                    if (checkText1 == 0 && checkText2 == 0) MessageBox.Show("Điểm xuất phá và điểm đích không tồn tại!");
                }
            }
            else
            {
                if (flag == true) MessageBox.Show("Hãy nhập đầy đủ điểm xuất phát và điểm đích");
            }


        }
        private void buttonShow_Click(object sender, EventArgs e)   // Đặt ở View
        {
            ResetPictureBoxImage();
            FindAndDraw(true);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditNode ae = new EditNode("Edit Node");
            ae.d += new EditNode.MyDel(ResetPictureBoxImage);
            ae.d += new EditNode.MyDel(CreateDynamicPoint);
            ae.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            AddEdge_DeleteEdge ad = new AddEdge_DeleteEdge("Delete Edge");
            ad.d += new AddEdge_DeleteEdge.MyDel(ResetPictureBoxImage);
            ad.d += new AddEdge_DeleteEdge.MyDel(CreateDynamicPoint);
            ad.ShowDialog();
            //buttonShow_Click(sender, e);
            ResetPictureBoxImage();
            FindAndDraw(false);
        }

        private void cmbAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAdd.SelectedIndex == 1)
            {
                AddNode ae = new AddNode("Add Node");
                ae.d += new AddNode.MyDel(ResetPictureBoxImage);
                ae.d += new AddNode.MyDel(CreateDynamicPoint);
                ae.ShowDialog();
                SetComboBox();
            }
            else if (cmbAdd.SelectedIndex == 2)
            {
                AddEdge_DeleteEdge ad = new AddEdge_DeleteEdge("Add Edge");
                ad.d += new AddEdge_DeleteEdge.MyDel(ResetPictureBoxImage);
                ad.d += new AddEdge_DeleteEdge.MyDel(CreateDynamicPoint);
                ad.ShowDialog();
                //buttonShow_Click(sender, e);
                ResetPictureBoxImage();
                FindAndDraw(false);
            }
            cmbAdd.SelectedIndex = 0;
        }



        private void btnShow_Click(object sender, EventArgs e)
        {
            ResetPictureBoxImage();
            FindAndDraw(true);
        }


        private void ResetPictureBoxImage()
        {
            copyMyImage = (Bitmap)myimage.Clone();
            if (trackBar1.Value != 0)
            {
                pictureBox1.Image = null;
                pictureBox1.Image = BLL_DoAn.Instance.ZoomPicture(copyMyImage, new Size(trackBar1.Value, trackBar1.Value));
                CreateDynamicButton();
                CreateDynamicPoint();
            }
            else
            {
                pictureBox1.Image = copyMyImage;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (trackBar1.Value == 1)
            {
                textBox1.Text = (e.Location.X * 2).ToString() + " : " + +e.Location.Y * 2;
            }
            else if (trackBar1.Value > 1)
            {
                textBox1.Text = (e.Location.X * 2 / trackBar1.Value).ToString() + " : " + +e.Location.Y * 2 / trackBar1.Value;
            }
        }

    }
}
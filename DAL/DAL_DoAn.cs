using Đồ_án_3_lớp.BLL;
using Đồ_án_3_lớp.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Đồ_án_3_lớp.DAL
{
    class DAL_DoAn
    {
        private static DAL_DoAn _Instance;
        public static DAL_DoAn Instance
        {
            get
            {
                if (_Instance == null) _Instance = new DAL_DoAn();
                return _Instance;
            }
            private set { }
        }
        private DAL_DoAn() { }
        public KHU GetKhu_DAL(DataRow i)
        {
            return new KHU
            {
                IdKhu = i["IdKhu"].ToString(),
                TenKhu = i["TenKhu"].ToString()
            };
        }
        public List<KHU> GetAllKhu_DAL()
        {
            string Query = "select * from KHU";
            List<KHU> data = new List<KHU>();
            foreach (DataRow i in DBHelper.Instance.GetRecord(Query).Rows)
            {
                data.Add(GetKhu_DAL(i));
            }
            return data;
        }
        public PHONGHOC GetPhongHoc_DAL(DataRow i)
        {
            return new PHONGHOC
            {
                IdPhong = i["IdPhong"].ToString(),
                IdKhu = i["IdKhu"].ToString(),
                STTPH = Convert.ToInt32(i["STTPH"].ToString()),
                X = Convert.ToInt32(i["X"].ToString()),
                Y = Convert.ToInt32(i["Y"].ToString()),
                Z = Convert.ToInt32(i["Z"].ToString()),
            };
        }
        public List<PHONGHOC> GetAllPhongHoc_DAL()
        {
            string Query = "select * from PHONGHOC";
            List<PHONGHOC> data = new List<PHONGHOC>();
            foreach (DataRow i in DBHelper.Instance.GetRecord(Query).Rows)
            {
                data.Add(GetPhongHoc_DAL(i));
            }
            return data;
        }
        public CAUTHANG GetCauThang_DAL(DataRow i)
        {
            return new CAUTHANG
            {
                IdCauThang = i["IdCauThang"].ToString(),
                IdKhu = i["IdKhu"].ToString(),
                STTCT = Convert.ToInt32(i["STTCT"].ToString()),
                X = Convert.ToInt32(i["X"].ToString()),
                Y = Convert.ToInt32(i["Y"].ToString()),
                Z = Convert.ToInt32(i["Z"].ToString()),
            };
        }
        public List<CAUTHANG> GetAllCauThang_DAL()
        {
            string Query = "select * from CauThang";
            List<CAUTHANG> data = new List<CAUTHANG>();
            foreach (DataRow i in DBHelper.Instance.GetRecord(Query).Rows)
            {
                data.Add(GetCauThang_DAL(i));
            }
            return data;
        }

        public ADDNODE GetAddNode_DAL(DataRow i)
        {
            return new ADDNODE
            {
                IdAddNode = i["IdAddNode"].ToString(),
                IdKhu = i["IdKhu"].ToString(),
                X = Convert.ToInt32(i["X"]),
                Y = Convert.ToInt32(i["Y"]),
            };
        }
        public List<ADDNODE> GetAllAddNode_DAL()
        {
            string Query = "Select * from ADDNODE";
            List<ADDNODE> data = new List<ADDNODE>();
            foreach (DataRow i in DBHelper.Instance.GetRecord(Query).Rows)
            {
                data.Add(GetAddNode_DAL(i));
            }
            return data;
        }
        public void InsertAddNode_DAL(Node node)
        {
            string Query = "insert into ADDNODE values ('" + node.name + "','" + node.IdKhu + "'," + node.x + "," + node.y + ")";
            DBHelper.Instance.UpdateCSDL(Query);
        }
        public void updateAddNode_DAL(Node node)
        {
            string Query = "update ADDNODE set X=" + node.x + ",Y=" + node.y + ",IdKhu=" +node.IdKhu+ " where IdAddNode='"+node.name+"'";
            DBHelper.Instance.UpdateCSDL(Query);
        }
    }
}

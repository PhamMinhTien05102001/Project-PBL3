using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Đồ_án_3_lớp.DTO
{
    public class Model
    {
        public Point startPossition { get; set; }
        public int numberOfRoom { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Model(Point point, int v, int w, int h)
        {
            startPossition = point;
            numberOfRoom = v;
            width = w;
            height = h;
        }
    }
}

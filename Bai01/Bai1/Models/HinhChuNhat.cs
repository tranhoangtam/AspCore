using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bai1.Models
{
    public class HinhChuNhat
    {
        //Field
        private double dai;
        private double rong;
        
        public double Dai
        {
            get => dai;
            set
            {
                if (value > 0)
                    dai = value;
                else
                    throw new Exception("Dài âm");
            }
        }
        public double Rong
        { get => rong; set => rong = value; }

        public double ChuVi => (dai + rong) * 2;//C# 7.0
        public double DienTich
        {
            get
            {
                return dai * rong;
            }
        }

        public string Chuoi()
        {
            return $"Hình chữ nhật: dài {dai}, rộng {rong} có diện tích {DienTich}, chu vi {ChuVi}";
        }
    }
}

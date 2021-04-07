using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FigureCreate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public struct Simple
        {
            public double xx; public double yy; public int ii;
        };
        FileInfo my_file = new FileInfo("SCRATCH");
        Simple s;
        double middle(double x1, double x2)
        { return (x1 + x2) / 2; }
        void star(double xc, double yc, double r)
        {
            double phi, phi_, r_half, r_double, factor = 0.0174533;
            double temp_x, temp_y;
            int i;
            if (r < 0.1) return;
            using (BinaryWriter fw = new BinaryWriter(my_file.Open(FileMode.Open,
            FileAccess.Write, FileShare.Write)))

            {
                fw.Seek(0, SeekOrigin.End);
                s.xx = xc + r; s.yy = yc; s.ii = 0;
                fw.Write(s.xx); fw.Write(s.yy); fw.Write(s.ii);
                for (i = 1; i <= 3; i++)
                {
                    phi = i * 120 * factor;
                    phi_ = (i + 1) * 120 * factor;
                    s.xx = xc; s.yy = yc; s.ii = 1;
                    fw.Write(s.xx); fw.Write(s.yy); fw.Write(s.ii);

                    s.xx = xc + r * Math.Cos(phi); s.yy = yc + r * Math.Sin(phi); s.ii = 1;
                    fw.Write(s.xx); fw.Write(s.yy); fw.Write(s.ii);
                    temp_x = middle(xc + r * Math.Cos(phi), xc + r * Math.Cos(phi_));
                    temp_y = middle(yc + r * Math.Sin(phi), yc + r * Math.Sin(phi_));
                    temp_x = middle(temp_x, xc);
                    temp_y = middle(temp_y, yc);

                    s.xx = temp_x; s.yy = temp_y; s.ii = 1;
                    fw.Write(s.xx); fw.Write(s.yy); fw.Write(s.ii);

                    s.xx = xc + r * Math.Cos(phi_); s.yy = yc + r * Math.Sin(phi_); s.ii = 1;
                    fw.Write(s.xx); fw.Write(s.yy); fw.Write(s.ii);
                }
                fw.Close();
            }
            r_half = r * 0.5;
            r_double = 2 * r;
            for (i = 0; i < 5; i++)
            {
                phi = (36 + i * 72) * factor;

                star(xc + r_double * Math.Cos(phi), yc + r_double * Math.Sin(phi), r_half);

            }
        }

            private void button1_Click(object sender, EventArgs e)
        {
            using (BinaryWriter fw = new BinaryWriter(my_file.Open(FileMode.Create, FileAccess.Write, FileShare.Write)))

            {
                fw.Close();
            }
            star(0.0, 0.0, 1);
            MessageBox.Show("Запись в файл произведена успешно!");
        }
    }
}

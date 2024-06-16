using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NTP_ÖDEV_1
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            string line = "";
            try
            {
                StreamReader sr = new StreamReader(@"C:\Users\faruk\Desktop\makine.txt");
                line = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string[] dizi = line.Split('-');


            if (comboBox1.Text == "Tüm Makineler")
            {

                int satir = dizi.Length / 4;
                dataGridView1.Rows.Add(satir);

                int indis = 0;
                for (int i = 0; i < satir; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = dizi[indis];
                        indis++;
                    }
                }

                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }
                chart1.Titles.Clear();
                chart1.Titles.Add("Tüm Makineler");
                chart1.Titles.Add("Makine 1(5),Makine 2(5),Makine 3(5)");
                for (int i = 1; i < dizi.Length; i += 4)
                {
                    chart1.Series["Hedef Miktar"].Points.AddXY(dizi[i], dizi[i + 1]);
                    chart1.Series["Üretilen Miktar"].Points.AddXY(dizi[i], dizi[i + 2]);
                }
            }
            else
            {
                List<string> list = new List<string>();
                string makine = comboBox1.Text;

                for (int i = 0; i < dizi.Length; i += 4)
                {
                    if (makine == dizi[i])
                    {
                        list.Add(dizi[i]);
                        list.Add(dizi[i + 1]);
                        list.Add(dizi[i + 2]);
                        list.Add(dizi[i + 3]);
                    }
                }

                int basIndis = 0;
                int bitIndis = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    if (dateTimePicker1.Value.ToShortDateString() == list[i])
                    {
                        basIndis = i - 1;
                    }
                    if (dateTimePicker2.Value.ToShortDateString() == list[i])
                    {
                        bitIndis = i + 3;
                    }
                }
                List<string> list2 = new List<string>();
                for (int i = basIndis; i < bitIndis; i++)
                {
                    list2.Add(list[i]);
                }
                int satir = list2.Count / 4;

                dataGridView1.Rows.Add(satir);

                int indis = 0;
                for (int i = 0; i < satir; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = list2[indis];
                        indis++;
                    }
                }
                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }
                chart1.Titles.Clear();


                chart1.Titles.Add(list2[0]);
                for (int i = 1; i < list2.Count; i += 4)
                {
                    chart1.Series["Hedef Miktar"].Points.AddXY(list2[i], list2[i + 1]);
                    chart1.Series["Üretilen Miktar"].Points.AddXY(list2[i], list2[i + 2]);
                }

            }
        }
    }
}

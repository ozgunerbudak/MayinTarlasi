using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MayinTarlasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MayinTarlasi mayin_tarlamiz;
        Image Mayin_Resim = Image.FromFile(@"Mayin.png");
        List<Mayin> mayinlar;
        private void Form1_Load(object sender, EventArgs e)
        {
            mayin_tarlamiz = new MayinTarlasi(new Size(400, 400), 20);
            panel1.Size = mayin_tarlamiz.buyuklugu;
            Mayin_ekle();
        }
        public void Mayin_ekle()
        {
            for (int x = 0; x <panel1.Width; x = x + 32)
            {
                for(int y=0;y <panel1.Height; y = y + 32)
                {
                    Button_ekle(new Point(x,y));
                }
            } 
        }
        public void Button_ekle(Point loc)
        {
            Button btn =new Button();
            btn.Name =  loc.X+""+loc.Y;
            btn.Size = new Size(32,32);
            btn.Location = loc;
            btn.Click += new EventHandler(btn_Click);
            panel1.Controls.Add(btn);
        }
        public void btn_Click(object sender, EventArgs e) 
        { 
            Button btn =(sender as Button);
            Mayin myn = mayin_tarlamiz.mayin_yeri(btn.Location);
            mayinlar = new List<Mayin>();
            if (myn.mayin_kontrolu)
            {
                MessageBox.Show("Kaybettin");
                Mayinlari_goster();
            }
            else
            {
                int s= etrafta_kac_mayin_var(myn);
                if (s==0)
                {
                    mayinlar.Add(myn);
                    for (int i=0; i<mayinlar.Count; i++)
                    {
                        Mayin item = mayinlar[i];
                        if (item.bakildi_ != null)
                        {
                            if (item.bakildi_==false&&item.mayin_kontrolu==false)
                            {
                                Button btnx = (Button)panel1.Controls.Find(item.konum_al.X + "" + item.konum_al.Y, false)[0];

                                if (etrafta_kac_mayin_var(mayinlar[i]) == 0)
                                {
                                    btnx.Enabled = false;
                                    item.bakildi_ = true;
                                    cevresindekileri_ekle(item);
                                }
                                else
                                {
                                    btnx.Text = etrafta_kac_mayin_var(item).ToString();
                                }
                                item.bakildi_ = true;
                            }
                        }
                        
                    }
                }
                else
                {
                    btn.Text=s.ToString();
                }
            }
        }
        public int etrafta_kac_mayin_var(Mayin m)
        {
            int sayi = 0;

            if (m.konum_al.X >0)
            {
                if (mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X -32, m.konum_al.Y )).mayin_kontrolu)
                {
                    sayi++;
                }

            }
                if (m.konum_al.Y < panel1.Height-32 && m.konum_al.X < panel1.Width-32)
            {
                if (mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X + 32, m.konum_al.Y + 32)).mayin_kontrolu)
                {
                    sayi++;
                }
            }  
            if (m.konum_al.X <panel1.Width-32) {
                if (mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X + 32, m.konum_al.Y)).mayin_kontrolu)
                {
                    sayi++;
                }
            }
            if (m.konum_al.X > 0&&m.konum_al.Y>0) {
                if (mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X - 32, m.konum_al.Y - 32)).mayin_kontrolu)
                {
                    sayi++;
                }
            }           
            if (m.konum_al.Y > 0)
            {
                if (mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X, m.konum_al.Y - 32)).mayin_kontrolu)
                {
                    sayi++;
                }
            }
            if (m.konum_al.X > 0&&m.konum_al.Y<panel1.Height-32)
            {
                if (mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X - 32, m.konum_al.Y + 32)).mayin_kontrolu)
                {
                    sayi++;
                }
            }
            if (m.konum_al.Y < panel1.Height-32)
            {
                if (mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X, m.konum_al.Y + 32)).mayin_kontrolu)
                {
                    sayi++;

                }
            }           
            if (m.konum_al.X > panel1.Width-32&&m.konum_al.Y>0)
            {
                if (mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X + 32, m.konum_al.Y - 32)).mayin_kontrolu)
                {
                    sayi++;
                }
            }         
            return sayi;
        }
        public void cevresindekileri_ekle(Mayin m)
        {
            bool b1=false;
            bool b2=false;
            bool b3=false;
            bool b4=false;
            if (m.konum_al.X > 0)
            {
                mayinlar.Add(mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X - 32, m.konum_al.Y)));
                b1 = true;
            }
            if (m.konum_al.Y > 0)
            {
                mayinlar.Add(mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X , m.konum_al.Y - 32)));
                b2 = true;
            }
            if (m.konum_al.X < panel1.Width)
            {
                mayinlar.Add(mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X + 32, m.konum_al.Y)));
                b3=true;
            }
            if (m.konum_al.Y < panel1.Height)
            {
                mayinlar.Add(mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X , m.konum_al.Y + 32)));
                b4 = true;
            }
            if (b1 & b2)
            {
                mayinlar.Add(mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X - 32, m.konum_al.Y-32)));
            }
            if (b1 & b4)
            {
                mayinlar.Add(mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X - 32, m.konum_al.Y + 32)));
            }
            if (b2 & b3)
            {
                mayinlar.Add(mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X + 32, m.konum_al.Y - 32)));
            }
            if (b1 & b4)
            {
                mayinlar.Add(mayin_tarlamiz.mayin_yeri(new Point(m.konum_al.X + 32, m.konum_al.Y + 32)));
            }
        }
        public void Mayinlari_goster()
        {
            foreach(Mayin item in mayin_tarlamiz.GetAllMayin)
            {
                if (item.mayin_kontrolu)
                {
                    Button btn = (Button)panel1.Controls.Find(item.konum_al.X + "" + item.konum_al.Y, false)[0];
                        btn.BackgroundImage = Mayin_Resim;
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }
    }
}

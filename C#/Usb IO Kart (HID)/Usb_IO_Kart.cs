using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsbLibrary;

namespace Usb_IO_Kart
{
    public partial class Usb_IO_Kart : Form
    {
        private UsbHidPort Usb_1 = new UsbHidPort();  // Projenin Target CPU x86 Olmalıdır. Çünkü usblibrary x86 Desteklemektedir.
        
        public Usb_IO_Kart()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Usb_1.RegisterHandle(Handle);
           
        }

        protected override void WndProc(ref Message m)
        {
            
            Usb_1.ParseMessages(ref m);
                     
            base.WndProc(ref m);
        }

       


        private void Usb1_Baglanan_Cihaz(System.Object sender, System.EventArgs e)
        {
           
            Usb1_Cihaz_Durumu_Label.Text = "Bağlı";
            Usb1_Cihaz_Durumu_Label.ForeColor = Color.Green;

            NotifyIcon Popup = new NotifyIcon();
            Popup.Icon = Usb_IO_Kart__HID_.Properties.Resources._957;
            Popup.Visible = true;
            Popup.ShowBalloonTip(500, "Bilgi ", "Usb 1 Bağlı ! ", ToolTipIcon.Info);
            Popup.Visible = false;
            Popup.Dispose();
        }
        private void Usb1_Cıkan_Cihaz(System.Object sender, System.EventArgs e)
        {
            if (InvokeRequired)
                Invoke(new EventHandler(Usb1_Cıkan_Cihaz), new object[] { sender, e });
        }
        private void Usb1_OnSpecifiedDeviceArrived(System.Object sender, System.EventArgs e)
        {
        }

        private void Usb1_OnSpecifiedDeviceRemoved(System.Object sender, System.EventArgs e)
        {
           
            
            Usb1_Cihaz_Durumu_Label.Text = "Bağlı Değil";
            Usb1_Cihaz_Durumu_Label.ForeColor = Color.Red;
            Usb1_input_1_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;
            Usb1_input_2_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;
            Usb1_input_3_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;
            Usb1_input_4_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;

            Usb1_Role_1_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;
            Usb1_Role_2_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;
            Usb1_Role_3_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;
            Usb1_Role_4_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;


            NotifyIcon Popup = new NotifyIcon();
            Popup.Icon = Usb_IO_Kart__HID_.Properties.Resources._957;
            Popup.Visible = true;
            Popup.ShowBalloonTip(500, "Bilgi ", "Usb 1 Çıkarıldı ! ", ToolTipIcon.Warning);
            Popup.Visible = false;
            Popup.Dispose();
        }


        private void Usb1_Data_Oku(System.Object senders, UsbLibrary.DataRecievedEventArgs Argsc)
        {
            string GelenData = "";
            if (InvokeRequired)
                Invoke(new DataRecievedEventHandler(Usb1_Data_Oku), new object[] { senders, Argsc });
            else
            {
                try
                {
                    byte[] GelenByteDizesi = new byte[9];
                    GelenByteDizesi = Argsc.data;
                    for (var i = 1; i <= 8; i++)
                        GelenData +=  GelenByteDizesi[i].ToString();
                }


                catch (Exception ex)


                {

                    MessageBox.Show(ex.Message.ToString());

                }

                if (GelenData.Length == 8)
                {
                    if (GelenData.Substring(0, 1) == "1")
                        Usb1_input_1_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Connect;
                    else
                        Usb1_input_1_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;

                    if (GelenData.Substring(1, 1) == "1")
                        Usb1_input_2_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Connect;
                    else
                        Usb1_input_2_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;

                    if (GelenData.Substring(2, 1) == "1")
                        Usb1_input_3_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Connect;
                    else
                        Usb1_input_3_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;
                    if (GelenData.Substring(3, 1) == "1")
                        Usb1_input_4_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Connect;
                    else
                        Usb1_input_4_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;

                    if (GelenData.Substring(4, 1) == "1")
                        Usb1_Role_1_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Connect;
                    else
                        Usb1_Role_1_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;

                    if (GelenData.Substring(5, 1) == "1")
                        Usb1_Role_2_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Connect;
                    else
                        Usb1_Role_2_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;

                    if (GelenData.Substring(6, 1) == "1")
                        Usb1_Role_3_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Connect;
                    else
                        Usb1_Role_3_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;

                    if (GelenData.Substring(7, 1) == "1")
                        Usb1_Role_4_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Connect;
                    else
                        Usb1_Role_4_Picture.Image = Usb_IO_Kart__HID_.Properties.Resources.Disconnect;
                }
            }
        }


        private void Usb_IO_Kart_Load(object sender, EventArgs e)
        {
            Usb_1.VendorId = 1590;  // USB 2 İçin 1591 Yazınız.
            Usb_1.ProductId = 1590; // USB 2 İçin 1591 Yazınız.





            Usb_1.OnSpecifiedDeviceArrived += Usb1_OnSpecifiedDeviceArrived;
            Usb_1.OnSpecifiedDeviceRemoved += Usb1_OnSpecifiedDeviceRemoved;
            Usb_1.OnDeviceArrived += Usb1_Baglanan_Cihaz;
            Usb_1.OnDeviceRemoved += Usb1_Cıkan_Cihaz;
            Usb_1.OnDataRecieved += Usb1_Data_Oku;

            if (Usb_1.CheckDevicePresent() == true)
            {
                Usb1_Cihaz_Durumu_Label.Text = "Bağlı";
                Usb1_Cihaz_Durumu_Label.ForeColor = Color.Green;
            }
            else
            {
                Usb1_Cihaz_Durumu_Label.Text = "Bağlı Değil";
                Usb1_Cihaz_Durumu_Label.ForeColor = Color.Red;
            }
        }

        private void Usb1_Role_1_Tetik_Buton_Click(object sender, EventArgs e)
        {
          
            
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 10; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Sorgu_Timer_Tick(object sender, EventArgs e)
        {
            if (Durum_Sorgula_Check_1.Checked == true)
            {
                byte[] ByteDize = new byte[65];
                ByteDize[1] = 64; // 64 Sorgu Komutudur ve ASCII ' Q Harfine Denk Gelir Qestion=Soru

                if (Usb_1.SpecifiedDevice != null)
                {
                    if (Usb_1.SpecifiedDevice != null)
                        Usb_1.SpecifiedDevice.SendData(ByteDize);
                }
            }

           
                
        }

        private void Usb1_Role_1_Cek_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 11; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Usb1_Role_1_Bırak_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 12; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Usb1_Input_1_Alındı_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 51; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Usb1_Role_2_Tetik_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 20; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Usb1_Role_2_Cek_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 21; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Usb1_Role_2_Bırak_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 22; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Usb1_Input_2_Alındı_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 52; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Usb1_Role_3_Tetik_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 30; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Usb1_Role_3_Cek_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 31; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Usb1_Role_3_Bırak_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 32; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Usb1_Input_3_Alındı_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 53; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Usb1_Role_4_Tetik_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 40; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Usb1_Role_4_Cek_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 41; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Usb1_Role_4_Bırak_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 42; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Usb1_Input_4_Alındı_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
				ByteDize[1] = 54; // 2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
            }
        }

        private void Role_Surelerini_Ayarla_Buton_Click(object sender, EventArgs e)
        {
            if (Usb_1.SpecifiedDevice != null)
            {
                byte[] ByteDize = new byte[65];                  // 65 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
                ByteDize[1] = Convert.ToByte(Convert.ToChar("R"));                 // 1.Dijit İnput Durum Bildirme Kodudur.  2.Dijit Hangi İnput' a Ait  işlem Yapılacağını Bildirir.;
                ByteDize[2] = Convert.ToByte(Convert.ToChar("T")); 
                ByteDize[3] = Convert.ToByte(Usb1_Role_1_Suresi_Text.Text);
                ByteDize[4] = Convert.ToByte(Usb1_Role_2_Suresi_Text.Text);
                ByteDize[5] = Convert.ToByte(Usb1_Role_3_Suresi_Text.Text);
                ByteDize[6] = Convert.ToByte(Usb1_Role_4_Suresi_Text.Text);

                Usb_1.SpecifiedDevice.SendData(ByteDize); // Not : Bazı Durumlarda Okunan input Bilgisinin Bir Defa Gelmesini İsteriz. Bu Komut Gönderildiğinde İnput 1,2,3,4 Aktif olsa bile Aktif olduğuna dair bilgi Gelmez.
            }
        }

        private void Usb_IO_Kart_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sorgu_Timer.Stop();
        }

        private void Usb1_Role_1_Picture_Click(object sender, EventArgs e)
        {

        }
    }
}

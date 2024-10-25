using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoTBDS
{
    public partial class Login : Form
    {
        string sConection = "";
        List<Image> images = new List<Image>();
        string[] location = new string[25];

        SqlConnection conection;

        public Login()
        {
            InitializeComponent();
            location[0] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_1.jpg";
            location[1] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_2.jpg";
            location[2] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_3.jpg";
            location[3] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_4.jpg";
            location[4] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_5.jpg";
            location[5] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_6.jpg";
            location[6] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_7.jpg";
            location[7] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_8.jpg";
            location[8] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_9.jpg";
            location[9] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_10.jpg";
            location[10] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_11.jpg";
            location[11] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_12.jpg";
            location[12] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_13.jpg";
            location[13] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_14.jpg";
            location[14] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_15.jpg";
            location[15] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_16.jpg";
            location[16] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_17.jpg";
            location[17] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_18.jpg";
            location[18] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_19.jpg";
            location[19] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_20.jpg";
            location[20] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_21.jpg";
            location[21] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_22.jpg";
            location[22] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_23.jpg";
            location[23] = @"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_user_24.jpg";
            diapo();
        }
        private void diapo()
        {
            for (int i = 0; i < 23; i++)
            {
                Bitmap bmp = new Bitmap(location[i]);
                images.Add(bmp);
            }
            images.Add(Properties.Resources.textbox_user_24);
        }
        private void Login_Load(object sender, EventArgs e)
        {
            tbPass.UseSystemPasswordChar = true;
        }

        private void tbUser_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tbUser.Text.Length >= 0 && tbUser.Text.Length <= 15)
                {
                    pictureBox1.Image = images[tbUser.Text.Length - 1];
                    pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else if (tbUser.Text.Length < 0)
                    pictureBox1.Image = Properties.Resources.debut;
                else pictureBox1.Image = images[22];
                {

                }
            }
            catch (Exception ex)
            {

            }
            

        }

        private void tbUser_Click(object sender, EventArgs e)
        {
            if (tbUser.Text.Length >= 0)
                pictureBox1.Image = images[tbUser.Text.Length];
            else
                pictureBox1.Image = Properties.Resources.debut;
        }

        private void tbPass_Click(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                tbPass.UseSystemPasswordChar = false;
                Bitmap bm = new Bitmap(@"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_passwordObserved.jpg");
                pictureBox1.Image = bm;
            }
            else
            {
                tbPass.UseSystemPasswordChar = true;
                Bitmap bm = new Bitmap(@"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_password.png");
                pictureBox1.Image = bm;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                tbPass.UseSystemPasswordChar = false;
                Bitmap bm = new Bitmap(@"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_passwordObserved.jpg");
                pictureBox1.Image = bm;
            }
            else
            {
                tbPass.UseSystemPasswordChar = true;
                Bitmap bm = new Bitmap(@"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_password.png");
                pictureBox1.Image = bm;
            }
        }
       
        public bool SesionIniciada(string sConection)
        {
            bool id = false;
            try
            {
                conection = new SqlConnection(sConection);
                conection.Open();
                id = true;
            }
            catch
            {
                id = false;
            }
            finally
            {
                conection.Close();
            }
            return id;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            
           sConection = @"Data Source = DESKTOP-T1NMD9I; Initial Catalog = Master; User ID =" + tbUser.Text+"; Password ="+tbPass.Text;
            if(SesionIniciada(sConection))
            {
                Form1 window = new Form1();
                window.Show();
                Hide();
            }else
            {
                MessageBox.Show("Contraseña Incorrecta","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void tbUser_Enter(object sender, EventArgs e)
        {
            if (tbUser.Text.Length >= 0)
                pictureBox1.Image = images[tbUser.Text.Length];
            else
                pictureBox1.Image = Properties.Resources.debut;
        }

        private void tbPass_Enter(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                tbPass.UseSystemPasswordChar = false;
                Bitmap bm = new Bitmap(@"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_passwordObserved.jpg");
                pictureBox1.Image = bm;
            }
            else
            {
                tbPass.UseSystemPasswordChar = true;
                Bitmap bm = new Bitmap(@"C:\Users\1\source\repos\ProyectoTBDS\animation\textbox_password.png");
                pictureBox1.Image = bm;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            tbUser.Text = "sa";
            tbUser.Focus();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            tbPass.Focus();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registrar newWindow = new Registrar();
            newWindow.Show();
            Hide();
        }
    }
}

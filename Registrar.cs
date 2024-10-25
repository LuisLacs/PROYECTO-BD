using DevExpress.XtraEditors.Drawing;
using SQLConnectLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class Registrar : Form
    {
        SqlConnection conexion;
        SQLConexion sql = new SQLConexion();
        string DefaultLanguage, DefaultDatabase, MapeoDatabase,sCadena;

        public Registrar()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            MapeoDatabase = string.Empty;
            //KBTO del futuro ocupa averiguar como crear un nuevo usuario :)
            if (tbUser.Text == string.Empty && tbPass.Text == string.Empty)
            {
                MessageBox.Show("Campo 'usuario' y campo 'contraseña' vacios", "Error");
            }else if (tbUser.Text == string.Empty)
            {
                MessageBox.Show("Campo 'usuario' vacio","Error" );
            }else if(tbPass.Text == string.Empty)
            {
                MessageBox.Show("Campo 'contraseña' vacio","Error");
            }else if(tbPass.Text != tbConfirmPass.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden","Error");
            }else
            {
                DefaultLanguage = cbLenguage.Text;
                DefaultDatabase = cbDatabase.Text;

                foreach (string s in clbBaseDeDatos.CheckedItems)
                {
                   
                    MapeoDatabase += "ALTER AUTHORIZATION ON database :: "+s+ " to " + tbUser.Text +"; ";
                  
                }
                sCadena = "IF NOT EXISTS (SELECT name FROM [sys].[server_principals] " +
                "WHERE name = '" + tbUser.Text + "') " +
                "Begin " +
                "CREATE LOGIN " + tbUser.Text +
                " WITH PASSWORD = '" + tbPass.Text + "', " +
                "CHECK_POLICY = OFF, " +
                "CHECK_EXPIRATION = OFF, " +
                "DEFAULT_LANGUAGE = [" + DefaultLanguage + "], " +
                "DEFAULT_DATABASE = [" + DefaultDatabase + "] " +
                MapeoDatabase +
                "END ";

                if(CreateLogin(sCadena))
                {
                    MessageBox.Show("El usuario se ha creado correctamente.","Exitoso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Registro fallido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               

            }
            
        }
        public static bool CreateLogin(string SQuery)
        {
            bool TodoOk = false;
            try
            {
                // Cadena de conexión a la base de datos
                string connectionString = "Server=(local);Database=master;Trusted_Connection=True";

                // Crea una conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abre la conexión
                    connection.Open();

                    // Crea un comando SQL para crear el usuario
                    string createLoginQuery = SQuery;

                    // Crea un objeto SqlCommand
                    using (SqlCommand command = new SqlCommand(createLoginQuery, connection))
                    {
                        // Ejecuta el comando
                        command.ExecuteNonQuery();
                    }
                }
                TodoOk = true;
            }
            catch
            {
                TodoOk = false;
            }
            return TodoOk;
            
        }
       
        private void Registrar_Load(object sender, EventArgs e)
        {
           this.Size = new Size(330, 377);
            tbPass.UseSystemPasswordChar = true;
            tbConfirmPass.UseSystemPasswordChar = true;


        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lkParametros_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Size = new Size(650, 377);
                DataTable tablaBD = new DataTable();
                sql.BaseDeDatosServer(ref tablaBD);
                clbBaseDeDatos.Items.Clear();
                cbDatabase.Items.Clear();
                foreach (DataRow row in tablaBD.Rows)
                {
                    clbBaseDeDatos.Items.Add(row[0].ToString());
                    cbDatabase.Items.Add(row[0].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}");
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login NW = new Login();
            NW.Show();
            Hide();
        }
    }
}

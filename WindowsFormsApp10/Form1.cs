using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp10
{
    public partial class FormMain : Form
    {

        static public string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=zoldseges";


        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            refreshDGV("%");
        }

        void refreshDGV(string filter)
        {
            dataGridView1.Rows.Clear();
            
            var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand(
                "select eladas.datum, zoldseg.nev, zoldseg.egysegAr, eladas.mennyiseg " +
                "from eladas, zoldseg " +
                "where eladas.zoldseg=zoldseg.id " +
                $"and zoldseg.nev like '{filter}'", connection);
            connection.Open();
            var reader = command.ExecuteReader();
            int bevetel = 0;
            while (reader.Read())
            {
                bevetel = (int)reader[2] * (int)reader[3];
                dataGridView1.Rows.Add(reader[0], reader[1], bevetel);

            }

            connection.Close();




        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            refreshDGV("%"+textBox1.Text+"%");
        }
    }
}

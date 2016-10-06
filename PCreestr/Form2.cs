using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace PCreestr
{
    public partial class Form2 : Form
    {
        string s;
        public Form2(Form1 Form1)
        {
            InitializeComponent();
            s = Form1.btnFind.Name;
        }

        OleDbConnection mycon = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\PCreestrBD.mdb");

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                mycon.Open();
                //mycom = new OleDbCommand(@"SELECT * FROM 'Список ПК' ", mycon);
                //OleDbDataReader dr = mycom.ExecuteReader();
                DataSet dataSet = new DataSet();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(@"SELECT * FROM [Справочник Пользователи Интернет статика]", mycon);
                dataAdapter.Fill(dataSet, "ИмяПК");

                /*foreach (DataTable table in dataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        foreach (DataColumn column in table.Columns)
                        {
                            MessageBox.Show("" + row[column]);
                        }
                    }
                }*/
                
                //dataGridView1.DataSource = dataSet;
                //dataGridView1.DataMember = "Имя ПК";
                //dataGridView1.Columns[0].Visible = false;
                //dataGridView1.RowHeadersVisible = false;
                //dataGridView1.AllowUserToAddRows = false;
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;



                ///вот этот кусок все портит
                //mycon = new MySqlConnection(constr);
                //mycom = new MySqlCommand(@"SELECT * FROM gost.s_service ", mycon);
                //mycom.CommandType = CommandType.Text;
                //MySqlDataReader dr = mycom.ExecuteReader();
                //dt = new DataTable();
                //dt.Load(dr);
                //dataGridView1.DataSource = dt;

                mycon.Close();
                ///вот этот кусок все портит
            }
            catch (Exception ee)
            { MessageBox.Show("ERROR :: " + ee); }
        }
    }
}

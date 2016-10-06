using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.OleDb;



namespace PCreestr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
           
    }

        //список всех переменных
        string constr = "Server=servgost;" +
                                    "port=3306;" +
                                    "Database=gost;" +
                                    "Uid=admin;" +
                                    "Pwd=;" +
                                    "CharSet = cp1251;" +
                                    "Allow Zero Datetime=true; ";
        //string colName = "Имя ПК"; //колонка с названием (Сейчас это ГОСТ, в будущем должно быть название ПК)
        string upd;
        int colbutton = 0;

        public static string BDsource = @"D:\PCreestrBD.mdb";
        OleDbConnection mycon = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + BDsource);
        //OleDbCommand mycom;
        //DataTable dt;
        DataTable dtSpisok;
        DataSet ds = new DataSet();
        DataSet dataSet = new DataSet();
        RichTextBox rtb = new RichTextBox();

        private void Form1_Load(object sender, EventArgs e)
        {
            rtb.Visible = false;
            //dbConnect();
            dbConnect1();
        }

        
        public void dbConnect1()
        {
            try
            {
                mycon.Open();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(@"SELECT * FROM [Список ПК]", mycon);
                //dataAdapter.Fill(dataSet, "Список ПК");

               
                dataAdapter.FillSchema(ds, SchemaType.Source, "Список ПК");
                dataAdapter.Fill(ds, "Список ПК");

                
                dtSpisok = ds.Tables["Список ПК"];

                //MessageBox.Show(dtSpisok.Rows[0]["Имя ПК"].ToString() + " | " + dtSpisok.Rows.Count );
            }
            catch (Exception e)
            { MessageBox.Show("ERROR :: " + e); }

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            
            //int n = dtSpisok.Rows.Count;
            //Predstavitelstvo();
            Label[] label_PCname = new Label[dtSpisok.Rows.Count];
            //Label[] label_PCname = new Label[dataGridView1.RowCount];
            //RichTextBox[] rtb = new RichTextBox[dataGridView1.RowCount];

            var strExpr = "[Имя ПК] LIKE '" + textBox1.Text + "'";
            ds.Tables[0].DefaultView.RowFilter = strExpr;
            
            ///нужен фильтр по бд, иначе выводит все компы, что есть в базе....
            ///не есть гуд
            for (colbutton = 0; colbutton < dtSpisok.Rows.Count; colbutton++) //нумерация лэйблов начинается с 0 //n - количество строк в гриде //colbutton - текущая строка
            {
                label_PCname[colbutton] = new Label();
                label_PCname[colbutton].Font = new Font("Tobota", 10, FontStyle.Italic);
                label_PCname[colbutton].Name = /*"label_PCname" +*/ colbutton.ToString();
                label_PCname[colbutton].Text = dtSpisok.Rows[colbutton]["Имя ПК"].ToString() + "\n" + dtSpisok.Rows[colbutton]["Инвентарный номер"].ToString();
                label_PCname[colbutton].Width = 400;
                label_PCname[colbutton].Height = 40;
                label_PCname[colbutton].BorderStyle = BorderStyle.FixedSingle;
                label_PCname[colbutton].TextAlign = ContentAlignment.MiddleCenter;
                label_PCname[colbutton].Location = new Point(textBox1.Location.X, btnFind.Location.Y + ((btnFind.Height + 10) * (colbutton + 1)));
                label_PCname[colbutton].Click += new System.EventHandler(label_PCname_Click);
                this.Controls.Add(label_PCname[colbutton]);
            }
        }

        private void label_PCname_Click(object sender, EventArgs e)
        {
            int ii = 0;
            Label label_PCname = (sender) as Label; //для определения какой лэйбл нажали
            for (ii = 1; ii < dtSpisok.Columns.Count; ii++) //заполнение переменной upd для дальнейшего вкручивания в форму с инфой //ii - счетчик для столбцов. можно использовать как переменную. в данном случае начиная со 0го и до 25 // нужно прикрутить переменную для последнего столбца, или уж хер с ней....
            {
                if (Convert.ToString(dtSpisok.Rows[Convert.ToInt32(label_PCname.Name)][ii].ToString()) != "")
                {
                    upd = upd + dtSpisok.Columns[ii].ColumnName + ": " + dtSpisok.Rows[Convert.ToInt32(label_PCname.Name)][ii].ToString() + "\n";
                }
            }

            Form2 infForm = new Form2(this);

            infForm.label1.Text = label_PCname.Text;
            infForm.label1.Font = new Font("Tobota", 12, FontStyle.Italic);
            infForm.label1.BorderStyle = BorderStyle.FixedSingle;
            infForm.label1.TextAlign = ContentAlignment.MiddleCenter;
            infForm.label1.AutoSize = false;
            infForm.label1.Dock = DockStyle.Fill;

            infForm.label2.Text = "Интернет-статика";
            infForm.label2.Font = new Font("Tobota", 12, FontStyle.Italic);
            infForm.label2.BorderStyle = BorderStyle.FixedSingle;
            infForm.label2.TextAlign = ContentAlignment.MiddleCenter;
            infForm.label2.AutoSize = false;
            infForm.label2.Dock = DockStyle.Fill;

            infForm.label3.Text = "Лицензирование";
            infForm.label3.Font = new Font("Tobota", 12, FontStyle.Italic);
            infForm.label3.BorderStyle = BorderStyle.FixedSingle;
            infForm.label3.TextAlign = ContentAlignment.MiddleCenter;
            infForm.label3.AutoSize = false;
            infForm.label3.Dock = DockStyle.Fill;

            infForm.richTextBox1.ReadOnly = true;
            infForm.richTextBox1.Text = upd;
            infForm.richTextBox1.Font = new Font("Tobota", 10, FontStyle.Regular);
            infForm.richTextBox1.UseWaitCursor = false;
            infForm.richTextBox1.Dock = DockStyle.Fill;

            infForm.tableLayoutPanel1.Dock = DockStyle.Fill;
            //infForm.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            //infForm.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            //infForm.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            //infForm.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));

            infForm.Size = new System.Drawing.Size(400, infForm.richTextBox1.Height + infForm.label1.Height + 100);
            infForm.MinimumSize = infForm.Size;
            infForm.ShowDialog(); //показывает вторую форму, блокирует управление первой //полезно, если не надо будет сравнивать два ПК
            //infForm.Show(); // показывает вторую форму
            upd = "";
        }



































        public void dbConnect()
        {
            try
            {
                mycon.Open();
                //mycom = new OleDbCommand(@"SELECT * FROM '[Список ПК]' ", mycon);
                //OleDbDataReader dr = mycom.ExecuteReader();

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(@"SELECT * FROM [Список ПК]", mycon);
                dataAdapter.Fill(dataSet, "Имя ПК");

                dataGridView1.DataSource = dataSet;
                dataGridView1.DataMember = "Имя ПК";
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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
            catch (Exception e)
            { MessageBox.Show("ERROR :: " + e); }
        }

        public void Predstavitelstvo()
        {
            var strExpr = "[Имя ПК] LIKE '" + textBox1.Text + "'";

            dataSet.Tables[0].DefaultView.RowFilter = strExpr;
            //dt.DefaultView.RowFilter = string.Format("[" + colName + "] LIKE '%{0}%'", textBox1.Text);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
       
        

       

        
    }
}

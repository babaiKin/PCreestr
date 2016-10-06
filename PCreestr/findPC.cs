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
    public partial class findPC : Form
    {
        public findPC()
        {
            InitializeComponent();
        }

        //string myDirectory = string.Empty;    //    инициализируем директорию
        //DataTable Emptable = null;    //    таблицу с данными
        //DataSet Employees = null;    //    данные
        public static string BDsource = @"D:\PCreestrBD.mdb";
        OleDbConnection mycon = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + BDsource);
    }
}

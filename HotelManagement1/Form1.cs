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
// 1) line 10 librery should be add..............

namespace HotelManagement1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // 5 ) call the Load() Function...........
            Load();
        }

     // 2) sql connection.......................

        SqlConnection con = new SqlConnection("Data Source=LAPTOP-LCD211ID\\SQLEXPRESS;Initial Catalog=SchoolDB;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader reader;
        string id;
        string sql;
        SqlDataAdapter adapter;
        bool Mode = true;
        //if the mode is true means ADD records otherwise UPDATE the recode.. 


        // 4) loard the database's datas to the Data Grid View...........

        public void Load()
        {
            try
            {

                sql = "select * from StudentT";
                cmd = new SqlCommand(sql, con);
                con.Open();

                reader = cmd.ExecuteReader();

                dataGridView1.Rows.Clear();

                while(reader.Read())
                {

                    dataGridView1.Rows.Add(reader[0], reader[1], reader[2], reader[3]);

                }
                con.Close();



            }
            catch(Exception ex) 
            {

                MessageBox.Show(ex.Message);

            }

        }


    // 3) codes for save button..................
        private void btnsave_Click(object sender, EventArgs e)
        {
            string name = txtname.Text;
            string course = txtcourse.Text;
            string fee = txtfee.Text;

            if(Mode == true)
            {
                sql = "insert into StudentT(stdname,course,fee) values(@stdname,@course,@fee)";
                con.Open();
                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@stdname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);

                MessageBox.Show("Record added");
                cmd.ExecuteNonQuery();

                txtname.Clear();
                txtcourse.Clear();  
                txtfee.Clear();
                txtname.Focus();




            }
            else
            {

            }
            con.Close();

        }
    }
}

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
            Load1();
        }

     // 2) sql connection.......................

        SqlConnection con = new SqlConnection("Data Source=LAPTOP-LCD211ID\\SQLEXPRESS;Initial Catalog=SchoolDB;Integrated Security=True");
        SqlCommand cmd;
        //SqlDataReader reader;
        string id;
        string sql;
        SqlDataAdapter adapter;
        bool Mode = true;
        //if the mode is true means ADD records otherwise UPDATE the recode.. 

        // 4) loard the database's datas to the Data Grid View...........

       public void Load1()
        {
            try{
                con.Open();
                cmd = new SqlCommand("select * from StudentT", con);
                SqlDataReader reader = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (reader.Read()) {
                    dataGridView1.Rows.Add(reader[0], reader[1], reader[2], reader[3]);
                }
                con.Close();
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
        }

        // 5) Edit button code to edit the details...........

        public void getID(String id)    // 6) this getID function came from the dataGride View,there for this function should be call inside the dataGride view's code section---------------------
        {
            con.Open();
            cmd = new SqlCommand("  select * from StudentT where id = '" + id + "'    " , con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read()){
                //user click the edit button data should be pass to the text boxes

                txtname.Text = reader[1].ToString();
                txtcourse.Text = reader[2].ToString();
                txtfee.Text = reader[3].ToString();
            }
            con.Close();
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

     // 6),7) dataGride view's code section..................
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["editColumn"].Index && e.RowIndex >= 0)
            {    //mode is true = ADD records , mode is false = UPDATE the recode..
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);  // call the getID function
            }
        }
    }
}

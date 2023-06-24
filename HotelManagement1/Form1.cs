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

                MessageBox.Show("Record added...........");
                cmd.ExecuteNonQuery();

                txtname.Clear();
                txtcourse.Clear();  
                txtfee.Clear();
                txtname.Focus();

            }
            else
            {
                /* 8) this part is the, user clicked the edit button details will appiers on the text boxes,after edit the data and after user should click the 
                      save button again ,now details edit and save to the dataGrid view..................... */

                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                sql = "update StudentT set stdname = @stdname, course = @course, fee = @fee where id = @id ";
                con.Open();
                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@stdname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                cmd.Parameters.AddWithValue("@id", id);

                MessageBox.Show("Record Updated..........");
                cmd.ExecuteNonQuery();

                txtname.Clear();
                txtcourse.Clear();
                txtfee.Clear();
                txtname.Focus();

                // 10) After edited the recode again button change as "Save"....
                btnsave.Text = "Save";
                Mode = true;

            }
            con.Close();

        }

     // 6),7) dataGride view's code section..................
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                                                                                                                            //  [[ EDIT and DELETE button code in if else ]]

            if (e.ColumnIndex == dataGridView1.Columns["editColumn"].Index && e.RowIndex >= 0){    
                                                                                                                          //mode is true = ADD records , mode is false = UPDATE the recode..
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);                                                                                               // call the getID function
                                                                                                                         // 9) when user click the edit button in the data Gride view,after Save" button should be change as "Edit"..........
                btnsave.Text = "Edit";
            }
            else if (e.ColumnIndex == dataGridView1.Columns["deleteColumn"].Index && e.RowIndex >= 0){                  // 13) if user click the "delete" button, data should be deleted.........

                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from StudentT where id = @id ";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Recode deleted");
                con.Close();




            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnclear_Click(object sender, EventArgs e)
        {
                                                                                                                          // 12) if user want clear the text boxes user should be click the "Edit" button and anfter "Clear" button...............
            txtname.Clear();
            txtcourse.Clear();
            txtfee.Clear();
            txtname.Focus();

            btnsave.Text = "Save";
            Mode = true;
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            Load1();                                                                                                               // 11) same load() functon.....

        }
    }
}

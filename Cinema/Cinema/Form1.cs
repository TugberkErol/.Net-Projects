using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Data.SqlClient;
namespace Cinema
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }
        public string conString = "Data Source=DESKTOP-FAQH7KF\\SQLEXPRESS;Initial Catalog=ConnectionDB;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
        {
                WebClient c = new WebClient();
                Console.Write("Film Ara: ");
              //  String word = Console.ReadLine();

            String word = textBox1.Text;
            var data = c.DownloadString("http://www.omdbapi.com/?s="+word+"&apikey=b04cee91");
                JObject o = JObject.Parse(data);
          
         
            try
            {
                JArray items = (JArray)o["Search"];

                Console.WriteLine(items.Count);

                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine(o["Search"][i]["Title"] + " " + o["Search"][i]["Year"] + " " + o["Search"][i]["Type"]);
                }


            }

            catch (NullReferenceException)
            {
                Console.Write("Film Bulunamadi");
            }

            String url = "" + o["Search"][0]["Poster"] + "";
            String url2 = "" + o["Search"][0]["imdbID"] + "";
            String url3 = "https://www.imdb.com/title/" + url2;
            String movieName = "" + o["Search"][0]["Title"]+"";
            String movieYear = "" + o["Search"][0]["Year"];
            linkLabel1.Text = url3;
            label1.Text = movieName;
            label2.Text = movieYear;
            pictureBox1.ImageLocation=url;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("" + linkLabel1.Text + "");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if (con.State == System.Data.ConnectionState.Open)
            {
                string q="insert into cinemaDB(movie_name,movie_year) values(@value1,@value2)";
            
                SqlCommand command = new SqlCommand(q, con);
                command.Parameters.AddWithValue("@value1", label1.Text);
                command.Parameters.AddWithValue("@value2",label2.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Sql Server Saved!");
                con.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlDataAdapter da = new SqlDataAdapter("Select * From CinemaDB ", conn);
                DataTable table = new DataTable();
                da.Fill(table);
               
                dataGridView1.DataSource = table;
                conn.Close();
            }
        }
    }
}

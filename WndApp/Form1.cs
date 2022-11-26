using System.Data;
using System.Data.SqlClient;

namespace WndApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //En kötü senaryo(Tüm kodu doðrudan buraya yazmamak gerekir...)
            
            //Connection String
            string strConn = "Data source=.;initial catalog=Calisma;integrated security=true";

            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            //1.Kullaným:Dogrudan belirli verileri tabloya ekleme...
            //SqlCommand cmd = new SqlCommand("INSERT INTO Kitaplar VALUES('Denemeler','Montaigne','Deneme',44)", conn);

            //2.Yontem:Stringlerin birleþtirilmesi
            //!!!DÝKKAT !!! UYARI !!! IKAZ
            //Bu yöntemi kullanmayýn
            //SqlCommand cmd = new SqlCommand("INSERT INTO Kitaplar VALUES('"+txtKitapAdi.Text+"','"+txtYazar.Text+"','"+txtKategori.Text+"',"+txtFiyat.Text+")", conn);

            //3.Yontem:SqlParameter
            //Daha güvenli...
            //Bu yöntemi kullanmanýz tavsiye edilir...
            SqlCommand cmd = new SqlCommand("INSERT INTO Kitaplar VALUES(@ad,@yazar,@kategori,@fiyat)", conn);

            cmd.Parameters.AddWithValue("@ad", txtKitapAdi.Text);
            cmd.Parameters.AddWithValue("@yazar", txtYazar.Text);
            cmd.Parameters.AddWithValue("@kategori",txtKategori.Text);
            cmd.Parameters.AddWithValue("@fiyat", double.Parse(txtFiyat.Text));

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private void btnListele1_Click(object sender, EventArgs e)
        {
            string strConn = "Data source=.;initial catalog=Calisma;integrated security=true";

            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Kitaplar";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                //listBox1.Items.Add(reader["KitapID"] + " " + reader[1]);
                //listBox1.Items.Add(reader[0] + " " + reader[1]);

                string satir = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    satir += reader[i] + " ";
                }
                listBox1.Items.Add(satir);
            }
            conn.Close();

        }

        private void btnListele3_Click(object sender, EventArgs e)
        {
            string strConn = "Data source=.;initial catalog=Calisma;integrated security=true";

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kitaplar", strConn);

            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void btnListele2_Click(object sender, EventArgs e)
        {
            string strConn = "Data source=.;initial catalog=Calisma;integrated security=true";

            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Kitaplar";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();

            //Okunan verileri jenerik listeye at...
            List<Kitap> kitaplar = new List<Kitap>();
            while (reader.Read())
            {
                Kitap kitap = new Kitap();
                kitap.KitapID = Convert.ToInt32(reader[0]);
                kitap.KitapAdi = reader.GetString(1);
                kitap.Yazar = reader[2].ToString();
                kitap.Kategori = reader[3].ToString();
                kitap.Fiyat = reader.GetDecimal(4);

                kitaplar.Add(kitap);
            }
            conn.Close();
            dataGridView1.DataSource = kitaplar;
        }
    }
}
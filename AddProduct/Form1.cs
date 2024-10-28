using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeechLib;//konuşma tanıma ve metin okuma
using System.Speech.Recognition;
using System.Windows.Forms;
using static Microsoft.Scripting.PerfTrack;
using System.Diagnostics;
using System.Data.SqlClient;
using static IronPython.Modules.PythonSocket;

namespace AddProduct
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection("Data source=LAPTOP-FHN7GUE1\\SQLEXPRESS; Initial Catalog=DpProduct; Integrated Security=TRUE");
        public Form1()
        {
            InitializeComponent();
        }

        //----------------------------------------------------------------------------------

        DpProductEntities db = new DpProductEntities();   //burdaki amacım tablolarıma ulaşmak işin db adında bir nesne oluşturduk.


        //-----------------------------------------------------------------------------------
        public void ProductList()
        {
            var products = db.TABLEPRODUCT.ToList();  // dp.TABLEPRODUCT tablosundaki tüm ürünleri bir listeye dönüştürür.
            dataGridView1.DataSource = products;     //product'tan gelen değerlerimiz.
        }

        //----------------------------------------------------------------------------------
        void enabled()
        {
            txtbrandbox.Enabled = false;

            txtnamebox.Enabled = false;

            txtstockbox.Enabled = false;

            txtcategorybox.Enabled = false;

            txtdatebox.Enabled = false;

            txtsellbox.Enabled = false;

            txtbuybox.Enabled = false;

            casebox.Enabled = false;

            label10.Enabled = false;
        }


        //----------------------------------------------------------------------------------


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string userInput = richTextBox1.Text.ToLower(); // Kullanıcı girişi küçük harfe dönüştürülür.

            switch (userInput)
            {

                case "list":
                    ProductList(); // (Veritabanı varsa) Ürünleri listeler.
                    break;
                // ... (ürün ekleme, silme, marka, fiyat vb. diğer komutlar)
                case "visual open":
                    // Kullanıcı girişi ile programı aç (program_adı.exe ile değiştirin)
                    Process.Start("\"C:\\Users\\kilic\\Desktop\\PROGRAMLAR\\VisualStudio.exe\"");
                    break;
                default:
                    // Tanınmayan komut (isteğe bağlı: hata mesajı görüntüleme)
                    break;
            }


            if (richTextBox1.Text == "List of products" || richTextBox1.Text == "List products" || richTextBox1.Text == "List" || richTextBox1.Text == "list" || richTextBox1.Text == "list products")
            {
                ProductList();
            }
            //--------------------ADD------------------------------------------------

            if (richTextBox1.Text == "Add" || richTextBox1.Text == "Addition" || richTextBox1.Text == "Join" || richTextBox1.Text == "Joining") {



                try
                {
                    connection.Open();

                    decimal buyPrice = decimal.Parse(txtbuybox.Text);
                    decimal sellPrice = decimal.Parse(txtsellbox.Text);
                    string dateAdded = "";//txtdatebox.Text;//string.Parse(txtdatebox.Text);
                    short stock = short.Parse(txtstockbox.Text);
                    bool productCase;// bool.Parse(casebox.Text);
                    if (casebox.Text == "Active")
                    {
                        productCase = true;
                    }
                    else
                    {
                        productCase = false;
                    }



                    // ... diğer sayısal metin kutuları için benzer dönüşümler

                    string sql = "Insert into TABLEPRODUCT (NAME, BRAND, CATEGORY, BUYPRICE, SELLPRICE, STOCK, PRODUCTCASE, DATEADDPRO) values (@name, @brand, @category, @buyPrice, @sellPrice, @stock, @productCase, @dateAdded)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@name", txtnamebox.Text);
                    command.Parameters.AddWithValue("@brand", txtbrandbox.Text);
                    command.Parameters.AddWithValue("@category", txtcategorybox.Text);
                    command.Parameters.AddWithValue("@buyPrice", buyPrice);
                    command.Parameters.AddWithValue("@sellPrice", sellPrice);
                    command.Parameters.AddWithValue("@stock", stock);
                    command.Parameters.AddWithValue("@productCase", productCase);
                    command.Parameters.AddWithValue("@dateAdded", dateAdded);


                    // ... diğer metin kutuları için parametreler ekleyin
                    command.ExecuteNonQuery();

                    MessageBox.Show("Ürün başarıyla eklendi!"); // Veya uygun geri bildirimde bulunun
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ürün eklenirken hata: " + ex.Message); // Kullanıcıyı hatadan haberdar edin
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }



                /*
                connection.Open();
                SqlCommand command = new SqlCommand("Insert into TABLEPRODUCT (NAME,BRAND,CATEGORY, BUYPRICE,SELLPRICE,STOCK,PRODUCTCASE, DATEADDPRO) values  ('"+txtnamebox.Text+"','"+txtbrandbox.Text+"','"+txtcategorybox.Text+"',+txtbuybox.Text+,+txtsellbox.Text+,+txtstockbox.Text+,'"+casebox.Text+"','"+txtdatebox.Text+"')", connection);
                command.ExecuteNonQuery();

                
                TABLEPRODUCT t = new TABLEPRODUCT
                {
                         

                    NAME = txtnamebox.Text,
                    BRAND = txtbrandbox.Text,
                    CATEGORY = txtcategorybox.Text,
                    BUYPRICE = decimal.Parse(txtbuybox.Text),
                    SELLPRICE = decimal.Parse(txtstockbox.Text),
                    STOCK = short.Parse(txtstockbox.Text),
                    PRODUCTCASE = bool.Parse(casebox.Text),
                    DATEADDPRO = short.Parse(txtdatebox.Text),

                };
                db.TABLEPRODUCT.Add(t);
                db.SaveChanges();
                */
                // label10.Text = "Products saved in Database";
                // connection.Close();

            }
            //----------------------------------UPDATE-------------------------------------------

            if (richTextBox1.Text == "Update" || richTextBox1.Text == "update")
            {

                try
                {
                    connection.Open();

                    decimal buyPrice = decimal.Parse(txtbuybox.Text);
                    decimal sellPrice = decimal.Parse(txtsellbox.Text);
                    string dateAdded = "";//txtdatebox.Text;//string.Parse(txtdatebox.Text);
                    short stock = short.Parse(txtstockbox.Text);
                    bool productCase;// bool.Parse(casebox.Text);
                    if (casebox.Text == "Active")
                    {
                        productCase = true;
                    }
                    else
                    {
                        productCase = false;
                    }
                    string sql = "UPDATE TABLEPRODUCT SET NAME=@name, BRAND=@brand, STOCK=@stock,BUYPRICE=@buy,SELLPRICE=@sell,CATEGORY=@category,DATEADDPRO=@date,PRODUCTCASE=@case WHERE  ID=@ıd" ;
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ıd", Convert.ToInt32(txtıdbox.Text));
                    command.Parameters.AddWithValue("@name", txtnamebox.Text);
                    command.Parameters.AddWithValue("@brand", txtbrandbox.Text);
                    command.Parameters.AddWithValue("@category", txtcategorybox.Text);
                    command.Parameters.AddWithValue("@buy", buyPrice);
                    command.Parameters.AddWithValue("@sell", sellPrice);
                    command.Parameters.AddWithValue("@stock", stock);
                    command.Parameters.AddWithValue("@case", productCase);
                    command.Parameters.AddWithValue("@date", dateAdded);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Ürün başarıyla Güncellendi!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ürün güncellenirken  hata: " + ex.Message); // Kullanıcıyı hatadan haberdar edin
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

            }
            //----------------------------------DELETE-------------------------------------------

            if (richTextBox1.Text == "Delete" || richTextBox1.Text == "delete")
            {
                try
                {

                    connection.Open();
                    string sql = "DELETE FROM TABLEPRODUCT WHERE ID=@ıd ";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ıd",Convert.ToInt32( txtıdbox.Text));
                    command.ExecuteNonQuery();
                    MessageBox.Show("Ürün başarıyla Silindi!"); // Veya uygun geri bildirimde bulunun

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ürün silinirken hata: " + ex.Message); // Kullanıcıyı hatadan haberdar edin
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }

        
            //--------------------NAME--------------------------------------------------------------
            if (txtnamebox.BackColor == Color.Lavender  && txtnamebox.Enabled == true)
            {
                txtnamebox.Text = richTextBox1.Text;
                enabled();
            }
            if (richTextBox1.Text== "Name" || richTextBox1.Text == "name" || richTextBox1.Text == "Named" || richTextBox1.Text == "Main" || richTextBox1.Text == "The name" || richTextBox1.Text == "The main")
            {
                txtnamebox.Enabled=true;
                txtnamebox.Focus();
                txtnamebox.BackColor = Color.Lavender;
            }
            //---------------------BRAND-------------------------------------------------------------
            if (txtbrandbox.BackColor == Color.Lavender && txtbrandbox.Enabled == true)
            {
                txtbrandbox.Text = richTextBox1.Text;
                enabled();
            }
            if (richTextBox1.Text == "Brand" || richTextBox1.Text == "brand" || richTextBox1.Text == "Brands" || richTextBox1.Text == "Mark" || richTextBox1.Text == "mark")
            {
                
                txtbrandbox.Focus();
                txtbrandbox.BackColor = Color.Lavender;
                txtbrandbox.Enabled = true;
            }
            //---------------------SELL----------------------------------------------------------
            if (txtsellbox.BackColor == Color.Lavender && txtsellbox.Enabled == true)
            {
                txtsellbox.Text = richTextBox1.Text;
                enabled();
            }
            if (richTextBox1.Text == "Sell price" || richTextBox1.Text == "sell price" || richTextBox1.Text == "Sales" || richTextBox1.Text == "sales" || richTextBox1.Text == "sell" || richTextBox1.Text == "Sell")
            {
                txtsellbox.Enabled = true;
                txtsellbox.Focus();
                txtsellbox.BackColor = Color.Lavender;
            }
            //----------------------BUY--------------------------------------------------------
            if (txtbuybox.BackColor == Color.Lavender && txtbuybox.Enabled == true)
            {
                txtbuybox.Text = richTextBox1.Text;
                enabled();
            }
            if (richTextBox1.Text == "Buy price" || richTextBox1.Text == "buy price" || richTextBox1.Text == "Cost" ||  richTextBox1.Text == "cost" || richTextBox1.Text == "By" || richTextBox1.Text == "By price" || richTextBox1.Text == "Buying price" || richTextBox1.Text == "Buy" )
            {
                txtbuybox.Enabled = true;
                txtbuybox.Focus();
                txtbuybox.BackColor = Color.Lavender;
            }
            //----------------------DATE------------------------------------------------------
            if (txtdatebox.BackColor == Color.Lavender && txtdatebox.Enabled == true)
            {
                enabled();
            }

            if (richTextBox1.Text == "Date" || richTextBox1.Text == "date" || richTextBox1.Text == "Dates" || richTextBox1.Text == "dates" || richTextBox1.Text == "Time")
            {
                txtdatebox.Enabled = true;
                txtdatebox.Focus();
                txtdatebox.BackColor = Color.Lavender;
            }
           
            //---------------------CATEGORY-------------------------------------------------------
            if (txtcategorybox.BackColor == Color.Lavender && txtcategorybox.Enabled == true)
            {
                txtcategorybox.Text = richTextBox1.Text;
                enabled();
            }
            if (richTextBox1.Text == "Category" || richTextBox1.Text == "category" || richTextBox1.Text == "Set" || richTextBox1.Text == "set" || richTextBox1.Text == "Categories")
            {
                txtcategorybox.Enabled = true;
                txtcategorybox.Focus();
                txtcategorybox.BackColor = Color.Lavender;
            }
            //-----------------------CASE----------------------------------------------------

            if (casebox.BackColor == Color.Lavender && casebox.Enabled == true)
            {
                casebox.Text = "Active";
                enabled();
            }
            if (richTextBox1.Text == "Case" || richTextBox1.Text == "case" || richTextBox1.Text == "State" || richTextBox1.Text == "state" || richTextBox1.Text == "States" || richTextBox1.Text == "Event")
            {
                casebox.Enabled = true;
                casebox.Focus();
                casebox.BackColor = Color.Lavender;
            }
            //-----------------------STOCK---------------------------------------------------
            if (txtstockbox.BackColor == Color.Lavender && txtstockbox.Enabled == true)
            {
                txtstockbox.Enabled = true;
                txtstockbox.Text = richTextBox1.Text;
                enabled();
            }
            if (richTextBox1.Text == "Stock" || richTextBox1.Text == "stock" || richTextBox1.Text == "Stocks")
            {
                txtstockbox.Enabled = true;
                txtstockbox.Focus();
                txtstockbox.BackColor = Color.Lavender;
            }

            //----------------------------------------------------------------------------------
            if (richTextBox1.Text == "Exit application" || richTextBox1.Text == "exit application" || richTextBox1.Text == "Exit app" || richTextBox1.Text == "Stop" || richTextBox1.Text == "exit app" || richTextBox1.Text == "Exit" || richTextBox1.Text == "exit")
            {
                timer1.Stop();
                Application.Exit();
            }
           
        }
        //-------------------------------------------------------------------------------------------------------------------------------------


        private void btnspeak_Click(object sender, EventArgs e)
        {
            SpVoice ses = new SpVoice();
            ses.Speak(richTextBox1.Text);
        }
        //-----------------------------------------------------------------------------------
        private void btnListen_Click(object sender, EventArgs e)
        {
            try
            {
            SpeechRecognitionEngine sr = new SpeechRecognitionEngine();//konuşma tanıma
            Grammar gramer = new DictationGrammar();
            sr.LoadGrammar(gramer);
                // İsteğe bağlı: Gürültü azaltma ve mikrofon seçimi kullanın
                //sr.AudioProcessingConfig = new AudioProcessingConfig();
                btnListen.Text = "Please Speak";
                sr.SetInputToDefaultAudioDevice();
                RecognitionResult res = sr.Recognize();
                richTextBox1.Text = res.Text;
            }
            catch (Exception)
            {
                richTextBox1.Text = "Error!";
            }
        }
        //--------------------------------------------------------------------------------------------

        private void btnProductAdd_Click(object sender, EventArgs e)
        {
            

                /*

                // t.PRODUCTCASE = bool.Parse(casebox.Text);
                // t.DATEADDPRO = short.Parse(txtdatebox.Text);



                db.TABLEPRODUCT.Add(new TABLEPRODUCT
                {
                    NAME = txtnamebox.Text,
                    BRAND = txtbrandbox.Text,
                    STOCK = short.Parse(txtstockbox.Text),
                    BUYPRICE = decimal.Parse(txtbuybox.Text),
                    SELLPRICE = decimal.Parse(txtsellbox.Text),
                    CATEGORY = txtcategorybox.Text,
                    DATEADDPRO = short.Parse(txtdatebox.Text),
                    PRODUCTCASE = true
                });
                db.SaveChanges();
                label10.Enabled = true;
                label10.Text = "Products saved in Database";
            }



            private void btnProductAdd_Click(object sender, EventArgs e)
            {
                try
                {
                    // SQL enjeksiyonunu önlemek için parametreli sorgular ile SQL ifadesini hazırlayın
                    string sql = "INSERT INTO TABLEPRODUCT (NAME, BRAND, CATEGORY, BUYPRICE, SELLPRICE, STOCK, PRODUCTCASE, DATEADDPRO) VALUES (@Name, @Brand, @Category, @BuyPrice, @SellPrice, @Stock, @ProductCase, @DateAdded)";

                    // Manuel dize birleştirmeden kaçınmak için parametreleri kullanın
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                new SqlParameter("@Name", txtnamebox.Text),
                new SqlParameter("@Brand", txtbrandbox.Text),
                new SqlParameter("@Category", txtcategorybox.Text),
                new SqlParameter("@BuyPrice", decimal.Parse(txtbuybox.Text)),
                new SqlParameter("@SellPrice", decimal.Parse(txtsellbox.Text)),
                new SqlParameter("@Stock", short.Parse(txtstockbox.Text)),
               // new SqlParameter("@ProductCase", bool.Parse(casebox.Text)),
                new SqlParameter("@DateAdded", DateTime.Now.ToString("MM.dd.yyyy")),
                    };

                    // SQL ifadesini parametrelerle çalıştırın
                    db.Database.ExecuteSqlCommand(sql, parameters);

                    label10.Enabled = true;
                    label10.Text = "Products Saved in Database";
                }
                catch (Exception ex)
                {
                    label10.Text = $"Error: {ex.Message}";
                }*/
            }
        
        private void Form1_Load(object sender, EventArgs e)
        {

           
           //enabled();
        //  timer1.Start();

        }

        private void txtdatebox_BackColorChanged(object sender, EventArgs e)
        {
            if (txtdatebox.BackColor== Color.Lavender)
            {
                txtdatebox.Text = DateTime.Now.ToString("MM.dd.yyyy");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label10_TextChanged(object sender, EventArgs e)
        {
            SpVoice ses = new SpVoice();
            ses.Speak(label10.Text);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            intro form = new intro();
            form.Show();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtıdbox.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtnamebox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtbrandbox.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtstockbox.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtbuybox.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtsellbox.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtcategorybox.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            casebox.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        private void textıdbox_TextChanged(object sender, EventArgs e)
        {

        }

       
       
        /*
private void timer1_Tick(object sender, EventArgs e)
{
try
{
SpeechRecognitionEngine sr = new SpeechRecognitionEngine();//konuşma tanıma
Grammar gramer = new DictationGrammar();
sr.LoadGrammar(gramer);
// İsteğe bağlı: Gürültü azaltma ve mikrofon seçimi kullanın
//sr.AudioProcessingConfig = new AudioProcessingConfig();
btnListen.Text = "Please Speak";
sr.SetInputToDefaultAudioDevice();
RecognitionResult res = sr.Recognize();
string text = res.Text;
richTextBox1.Text = text;
}
catch (Exception)
{
richTextBox1.Text = "Error!";
}
}
*/
    }
       
}

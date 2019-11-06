using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Курсова2._0
{
    public partial class InternetAndTelevisionCompany : Form
    {
        public const string FILE_NAME = "info.dat"; /*file name -- creted in same directory (project directory)*/
        public List<Contract> ContractList = new List<Contract>();

        public BinaryFormatter bf = new BinaryFormatter();
        public FileStream fs;
        private List<Contract> result;

        public InternetAndTelevisionCompany()
        {
            InitializeComponent();
            if (File.Exists(FILE_NAME))
            {
                fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                if (fs.Position < fs.Length)
                    ContractList = (List<Contract>)bf.Deserialize(fs);
                fs.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(FILE_NAME))
            {
                fs = new FileStream(FILE_NAME, FileMode.CreateNew);
            }
            contractBindingSource.DataSource = ContractList;

            listBox1.Items.Clear();
            int all = 0;
            var query = ContractList.Select(x => x.Adress).GroupBy(s => s).Select(g => new { Adres = g.Key, Count = g.Count() });
            foreach (var result in query)
            {
                if (result.Adres.Length > 7)
                {
                    listBox1.Items.Add(result.Adres + "\t" + result.Count);
                }
                else
                {
                    listBox1.Items.Add(result.Adres + "\t\t" + result.Count);
                }
                all += result.Count;
                
            }
            listBox1.Items.Add("----------------------------------");
            listBox1.Items.Add("Total number in data bese:\t" + all);
            string city = "";
            //int tax = 0;
            var result1 = ContractList.GroupBy(o => o.Adress)
              .Select(grp => new Contract
              {
                  Adress = grp.Key,
                  Tax = grp.Sum(o => o.Tax)
              });
            double max = 0;
            
            foreach (var item in result1)
            {
                if (max < item.Tax)
                {
                    max = item.Tax;
                    city = item.Adress;
                }
            }
            listBox1.Items.Add("----------------------------------");
            listBox1.Items.Add("Most profit town");
            if (city.Length>7)
            {
                listBox1.Items.Add(city + "\t" + max);
            }
            else
            {
                listBox1.Items.Add(city + "\t\t" + max);
            }
            
            label1.Text = "Total number of contracts " + Convert.ToString(ContractList.Count);
        }

        private void contractBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            fs = new FileStream(FILE_NAME, FileMode.OpenOrCreate);

            {
                bf.Serialize(fs, ContractList);
                fs.Close();
            }

        }

        private void BT1_Click(object sender, EventArgs e)
        {

            listBox1.Items.Clear();
            int all = 0;
            var query = ContractList.Select(x => x.Adress).GroupBy(s => s).Select(g => new { Adres = g.Key, Count = g.Count() });
            foreach (var result in query)
            {
                listBox1.Items.Add(result.Adres + "\t\t"+result.Count);
                all += result.Count;
            }
            listBox1.Items.Add("----------------------------------");
            listBox1.Items.Add("Total number in data base:\t" + all);
        }

        private void TB1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void contractBindingNavigator_TextChanged(object sender, EventArgs e)
        {

            if (EnterName.Text.Length == 0)
            {
                dataGridView1.Rows.Clear();

            }
            else
            {


                dataGridView1.Rows.Clear();
                result = ContractList.FindAll(delegate
                (Contract c)
                {
                    string a = c.Name.ToUpper();
                    string b = EnterName.Text.ToUpper().ToString();
                    return a.Contains(b);
                });
                if (result.Count != 0)
                {
                    foreach (Contract c in result)
                    {
                        dataGridView1.Rows.Add(c.ID, c.Date, c.Adress, c.Name, c.Type, c.Tax);
                    }

                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
            dataGridView1.Rows.Clear();
        }

        private void contractDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure", "Exit",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void информацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 info = new Form2();
            info.ShowDialog();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
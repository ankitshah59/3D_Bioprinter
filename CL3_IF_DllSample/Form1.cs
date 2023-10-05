using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CL3_IF_DllSample
{
    public partial class Form1 : Form
    {
        private List<string> items = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {

        }

        private void removeButton_Click(object sender, EventArgs e)
        {

        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newItem = itemTextBox.Text.Trim();
            if (newItem != "")
            {
                items.Add(newItem);
                itemListBox.DataSource = null;
                itemListBox.DataSource = items;
                itemTextBox.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int selectedIndex = itemListBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                items.RemoveAt(selectedIndex);
                itemListBox.DataSource = null;
                itemListBox.DataSource = items;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int selectedIndex = itemListBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                string updatedItem = itemTextBox.Text.Trim();
                if (updatedItem != "")
                {
                    items[selectedIndex] = updatedItem;
                    itemListBox.DataSource = null;
                    itemListBox.DataSource = items;
                    itemTextBox.Clear();
                }
            }
        }
    }
}

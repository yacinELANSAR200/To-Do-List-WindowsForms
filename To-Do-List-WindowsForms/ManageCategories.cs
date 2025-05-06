using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_Do_List_WindowsForms
{
    public partial class ManageCategories : Form
    {
        Form1 frm1;
        byte Id = 0;

        public ManageCategories(Form1 mainForm)
        {
            InitializeComponent();
            this.frm1 = mainForm;
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                return;

            }

            if (btnAddCategory.Text == "Edit")
            {
                foreach (ListViewItem itm in listView1.Items)
                {
                    if (itm.Selected)
                    {
                        itm.SubItems[1].Text = textBox1.Text;
                        frm1.editCategoryToMenuStrip(textBox1.Text,itm.Index);
                        textBox1.Clear();
                        btnAddCategory.Text = "Add";
                        return ;
                    }
                }

            }
            ListViewItem item = new ListViewItem((++Id).ToString());
            item.SubItems.Add(textBox1.Text);
            frm1.addCategoryToMenuStrip(textBox1.Text);
            listView1.Items.Add(item);
            textBox1.Clear();
            btnAddCategory.Text = "Add";
            
        }

       

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                e.Cancel = true;
                textBox1.Focus();
                errorProvider1.SetError(textBox1, " Category Name should have a value");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox1, "");
            }
        }

        

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //in case you want to delete selected items only
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Selected)
                {
                    listView1.Items.RemoveAt(item.Index);
                }
            }
            //in case you want to delete all items
            /*
             while (listView1.Items.Count>0)
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
            }
            */
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            btnAddCategory.Text = "Edit";
            
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Selected)
                {
                    textBox1.Text = item.SubItems[1].Text; 
                    break;
                }
            }

           
        }

    }
}

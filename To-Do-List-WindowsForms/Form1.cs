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
    public partial class Form1 : Form
    {
        byte Id = 0;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

        }
        public void addCategoryToMenuStrip(string CategoryName)
        {
            
            menuStrip1.Items.Add(CategoryName);
            comboBoxCategory.Items.Add(CategoryName);
        }
        public void editCategoryToMenuStrip(string CategoryName,int index)
        {
            comboBoxCategory.Items[index] = CategoryName;
            index++;
            menuStrip1.Items[index].Text = CategoryName;
        }
        
        private void btnManageCategories_Click(object sender, EventArgs e)
        {

            ManageCategories manageForm = new ManageCategories(this);
           
            manageForm.Show();
            
        }
        bool checkFields()
        {
            if (string.IsNullOrEmpty(txtDescription.Text) || comboBoxCategory.SelectedIndex==-1
                )
            {
                return false;
            }
            return true;
        }
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            if (!checkFields())
            {
                return;
            }
            ListViewItem item = new ListViewItem((++Id).ToString());
            item.ImageKey = "pending.png";
            item.SubItems.Add(txtDescription.Text.Trim());
            item.SubItems.Add(comboBoxCategory.Text.Trim());
            item.SubItems.Add(dateTimePickerStart.Text);
            item.SubItems.Add(dateTimePickerEnd.Text);
            item.SubItems.Add("Pending");

            listView1.Items.Add(item);
           
            txtDescription.Clear();

        }

        private void tsmCompleteTask_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {

                if (item.Checked)
                {
                    if (item.SubItems[5].Text == "Pending")
                    {
                        item.SubItems[5].Text = "Completed";
                        item.ImageKey = "completed.png";

                    }
                    
                }
                else
                {
                    item.ImageKey = "pending.png";
                    item.SubItems[5].Text = "Pending";

                    item.Checked = false;
                }
            }
        }
        private bool checkElementIfExistInLV(ListView lv,ListViewItem item)
        {
            foreach (ListViewItem itm in lv.Items)
            {
                if (itm.SubItems[0].Text == item.SubItems[0].Text)
                {
                    return true;
                }
            }
            return false;
        }
        private void getItemsWithStatus()
        {
            foreach (ListViewItem item in listView1.Items)
            {
                ListViewItem itm = new ListViewItem(item.SubItems[0].Text);
                itm.SubItems.Add(item.SubItems[1].Text);
                itm.SubItems.Add(item.SubItems[2].Text);
                itm.SubItems.Add(item.SubItems[3].Text);
                itm.SubItems.Add(item.SubItems[4].Text);
                itm.SubItems.Add(item.SubItems[5].Text);
                if (itm.SubItems[5].Text == "Pending")
                {
                    
                    itm.ImageKey = "pending.png";
                    if (!checkElementIfExistInLV(listView3,itm))
                    {
                    listView3.Items.Add(itm);      
                        
                    }
                }
                else if(itm.SubItems[5].Text == "Completed")
                {
                        itm.ImageKey = "completed.png";
                    
                    itm.Checked = true;
                    if (!checkElementIfExistInLV(listView2, itm))
                    {
                        listView2.Items.Add(itm);

                    }
                }
            }
        }
        private void cleanListView(ListView lv)
        {
            while(lv.Items.Count>0)
            {
                lv.Items.Remove(lv.Items[0]);
            }
        }
        private void completedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getItemsWithStatus();
           listView2.Visible = true;
           listView1.Visible=false;
           listView3.Visible=false;
           listView4.Visible=false;
            cleanListView(listView3);

        }

        private void pendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getItemsWithStatus();
            listView3.Visible = true;
            listView1.Visible = false;
            listView2.Visible = false;
            listView4.Visible = false;
            cleanListView(listView2);

        }
        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Visible = true;
            listView2.Visible = false;
            listView3.Visible = false;
            listView4.Visible = false;
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                e.Cancel = true;
                txtDescription.Focus();
                errorProvider1.SetError(txtDescription, $"description should have a value");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtDescription, "");
            }
        }

        private void comboBoxCategory_Validating(object sender, CancelEventArgs e)
        {
            if (comboBoxCategory.SelectedIndex == -1)
            {

                e.Cancel = true;
                comboBoxCategory.Focus();
                errorProvider1.SetError(comboBoxCategory, $"A Category should be selected");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBoxCategory, "");
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string category = e.ClickedItem.Text;
            cleanListView(listView4);
            listView4.Visible = true;
            listView1.Visible = false;
            listView2.Visible = false;
            listView3.Visible = false;
            foreach (ListViewItem item in listView1.Items)
            {
                
                if (item.SubItems[2].Text==category)
                {
                    ListViewItem itm = new ListViewItem(item.SubItems[0].Text);
                    itm.SubItems.Add(item.SubItems[1].Text);
                    itm.SubItems.Add(item.SubItems[2].Text);
                    itm.SubItems.Add(item.SubItems[3].Text);
                    itm.SubItems.Add(item.SubItems[4].Text);
                    itm.SubItems.Add(item.SubItems[5].Text);
                    if (itm.SubItems[5].Text == "Completed")
                    {
                        itm.Checked = true;
                        itm.ImageKey = "completed.png";
                    }
                    else
                    {
                        itm.ImageKey = "pending.png";

                    }
                    listView4.Items.Add(itm);  
                }
            }

        }

        private void btnGuide_Click(object sender, EventArgs e)
        {
            if (tabControl1.Visible == true)
            {
                tabControl1.Visible = false;
            }
            else
                tabControl1.Visible=true;
        }

        private void deleteTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Checked )
                {
                    listView1.Items.RemoveAt(item.Index);
                   
                }
            }
            foreach (ListViewItem item in listView2.Items)
            {
                if (item.Checked )
                {
                    listView2.Items.RemoveAt(item.Index);
                   
                }
            }


        }
    }
}

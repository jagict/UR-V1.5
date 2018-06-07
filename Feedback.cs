using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UR_点动控制器
{
    public partial class Feedback : Form
    {
        public Feedback()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetDataObject(linkLabel1.Text);
            MessageBox.Show("已将邮箱复制到剪贴板");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetDataObject(linkLabel2.Text);
            MessageBox.Show("已将QQ号复制到剪贴板");
        }

        private void Feedback_Load(object sender, EventArgs e)
        {

        }



    }
}

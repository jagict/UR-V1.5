namespace UR_点动控制器
{
    partial class Painting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStrip_New = new System.Windows.Forms.ToolStripMenuItem();
            this.Output_Points_Output = new System.Windows.Forms.ToolStripMenuItem();
            this.textboxCommand = new System.Windows.Forms.RichTextBox();
            this.Output_Points_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Output_Image_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStrip_New,
            this.Output_Points_Output,
            this.Output_Points_Save,
            this.Output_Image_Save});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(980, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStrip_New
            // 
            this.ToolStrip_New.Name = "ToolStrip_New";
            this.ToolStrip_New.Size = new System.Drawing.Size(44, 21);
            this.ToolStrip_New.Text = "新建";
            this.ToolStrip_New.Click += new System.EventHandler(this.ToolStrip_New_Click);
            // 
            // Output_Points_Output
            // 
            this.Output_Points_Output.Name = "Output_Points_Output";
            this.Output_Points_Output.Size = new System.Drawing.Size(44, 21);
            this.Output_Points_Output.Text = "输出";
            this.Output_Points_Output.Click += new System.EventHandler(this.Output_Points_Output_Click);
            // 
            // textboxCommand
            // 
            this.textboxCommand.Location = new System.Drawing.Point(0, 445);
            this.textboxCommand.Name = "textboxCommand";
            this.textboxCommand.Size = new System.Drawing.Size(983, 84);
            this.textboxCommand.TabIndex = 2;
            this.textboxCommand.Text = "提示：先新建，然后鼠标左键双击开始绘制，右键单击结束绘制（鼠标左键不双击，则不记录任何点位）!";
            // 
            // Output_Points_Save
            // 
            this.Output_Points_Save.Name = "Output_Points_Save";
            this.Output_Points_Save.Size = new System.Drawing.Size(68, 21);
            this.Output_Points_Save.Text = "保存程序";
            this.Output_Points_Save.Click += new System.EventHandler(this.Output_Points_Save_Click);
            // 
            // Output_Image_Save
            // 
            this.Output_Image_Save.Name = "Output_Image_Save";
            this.Output_Image_Save.Size = new System.Drawing.Size(68, 21);
            this.Output_Image_Save.Text = "保存图像";
            this.Output_Image_Save.Click += new System.EventHandler(this.Output_Image_Save_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(980, 400);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // Painting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 528);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textboxCommand);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Painting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Painting";
            this.Load += new System.EventHandler(this.Painting_Load);
            this.SizeChanged += new System.EventHandler(this.Form_SizeChange);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form_MousePaint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.WindowDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStrip_New;
        private System.Windows.Forms.ToolStripMenuItem Output_Points_Output;
        private System.Windows.Forms.RichTextBox textboxCommand;
        private System.Windows.Forms.ToolStripMenuItem Output_Points_Save;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem Output_Image_Save;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
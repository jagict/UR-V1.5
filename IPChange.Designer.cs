namespace UR_点动控制器
{
    partial class IPChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IPChange));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChangeIP = new System.Windows.Forms.Button();
            this.btnAutoGetIP = new System.Windows.Forms.Button();
            this.CurrentIP = new System.Windows.Forms.TextBox();
            this.CurrentSubMask = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.OtherIP = new System.Windows.Forms.TextBox();
            this.btnPingIP = new System.Windows.Forms.Button();
            this.btnRefreshIP = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(24, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前IP：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(24, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "当前SubMask：";
            // 
            // btnChangeIP
            // 
            this.btnChangeIP.Location = new System.Drawing.Point(416, 13);
            this.btnChangeIP.Name = "btnChangeIP";
            this.btnChangeIP.Size = new System.Drawing.Size(56, 39);
            this.btnChangeIP.TabIndex = 2;
            this.btnChangeIP.Text = "修改";
            this.btnChangeIP.UseVisualStyleBackColor = true;
            this.btnChangeIP.Click += new System.EventHandler(this.btnChangeIP_Click);
            // 
            // btnAutoGetIP
            // 
            this.btnAutoGetIP.Location = new System.Drawing.Point(416, 64);
            this.btnAutoGetIP.Name = "btnAutoGetIP";
            this.btnAutoGetIP.Size = new System.Drawing.Size(123, 41);
            this.btnAutoGetIP.TabIndex = 3;
            this.btnAutoGetIP.Text = "自动获取";
            this.btnAutoGetIP.UseVisualStyleBackColor = true;
            this.btnAutoGetIP.Click += new System.EventHandler(this.btnAutoGetIP_Click);
            // 
            // CurrentIP
            // 
            this.CurrentIP.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CurrentIP.Location = new System.Drawing.Point(107, 19);
            this.CurrentIP.Name = "CurrentIP";
            this.CurrentIP.Size = new System.Drawing.Size(288, 33);
            this.CurrentIP.TabIndex = 4;
            // 
            // CurrentSubMask
            // 
            this.CurrentSubMask.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CurrentSubMask.Location = new System.Drawing.Point(170, 68);
            this.CurrentSubMask.Name = "CurrentSubMask";
            this.CurrentSubMask.Size = new System.Drawing.Size(225, 33);
            this.CurrentSubMask.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(24, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ping远程地址：";
            // 
            // OtherIP
            // 
            this.OtherIP.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OtherIP.Location = new System.Drawing.Point(170, 116);
            this.OtherIP.Name = "OtherIP";
            this.OtherIP.Size = new System.Drawing.Size(225, 33);
            this.OtherIP.TabIndex = 7;
            // 
            // btnPingIP
            // 
            this.btnPingIP.Location = new System.Drawing.Point(416, 111);
            this.btnPingIP.Name = "btnPingIP";
            this.btnPingIP.Size = new System.Drawing.Size(123, 41);
            this.btnPingIP.TabIndex = 8;
            this.btnPingIP.Text = "Ping";
            this.btnPingIP.UseVisualStyleBackColor = true;
            this.btnPingIP.Click += new System.EventHandler(this.btnPingIP_Click);
            // 
            // btnRefreshIP
            // 
            this.btnRefreshIP.Location = new System.Drawing.Point(478, 13);
            this.btnRefreshIP.Name = "btnRefreshIP";
            this.btnRefreshIP.Size = new System.Drawing.Size(61, 39);
            this.btnRefreshIP.TabIndex = 9;
            this.btnRefreshIP.Text = "刷新";
            this.btnRefreshIP.UseVisualStyleBackColor = true;
            this.btnRefreshIP.Click += new System.EventHandler(this.btnRefreshIP_Click);
            // 
            // IPChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 164);
            this.Controls.Add(this.btnRefreshIP);
            this.Controls.Add(this.btnPingIP);
            this.Controls.Add(this.OtherIP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CurrentSubMask);
            this.Controls.Add(this.CurrentIP);
            this.Controls.Add(this.btnAutoGetIP);
            this.Controls.Add(this.btnChangeIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IPChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IPChange";
            this.Load += new System.EventHandler(this.IPChange_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChangeIP;
        private System.Windows.Forms.Button btnAutoGetIP;
        private System.Windows.Forms.TextBox CurrentIP;
        private System.Windows.Forms.TextBox CurrentSubMask;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox OtherIP;
        private System.Windows.Forms.Button btnPingIP;
        private System.Windows.Forms.Button btnRefreshIP;
    }
}
namespace UR_点动控制器
{
    partial class Dashboard
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.label1 = new System.Windows.Forms.Label();
            this.txtProgramPath = new System.Windows.Forms.TextBox();
            this.btnGetCurrentProgram = new System.Windows.Forms.Button();
            this.btnLoadCurrentProgram = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustomCommand = new System.Windows.Forms.TextBox();
            this.btnSendCommand = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRunbtnRun = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnShutdown = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFeedback = new System.Windows.Forms.ListBox();
            this.FeedbackRightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RightMenu_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.RightMenu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.RightMenu_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.UserRoleBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.FeedbackRightMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "加载程序：";
            // 
            // txtProgramPath
            // 
            this.txtProgramPath.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProgramPath.Location = new System.Drawing.Point(12, 46);
            this.txtProgramPath.Name = "txtProgramPath";
            this.txtProgramPath.Size = new System.Drawing.Size(722, 33);
            this.txtProgramPath.TabIndex = 1;
            this.txtProgramPath.Text = "/programs/A/B.urp";
            // 
            // btnGetCurrentProgram
            // 
            this.btnGetCurrentProgram.Location = new System.Drawing.Point(323, 368);
            this.btnGetCurrentProgram.Name = "btnGetCurrentProgram";
            this.btnGetCurrentProgram.Size = new System.Drawing.Size(120, 41);
            this.btnGetCurrentProgram.TabIndex = 2;
            this.btnGetCurrentProgram.Text = "获取当前加载程序";
            this.btnGetCurrentProgram.UseVisualStyleBackColor = true;
            this.btnGetCurrentProgram.Click += new System.EventHandler(this.btnGetCurrentProgram_Click);
            // 
            // btnLoadCurrentProgram
            // 
            this.btnLoadCurrentProgram.Location = new System.Drawing.Point(634, 85);
            this.btnLoadCurrentProgram.Name = "btnLoadCurrentProgram";
            this.btnLoadCurrentProgram.Size = new System.Drawing.Size(100, 36);
            this.btnLoadCurrentProgram.TabIndex = 3;
            this.btnLoadCurrentProgram.Text = "加载该程序";
            this.btnLoadCurrentProgram.UseVisualStyleBackColor = true;
            this.btnLoadCurrentProgram.Click += new System.EventHandler(this.btnLoadCurrentProgram_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(7, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "自定义命令：";
            // 
            // txtCustomCommand
            // 
            this.txtCustomCommand.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCustomCommand.Location = new System.Drawing.Point(10, 158);
            this.txtCustomCommand.Name = "txtCustomCommand";
            this.txtCustomCommand.Size = new System.Drawing.Size(724, 33);
            this.txtCustomCommand.TabIndex = 5;
            this.txtCustomCommand.Text = "popup Hello World!!!";
            // 
            // btnSendCommand
            // 
            this.btnSendCommand.Location = new System.Drawing.Point(634, 197);
            this.btnSendCommand.Name = "btnSendCommand";
            this.btnSendCommand.Size = new System.Drawing.Size(100, 36);
            this.btnSendCommand.TabIndex = 6;
            this.btnSendCommand.Text = "发送";
            this.btnSendCommand.UseVisualStyleBackColor = true;
            this.btnSendCommand.Click += new System.EventHandler(this.btnSendCommand_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(12, 328);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "其他常用命令：";
            // 
            // btnRunbtnRun
            // 
            this.btnRunbtnRun.Location = new System.Drawing.Point(11, 368);
            this.btnRunbtnRun.Name = "btnRunbtnRun";
            this.btnRunbtnRun.Size = new System.Drawing.Size(66, 41);
            this.btnRunbtnRun.TabIndex = 8;
            this.btnRunbtnRun.Text = "运行";
            this.btnRunbtnRun.UseVisualStyleBackColor = true;
            this.btnRunbtnRun.Click += new System.EventHandler(this.btnRunbtnRun_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(83, 368);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(70, 41);
            this.btnPause.TabIndex = 9;
            this.btnPause.Text = "暂停";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(159, 368);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(68, 41);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnShutdown
            // 
            this.btnShutdown.Location = new System.Drawing.Point(233, 368);
            this.btnShutdown.Name = "btnShutdown";
            this.btnShutdown.Size = new System.Drawing.Size(68, 41);
            this.btnShutdown.TabIndex = 11;
            this.btnShutdown.Text = "关机";
            this.btnShutdown.UseVisualStyleBackColor = true;
            this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(7, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 25);
            this.label4.TabIndex = 12;
            this.label4.Text = "UR反馈信息：";
            // 
            // txtFeedback
            // 
            this.txtFeedback.ContextMenuStrip = this.FeedbackRightMenu;
            this.txtFeedback.FormattingEnabled = true;
            this.txtFeedback.ItemHeight = 12;
            this.txtFeedback.Location = new System.Drawing.Point(10, 265);
            this.txtFeedback.Name = "txtFeedback";
            this.txtFeedback.Size = new System.Drawing.Size(724, 88);
            this.txtFeedback.TabIndex = 13;
            // 
            // FeedbackRightMenu
            // 
            this.FeedbackRightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RightMenu_Copy,
            this.RightMenu_Delete,
            this.RightMenu_Clear});
            this.FeedbackRightMenu.Name = "contextMenuStrip1";
            this.FeedbackRightMenu.Size = new System.Drawing.Size(125, 70);
            // 
            // RightMenu_Copy
            // 
            this.RightMenu_Copy.Name = "RightMenu_Copy";
            this.RightMenu_Copy.Size = new System.Drawing.Size(124, 22);
            this.RightMenu_Copy.Text = "复制该行";
            this.RightMenu_Copy.Click += new System.EventHandler(this.RightMenu_Copy_Click);
            // 
            // RightMenu_Delete
            // 
            this.RightMenu_Delete.Name = "RightMenu_Delete";
            this.RightMenu_Delete.Size = new System.Drawing.Size(124, 22);
            this.RightMenu_Delete.Text = "删除这行";
            this.RightMenu_Delete.Click += new System.EventHandler(this.RightMenu_Delete_Click);
            // 
            // RightMenu_Clear
            // 
            this.RightMenu_Clear.Name = "RightMenu_Clear";
            this.RightMenu_Clear.Size = new System.Drawing.Size(124, 22);
            this.RightMenu_Clear.Text = "清空所有";
            this.RightMenu_Clear.Click += new System.EventHandler(this.RightMenu_Clear_Click);
            // 
            // UserRoleBox
            // 
            this.UserRoleBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UserRoleBox.FormattingEnabled = true;
            this.UserRoleBox.Location = new System.Drawing.Point(634, 386);
            this.UserRoleBox.Name = "UserRoleBox";
            this.UserRoleBox.Size = new System.Drawing.Size(87, 24);
            this.UserRoleBox.TabIndex = 15;
            this.UserRoleBox.Text = "程序员";
            this.UserRoleBox.SelectedIndexChanged += new System.EventHandler(this.ChangeRole);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(632, 368);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "设置用户角色：";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 422);
            this.Controls.Add(this.txtFeedback);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnShutdown);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.UserRoleBox);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnRunbtnRun);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSendCommand);
            this.Controls.Add(this.txtCustomCommand);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLoadCurrentProgram);
            this.Controls.Add(this.txtProgramPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetCurrentProgram);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.FeedbackRightMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProgramPath;
        private System.Windows.Forms.Button btnGetCurrentProgram;
        private System.Windows.Forms.Button btnLoadCurrentProgram;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustomCommand;
        private System.Windows.Forms.Button btnSendCommand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRunbtnRun;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnShutdown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox txtFeedback;
        private System.Windows.Forms.ComboBox UserRoleBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip FeedbackRightMenu;
        private System.Windows.Forms.ToolStripMenuItem RightMenu_Copy;
        private System.Windows.Forms.ToolStripMenuItem RightMenu_Delete;
        private System.Windows.Forms.ToolStripMenuItem RightMenu_Clear;
    }
}
namespace UR_点动控制器
{
    partial class Teach
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
            this.txtRecordTick = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStartTeach = new System.Windows.Forms.Button();
            this.btnStopTeach = new System.Windows.Forms.Button();
            this.btnShowTeachResult = new System.Windows.Forms.Button();
            this.TeachTimer = new System.Windows.Forms.Timer(this.components);
            this.btnSendTeachResult = new System.Windows.Forms.Button();
            this.TextBoxPointResult = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRecordLimit = new System.Windows.Forms.TextBox();
            this.labelCurrent = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAccleration = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRadius = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnResetTeachResult = new System.Windows.Forms.Button();
            this.btnSaveTeachResult = new System.Windows.Forms.Button();
            this.radioAdd_Nothing = new System.Windows.Forms.RadioButton();
            this.panelAdd = new System.Windows.Forms.Panel();
            this.radioAdd_Tool = new System.Windows.Forms.RadioButton();
            this.radioAdd_Base = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioMoveP = new System.Windows.Forms.RadioButton();
            this.radioMoveL = new System.Windows.Forms.RadioButton();
            this.radioMoveJ = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.panelAdd.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRecordTick
            // 
            this.txtRecordTick.Location = new System.Drawing.Point(83, 29);
            this.txtRecordTick.Name = "txtRecordTick";
            this.txtRecordTick.Size = new System.Drawing.Size(56, 21);
            this.txtRecordTick.TabIndex = 0;
            this.txtRecordTick.Text = "100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "采样频率：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "毫秒/次";
            // 
            // btnStartTeach
            // 
            this.btnStartTeach.Location = new System.Drawing.Point(493, 30);
            this.btnStartTeach.Name = "btnStartTeach";
            this.btnStartTeach.Size = new System.Drawing.Size(114, 28);
            this.btnStartTeach.TabIndex = 3;
            this.btnStartTeach.Text = "开始加强示教";
            this.btnStartTeach.UseVisualStyleBackColor = true;
            this.btnStartTeach.Click += new System.EventHandler(this.btnStartTeach_Click);
            // 
            // btnStopTeach
            // 
            this.btnStopTeach.Location = new System.Drawing.Point(615, 30);
            this.btnStopTeach.Name = "btnStopTeach";
            this.btnStopTeach.Size = new System.Drawing.Size(114, 28);
            this.btnStopTeach.TabIndex = 4;
            this.btnStopTeach.Text = "停止加强示教";
            this.btnStopTeach.UseVisualStyleBackColor = true;
            this.btnStopTeach.Click += new System.EventHandler(this.btnStopTeach_Click);
            // 
            // btnShowTeachResult
            // 
            this.btnShowTeachResult.Location = new System.Drawing.Point(862, 30);
            this.btnShowTeachResult.Name = "btnShowTeachResult";
            this.btnShowTeachResult.Size = new System.Drawing.Size(114, 28);
            this.btnShowTeachResult.TabIndex = 5;
            this.btnShowTeachResult.Text = "刷新示教输出";
            this.btnShowTeachResult.UseVisualStyleBackColor = true;
            this.btnShowTeachResult.Click += new System.EventHandler(this.btnShowTeach_Click);
            // 
            // TeachTimer
            // 
            this.TeachTimer.Tick += new System.EventHandler(this.TeachTimer_Tick);
            // 
            // btnSendTeachResult
            // 
            this.btnSendTeachResult.Location = new System.Drawing.Point(862, 64);
            this.btnSendTeachResult.Name = "btnSendTeachResult";
            this.btnSendTeachResult.Size = new System.Drawing.Size(114, 28);
            this.btnSendTeachResult.TabIndex = 6;
            this.btnSendTeachResult.Text = "直接发给机器人";
            this.btnSendTeachResult.UseVisualStyleBackColor = true;
            this.btnSendTeachResult.Click += new System.EventHandler(this.btnSendTeachResult_Click);
            // 
            // TextBoxPointResult
            // 
            this.TextBoxPointResult.Location = new System.Drawing.Point(12, 173);
            this.TextBoxPointResult.Name = "TextBoxPointResult";
            this.TextBoxPointResult.Size = new System.Drawing.Size(964, 283);
            this.TextBoxPointResult.TabIndex = 7;
            this.TextBoxPointResult.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "次";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "极限容量：";
            // 
            // txtRecordLimit
            // 
            this.txtRecordLimit.Location = new System.Drawing.Point(83, 64);
            this.txtRecordLimit.Name = "txtRecordLimit";
            this.txtRecordLimit.Size = new System.Drawing.Size(56, 21);
            this.txtRecordLimit.TabIndex = 8;
            this.txtRecordLimit.Text = "1000";
            // 
            // labelCurrent
            // 
            this.labelCurrent.AutoSize = true;
            this.labelCurrent.Location = new System.Drawing.Point(85, 104);
            this.labelCurrent.Name = "labelCurrent";
            this.labelCurrent.Size = new System.Drawing.Size(17, 12);
            this.labelCurrent.TabIndex = 13;
            this.labelCurrent.Text = "？";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "当前使用：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(336, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "m/s^2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(215, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "加速度：";
            // 
            // txtAccleration
            // 
            this.txtAccleration.Location = new System.Drawing.Point(274, 30);
            this.txtAccleration.Name = "txtAccleration";
            this.txtAccleration.Size = new System.Drawing.Size(56, 21);
            this.txtAccleration.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(336, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "m/s";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(215, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "速度：";
            // 
            // txtSpeed
            // 
            this.txtSpeed.Location = new System.Drawing.Point(274, 63);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(56, 21);
            this.txtSpeed.TabIndex = 17;
            this.txtSpeed.Text = "0.3";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(336, 102);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 12);
            this.label10.TabIndex = 22;
            this.label10.Text = "m/s";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(215, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 21;
            this.label11.Text = "半径：";
            // 
            // txtRadius
            // 
            this.txtRadius.Location = new System.Drawing.Point(274, 98);
            this.txtRadius.Name = "txtRadius";
            this.txtRadius.Size = new System.Drawing.Size(56, 21);
            this.txtRadius.TabIndex = 20;
            this.txtRadius.Text = "0.018";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(215, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(233, 12);
            this.label12.TabIndex = 23;
            this.label12.Text = "下面三个参数如果改为空则不添加到命令中";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(492, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(197, 12);
            this.label13.TabIndex = 24;
            this.label13.Text = "切换单选按钮，再点击查看示教输出";
            // 
            // btnResetTeachResult
            // 
            this.btnResetTeachResult.Location = new System.Drawing.Point(737, 30);
            this.btnResetTeachResult.Name = "btnResetTeachResult";
            this.btnResetTeachResult.Size = new System.Drawing.Size(114, 28);
            this.btnResetTeachResult.TabIndex = 25;
            this.btnResetTeachResult.Text = "重置加强示教";
            this.btnResetTeachResult.UseVisualStyleBackColor = true;
            this.btnResetTeachResult.Click += new System.EventHandler(this.btnResetTeachResult_Click);
            // 
            // btnSaveTeachResult
            // 
            this.btnSaveTeachResult.Location = new System.Drawing.Point(862, 98);
            this.btnSaveTeachResult.Name = "btnSaveTeachResult";
            this.btnSaveTeachResult.Size = new System.Drawing.Size(114, 28);
            this.btnSaveTeachResult.TabIndex = 26;
            this.btnSaveTeachResult.Text = "保存当前结果";
            this.btnSaveTeachResult.UseVisualStyleBackColor = true;
            this.btnSaveTeachResult.Click += new System.EventHandler(this.btnSaveTeachResult_Click);
            // 
            // radioAdd_Nothing
            // 
            this.radioAdd_Nothing.AutoSize = true;
            this.radioAdd_Nothing.Location = new System.Drawing.Point(3, 8);
            this.radioAdd_Nothing.Name = "radioAdd_Nothing";
            this.radioAdd_Nothing.Size = new System.Drawing.Size(59, 16);
            this.radioAdd_Nothing.TabIndex = 30;
            this.radioAdd_Nothing.Text = "不合成";
            this.radioAdd_Nothing.UseVisualStyleBackColor = true;
            // 
            // panelAdd
            // 
            this.panelAdd.Controls.Add(this.radioAdd_Tool);
            this.panelAdd.Controls.Add(this.radioAdd_Base);
            this.panelAdd.Controls.Add(this.radioAdd_Nothing);
            this.panelAdd.Location = new System.Drawing.Point(493, 97);
            this.panelAdd.Name = "panelAdd";
            this.panelAdd.Size = new System.Drawing.Size(358, 32);
            this.panelAdd.TabIndex = 31;
            // 
            // radioAdd_Tool
            // 
            this.radioAdd_Tool.AutoSize = true;
            this.radioAdd_Tool.Checked = true;
            this.radioAdd_Tool.Location = new System.Drawing.Point(221, 8);
            this.radioAdd_Tool.Name = "radioAdd_Tool";
            this.radioAdd_Tool.Size = new System.Drawing.Size(83, 16);
            this.radioAdd_Tool.TabIndex = 32;
            this.radioAdd_Tool.TabStop = true;
            this.radioAdd_Tool.Text = "与Tool合成";
            this.radioAdd_Tool.UseVisualStyleBackColor = true;
            // 
            // radioAdd_Base
            // 
            this.radioAdd_Base.AutoSize = true;
            this.radioAdd_Base.Location = new System.Drawing.Point(105, 8);
            this.radioAdd_Base.Name = "radioAdd_Base";
            this.radioAdd_Base.Size = new System.Drawing.Size(83, 16);
            this.radioAdd_Base.TabIndex = 31;
            this.radioAdd_Base.Text = "与Base合成";
            this.radioAdd_Base.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioMoveP);
            this.panel1.Controls.Add(this.radioMoveL);
            this.panel1.Controls.Add(this.radioMoveJ);
            this.panel1.Location = new System.Drawing.Point(493, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 32);
            this.panel1.TabIndex = 33;
            // 
            // radioMoveP
            // 
            this.radioMoveP.AutoSize = true;
            this.radioMoveP.Location = new System.Drawing.Point(221, 8);
            this.radioMoveP.Name = "radioMoveP";
            this.radioMoveP.Size = new System.Drawing.Size(77, 16);
            this.radioMoveP.TabIndex = 32;
            this.radioMoveP.Text = "MoveP方式";
            this.radioMoveP.UseVisualStyleBackColor = true;
            // 
            // radioMoveL
            // 
            this.radioMoveL.AutoSize = true;
            this.radioMoveL.Checked = true;
            this.radioMoveL.Location = new System.Drawing.Point(105, 8);
            this.radioMoveL.Name = "radioMoveL";
            this.radioMoveL.Size = new System.Drawing.Size(77, 16);
            this.radioMoveL.TabIndex = 31;
            this.radioMoveL.TabStop = true;
            this.radioMoveL.Text = "MoveL方式";
            this.radioMoveL.UseVisualStyleBackColor = true;
            // 
            // radioMoveJ
            // 
            this.radioMoveJ.AutoSize = true;
            this.radioMoveJ.Location = new System.Drawing.Point(3, 8);
            this.radioMoveJ.Name = "radioMoveJ";
            this.radioMoveJ.Size = new System.Drawing.Size(77, 16);
            this.radioMoveJ.TabIndex = 30;
            this.radioMoveJ.Text = "MoveJ方式";
            this.radioMoveJ.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(137, 12);
            this.label14.TabIndex = 34;
            this.label14.Text = "尽量不要修改这两个参数";
            // 
            // Teach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 468);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelAdd);
            this.Controls.Add(this.btnSaveTeachResult);
            this.Controls.Add(this.btnResetTeachResult);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtRadius);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtSpeed);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtAccleration);
            this.Controls.Add(this.labelCurrent);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRecordLimit);
            this.Controls.Add(this.TextBoxPointResult);
            this.Controls.Add(this.btnSendTeachResult);
            this.Controls.Add(this.btnShowTeachResult);
            this.Controls.Add(this.btnStopTeach);
            this.Controls.Add(this.btnStartTeach);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRecordTick);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Teach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Teach";
            this.Load += new System.EventHandler(this.Teach_Load);
            this.panelAdd.ResumeLayout(false);
            this.panelAdd.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRecordTick;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStartTeach;
        private System.Windows.Forms.Button btnStopTeach;
        private System.Windows.Forms.Button btnShowTeachResult;
        private System.Windows.Forms.Timer TeachTimer;
        private System.Windows.Forms.Button btnSendTeachResult;
        private System.Windows.Forms.RichTextBox TextBoxPointResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRecordLimit;
        private System.Windows.Forms.Label labelCurrent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAccleration;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRadius;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnResetTeachResult;
        private System.Windows.Forms.Button btnSaveTeachResult;
        private System.Windows.Forms.RadioButton radioAdd_Nothing;
        private System.Windows.Forms.Panel panelAdd;
        private System.Windows.Forms.RadioButton radioAdd_Tool;
        private System.Windows.Forms.RadioButton radioAdd_Base;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioMoveP;
        private System.Windows.Forms.RadioButton radioMoveL;
        private System.Windows.Forms.RadioButton radioMoveJ;
        private System.Windows.Forms.Label label14;
    }
}
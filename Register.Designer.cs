namespace UR_点动控制器
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.btn_RegisterWrite = new System.Windows.Forms.Button();
            this.txtWriteNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReadNum = new System.Windows.Forms.TextBox();
            this.btn_RegisterRead = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWriteValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReadValues = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtReadStartAddress = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtWriteStartAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_RegisterWrite
            // 
            this.btn_RegisterWrite.Location = new System.Drawing.Point(774, 197);
            this.btn_RegisterWrite.Name = "btn_RegisterWrite";
            this.btn_RegisterWrite.Size = new System.Drawing.Size(74, 33);
            this.btn_RegisterWrite.TabIndex = 0;
            this.btn_RegisterWrite.Text = "写入";
            this.btn_RegisterWrite.UseVisualStyleBackColor = true;
            this.btn_RegisterWrite.Click += new System.EventHandler(this.btn_RegisterWrite_Click);
            // 
            // txtWriteNum
            // 
            this.txtWriteNum.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWriteNum.Location = new System.Drawing.Point(17, 200);
            this.txtWriteNum.Name = "txtWriteNum";
            this.txtWriteNum.Size = new System.Drawing.Size(75, 33);
            this.txtWriteNum.TabIndex = 1;
            this.txtWriteNum.Text = "3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "目标寄存器个数 ：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "目标寄存器个数：";
            // 
            // txtReadNum
            // 
            this.txtReadNum.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtReadNum.Location = new System.Drawing.Point(14, 87);
            this.txtReadNum.Name = "txtReadNum";
            this.txtReadNum.Size = new System.Drawing.Size(78, 33);
            this.txtReadNum.TabIndex = 4;
            this.txtReadNum.Text = "6";
            // 
            // btn_RegisterRead
            // 
            this.btn_RegisterRead.Location = new System.Drawing.Point(774, 87);
            this.btn_RegisterRead.Name = "btn_RegisterRead";
            this.btn_RegisterRead.Size = new System.Drawing.Size(74, 33);
            this.btn_RegisterRead.TabIndex = 5;
            this.btn_RegisterRead.Text = "读取";
            this.btn_RegisterRead.UseVisualStyleBackColor = true;
            this.btn_RegisterRead.Click += new System.EventHandler(this.btn_RegisterRead_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(409, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(202, 25);
            this.label6.TabIndex = 10;
            this.label6.Text = "目标寄存器写入数值：";
            // 
            // txtWriteValue
            // 
            this.txtWriteValue.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWriteValue.Location = new System.Drawing.Point(414, 197);
            this.txtWriteValue.Name = "txtWriteValue";
            this.txtWriteValue.Size = new System.Drawing.Size(342, 33);
            this.txtWriteValue.TabIndex = 9;
            this.txtWriteValue.Text = "12345|65533|35689";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(503, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "UR的可写寄存器号为：128-255，写入的数据必须为0-65535整数,支持单个与多个寄存器的写入";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(497, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "UR的可读寄存器号包含只读寄存器，读取的数据是0-65535整数,支持单个与多个寄存器的读取";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(409, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(202, 25);
            this.label5.TabIndex = 13;
            this.label5.Text = "目标寄存器读到数值：";
            // 
            // txtReadValues
            // 
            this.txtReadValues.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtReadValues.Location = new System.Drawing.Point(414, 87);
            this.txtReadValues.Name = "txtReadValues";
            this.txtReadValues.Size = new System.Drawing.Size(342, 33);
            this.txtReadValues.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(191, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(202, 25);
            this.label7.TabIndex = 15;
            this.label7.Text = "目标寄存器起始地址：";
            // 
            // txtReadStartAddress
            // 
            this.txtReadStartAddress.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtReadStartAddress.Location = new System.Drawing.Point(196, 87);
            this.txtReadStartAddress.Name = "txtReadStartAddress";
            this.txtReadStartAddress.Size = new System.Drawing.Size(78, 33);
            this.txtReadStartAddress.TabIndex = 16;
            this.txtReadStartAddress.Text = "400";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(191, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(208, 25);
            this.label8.TabIndex = 17;
            this.label8.Text = "目标寄存器起始地址 ：";
            // 
            // txtWriteStartAddress
            // 
            this.txtWriteStartAddress.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWriteStartAddress.Location = new System.Drawing.Point(196, 200);
            this.txtWriteStartAddress.Name = "txtWriteStartAddress";
            this.txtWriteStartAddress.Size = new System.Drawing.Size(75, 33);
            this.txtWriteStartAddress.TabIndex = 18;
            this.txtWriteStartAddress.Text = "155";
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 241);
            this.Controls.Add(this.txtWriteStartAddress);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtReadStartAddress);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtReadValues);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtWriteValue);
            this.Controls.Add(this.btn_RegisterRead);
            this.Controls.Add(this.txtReadNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWriteNum);
            this.Controls.Add(this.btn_RegisterWrite);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.Load += new System.EventHandler(this.Register_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_RegisterWrite;
        private System.Windows.Forms.TextBox txtWriteNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReadNum;
        private System.Windows.Forms.Button btn_RegisterRead;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtWriteValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtReadValues;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtReadStartAddress;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtWriteStartAddress;
    }
}
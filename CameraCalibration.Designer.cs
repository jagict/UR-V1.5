namespace UR_点动控制器
{
    partial class CameraCalibration
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
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.btnSnapshot = new System.Windows.Forms.Button();
            this.btnSetMatch = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.label_Error = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_R_Angle = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_Y = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_X = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGoSnapPos = new System.Windows.Forms.Button();
            this.btnRecordSnapPos = new System.Windows.Forms.Button();
            this.txt_O_X = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_O_Y = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_A_Y = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_A_X = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_B_Y = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_B_X = new System.Windows.Forms.TextBox();
            this.btnCalculateResult = new System.Windows.Forms.Button();
            this.label_Accurary = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label_R_Radius = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label17 = new System.Windows.Forms.Label();
            this.txtCurrentSnapPos = new System.Windows.Forms.TextBox();
            this.btn_TriggerSocket = new System.Windows.Forms.Button();
            this.btn_TriggerModbus = new System.Windows.Forms.Button();
            this.btnRestModbus = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.listBox_Debug = new System.Windows.Forms.ListBox();
            this.btn_TriggerSlave = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxCommand = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 1600, 1200);
            this.hWindowControl1.Location = new System.Drawing.Point(8, 7);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(800, 600);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(800, 600);
            // 
            // btnSnapshot
            // 
            this.btnSnapshot.Location = new System.Drawing.Point(1097, 7);
            this.btnSnapshot.Name = "btnSnapshot";
            this.btnSnapshot.Size = new System.Drawing.Size(82, 32);
            this.btnSnapshot.TabIndex = 16;
            this.btnSnapshot.Text = "截图";
            this.btnSnapshot.UseVisualStyleBackColor = true;
            this.btnSnapshot.Click += new System.EventHandler(this.btnSnapshot_Click);
            // 
            // btnSetMatch
            // 
            this.btnSetMatch.Location = new System.Drawing.Point(710, 614);
            this.btnSetMatch.Name = "btnSetMatch";
            this.btnSetMatch.Size = new System.Drawing.Size(98, 27);
            this.btnSetMatch.TabIndex = 15;
            this.btnSetMatch.Text = "设置当前特征";
            this.btnSetMatch.UseVisualStyleBackColor = true;
            this.btnSetMatch.Click += new System.EventHandler(this.btnSetMatch_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(606, 614);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(98, 27);
            this.btnOpenFile.TabIndex = 14;
            this.btnOpenFile.Text = "浏览静态图像";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(965, 7);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(82, 32);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(815, 7);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(82, 32);
            this.btnRun.TabIndex = 12;
            this.btnRun.Text = "运行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label_Error
            // 
            this.label_Error.AutoSize = true;
            this.label_Error.Location = new System.Drawing.Point(411, 621);
            this.label_Error.Name = "label_Error";
            this.label_Error.Size = new System.Drawing.Size(23, 12);
            this.label_Error.TabIndex = 24;
            this.label_Error.Text = "???";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(367, 621);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "Error:";
            // 
            // label_R_Angle
            // 
            this.label_R_Angle.AutoSize = true;
            this.label_R_Angle.Location = new System.Drawing.Point(203, 621);
            this.label_R_Angle.Name = "label_R_Angle";
            this.label_R_Angle.Size = new System.Drawing.Size(23, 12);
            this.label_R_Angle.TabIndex = 22;
            this.label_R_Angle.Text = "???";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(143, 621);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "R(Angle):";
            // 
            // label_Y
            // 
            this.label_Y.AutoSize = true;
            this.label_Y.Location = new System.Drawing.Point(94, 621);
            this.label_Y.Name = "label_Y";
            this.label_Y.Size = new System.Drawing.Size(23, 12);
            this.label_Y.TabIndex = 20;
            this.label_Y.Text = "???";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 621);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "Y:";
            // 
            // label_X
            // 
            this.label_X.AutoSize = true;
            this.label_X.Location = new System.Drawing.Point(32, 621);
            this.label_X.Name = "label_X";
            this.label_X.Size = new System.Drawing.Size(23, 12);
            this.label_X.TabIndex = 18;
            this.label_X.Text = "???";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 621);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "X:";
            // 
            // btnGoSnapPos
            // 
            this.btnGoSnapPos.Location = new System.Drawing.Point(1029, 110);
            this.btnGoSnapPos.Name = "btnGoSnapPos";
            this.btnGoSnapPos.Size = new System.Drawing.Size(150, 39);
            this.btnGoSnapPos.TabIndex = 26;
            this.btnGoSnapPos.Text = "回拍照位置";
            this.btnGoSnapPos.UseVisualStyleBackColor = true;
            this.btnGoSnapPos.Click += new System.EventHandler(this.btnGoSnapPos_Click);
            // 
            // btnRecordSnapPos
            // 
            this.btnRecordSnapPos.Location = new System.Drawing.Point(814, 110);
            this.btnRecordSnapPos.Name = "btnRecordSnapPos";
            this.btnRecordSnapPos.Size = new System.Drawing.Size(150, 39);
            this.btnRecordSnapPos.TabIndex = 27;
            this.btnRecordSnapPos.Text = "记录当前姿态为拍照位置";
            this.btnRecordSnapPos.UseVisualStyleBackColor = true;
            this.btnRecordSnapPos.Click += new System.EventHandler(this.btnRecordSnapPos_Click);
            // 
            // txt_O_X
            // 
            this.txt_O_X.Location = new System.Drawing.Point(837, 193);
            this.txt_O_X.Name = "txt_O_X";
            this.txt_O_X.Size = new System.Drawing.Size(88, 21);
            this.txt_O_X.TabIndex = 28;
            this.txt_O_X.Text = "105";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(814, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 29;
            this.label2.Text = "参考O点像素坐标:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(814, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "X:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(936, 196);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 32;
            this.label7.Text = "Y:";
            // 
            // txt_O_Y
            // 
            this.txt_O_Y.Location = new System.Drawing.Point(959, 193);
            this.txt_O_Y.Name = "txt_O_Y";
            this.txt_O_Y.Size = new System.Drawing.Size(88, 21);
            this.txt_O_Y.TabIndex = 31;
            this.txt_O_Y.Text = "66";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(936, 261);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 37;
            this.label8.Text = "Y:";
            // 
            // txt_A_Y
            // 
            this.txt_A_Y.Location = new System.Drawing.Point(959, 258);
            this.txt_A_Y.Name = "txt_A_Y";
            this.txt_A_Y.Size = new System.Drawing.Size(88, 21);
            this.txt_A_Y.TabIndex = 36;
            this.txt_A_Y.Text = "66";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(814, 261);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 35;
            this.label9.Text = "X:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(814, 234);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 12);
            this.label10.TabIndex = 34;
            this.label10.Text = "参考A点像素坐标:";
            // 
            // txt_A_X
            // 
            this.txt_A_X.Location = new System.Drawing.Point(837, 258);
            this.txt_A_X.Name = "txt_A_X";
            this.txt_A_X.Size = new System.Drawing.Size(88, 21);
            this.txt_A_X.TabIndex = 33;
            this.txt_A_X.Text = "748";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(936, 324);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 42;
            this.label11.Text = "Y:";
            // 
            // txt_B_Y
            // 
            this.txt_B_Y.Location = new System.Drawing.Point(959, 321);
            this.txt_B_Y.Name = "txt_B_Y";
            this.txt_B_Y.Size = new System.Drawing.Size(88, 21);
            this.txt_B_Y.TabIndex = 41;
            this.txt_B_Y.Text = "582";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(814, 324);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 40;
            this.label12.Text = "X:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(814, 297);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 12);
            this.label13.TabIndex = 39;
            this.label13.Text = "参考B点像素坐标:";
            // 
            // txt_B_X
            // 
            this.txt_B_X.Location = new System.Drawing.Point(837, 321);
            this.txt_B_X.Name = "txt_B_X";
            this.txt_B_X.Size = new System.Drawing.Size(88, 21);
            this.txt_B_X.TabIndex = 38;
            this.txt_B_X.Text = "105";
            // 
            // btnCalculateResult
            // 
            this.btnCalculateResult.Location = new System.Drawing.Point(1102, 169);
            this.btnCalculateResult.Name = "btnCalculateResult";
            this.btnCalculateResult.Size = new System.Drawing.Size(77, 173);
            this.btnCalculateResult.TabIndex = 43;
            this.btnCalculateResult.Text = "计算当前标定结果";
            this.btnCalculateResult.UseVisualStyleBackColor = true;
            this.btnCalculateResult.Click += new System.EventHandler(this.btnCalculateResult_Click);
            // 
            // label_Accurary
            // 
            this.label_Accurary.AutoSize = true;
            this.label_Accurary.Location = new System.Drawing.Point(519, 621);
            this.label_Accurary.Name = "label_Accurary";
            this.label_Accurary.Size = new System.Drawing.Size(23, 12);
            this.label_Accurary.TabIndex = 45;
            this.label_Accurary.Text = "???";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(456, 621);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 12);
            this.label15.TabIndex = 44;
            this.label15.Text = "Accuracy:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(565, 621);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 12);
            this.label16.TabIndex = 46;
            this.label16.Text = "mm/px";
            // 
            // label_R_Radius
            // 
            this.label_R_Radius.AutoSize = true;
            this.label_R_Radius.Location = new System.Drawing.Point(313, 621);
            this.label_R_Radius.Name = "label_R_Radius";
            this.label_R_Radius.Size = new System.Drawing.Size(23, 12);
            this.label_R_Radius.TabIndex = 48;
            this.label_R_Radius.Text = "???";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(247, 621);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 47;
            this.label18.Text = "R(Radius):";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "sa";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(814, 58);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(83, 12);
            this.label17.TabIndex = 49;
            this.label17.Text = "当前拍照位置:";
            // 
            // txtCurrentSnapPos
            // 
            this.txtCurrentSnapPos.Location = new System.Drawing.Point(815, 73);
            this.txtCurrentSnapPos.Name = "txtCurrentSnapPos";
            this.txtCurrentSnapPos.Size = new System.Drawing.Size(364, 21);
            this.txtCurrentSnapPos.TabIndex = 50;
            // 
            // btn_TriggerSocket
            // 
            this.btn_TriggerSocket.Location = new System.Drawing.Point(814, 359);
            this.btn_TriggerSocket.Name = "btn_TriggerSocket";
            this.btn_TriggerSocket.Size = new System.Drawing.Size(193, 26);
            this.btn_TriggerSocket.TabIndex = 51;
            this.btn_TriggerSocket.Text = "触发（Socket纯主机方式）";
            this.btn_TriggerSocket.UseVisualStyleBackColor = true;
            this.btn_TriggerSocket.Click += new System.EventHandler(this.btn_TriggerSocket_Click);
            // 
            // btn_TriggerModbus
            // 
            this.btn_TriggerModbus.Location = new System.Drawing.Point(814, 423);
            this.btn_TriggerModbus.Name = "btn_TriggerModbus";
            this.btn_TriggerModbus.Size = new System.Drawing.Size(192, 25);
            this.btn_TriggerModbus.TabIndex = 52;
            this.btn_TriggerModbus.Text = "触发（Moubus TCP方式）";
            this.btn_TriggerModbus.UseVisualStyleBackColor = true;
            this.btn_TriggerModbus.Click += new System.EventHandler(this.btn_TriggerModbus_Click);
            // 
            // btnRestModbus
            // 
            this.btnRestModbus.Location = new System.Drawing.Point(1062, 359);
            this.btnRestModbus.Name = "btnRestModbus";
            this.btnRestModbus.Size = new System.Drawing.Size(117, 89);
            this.btnRestModbus.TabIndex = 53;
            this.btnRestModbus.Text = "重置（Moubus TCP寄存器）";
            this.btnRestModbus.UseVisualStyleBackColor = true;
            this.btnRestModbus.Click += new System.EventHandler(this.btnRestModbus_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(814, 471);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(125, 12);
            this.label14.TabIndex = 54;
            this.label14.Text = "当前特征差异(与O点):";
            // 
            // listBox_Debug
            // 
            this.listBox_Debug.FormattingEnabled = true;
            this.listBox_Debug.ItemHeight = 12;
            this.listBox_Debug.Location = new System.Drawing.Point(814, 496);
            this.listBox_Debug.Name = "listBox_Debug";
            this.listBox_Debug.Size = new System.Drawing.Size(365, 52);
            this.listBox_Debug.TabIndex = 55;
            // 
            // btn_TriggerSlave
            // 
            this.btn_TriggerSlave.Location = new System.Drawing.Point(813, 391);
            this.btn_TriggerSlave.Name = "btn_TriggerSlave";
            this.btn_TriggerSlave.Size = new System.Drawing.Size(193, 26);
            this.btn_TriggerSlave.TabIndex = 56;
            this.btn_TriggerSlave.Text = "触发（Socket主从机方式）";
            this.btn_TriggerSlave.UseVisualStyleBackColor = true;
            this.btn_TriggerSlave.Click += new System.EventHandler(this.btn_TriggerSlave_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(814, 565);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(89, 12);
            this.label19.TabIndex = 57;
            this.label19.Text = "当前发送指令：";
            // 
            // textBoxCommand
            // 
            this.textBoxCommand.Location = new System.Drawing.Point(815, 586);
            this.textBoxCommand.Multiline = true;
            this.textBoxCommand.Name = "textBoxCommand";
            this.textBoxCommand.Size = new System.Drawing.Size(364, 55);
            this.textBoxCommand.TabIndex = 58;
            // 
            // CameraCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 648);
            this.Controls.Add(this.textBoxCommand);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.btn_TriggerSlave);
            this.Controls.Add(this.listBox_Debug);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnRestModbus);
            this.Controls.Add(this.btn_TriggerModbus);
            this.Controls.Add(this.btn_TriggerSocket);
            this.Controls.Add(this.txtCurrentSnapPos);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label_R_Radius);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label_Accurary);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btnCalculateResult);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txt_B_Y);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txt_B_X);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_A_Y);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txt_A_X);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_O_Y);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_O_X);
            this.Controls.Add(this.btnRecordSnapPos);
            this.Controls.Add(this.btnGoSnapPos);
            this.Controls.Add(this.label_Error);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_R_Angle);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label_Y);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_X);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSnapshot);
            this.Controls.Add(this.btnSetMatch);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.hWindowControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CameraCalibration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CameraCalibration";
            this.Load += new System.EventHandler(this.CameraCalibration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.Button btnSnapshot;
        private System.Windows.Forms.Button btnSetMatch;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label_Error;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_R_Angle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_Y;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_X;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGoSnapPos;
        private System.Windows.Forms.Button btnRecordSnapPos;
        private System.Windows.Forms.TextBox txt_O_X;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_O_Y;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_A_Y;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_A_X;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_B_Y;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_B_X;
        private System.Windows.Forms.Button btnCalculateResult;
        private System.Windows.Forms.Label label_Accurary;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label_R_Radius;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtCurrentSnapPos;
        private System.Windows.Forms.Button btn_TriggerSocket;
        private System.Windows.Forms.Button btn_TriggerModbus;
        private System.Windows.Forms.Button btnRestModbus;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ListBox listBox_Debug;
        private System.Windows.Forms.Button btn_TriggerSlave;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxCommand;
    }
}
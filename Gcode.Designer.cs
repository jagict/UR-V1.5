namespace UR_点动控制器
{
    partial class GCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GCode));
            this.label1 = new System.Windows.Forms.Label();
            this.txtPose1 = new System.Windows.Forms.TextBox();
            this.txtPose2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPose3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPos1_Relative = new System.Windows.Forms.Button();
            this.btnPos2_Relative = new System.Windows.Forms.Button();
            this.btnPos3_Relative = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtSpeedFast = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSetSpeed = new System.Windows.Forms.Button();
            this.btnResetSpeed = new System.Windows.Forms.Button();
            this.btnLoadGCode = new System.Windows.Forms.Button();
            this.btnConvertCode = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRadius = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSpeedSlow = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtAcclerationFast = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAcclerationSlow = new System.Windows.Forms.TextBox();
            this.btnFilterGCode = new System.Windows.Forms.Button();
            this.btnSendCode = new System.Windows.Forms.Button();
            this.btnSaveCode = new System.Windows.Forms.Button();
            this.textBoxCodeList = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioMoveP = new System.Windows.Forms.RadioButton();
            this.radioMoveL = new System.Windows.Forms.RadioButton();
            this.radioMoveJ = new System.Windows.Forms.RadioButton();
            this.btnEmbedCode = new System.Windows.Forms.Button();
            this.btnGoZeroPoint = new System.Windows.Forms.Button();
            this.btnGoAPoint = new System.Windows.Forms.Button();
            this.btnGoBPoint = new System.Windows.Forms.Button();
            this.txtPosOA = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPosOB = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnRefreshVectors = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.btnTryTouch = new System.Windows.Forms.Button();
            this.btnDrawCoordinateSystem = new System.Windows.Forms.Button();
            this.btnDriveOA = new System.Windows.Forms.Button();
            this.btnDriveOB = new System.Windows.Forms.Button();
            this.btnWriteConfigFile = new System.Windows.Forms.Button();
            this.btnDriveAO = new System.Windows.Forms.Button();
            this.btnDriveBO = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtPosOC = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnVerifyVectors = new System.Windows.Forms.Button();
            this.btnDriveOC = new System.Windows.Forms.Button();
            this.btnDriveCO = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前O点坐标：";
            // 
            // txtPose1
            // 
            this.txtPose1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPose1.Location = new System.Drawing.Point(12, 34);
            this.txtPose1.Name = "txtPose1";
            this.txtPose1.Size = new System.Drawing.Size(474, 23);
            this.txtPose1.TabIndex = 1;
            // 
            // txtPose2
            // 
            this.txtPose2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPose2.Location = new System.Drawing.Point(12, 93);
            this.txtPose2.Name = "txtPose2";
            this.txtPose2.Size = new System.Drawing.Size(474, 23);
            this.txtPose2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "当前A点坐标：";
            // 
            // txtPose3
            // 
            this.txtPose3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPose3.Location = new System.Drawing.Point(12, 147);
            this.txtPose3.Name = "txtPose3";
            this.txtPose3.Size = new System.Drawing.Size(474, 23);
            this.txtPose3.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(10, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "当前B点坐标：";
            // 
            // btnPos1_Relative
            // 
            this.btnPos1_Relative.Location = new System.Drawing.Point(352, 8);
            this.btnPos1_Relative.Name = "btnPos1_Relative";
            this.btnPos1_Relative.Size = new System.Drawing.Size(134, 24);
            this.btnPos1_Relative.TabIndex = 9;
            this.btnPos1_Relative.Text = "将当前位置设为O点";
            this.btnPos1_Relative.UseVisualStyleBackColor = true;
            this.btnPos1_Relative.Click += new System.EventHandler(this.btnPos1_Relative_Click);
            // 
            // btnPos2_Relative
            // 
            this.btnPos2_Relative.Location = new System.Drawing.Point(352, 66);
            this.btnPos2_Relative.Name = "btnPos2_Relative";
            this.btnPos2_Relative.Size = new System.Drawing.Size(134, 24);
            this.btnPos2_Relative.TabIndex = 10;
            this.btnPos2_Relative.Text = "将当前位置设为A点";
            this.btnPos2_Relative.UseVisualStyleBackColor = true;
            this.btnPos2_Relative.Click += new System.EventHandler(this.btnPos2_Relative_Click);
            // 
            // btnPos3_Relative
            // 
            this.btnPos3_Relative.Location = new System.Drawing.Point(352, 121);
            this.btnPos3_Relative.Name = "btnPos3_Relative";
            this.btnPos3_Relative.Size = new System.Drawing.Size(134, 23);
            this.btnPos3_Relative.TabIndex = 11;
            this.btnPos3_Relative.Text = "将当前位置设为B点";
            this.btnPos3_Relative.UseVisualStyleBackColor = true;
            this.btnPos3_Relative.Click += new System.EventHandler(this.btnPos3_Relative_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtSpeedFast
            // 
            this.txtSpeedFast.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSpeedFast.Location = new System.Drawing.Point(887, 40);
            this.txtSpeedFast.Name = "txtSpeedFast";
            this.txtSpeedFast.Size = new System.Drawing.Size(53, 21);
            this.txtSpeedFast.TabIndex = 13;
            this.txtSpeedFast.Text = "800";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(817, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "快进速度：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(950, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "mm/s";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(951, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "mm/s";
            // 
            // btnSetSpeed
            // 
            this.btnSetSpeed.Location = new System.Drawing.Point(1009, 9);
            this.btnSetSpeed.Name = "btnSetSpeed";
            this.btnSetSpeed.Size = new System.Drawing.Size(53, 55);
            this.btnSetSpeed.TabIndex = 18;
            this.btnSetSpeed.Text = "设置";
            this.btnSetSpeed.UseVisualStyleBackColor = true;
            this.btnSetSpeed.Click += new System.EventHandler(this.btnSetSpeed_Click);
            // 
            // btnResetSpeed
            // 
            this.btnResetSpeed.Location = new System.Drawing.Point(1070, 9);
            this.btnResetSpeed.Name = "btnResetSpeed";
            this.btnResetSpeed.Size = new System.Drawing.Size(57, 54);
            this.btnResetSpeed.TabIndex = 19;
            this.btnResetSpeed.Text = "重置";
            this.btnResetSpeed.UseVisualStyleBackColor = true;
            this.btnResetSpeed.Click += new System.EventHandler(this.btnResetSpeed_Click);
            // 
            // btnLoadGCode
            // 
            this.btnLoadGCode.Location = new System.Drawing.Point(494, 255);
            this.btnLoadGCode.Name = "btnLoadGCode";
            this.btnLoadGCode.Size = new System.Drawing.Size(78, 33);
            this.btnLoadGCode.TabIndex = 21;
            this.btnLoadGCode.Text = "载入G代码";
            this.btnLoadGCode.UseVisualStyleBackColor = true;
            this.btnLoadGCode.Click += new System.EventHandler(this.btnLoadGCode_Click);
            // 
            // btnConvertCode
            // 
            this.btnConvertCode.Location = new System.Drawing.Point(608, 255);
            this.btnConvertCode.Name = "btnConvertCode";
            this.btnConvertCode.Size = new System.Drawing.Size(78, 33);
            this.btnConvertCode.TabIndex = 22;
            this.btnConvertCode.Text = "转为UR代码";
            this.btnConvertCode.UseVisualStyleBackColor = true;
            this.btnConvertCode.Click += new System.EventHandler(this.btnConvertCode_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(950, 142);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 31;
            this.label10.Text = "mm";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(817, 142);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 30;
            this.label11.Text = "交融半径：";
            // 
            // txtRadius
            // 
            this.txtRadius.Location = new System.Drawing.Point(888, 138);
            this.txtRadius.Name = "txtRadius";
            this.txtRadius.Size = new System.Drawing.Size(56, 21);
            this.txtRadius.TabIndex = 29;
            this.txtRadius.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(817, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 27;
            this.label9.Text = "加工速度：";
            // 
            // txtSpeedSlow
            // 
            this.txtSpeedSlow.Location = new System.Drawing.Point(887, 107);
            this.txtSpeedSlow.Name = "txtSpeedSlow";
            this.txtSpeedSlow.Size = new System.Drawing.Size(56, 21);
            this.txtSpeedSlow.TabIndex = 26;
            this.txtSpeedSlow.Text = "30";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(962, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 25;
            this.label12.Text = "mm/s^2";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(817, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 24;
            this.label13.Text = "快进加速度：";
            // 
            // txtAcclerationFast
            // 
            this.txtAcclerationFast.Location = new System.Drawing.Point(900, 11);
            this.txtAcclerationFast.Name = "txtAcclerationFast";
            this.txtAcclerationFast.Size = new System.Drawing.Size(56, 21);
            this.txtAcclerationFast.TabIndex = 23;
            this.txtAcclerationFast.Text = "80";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(962, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 34;
            this.label7.Text = "mm/s^2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(817, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 33;
            this.label8.Text = "加工加速度：";
            // 
            // txtAcclerationSlow
            // 
            this.txtAcclerationSlow.Location = new System.Drawing.Point(900, 75);
            this.txtAcclerationSlow.Name = "txtAcclerationSlow";
            this.txtAcclerationSlow.Size = new System.Drawing.Size(56, 21);
            this.txtAcclerationSlow.TabIndex = 32;
            this.txtAcclerationSlow.Text = "30";
            // 
            // btnFilterGCode
            // 
            this.btnFilterGCode.Location = new System.Drawing.Point(557, 214);
            this.btnFilterGCode.Name = "btnFilterGCode";
            this.btnFilterGCode.Size = new System.Drawing.Size(78, 33);
            this.btnFilterGCode.TabIndex = 35;
            this.btnFilterGCode.Text = "过滤G代码";
            this.btnFilterGCode.UseVisualStyleBackColor = true;
            this.btnFilterGCode.Click += new System.EventHandler(this.btnFilterGCode_Click);
            // 
            // btnSendCode
            // 
            this.btnSendCode.Location = new System.Drawing.Point(807, 214);
            this.btnSendCode.Name = "btnSendCode";
            this.btnSendCode.Size = new System.Drawing.Size(97, 33);
            this.btnSendCode.TabIndex = 36;
            this.btnSendCode.Text = "发送当前UR代码";
            this.btnSendCode.UseVisualStyleBackColor = true;
            this.btnSendCode.Click += new System.EventHandler(this.btnSendCode_Click);
            // 
            // btnSaveCode
            // 
            this.btnSaveCode.Location = new System.Drawing.Point(872, 255);
            this.btnSaveCode.Name = "btnSaveCode";
            this.btnSaveCode.Size = new System.Drawing.Size(108, 33);
            this.btnSaveCode.TabIndex = 37;
            this.btnSaveCode.Text = "保存当前UR代码";
            this.btnSaveCode.UseVisualStyleBackColor = true;
            this.btnSaveCode.Click += new System.EventHandler(this.btnSaveCode_Click);
            // 
            // textBoxCodeList
            // 
            this.textBoxCodeList.Location = new System.Drawing.Point(8, 357);
            this.textBoxCodeList.Name = "textBoxCodeList";
            this.textBoxCodeList.Size = new System.Drawing.Size(1112, 183);
            this.textBoxCodeList.TabIndex = 38;
            this.textBoxCodeList.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioMoveP);
            this.panel1.Controls.Add(this.radioMoveL);
            this.panel1.Controls.Add(this.radioMoveJ);
            this.panel1.Location = new System.Drawing.Point(1011, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(109, 71);
            this.panel1.TabIndex = 51;
            // 
            // radioMoveP
            // 
            this.radioMoveP.AutoSize = true;
            this.radioMoveP.Location = new System.Drawing.Point(3, 52);
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
            this.radioMoveL.Location = new System.Drawing.Point(3, 30);
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
            // btnEmbedCode
            // 
            this.btnEmbedCode.Location = new System.Drawing.Point(739, 255);
            this.btnEmbedCode.Name = "btnEmbedCode";
            this.btnEmbedCode.Size = new System.Drawing.Size(78, 33);
            this.btnEmbedCode.TabIndex = 52;
            this.btnEmbedCode.Text = "加壳UR代码";
            this.btnEmbedCode.UseVisualStyleBackColor = true;
            this.btnEmbedCode.Click += new System.EventHandler(this.btnEmbedCode_Click);
            // 
            // btnGoZeroPoint
            // 
            this.btnGoZeroPoint.Location = new System.Drawing.Point(528, 40);
            this.btnGoZeroPoint.Name = "btnGoZeroPoint";
            this.btnGoZeroPoint.Size = new System.Drawing.Size(61, 24);
            this.btnGoZeroPoint.TabIndex = 53;
            this.btnGoZeroPoint.Text = "回到O点";
            this.btnGoZeroPoint.UseVisualStyleBackColor = true;
            this.btnGoZeroPoint.Click += new System.EventHandler(this.btnGoZeroPoint_Click);
            // 
            // btnGoAPoint
            // 
            this.btnGoAPoint.Location = new System.Drawing.Point(653, 40);
            this.btnGoAPoint.Name = "btnGoAPoint";
            this.btnGoAPoint.Size = new System.Drawing.Size(61, 24);
            this.btnGoAPoint.TabIndex = 56;
            this.btnGoAPoint.Text = "回到A点";
            this.btnGoAPoint.UseVisualStyleBackColor = true;
            this.btnGoAPoint.Click += new System.EventHandler(this.btnGoAPoint_Click);
            // 
            // btnGoBPoint
            // 
            this.btnGoBPoint.Location = new System.Drawing.Point(528, 107);
            this.btnGoBPoint.Name = "btnGoBPoint";
            this.btnGoBPoint.Size = new System.Drawing.Size(61, 24);
            this.btnGoBPoint.TabIndex = 57;
            this.btnGoBPoint.Text = "回到B点";
            this.btnGoBPoint.UseVisualStyleBackColor = true;
            this.btnGoBPoint.Click += new System.EventHandler(this.btnGoBPoint_Click);
            // 
            // txtPosOA
            // 
            this.txtPosOA.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPosOA.Location = new System.Drawing.Point(11, 210);
            this.txtPosOA.Name = "txtPosOA";
            this.txtPosOA.Size = new System.Drawing.Size(475, 23);
            this.txtPosOA.TabIndex = 59;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(9, 187);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(119, 14);
            this.label14.TabIndex = 58;
            this.label14.Text = "当前OA单位向量：";
            // 
            // txtPosOB
            // 
            this.txtPosOB.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPosOB.Location = new System.Drawing.Point(12, 266);
            this.txtPosOB.Name = "txtPosOB";
            this.txtPosOB.Size = new System.Drawing.Size(474, 23);
            this.txtPosOB.TabIndex = 61;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(10, 243);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(119, 14);
            this.label15.TabIndex = 60;
            this.label15.Text = "当前OB单位向量：";
            // 
            // btnRefreshVectors
            // 
            this.btnRefreshVectors.Location = new System.Drawing.Point(494, 305);
            this.btnRefreshVectors.Name = "btnRefreshVectors";
            this.btnRefreshVectors.Size = new System.Drawing.Size(140, 46);
            this.btnRefreshVectors.TabIndex = 62;
            this.btnRefreshVectors.Text = "刷新坐标系单位向量";
            this.btnRefreshVectors.UseVisualStyleBackColor = true;
            this.btnRefreshVectors.Click += new System.EventHandler(this.btnRefreshVectors_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(491, 187);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(294, 14);
            this.label16.TabIndex = 63;
            this.label16.Text = "请在加壳UR代码之前尝试触摸(微调OAB三点)：";
            // 
            // btnTryTouch
            // 
            this.btnTryTouch.Location = new System.Drawing.Point(673, 214);
            this.btnTryTouch.Name = "btnTryTouch";
            this.btnTryTouch.Size = new System.Drawing.Size(78, 33);
            this.btnTryTouch.TabIndex = 64;
            this.btnTryTouch.Text = "尝试触摸";
            this.btnTryTouch.UseVisualStyleBackColor = true;
            this.btnTryTouch.Click += new System.EventHandler(this.btnTryTouch_Click);
            // 
            // btnDrawCoordinateSystem
            // 
            this.btnDrawCoordinateSystem.Location = new System.Drawing.Point(966, 214);
            this.btnDrawCoordinateSystem.Name = "btnDrawCoordinateSystem";
            this.btnDrawCoordinateSystem.Size = new System.Drawing.Size(125, 35);
            this.btnDrawCoordinateSystem.TabIndex = 65;
            this.btnDrawCoordinateSystem.Text = "绘制当前Base坐标系";
            this.btnDrawCoordinateSystem.UseVisualStyleBackColor = true;
            this.btnDrawCoordinateSystem.Click += new System.EventHandler(this.btnDrawCoordinateSystem_Click);
            // 
            // btnDriveOA
            // 
            this.btnDriveOA.Location = new System.Drawing.Point(755, 107);
            this.btnDriveOA.Name = "btnDriveOA";
            this.btnDriveOA.Size = new System.Drawing.Size(31, 24);
            this.btnDriveOA.TabIndex = 66;
            this.btnDriveOA.Text = "→";
            this.btnDriveOA.UseVisualStyleBackColor = true;
            this.btnDriveOA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DriveOA_Down);
            this.btnDriveOA.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DriveOA_Up);
            // 
            // btnDriveOB
            // 
            this.btnDriveOB.Location = new System.Drawing.Point(726, 135);
            this.btnDriveOB.Name = "btnDriveOB";
            this.btnDriveOB.Size = new System.Drawing.Size(31, 24);
            this.btnDriveOB.TabIndex = 67;
            this.btnDriveOB.Text = "↓";
            this.btnDriveOB.UseVisualStyleBackColor = true;
            this.btnDriveOB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DriveOB_Down);
            this.btnDriveOB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DriveOB_Up);
            // 
            // btnWriteConfigFile
            // 
            this.btnWriteConfigFile.Location = new System.Drawing.Point(795, 305);
            this.btnWriteConfigFile.Name = "btnWriteConfigFile";
            this.btnWriteConfigFile.Size = new System.Drawing.Size(199, 46);
            this.btnWriteConfigFile.TabIndex = 68;
            this.btnWriteConfigFile.Text = "将当前所有向量配置作为相机标定";
            this.btnWriteConfigFile.UseVisualStyleBackColor = true;
            this.btnWriteConfigFile.Click += new System.EventHandler(this.btnWriteConfigFile_Click);
            // 
            // btnDriveAO
            // 
            this.btnDriveAO.Location = new System.Drawing.Point(696, 107);
            this.btnDriveAO.Name = "btnDriveAO";
            this.btnDriveAO.Size = new System.Drawing.Size(31, 24);
            this.btnDriveAO.TabIndex = 69;
            this.btnDriveAO.Text = "←";
            this.btnDriveAO.UseVisualStyleBackColor = true;
            this.btnDriveAO.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DriveAO_Down);
            this.btnDriveAO.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DriveAO_Up);
            // 
            // btnDriveBO
            // 
            this.btnDriveBO.Location = new System.Drawing.Point(726, 79);
            this.btnDriveBO.Name = "btnDriveBO";
            this.btnDriveBO.Size = new System.Drawing.Size(31, 24);
            this.btnDriveBO.TabIndex = 70;
            this.btnDriveBO.Text = "↑";
            this.btnDriveBO.UseVisualStyleBackColor = true;
            this.btnDriveBO.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DriveBO_Down);
            this.btnDriveBO.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DriveBO_Up);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::UR_点动控制器.Properties.Resources.GCode;
            this.pictureBox1.Location = new System.Drawing.Point(492, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(310, 160);
            this.pictureBox1.TabIndex = 55;
            this.pictureBox1.TabStop = false;
            // 
            // txtPosOC
            // 
            this.txtPosOC.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPosOC.Location = new System.Drawing.Point(12, 326);
            this.txtPosOC.Name = "txtPosOC";
            this.txtPosOC.Size = new System.Drawing.Size(474, 23);
            this.txtPosOC.TabIndex = 72;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(10, 303);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(119, 14);
            this.label17.TabIndex = 71;
            this.label17.Text = "当前OC单位向量：";
            // 
            // btnVerifyVectors
            // 
            this.btnVerifyVectors.Location = new System.Drawing.Point(643, 305);
            this.btnVerifyVectors.Name = "btnVerifyVectors";
            this.btnVerifyVectors.Size = new System.Drawing.Size(140, 46);
            this.btnVerifyVectors.TabIndex = 73;
            this.btnVerifyVectors.Text = "验证坐标系单位向量";
            this.btnVerifyVectors.UseVisualStyleBackColor = true;
            this.btnVerifyVectors.Click += new System.EventHandler(this.btnVerifyVectors_Click);
            // 
            // btnDriveOC
            // 
            this.btnDriveOC.Location = new System.Drawing.Point(352, 297);
            this.btnDriveOC.Name = "btnDriveOC";
            this.btnDriveOC.Size = new System.Drawing.Size(31, 24);
            this.btnDriveOC.TabIndex = 74;
            this.btnDriveOC.Text = "↑";
            this.btnDriveOC.UseVisualStyleBackColor = true;
            this.btnDriveOC.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DriveOC_Down);
            this.btnDriveOC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DriveOC_Up);
            // 
            // btnDriveCO
            // 
            this.btnDriveCO.Location = new System.Drawing.Point(389, 297);
            this.btnDriveCO.Name = "btnDriveCO";
            this.btnDriveCO.Size = new System.Drawing.Size(31, 24);
            this.btnDriveCO.TabIndex = 75;
            this.btnDriveCO.Text = "↓";
            this.btnDriveCO.UseVisualStyleBackColor = true;
            this.btnDriveCO.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DriveCO_Down);
            this.btnDriveCO.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DriveCO_Up);
            // 
            // GCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 552);
            this.Controls.Add(this.btnDriveCO);
            this.Controls.Add(this.btnDriveOC);
            this.Controls.Add(this.btnVerifyVectors);
            this.Controls.Add(this.txtPosOC);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.btnDriveBO);
            this.Controls.Add(this.btnDriveAO);
            this.Controls.Add(this.btnWriteConfigFile);
            this.Controls.Add(this.btnDriveOB);
            this.Controls.Add(this.btnDriveOA);
            this.Controls.Add(this.btnDrawCoordinateSystem);
            this.Controls.Add(this.btnTryTouch);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnGoBPoint);
            this.Controls.Add(this.btnGoAPoint);
            this.Controls.Add(this.btnGoZeroPoint);
            this.Controls.Add(this.btnRefreshVectors);
            this.Controls.Add(this.txtPosOB);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtPosOA);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnEmbedCode);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBoxCodeList);
            this.Controls.Add(this.btnSaveCode);
            this.Controls.Add(this.btnSendCode);
            this.Controls.Add(this.btnFilterGCode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtAcclerationSlow);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtRadius);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtSpeedSlow);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtAcclerationFast);
            this.Controls.Add(this.btnConvertCode);
            this.Controls.Add(this.btnLoadGCode);
            this.Controls.Add(this.btnResetSpeed);
            this.Controls.Add(this.btnSetSpeed);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSpeedFast);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnPos3_Relative);
            this.Controls.Add(this.btnPos2_Relative);
            this.Controls.Add(this.btnPos1_Relative);
            this.Controls.Add(this.txtPose3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPose2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPose1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GCode";
            this.Load += new System.EventHandler(this.PoseDifference_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPose1;
        private System.Windows.Forms.TextBox txtPose2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPose3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPos1_Relative;
        private System.Windows.Forms.Button btnPos2_Relative;
        private System.Windows.Forms.Button btnPos3_Relative;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtSpeedFast;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSetSpeed;
        private System.Windows.Forms.Button btnResetSpeed;
        private System.Windows.Forms.Button btnLoadGCode;
        private System.Windows.Forms.Button btnConvertCode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRadius;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSpeedSlow;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtAcclerationFast;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAcclerationSlow;
        private System.Windows.Forms.Button btnFilterGCode;
        private System.Windows.Forms.Button btnSendCode;
        private System.Windows.Forms.Button btnSaveCode;
        private System.Windows.Forms.RichTextBox textBoxCodeList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioMoveP;
        private System.Windows.Forms.RadioButton radioMoveL;
        private System.Windows.Forms.RadioButton radioMoveJ;
        private System.Windows.Forms.Button btnEmbedCode;
        private System.Windows.Forms.Button btnGoZeroPoint;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnGoAPoint;
        private System.Windows.Forms.Button btnGoBPoint;
        private System.Windows.Forms.TextBox txtPosOA;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPosOB;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnRefreshVectors;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnTryTouch;
        private System.Windows.Forms.Button btnDrawCoordinateSystem;
        private System.Windows.Forms.Button btnDriveOA;
        private System.Windows.Forms.Button btnDriveOB;
        private System.Windows.Forms.Button btnWriteConfigFile;
        private System.Windows.Forms.Button btnDriveAO;
        private System.Windows.Forms.Button btnDriveBO;
        private System.Windows.Forms.TextBox txtPosOC;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnVerifyVectors;
        private System.Windows.Forms.Button btnDriveOC;
        private System.Windows.Forms.Button btnDriveCO;
    }
}
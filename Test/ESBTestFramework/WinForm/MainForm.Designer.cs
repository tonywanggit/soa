namespace ESB.TestFramework.WinForm
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPressiure = new System.Windows.Forms.TabPage();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMethodName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCallCenterUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.chkIsStepUp = new System.Windows.Forms.CheckBox();
            this.tbThreadNum = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.tabFunction = new System.Windows.Forms.TabPage();
            this.lblInvokeNum = new System.Windows.Forms.Label();
            this.lblThreadNum = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPressiure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbThreadNum)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPressiure);
            this.tabControl1.Controls.Add(this.tabFunction);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(801, 431);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPressiure
            // 
            this.tabPressiure.Controls.Add(this.lblThreadNum);
            this.tabPressiure.Controls.Add(this.lblInvokeNum);
            this.tabPressiure.Controls.Add(this.btnStop);
            this.tabPressiure.Controls.Add(this.txtMessage);
            this.tabPressiure.Controls.Add(this.label6);
            this.tabPressiure.Controls.Add(this.txtMethodName);
            this.tabPressiure.Controls.Add(this.label5);
            this.tabPressiure.Controls.Add(this.txtVersion);
            this.tabPressiure.Controls.Add(this.label4);
            this.tabPressiure.Controls.Add(this.txtServiceName);
            this.tabPressiure.Controls.Add(this.label3);
            this.tabPressiure.Controls.Add(this.txtCallCenterUrl);
            this.tabPressiure.Controls.Add(this.label2);
            this.tabPressiure.Controls.Add(this.btnStart);
            this.tabPressiure.Controls.Add(this.chkIsStepUp);
            this.tabPressiure.Controls.Add(this.tbThreadNum);
            this.tabPressiure.Controls.Add(this.label1);
            this.tabPressiure.Location = new System.Drawing.Point(4, 22);
            this.tabPressiure.Name = "tabPressiure";
            this.tabPressiure.Padding = new System.Windows.Forms.Padding(3);
            this.tabPressiure.Size = new System.Drawing.Size(793, 405);
            this.tabPressiure.TabIndex = 0;
            this.tabPressiure.Text = "压力测试";
            this.tabPressiure.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(696, 77);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 45);
            this.btnStop.TabIndex = 14;
            this.btnStop.Text = "停止测试";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(87, 182);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(576, 205);
            this.txtMessage.TabIndex = 13;
            this.txtMessage.Text = "Message=id=89&Name=555";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 185);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "消息内容：";
            // 
            // txtMethodName
            // 
            this.txtMethodName.Location = new System.Drawing.Point(472, 139);
            this.txtMethodName.Name = "txtMethodName";
            this.txtMethodName.Size = new System.Drawing.Size(191, 21);
            this.txtMethodName.TabIndex = 11;
            this.txtMethodName.Text = "GET:JSON:CollocationFilter";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(405, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "方法名称：";
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(347, 139);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(29, 21);
            this.txtVersion.TabIndex = 9;
            this.txtVersion.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(300, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "版本：";
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(87, 139);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(191, 21);
            this.txtServiceName.TabIndex = 7;
            this.txtServiceName.Text = "WXSC_WeiXinServiceForApp";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "服务名称：";
            // 
            // txtCallCenterUrl
            // 
            this.txtCallCenterUrl.Location = new System.Drawing.Point(87, 90);
            this.txtCallCenterUrl.Name = "txtCallCenterUrl";
            this.txtCallCenterUrl.Size = new System.Drawing.Size(576, 21);
            this.txtCallCenterUrl.TabIndex = 5;
            this.txtCallCenterUrl.Text = "http://10.100.20.214/CallCenter/ESB_InvokeService.ashx";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "调用中心：";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(696, 15);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 45);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "开始测试";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // chkIsStepUp
            // 
            this.chkIsStepUp.AutoSize = true;
            this.chkIsStepUp.Location = new System.Drawing.Point(591, 29);
            this.chkIsStepUp.Name = "chkIsStepUp";
            this.chkIsStepUp.Size = new System.Drawing.Size(72, 16);
            this.chkIsStepUp.TabIndex = 2;
            this.chkIsStepUp.Text = "逐步递增";
            this.chkIsStepUp.UseVisualStyleBackColor = true;
            // 
            // tbThreadNum
            // 
            this.tbThreadNum.Location = new System.Drawing.Point(87, 17);
            this.tbThreadNum.Maximum = 50;
            this.tbThreadNum.Minimum = 1;
            this.tbThreadNum.Name = "tbThreadNum";
            this.tbThreadNum.Size = new System.Drawing.Size(471, 45);
            this.tbThreadNum.TabIndex = 1;
            this.tbThreadNum.Value = 1;
            this.tbThreadNum.Scroll += new System.EventHandler(this.tbThreadNum_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "线程数：";
            // 
            // tabFunction
            // 
            this.tabFunction.Location = new System.Drawing.Point(4, 22);
            this.tabFunction.Name = "tabFunction";
            this.tabFunction.Padding = new System.Windows.Forms.Padding(3);
            this.tabFunction.Size = new System.Drawing.Size(793, 405);
            this.tabFunction.TabIndex = 1;
            this.tabFunction.Text = "功能测试";
            this.tabFunction.UseVisualStyleBackColor = true;
            // 
            // lblInvokeNum
            // 
            this.lblInvokeNum.AutoSize = true;
            this.lblInvokeNum.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvokeNum.ForeColor = System.Drawing.Color.Red;
            this.lblInvokeNum.Location = new System.Drawing.Point(564, 48);
            this.lblInvokeNum.Name = "lblInvokeNum";
            this.lblInvokeNum.Size = new System.Drawing.Size(14, 14);
            this.lblInvokeNum.TabIndex = 15;
            this.lblInvokeNum.Text = "0";
            // 
            // lblThreadNum
            // 
            this.lblThreadNum.AutoSize = true;
            this.lblThreadNum.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThreadNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblThreadNum.Location = new System.Drawing.Point(564, 17);
            this.lblThreadNum.Name = "lblThreadNum";
            this.lblThreadNum.Size = new System.Drawing.Size(14, 14);
            this.lblThreadNum.TabIndex = 16;
            this.lblThreadNum.Text = "1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 431);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "MBSOA测试框架";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPressiure.ResumeLayout(false);
            this.tabPressiure.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbThreadNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPressiure;
        private System.Windows.Forms.TabPage tabFunction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbThreadNum;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox chkIsStepUp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCallCenterUrl;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMethodName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblInvokeNum;
        private System.Windows.Forms.Label lblThreadNum;
    }
}
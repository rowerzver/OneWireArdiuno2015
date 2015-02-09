﻿namespace SerialPortUsing
{
	partial class TestForm
	{
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.CB_displayCOMs = new System.Windows.Forms.Button();
			this.CB_displayUSBs = new System.Windows.Forms.Button();
			this.CB_openPort = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.CB_closePort = new System.Windows.Forms.Button();
			this.tB_PortToOpen = new System.Windows.Forms.TextBox();
			this.CB_writeToPort = new System.Windows.Forms.Button();
			this.tB_toWriteToPort = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.CB_readFromPort = new System.Windows.Forms.Button();
			this.nUD_bytesToRead = new System.Windows.Forms.NumericUpDown();
			this.tB_gotFromPort = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tab_testing = new System.Windows.Forms.TabPage();
			this.button1 = new System.Windows.Forms.Button();
			this.tab_uidReading = new System.Windows.Forms.TabPage();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.CoB_coms = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.nUD_bytesToRead)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tab_testing.SuspendLayout();
			this.tab_uidReading.SuspendLayout();
			this.SuspendLayout();
			// 
			// CB_displayCOMs
			// 
			this.CB_displayCOMs.Location = new System.Drawing.Point(6, 6);
			this.CB_displayCOMs.Name = "CB_displayCOMs";
			this.CB_displayCOMs.Size = new System.Drawing.Size(191, 23);
			this.CB_displayCOMs.TabIndex = 0;
			this.CB_displayCOMs.Text = "Имена и доступность COM";
			this.CB_displayCOMs.UseVisualStyleBackColor = true;
			this.CB_displayCOMs.Click += new System.EventHandler(this.CB_displayCOMs_Click);
			// 
			// CB_displayUSBs
			// 
			this.CB_displayUSBs.Location = new System.Drawing.Point(203, 6);
			this.CB_displayUSBs.Name = "CB_displayUSBs";
			this.CB_displayUSBs.Size = new System.Drawing.Size(191, 23);
			this.CB_displayUSBs.TabIndex = 2;
			this.CB_displayUSBs.Text = "Имена и доступность USB";
			this.CB_displayUSBs.UseVisualStyleBackColor = true;
			this.CB_displayUSBs.Click += new System.EventHandler(this.CB_displayUSBs_Click);
			// 
			// CB_openPort
			// 
			this.CB_openPort.Location = new System.Drawing.Point(134, 17);
			this.CB_openPort.Name = "CB_openPort";
			this.CB_openPort.Size = new System.Drawing.Size(229, 23);
			this.CB_openPort.TabIndex = 3;
			this.CB_openPort.Text = "Открыть порт";
			this.CB_openPort.UseVisualStyleBackColor = true;
			this.CB_openPort.Click += new System.EventHandler(this.CB_openPort_Click);
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(6, 35);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(448, 329);
			this.listBox1.TabIndex = 4;
			this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
			// 
			// CB_closePort
			// 
			this.CB_closePort.Location = new System.Drawing.Point(49, 103);
			this.CB_closePort.Name = "CB_closePort";
			this.CB_closePort.Size = new System.Drawing.Size(191, 23);
			this.CB_closePort.TabIndex = 5;
			this.CB_closePort.Text = "Закрыть порт";
			this.CB_closePort.UseVisualStyleBackColor = true;
			this.CB_closePort.Click += new System.EventHandler(this.CB_closePort_Click);
			// 
			// tB_PortToOpen
			// 
			this.tB_PortToOpen.Location = new System.Drawing.Point(6, 19);
			this.tB_PortToOpen.Name = "tB_PortToOpen";
			this.tB_PortToOpen.Size = new System.Drawing.Size(100, 20);
			this.tB_PortToOpen.TabIndex = 6;
			this.tB_PortToOpen.Text = "COM1";
			// 
			// CB_writeToPort
			// 
			this.CB_writeToPort.Location = new System.Drawing.Point(134, 46);
			this.CB_writeToPort.Name = "CB_writeToPort";
			this.CB_writeToPort.Size = new System.Drawing.Size(229, 23);
			this.CB_writeToPort.TabIndex = 7;
			this.CB_writeToPort.Text = "Писать в порт";
			this.CB_writeToPort.UseVisualStyleBackColor = true;
			this.CB_writeToPort.Click += new System.EventHandler(this.CB_writeToPort_Click);
			// 
			// tB_toWriteToPort
			// 
			this.tB_toWriteToPort.Location = new System.Drawing.Point(6, 48);
			this.tB_toWriteToPort.Name = "tB_toWriteToPort";
			this.tB_toWriteToPort.Size = new System.Drawing.Size(100, 20);
			this.tB_toWriteToPort.TabIndex = 8;
			this.tB_toWriteToPort.Text = "WorD";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(112, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(13, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = ">";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(112, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(13, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = ">";
			// 
			// CB_readFromPort
			// 
			this.CB_readFromPort.Location = new System.Drawing.Point(66, 75);
			this.CB_readFromPort.Name = "CB_readFromPort";
			this.CB_readFromPort.Size = new System.Drawing.Size(129, 23);
			this.CB_readFromPort.TabIndex = 11;
			this.CB_readFromPort.Text = "Читать из порта";
			this.CB_readFromPort.UseVisualStyleBackColor = true;
			this.CB_readFromPort.Click += new System.EventHandler(this.CB_readFromPort_Click);
			// 
			// nUD_bytesToRead
			// 
			this.nUD_bytesToRead.Location = new System.Drawing.Point(6, 77);
			this.nUD_bytesToRead.Name = "nUD_bytesToRead";
			this.nUD_bytesToRead.Size = new System.Drawing.Size(54, 20);
			this.nUD_bytesToRead.TabIndex = 12;
			this.nUD_bytesToRead.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
			// 
			// tB_gotFromPort
			// 
			this.tB_gotFromPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tB_gotFromPort.Location = new System.Drawing.Point(219, 77);
			this.tB_gotFromPort.Name = "tB_gotFromPort";
			this.tB_gotFromPort.Size = new System.Drawing.Size(140, 20);
			this.tB_gotFromPort.TabIndex = 13;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.tB_PortToOpen);
			this.groupBox1.Controls.Add(this.tB_gotFromPort);
			this.groupBox1.Controls.Add(this.CB_openPort);
			this.groupBox1.Controls.Add(this.nUD_bytesToRead);
			this.groupBox1.Controls.Add(this.CB_closePort);
			this.groupBox1.Controls.Add(this.CB_readFromPort);
			this.groupBox1.Controls.Add(this.CB_writeToPort);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.tB_toWriteToPort);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(460, 35);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(368, 137);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "COM port using";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(200, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(13, 13);
			this.label3.TabIndex = 14;
			this.label3.Text = ">";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tab_testing);
			this.tabControl1.Controls.Add(this.tab_uidReading);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(842, 414);
			this.tabControl1.TabIndex = 15;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tab_testing
			// 
			this.tab_testing.Controls.Add(this.button1);
			this.tab_testing.Controls.Add(this.CB_displayCOMs);
			this.tab_testing.Controls.Add(this.groupBox1);
			this.tab_testing.Controls.Add(this.CB_displayUSBs);
			this.tab_testing.Controls.Add(this.listBox1);
			this.tab_testing.Location = new System.Drawing.Point(4, 22);
			this.tab_testing.Name = "tab_testing";
			this.tab_testing.Padding = new System.Windows.Forms.Padding(3);
			this.tab_testing.Size = new System.Drawing.Size(834, 388);
			this.tab_testing.TabIndex = 0;
			this.tab_testing.Text = "test";
			this.tab_testing.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(724, 341);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(84, 23);
			this.button1.TabIndex = 15;
			this.button1.Text = "Авторизация";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// tab_uidReading
			// 
			this.tab_uidReading.Controls.Add(this.textBox1);
			this.tab_uidReading.Controls.Add(this.button3);
			this.tab_uidReading.Controls.Add(this.label4);
			this.tab_uidReading.Controls.Add(this.CoB_coms);
			this.tab_uidReading.Location = new System.Drawing.Point(4, 22);
			this.tab_uidReading.Name = "tab_uidReading";
			this.tab_uidReading.Padding = new System.Windows.Forms.Padding(3);
			this.tab_uidReading.Size = new System.Drawing.Size(834, 388);
			this.tab_uidReading.TabIndex = 1;
			this.tab_uidReading.Text = "uid";
			this.tab_uidReading.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(6, 46);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(822, 336);
			this.textBox1.TabIndex = 3;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(133, 17);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(123, 23);
			this.button3.TabIndex = 2;
			this.button3.Text = "Listen for ASCII output";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(133, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "COM-порт для слушания:";
			// 
			// CoB_coms
			// 
			this.CoB_coms.FormattingEnabled = true;
			this.CoB_coms.Location = new System.Drawing.Point(6, 19);
			this.CoB_coms.Name = "CoB_coms";
			this.CoB_coms.Size = new System.Drawing.Size(121, 21);
			this.CoB_coms.TabIndex = 0;
			// 
			// TestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(863, 435);
			this.Controls.Add(this.tabControl1);
			this.Name = "TestForm";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.nUD_bytesToRead)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tab_testing.ResumeLayout(false);
			this.tab_uidReading.ResumeLayout(false);
			this.tab_uidReading.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button CB_displayCOMs;
		private System.Windows.Forms.Button CB_displayUSBs;
		private System.Windows.Forms.Button CB_openPort;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button CB_closePort;
		private System.Windows.Forms.TextBox tB_PortToOpen;
		private System.Windows.Forms.Button CB_writeToPort;
		private System.Windows.Forms.TextBox tB_toWriteToPort;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button CB_readFromPort;
		private System.Windows.Forms.NumericUpDown nUD_bytesToRead;
		private System.Windows.Forms.TextBox tB_gotFromPort;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tab_testing;
		private System.Windows.Forms.TabPage tab_uidReading;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox CoB_coms;
        private System.Windows.Forms.Button button1;
	}
}


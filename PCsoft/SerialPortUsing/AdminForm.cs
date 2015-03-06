﻿using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.IO.Ports;
using System.Windows.Forms;
using SerialPortUsing.Access_control_in_OneWireTableAdapters;
using SerialPortUsing.Properties;

namespace SerialPortUsing {
	public partial class AdminForm : Form
	{
	    private const int GROUP_BOXES_PADDING = 10;
		private const int GROUP_BOXES_TOP = 21;

		public AdminForm() {
			InitializeComponent();
		    gb_users.Location = gb_sys.Location;
		    gb_users.Size = gb_sys.Size;
            openFileDialog1.Filter = "accdb files (*.accdb)|*.accdb|All files (*.*)|*.*";//Маска OpenFileDialog
		    openFileDialog2.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";


			MakeGroupBoxesUp();
		}

		private void MakeGroupBoxesUp() {
			gb_staff.Left = gb_sys.Left = gb_users.Left = tv_navigation.Width + GROUP_BOXES_PADDING + tv_navigation.Left;
			gb_staff.Top = gb_sys.Top = gb_users.Top = GROUP_BOXES_TOP;
			gb_staff.Width = gb_sys.Width = gb_users.Width = this.Width - gb_staff.Left - GROUP_BOXES_PADDING*2;
			gb_staff.Height = gb_sys.Height = gb_users.Height = this.Height - 72;
		}

		private void AdminForm_Load(object sender, EventArgs e)
		{
			// TODO: This line of code loads data into the 'access_control_in_OneWire.Staff' table. You can move, or remove it, as needed.
			this.staffTableAdapter.Fill(this.access_control_in_OneWire.Staff);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "access_control_in_OneWire.SystemUsers". При необходимости она может быть перемещена или удалена.
            this.systemUsersTableAdapter.Fill(this.access_control_in_OneWire.SystemUsers);
			

			InitializeQueries();

            foreach (string port in SerialPort.GetPortNames()){
                cb_ComPort.Items.Add(port);
            }
            gb_staff.BringToFront();
		}

		private void InitializeQueries()
		{
			systemUsersTableAdapter.Adapter.DeleteCommand = new OleDbCommand("Delete * from SystemUsers where Uid = par0", systemUsersTableAdapter.Connection);
			systemUsersTableAdapter.Adapter.UpdateCommand = new OleDbCommand("UPDATE SystemUsers SET SystemUsers.Uid = newuid, SystemUsers.userType = utype, SystemUsers.login = log, SystemUsers.password = pass WHERE SystemUsers.Uid = olduid", systemUsersTableAdapter.Connection); // Don't delete table names or it'll crash.
			staffTableAdapter.Adapter.InsertCommand =  new OleDbCommand("INSERT INTO staff (Сотрудник, Должность, UID, Фото, [Табельный номер], [Номер паспорта], [Дата найма], График, Заблокирован, Подразделение, [Тип UID]) VALUES (Сотр, Долж, Юид, Фотка, Табель, НомерПасп, ДатаН, Графк, Забл, Подразд, ТипЮид)", staffTableAdapter.Connection);
		}

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void панельНавигатораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switchNavigationPanel(панельНавигатораToolStripMenuItem.Checked);
        }

	    private void switchNavigationPanel(bool open)
	    {
	        if (open)
	        {
                tv_navigation.Width = tv_navigation.Width;
	        }
	        else
	        {
                tv_navigation.Width = 0;
	        }

            // TODO: do this for all groupboxes!
            gb_sys.Left = tv_navigation.Width + GROUP_BOXES_PADDING;

            // TODO: do this for all groupboxes!
            gb_sys.Width = this.Width - tv_navigation.Width - GROUP_BOXES_PADDING*2 - tv_navigation.Left;

	    }

        private void b_saveConfig_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            tb_dbpath.Text = openFileDialog1.FileName;
        }



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (tv_navigation.SelectedNode.Text){
                case "Настройки":
					gb_sys.BringToFront();
                    break;
				case "Пользователи":
					gb_users.BringToFront();
					break;
				case "Сотрудники":
					gb_staff.BringToFront();
					break;
            }
            //MessageBox.Show(tv_navigation.SelectedNode.Text);
        }

        private void b_addUser_Click(object sender, EventArgs e)
        {
			if (string.IsNullOrEmpty(tB_UID.Text) || string.IsNullOrEmpty(cB_userType.Text) 
			|| string.IsNullOrEmpty(tB_login.Text) || string.IsNullOrEmpty(tB_password.Text))
			{
				MessageBox.Show("Лох, пидр, нет друзей!!!");
				return;
			}
	        systemUsersTableAdapter.Insert(tB_UID.Text, cB_userType.Text, tB_login.Text, tB_password.Text);
			this.systemUsersTableAdapter.Fill(this.access_control_in_OneWire.SystemUsers);
	        //dgv_users.Rows.Add(tB_UID.Text, cB_userType.Text, tB_login.Text, tB_password.Text);
        }

		private void b_delUser_Click(object sender, EventArgs e)
		{

			var rez = MessageBox.Show(this, "Вы действительно хотите удалить " + dgv_users.SelectedRows.Count +" записей?", "Подтверждение", MessageBoxButtons.OKCancel);
			if(rez==DialogResult.Cancel)return;

			systemUsersTableAdapter.Adapter.DeleteCommand.Connection.Open();
			foreach (DataGridViewRow r in dgv_users.SelectedRows)
			{
				string test = r.Cells[0].Value.ToString();
				systemUsersTableAdapter.Adapter.DeleteCommand.Parameters.Add(new OleDbParameter("par0", test));
				systemUsersTableAdapter.Adapter.DeleteCommand.ExecuteNonQuery();
				systemUsersTableAdapter.Adapter.DeleteCommand.Parameters.Clear();
			}
			systemUsersTableAdapter.Adapter.DeleteCommand.Connection.Close();
			this.systemUsersTableAdapter.Fill(this.access_control_in_OneWire.SystemUsers);
		}



		private void dgv_users_CellDoubleClick(object sender, DataGridViewCellEventArgs e){
			tB_UID.Text = dgv_users.SelectedRows[0].Cells[0].Value.ToString(); 
			cB_userType.Text = dgv_users.SelectedRows[0].Cells[1].Value.ToString();
			tB_login.Text = dgv_users.SelectedRows[0].Cells[2].Value.ToString();
			tB_password.Text = dgv_users.SelectedRows[0].Cells[3].Value.ToString();
		}

		private void b_editUser_Click(object sender, EventArgs e)
		{
			if (dgv_users.SelectedRows.Count == 0 || dgv_users.SelectedRows.Count > 1){
				MessageBox.Show(this, "Должна быть выделена одна строка.", "Некорректное выделение");
				return;
			}
			systemUsersTableAdapter.Adapter.UpdateCommand.Connection.Open();
			systemUsersTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("newuid", tB_UID.Text));
			systemUsersTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("utype", cB_userType.Text));
			systemUsersTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("log", tB_login.Text));
			systemUsersTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("pass", tB_password.Text));
			systemUsersTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("olduid", dgv_users.SelectedRows[0].Cells[0].Value.ToString()));
			systemUsersTableAdapter.Adapter.UpdateCommand.ExecuteNonQuery();
			systemUsersTableAdapter.Adapter.UpdateCommand.Parameters.Clear();
			systemUsersTableAdapter.Adapter.UpdateCommand.Connection.Close();

			this.systemUsersTableAdapter.Fill(this.access_control_in_OneWire.SystemUsers);
		}

        private void b_choosePhoto_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            tB_photoPath.Text = openFileDialog2.FileName;
        }

        private void b_addStaff_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tB_staff.Text) || string.IsNullOrEmpty(cB_profession.Text)
            || string.IsNullOrEmpty(tB_photoPath.Text) || string.IsNullOrEmpty(tB_UID_from_gb_staff.Text)
            || string.IsNullOrEmpty(tB_number.Text) || string.IsNullOrEmpty(tB_multiPasport.Text)
            || string.IsNullOrEmpty(dateTimePicker1.Text) || string.IsNullOrEmpty(cB_workTime.Text)
            || string.IsNullOrEmpty(cB_subdivision.Text) || string.IsNullOrEmpty(cB_UID_type_from_gb_staff.Text))
            {
                MessageBox.Show("Лох, пидр, нет друзей!!!");
                return;
            }
			////staffTableAdapter.Insert(
			//cB_profession.Text, 
			//tB_photoPath.Text, 
			//tB_UID_from_gb_staff.Text, 
			//Convert.ToDouble(tB_number.Text), 
			//Convert.ToDouble(tB_multiPasport.Text),
			// DateTime.Parse(dateTimePicker1.Value.ToString("yy-mm-dd")), 
			//cB_workTime.Text, 
			//Convert.ToChar(cB_blocked.Checked), 
			//cB_subdivision.Text, 
			//cB_UID_type_from_gb_staff.Text,
			//// tB_staff.Text);
			////System.Data.SqlTypes.SqlDateTime
			staffTableAdapter.Adapter.InsertCommand.Connection.Open();
			staffTableAdapter.Adapter.InsertCommand.Parameters.Clear();
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Сотр", tB_staff.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Долж", cB_profession.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Юид", tB_UID_from_gb_staff.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Фотка", tB_photoPath.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Табель", tB_number.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("НомерПасп", tB_multiPasport.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("ДатаН", dateTimePicker1.Value.ToString("yy-MM-dd")));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Графк", cB_workTime.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Забл", cB_blocked.Checked?"1":"0"));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Подразд", cB_subdivision.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("ТипЮид", cB_UID_type_from_gb_staff.Text));
			//(Сотр, Долж, Юид, Фотка, Табель, НомерПасп, ДатаН, Графк, Забл, Подразд, ТипЮид)
			staffTableAdapter.Adapter.InsertCommand.ExecuteNonQuery();
			staffTableAdapter.Adapter.InsertCommand.Connection.Close();

            staffTableAdapter.Fill(access_control_in_OneWire.Staff);
        }

        private void b_saveConfig_Click_1(object sender, EventArgs e){
            Settings.Default.COMName = cb_ComPort.Text;
            Settings.Default.BaudRate = Convert.ToInt32(cb_speed.Text);
            Settings.Default.Save();
        }

        private void dgv_staff_CellDoubleClick(object sender, DataGridViewCellEventArgs e){
            tB_staff.Text =						dgv_staff.SelectedRows[0].Cells[0].Value.ToString(); 
			cB_profession.Text =				dgv_staff.SelectedRows[0].Cells[1].Value.ToString();
			tB_photoPath.Text =					dgv_staff.SelectedRows[0].Cells[2].Value.ToString();
			tB_UID_from_gb_staff.Text =			dgv_staff.SelectedRows[0].Cells[3].Value.ToString();
            tB_number.Text =					dgv_staff.SelectedRows[0].Cells[4].Value.ToString();
            tB_multiPasport.Text =				dgv_staff.SelectedRows[0].Cells[5].Value.ToString();
            dateTimePicker1.Text =				dgv_staff.SelectedRows[0].Cells[6].Value.ToString();
            cB_workTime.Text =					dgv_staff.SelectedRows[0].Cells[7].Value.ToString();
			cB_blocked.Checked =				Convert.ToBoolean(dgv_staff.SelectedRows[0].Cells[8].Value);
            cB_subdivision.Text =				dgv_staff.SelectedRows[0].Cells[9].Value.ToString();
			cB_UID_type_from_gb_staff.Text =	dgv_staff.SelectedRows[0].Cells[10].Value.ToString();
        }
	}
}

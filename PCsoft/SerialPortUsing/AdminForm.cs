﻿using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using SerialPortUsing.Access_control_in_OneWireTableAdapters;
using SerialPortUsing.Properties;

namespace SerialPortUsing {
	public partial class AdminForm : Form
	{
		#region Constants
	    private const int GROUP_BOXES_PADDING = 5;
		private const int GROUP_BOXES_TOP = 25;
		private const int GROUP_BOXES_VERTICAL_MARGIN = 72;
		private const int NAVIGATION_PANEL_WIDTH = 132;
		private const int NAVIGATION_PANEL_LEFT = 12;
		#endregion

		#region Structing
		public AdminForm() {
			InitializeComponent();
		    gb_users.Location = gb_sys.Location;
		    gb_users.Size = gb_sys.Size;
            openFileDialog1.Filter = "accdb files (*.accdb)|*.accdb|All files (*.*)|*.*";//Маска OpenFileDialog
		    openFileDialog2.Filter = "jpg files (*.jpg)|*.jpg||*.png|All files (*.*)|*.*";


			MakeGroupBoxesUp();
		}

		private void MakeGroupBoxesUp() { // TODO: do this for all groupboxes!
			gB_event.Left =		gb_users.Left =		gb_staff.Left =		gb_sys.Left =		
					tv_navigation.Left + tv_navigation.Width + GROUP_BOXES_PADDING;
			gB_event.Top =		gb_users.Top =		gb_staff.Top =		gb_sys.Top =		
					gb_users.Top = GROUP_BOXES_TOP;
			gB_event.Width =	gb_users.Width =	gb_staff.Width =	gb_sys.Width =		
					this.Width - GROUP_BOXES_PADDING * 6 - tv_navigation.Width - tv_navigation.Left;
			gB_event.Height =	gb_users.Height =	gb_staff.Height =	gb_sys.Height =
					this.Height - GROUP_BOXES_VERTICAL_MARGIN;
		}

		private void AdminForm_Load(object sender, EventArgs e)
		{
			// TODO: данная строка кода позволяет загрузить данные в таблицу "access_control_in_OneWire.EventLog". При необходимости она может быть перемещена или удалена.
			//this.joinedEventLogAdapter.Fill(this.access_control_in_OneWire.JoinedEventLog);
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
			systemUsersTableAdapter.Adapter.DeleteCommand = new OleDbCommand("Delete * from SystemUsers where login = par0", systemUsersTableAdapter.Connection);
			systemUsersTableAdapter.Adapter.UpdateCommand = new OleDbCommand("UPDATE SystemUsers SET SystemUsers.userType = utype, SystemUsers.login = log, SystemUsers.PasswordHashe = passHashe, SystemUsers.PasswordSalt = passSalt WHERE SystemUsers.login = oldLogin", systemUsersTableAdapter.Connection); // Don't delete table names or it'll crash.
			staffTableAdapter.Adapter.InsertCommand =  new OleDbCommand("INSERT INTO staff (Сотрудник, Должность, UID, Фото, [Номер паспорта], [Дата найма], График, Заблокирован, Подразделение, [Тип UID]) VALUES (Сотр, Долж, Юид, Фотка, НомерПасп, ДатаН, Графк, Забл, Подразд, ТипЮид)", staffTableAdapter.Connection);
			staffTableAdapter.Adapter.DeleteCommand = new OleDbCommand("Delete * from Staff where [Табельный номер] = par1", staffTableAdapter.Connection);
			staffTableAdapter.Adapter.UpdateCommand = new OleDbCommand("UPDATE Staff SET Staff.Сотрудник = @staffnames, Staff.Должность = @profession, Staff.UID = @StaffUID, Staff.Фото = @Photo,"+/*" Staff.[Табельный номер] = @newNumber,"+*/" Staff.[Номер паспорта] = @multiPasport, Staff.[Дата найма] = @empdate, Staff.График = @workTime, Staff.Заблокирован = @blocked, Staff.Подразделение = @subdivision, Staff.[Тип UID] = @TypeUID WHERE Staff.[Табельный номер] = @oldNumber", staffTableAdapter.Connection);
		}
		#endregion

		#region Navigation panel switching
        private void панельНавигатораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetNavigationPanelState(панельНавигатораToolStripMenuItem.Checked);
        }

	    private void SetNavigationPanelState(bool open)
	    {
	        if (open)
	        {
                tv_navigation.Width = NAVIGATION_PANEL_WIDTH;
				tv_navigation.Left = NAVIGATION_PANEL_LEFT;
	        }
	        else
	        {
				tv_navigation.Left = 0;
                tv_navigation.Width = 0;
	        }

            // TODO: do this for all groupboxes!
			 gB_event.Left = gb_staff.Left = gb_sys.Left = gb_users.Left = 
					tv_navigation.Left + tv_navigation.Width + GROUP_BOXES_PADDING;
			 gB_event.Width = gb_staff.Width = gb_sys.Width = gb_users.Width = 
					this.Width - GROUP_BOXES_PADDING * 6 - tv_navigation.Width - tv_navigation.Left;

	    }
		#endregion

		#region GeneralSettings groupbox controls events
        private void b_DBPath_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            tb_dbpath.Text = openFileDialog1.FileName;
        }
		private void b_saveConfig_Click_1(object sender, EventArgs e) {
			Settings.Default.COMName = cb_ComPort.Text;
			Settings.Default.BaudRate = Convert.ToInt32(cb_speed.Text);
			Settings.Default.Save();
		}
		#endregion

		#region Users groupbox controls events
        private void b_addUser_Click(object sender, EventArgs e)
        {
			if ( string.IsNullOrEmpty(cB_userType.Text) || string.IsNullOrEmpty(tB_login.Text) || string.IsNullOrEmpty(tB_password.Text))
			{
				MessageBox.Show(this, "Нельзя внести пустое значение параметра." + Environment.NewLine + "Пожалуйста, введите все параметры.", "Ошибка");
				return;
			}
	        string pass = tB_password.Text;
	        string salt = "";
	        string hashe = SHA512.EncodePassword(pass, 16, ref salt);
	        try
	        {
		        systemUsersTableAdapter.Insert(cB_userType.Text, tB_login.Text, hashe, salt);
	        }
	        catch (Exception ex)
	        {
		        MessageBox.Show(ex.Message);
	        }
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
				string login = r.Cells[1].Value.ToString();
				systemUsersTableAdapter.Adapter.DeleteCommand.Parameters.Add(new OleDbParameter("par0", login));
				systemUsersTableAdapter.Adapter.DeleteCommand.ExecuteNonQuery();
				systemUsersTableAdapter.Adapter.DeleteCommand.Parameters.Clear();
			}
			systemUsersTableAdapter.Adapter.DeleteCommand.Connection.Close();
			 this.systemUsersTableAdapter.Fill(access_control_in_OneWire.SystemUsers);
		}



		private void dgv_users_CellDoubleClick(object sender, DataGridViewCellEventArgs e){
			cB_userType.Text = dgv_users.SelectedRows[0].Cells[0].Value.ToString();
			tB_login.Text = dgv_users.SelectedRows[0].Cells[1].Value.ToString();
			tB_password.Text = "";
		}

		private void b_editUser_Click(object sender, EventArgs e)
		{
			if (dgv_users.SelectedRows.Count == 0 || dgv_users.SelectedRows.Count > 1){
				MessageBox.Show(this, "Должна быть выделена одна строка.", "Некорректное выделение");
				return;
			}

			string pass = tB_password.Text;
			string salt = "";
			string hashe = SHA512.EncodePassword(pass, 16, ref salt);

			systemUsersTableAdapter.Adapter.UpdateCommand.Connection.Open();
			systemUsersTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("utype", cB_userType.Text));
			systemUsersTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("log", tB_login.Text));
			systemUsersTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("passHashe", hashe));
			systemUsersTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("passSalt", salt));
			systemUsersTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("oldLogin", dgv_users.SelectedRows[0].Cells[1].Value.ToString()));
			systemUsersTableAdapter.Adapter.UpdateCommand.ExecuteNonQuery();
			systemUsersTableAdapter.Adapter.UpdateCommand.Parameters.Clear();
			systemUsersTableAdapter.Adapter.UpdateCommand.Connection.Close();

			this.systemUsersTableAdapter.Fill(this.access_control_in_OneWire.SystemUsers);
		}
		#endregion

		#region Staff groupbox controls events
        private void b_choosePhoto_Click(object sender, EventArgs e){
            openFileDialog2.ShowDialog();
            tB_photoPath.Text = openFileDialog2.FileName;
        }

        private void b_addStaff_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tB_staff.Text) || string.IsNullOrEmpty(cB_profession.Text)
            || string.IsNullOrEmpty(tB_photoPath.Text) || string.IsNullOrEmpty(tB_UID_from_gb_staff.Text)
            || /*string.IsNullOrEmpty(tB_number.Text) ||*/ string.IsNullOrEmpty(tB_multiPasport.Text)
            || string.IsNullOrEmpty(dateTimePicker1.Text) || string.IsNullOrEmpty(cB_workTime.Text)
            || string.IsNullOrEmpty(cB_subdivision.Text) || string.IsNullOrEmpty(cB_UID_type_from_gb_staff.Text))
            {
                MessageBox.Show(this, "Нельзя внести пустое значение параметра."+Environment.NewLine+"Пожалуйста, введите все параметры.", "Ошибка");
                return;
            }
			staffTableAdapter.Adapter.InsertCommand.Connection.Open();
			staffTableAdapter.Adapter.InsertCommand.Parameters.Clear();
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Сотр", tB_staff.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Долж", cB_profession.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Юид", tB_UID_from_gb_staff.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Фотка", tB_photoPath.Text));
			//staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Табель", tB_number.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("НомерПасп", tB_multiPasport.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("ДатаН", dateTimePicker1.Value.ToString("yy-MM-dd")));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Графк", cB_workTime.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Забл", cB_blocked.Checked?"1":"0"));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("Подразд", cB_subdivision.Text));
			staffTableAdapter.Adapter.InsertCommand.Parameters.Add(new OleDbParameter("ТипЮид", cB_UID_type_from_gb_staff.Text));
			//(Сотр, Долж, Юид, Фотка, Табель, НомерПасп, ДатаН, Графк, Забл, Подразд, ТипЮид)
	        try{
				staffTableAdapter.Adapter.InsertCommand.ExecuteNonQuery();
	        }catch (Exception ex){MessageBox.Show(ex.Message);}
			
			staffTableAdapter.Adapter.InsertCommand.Connection.Close();

            staffTableAdapter.Fill(access_control_in_OneWire.Staff);
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

		private void b_delStaff_Click(object sender, EventArgs e)
		{
			var rez = MessageBox.Show(this, "Вы действительно хотите удалить " + dgv_staff.SelectedRows.Count + " записей?", "Подтверждение", MessageBoxButtons.OKCancel);
			if (rez == DialogResult.Cancel) return;

			staffTableAdapter.Adapter.DeleteCommand.Connection.Open();
			foreach (DataGridViewRow m in dgv_staff.SelectedRows)
			{
				string test = m.Cells[4].Value.ToString();
				staffTableAdapter.Adapter.DeleteCommand.Parameters.Add(new OleDbParameter("par1", test));
				staffTableAdapter.Adapter.DeleteCommand.ExecuteNonQuery();
				staffTableAdapter.Adapter.DeleteCommand.Parameters.Clear();
			}
			staffTableAdapter.Adapter.DeleteCommand.Connection.Close();
			this.staffTableAdapter.Fill(this.access_control_in_OneWire.Staff);
		}

		private void b_edditStaff_Click(object sender, EventArgs e){
			if (string.IsNullOrEmpty(tB_staff.Text) || string.IsNullOrEmpty(cB_profession.Text)
				|| string.IsNullOrEmpty(tB_photoPath.Text) || string.IsNullOrEmpty(tB_UID_from_gb_staff.Text)
				/*|| string.IsNullOrEmpty(tB_number.Text)*/ || string.IsNullOrEmpty(tB_multiPasport.Text)
				|| string.IsNullOrEmpty(dateTimePicker1.Text) || string.IsNullOrEmpty(cB_workTime.Text)
				|| string.IsNullOrEmpty(cB_subdivision.Text) || string.IsNullOrEmpty(cB_UID_type_from_gb_staff.Text))
			{
				MessageBox.Show(this, "Нельзя внести пустое значение параметра." + Environment.NewLine + "Пожалуйста, введите все параметры.", "Ошибка");
				return;
			}
			if (dgv_staff.SelectedRows.Count == 0 || dgv_staff.SelectedRows.Count > 1){
				MessageBox.Show(this, "Должна быть выделена одна строка.", "Некорректное выделение");
				return;
			}
			staffTableAdapter.Adapter.UpdateCommand.Connection.Open();
			staffTableAdapter.Adapter.UpdateCommand.Parameters.Clear();
			staffTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("@staffnames", tB_staff.Text));
			staffTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("@profession", cB_profession.Text));
			staffTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("@StaffUID", tB_UID_from_gb_staff.Text));
			staffTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("@Photo", tB_photoPath.Text));
			//staffTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("@newNumber", tB_number.Text));
			staffTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("@multiPasport", tB_multiPasport.Text));
			staffTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("@empdate", dateTimePicker1.Value.ToString("yy-MM-dd")));
			staffTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("@workTime", cB_workTime.Text));
			staffTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("@blocked", cB_blocked.Checked?"1":"0"));
			staffTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("@subdivision", cB_subdivision.Text));
			staffTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("@TypeUID", cB_UID_type_from_gb_staff.Text));
			staffTableAdapter.Adapter.UpdateCommand.Parameters.Add(new OleDbParameter("@oldNumber", dgv_staff.SelectedRows[0].Cells[4].Value.ToString()));
			staffTableAdapter.Adapter.UpdateCommand.ExecuteNonQuery();
			staffTableAdapter.Adapter.UpdateCommand.Parameters.Clear();
			staffTableAdapter.Adapter.UpdateCommand.Connection.Close();

			this.staffTableAdapter.Fill(this.access_control_in_OneWire.Staff);
		}

		private void b_readUIDStaff_Click(object sender, EventArgs e){
			UIDReadingForm	uidReadingForm = new UIDReadingForm();
			DialogResult rez = uidReadingForm.ShowDialog();
			if(rez == DialogResult.OK)
				tB_UID_from_gb_staff.Text = uidReadingForm.ResultingUID;
		}
		#endregion

		#region Events groupbox controls events
		private void выходToolStripMenuItem_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void b_setPeriod_Click(object sender, EventArgs e) {
			DateTime todayStart = DateTime.Now; // Датовремя, указывающая на ПЕРВУЮ секунду текущих суток.
			todayStart = todayStart.AddHours(-todayStart.Hour);
			todayStart = todayStart.AddMinutes(-todayStart.Minute);
			todayStart = todayStart.AddSeconds(-todayStart.Second);
			DateTime todayEnd = DateTime.Now; // Датовремя, указывающая на ПОСЛЕДНЮЮ секунду текущих суток. Да, немного в будущее.
			todayEnd = todayEnd.AddHours(23 - todayEnd.Hour);
			todayEnd = todayEnd.AddMinutes(59 - todayEnd.Minute);
			todayEnd = todayEnd.AddSeconds(59 - todayEnd.Second);
			switch (((Button)sender).Text) {
				case "Сегодня":
					joinedEventLogAdapter.FillByPeriod(access_control_in_OneWire.JoinedEventLog, todayStart, todayEnd);
					break;
				case "Последняя неделя":
					joinedEventLogAdapter.FillByPeriod(access_control_in_OneWire.JoinedEventLog, todayStart.AddDays(-7), todayEnd);
					break;
				case "Последние 10 дней":
					joinedEventLogAdapter.FillByPeriod(access_control_in_OneWire.JoinedEventLog, todayStart.AddDays(-10), todayEnd);
					break;
				case "С начала месяца":
					joinedEventLogAdapter.FillByPeriod(access_control_in_OneWire.JoinedEventLog, todayStart.AddDays(-todayEnd.Day), todayEnd);
					break;
				case "Прошлый месяц":
					joinedEventLogAdapter.FillByPeriod(access_control_in_OneWire.JoinedEventLog, todayStart.AddDays(-todayEnd.Day).AddMonths(-1), todayEnd.AddDays(-todayEnd.Day));
					break;
				case "Показать данные за промежуток"://dateTimePicker1.Value.ToString("yy-MM-dd")
					DateTime periodStart = dtp_eventStart.Value.AddHours(-dtp_eventStart.Value.Hour).AddMinutes(-dtp_eventStart.Value.Minute).AddSeconds(-dtp_eventStart.Value.Second);
					DateTime periodEnd = dtp_eventEnd.Value.AddHours(23 - dtp_eventEnd.Value.Hour).AddMinutes(59 - dtp_eventEnd.Value.Minute).AddSeconds(59 - dtp_eventEnd.Value.Second);
					joinedEventLogAdapter.FillByPeriod(access_control_in_OneWire.JoinedEventLog, periodStart, periodEnd);
					break;
			}
		}
		private void dgv_event_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
		}
		private void dgv_event_SelectionChanged(object sender, EventArgs e) {
			if(dgv_event.SelectedRows.Count==0)return;

			Application.DoEvents();

			string path = dgv_event.SelectedRows[0].Cells[3].Value.ToString();
			if (!string.IsNullOrEmpty(path)) {
				try {
					pic_event_photo.Image = new Bitmap(path);
				}
				catch (Exception) {
					MessageBox.Show(this, "Не удалось подгрузить фотографию по пути \n" + path, "Пичаль :\\");
				}
			}
		}
		#endregion

		#region Navigation Actions
		private void tv_navigation_AfterSelect(object sender, TreeViewEventArgs e) {
			SwitchSection(tv_navigation.SelectedNode.Text);
		}

		private void NavigationMenuClick(object sender, EventArgs e) {
			SwitchSection(((ToolStripMenuItem)sender).Text);
		}
		private void SwitchSection(string sectionName) {
			switch (sectionName) {
				case "Система":
				case "Основное":
					gb_sys.BringToFront();
					break;
				case "Пользователи":
					gb_users.BringToFront();
					break;
				case "Сотрудники":
					gb_staff.BringToFront();
					break;
				case "Журнал пропуска":
					gB_event.BringToFront();
					break;
			}
		}
		#endregion

		

		#region Some old shit

		private void b_showEvent_Click(object sender, EventArgs e) {

			//joinedEventLogAdapter.ClearBeforeFill = true;

			////joinedEventLogAdapter.Adapter.SelectCommand = new OleDbCommand("SELECT Staff.Сотрудник, Staff.Должность, Staff.UID, Staff.Фото, EventLog.EnterTime, " +
			////														 "EventLog.ExitTime, Staff.[Табельный номер], Staff.[Номер паспорта], Staff.График, " +
			////														 "Staff.Подразделение, Staff.[Тип UID] FROM  (EventLog INNER JOIN Staff ON EventLog.Uid = Staff.UID) " +
			////														 "WHERE EnterTime > DateSerial(Year(NOW()), Month(NOW()), Day(NOW())-7)ORDER BY ENTERTIME", joinedEventLogAdapter.Connection);
			//joinedEventLogAdapter.Adapter.SelectCommand = new OleDbCommand("SELECT * from (EventLog INNER JOIN Staff ON EventLog.Uid = Staff.UID) ORDER BY ENTERTIME DESC", joinedEventLogAdapter.Connection);
			//joinedEventLogAdapter.Adapter.SelectCommand.Connection.Open();
			//var reader = joinedEventLogAdapter.Adapter.SelectCommand.ExecuteReader();
			//joinedEventLogAdapter.Adapter.SelectCommand.Connection.Close();

			//eventLogAdapter.Adapter.SelectCommand = new OleDbCommand("SELECT Staff.Сотрудник, Staff.Должность, Staff.UID, Staff.Фото, " +
			//"EventLog.EnterTime, EventLog.ExitTime, Staff.[Табельный номер], Staff.[Номер паспорта], Staff.График, Staff.Подразделение, " +
			//"Staff.[Тип UID] FROM  (EventLog INNER JOIN Staff ON EventLog.Uid = Staff.UID) " +
			//"WHERE EnterTime > DateSerial(Year(NOW()), Month(NOW()), Day(NOW())-7)", eventLogAdapter.Connection);


			//systemUsersTableAdapter.Adapter.DeleteCommand = new OleDbCommand("Delete * from SystemUsers where Uid = par0", systemUsersTableAdapter.Connection);
			//systemUsersTableAdapter.Adapter.UpdateCommand = new OleDbCommand("UPDATE SystemUsers SET SystemUsers.Uid = newuid, SystemUsers.userType = utype, SystemUsers.login = log, SystemUsers.password = pass WHERE SystemUsers.Uid = olduid", systemUsersTableAdapter.Connection); // Don't delete table names or it'll crash.
			//staffTableAdapter.Adapter.InsertCommand = new OleDbCommand("INSERT INTO staff (Сотрудник, Должность, UID, Фото, [Табельный номер], [Номер паспорта], [Дата найма], График, Заблокирован, Подразделение, [Тип UID]) VALUES (Сотр, Долж, Юид, Фотка, Табель, НомерПасп, ДатаН, Графк, Забл, Подразд, ТипЮид)", staffTableAdapter.Connection);
			//staffTableAdapter.Adapter.DeleteCommand = new OleDbCommand("Delete * from Staff where [Табельный номер] = par1", staffTableAdapter.Connection);
			//WHERE EnterTime > DateSerial(Year(NOW()), Month(NOW()), Day(NOW()-7))

			//access_control_in_OneWire.JoinedEventLog.Clear();
			//joinedEventLogAdapter.FillLastWeek(access_control_in_OneWire.JoinedEventLog);
		}

		#endregion

		private void chB_users_hidePass_CheckedChanged(object sender, EventArgs e)
		{
			if (chB_users_hidePass.Checked)
				tB_password.PasswordChar = (char)0;
			else
				tB_password.PasswordChar = (char)0;
		}
	}
}

using System;
using System.IO;
using System.Windows.Forms;

namespace QLTK___SP
{
	// Token: 0x02000002 RID: 2
	internal class Data
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public DataGridView DataGridView
		{
			get
			{
				return this.dataGridView;
			}
			set
			{
				this.dataGridView = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000208C File Offset: 0x0000028C
		public TextBox Acc
		{
			get
			{
				return this.acc;
			}
			set
			{
				this.acc = value;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002096 File Offset: 0x00000296
		public Data(DataGridView dataGridView, TextBox textBox)
		{
			this.dataGridView = dataGridView;
			this.Acc = textBox;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020AF File Offset: 0x000002AF
		public Data(TextBox textBox)
		{
			this.Acc = textBox;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020C4 File Offset: 0x000002C4
		public void ExporFile()
		{
			for (int i = 0; i < this.dataGridView.Rows.Count; i++)
			{
				this.DataGridView.Rows[i].Cells[0].Value = i;
			}
			TextWriter textWriter = new StreamWriter("Nro_244_Data//Resources//Data//Account.txt");
			for (int j = 0; j < this.dataGridView.Rows.Count; j++)
			{
				for (int k = 0; k < this.dataGridView.Columns.Count - 2; k++)
				{
					textWriter.Write(this.dataGridView.Rows[j].Cells[k].Value.ToString() + "|");
				}
				textWriter.WriteLine("");
			}
			textWriter.Close();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021C0 File Offset: 0x000003C0
		public void LoadFile()
		{
			try
			{
				this.dataGridView.Rows.Clear();
				string[] array = File.ReadAllLines("Nro_244_Data//Resources//Data//Account.txt");
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].ToString().Split(new char[] { '|' });
					this.dataGridView.Rows.Add(new object[]
					{
						array2[0],
						array2[1],
						array2[2],
						array2[3],
						array2[4],
						array2[5],
						array2[6]
					});
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000001 RID: 1
		public DataGridView dataGridView;

		// Token: 0x04000002 RID: 2
		private TextBox acc;
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json.Linq;

namespace QLTK___SP
{
	// Token: 0x02000003 RID: 3
	public class Form1 : Form
	{
		// Token: 0x06000009 RID: 9
		[DllImport("user32.dll")]
		private static extern int SetWindowText(IntPtr hWnd, string text);

		// Token: 0x0600000A RID: 10
		[DllImport("user32.dll")]
		private static extern IntPtr FillWindow(string lpClassName, string lpWindowName);

		// Token: 0x0600000B RID: 11
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

		// Token: 0x0600000C RID: 12
		[DllImport("user32.dll")]
		private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x0600000D RID: 13
		[DllImport("user32.dll")]
		private static extern bool GetWindowRect(IntPtr hWnd, ref Form1.RECT lpRect);

		// Token: 0x0600000E RID: 14
		[DllImport("user32.dll")]
		private static extern bool IsWindow(IntPtr hWnd);

		// Token: 0x0600000F RID: 15
		[DllImport("user32.dll")]
		private static extern bool IsHungAppWindow(IntPtr hwnd);

		// Token: 0x06000010 RID: 16
		[DllImport("user32.dll")]
		private static extern bool EnumWindows(Form1.EnumWindowsProc lpEnumFunc, IntPtr lParam);

		// Token: 0x06000011 RID: 17
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x06000012 RID: 18
		[DllImport("user32.dll")]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		// Token: 0x06000013 RID: 19
		[DllImport("user32.dll")]
		private static extern bool IsWindowVisible(IntPtr hWnd);

		// Token: 0x06000014 RID: 20
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000015 RID: 21
		[DllImport("user32.dll")]
		private static extern bool DestroyWindow(IntPtr hWnd);

		// Token: 0x06000016 RID: 22
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		// Token: 0x06000017 RID: 23
		[DllImport("user32.dll")]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		// Token: 0x06000018 RID: 24
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

		// Token: 0x06000019 RID: 25 RVA: 0x00002274 File Offset: 0x00000474
		public Form1()
		{
			this.InitializeComponent();
			this.listener.Start(this.dataGridView1);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022EC File Offset: 0x000004EC
		private string GenerateUniqueCodeMAC()
		{
			string macAddress = this.GetMacAddress();
			string name = this.ReadNameFromJson();
			bool flag = string.IsNullOrEmpty(macAddress);
			string text;
			if (flag)
			{
				MessageBox.Show("Không lấy được địa chỉ MAC hợp lệ!");
				text = "ERROR";
			}
			else
			{
				Form1.IP = macAddress;
				Form1.Tool = name;
				using (SHA256 sha256 = SHA256.Create())
				{
					byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(macAddress));
					text = BitConverter.ToString(hashBytes).Replace("-", "").Substring(0, 16);
				}
			}
			return text;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002390 File Offset: 0x00000590
		private string GetMacAddress()
		{
			foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
			{
				bool flag = nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && !string.IsNullOrEmpty(nic.GetPhysicalAddress().ToString());
				if (flag)
				{
					return nic.GetPhysicalAddress().ToString();
				}
			}
			return null;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002400 File Offset: 0x00000600
		private async void ShowRemainingDays()
		{
			ValueTuple<bool, bool, int, DateTime?, bool> valueTuple = await Task.Run<ValueTuple<bool, bool, int, DateTime?, bool>>(() => this.GetRemainingDays(Form1.IP + this.currentCode + Form1.Tool));
			ValueTuple<bool, bool, int, DateTime?, bool> result = valueTuple;
			valueTuple = default(ValueTuple<bool, bool, int, DateTime?, bool>);
			if (result.Item2)
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.Text = string.Format("Chào mừng {0} | {1} | HSD: {2} ngày", Form1.KhachHang, Form1.NameTool, result.Item3);
				}));
			}
			else
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.groupBox1.Visible = false;
					this.dataGridView1.Visible = false;
					this.groupBox2.Visible = false;
					MessageBox.Show("Bạn chưa mua tool! Vui lòng ib fb TienThanh để kick hoạt key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}));
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000243C File Offset: 0x0000063C
		private SheetsService GetSheetsService()
		{
			SheetsService sheetsService;
			try
			{
				string credentialPath = Path.Combine(Application.StartupPath, this.serviceAccountKey);
				bool flag = !File.Exists(credentialPath);
				if (flag)
				{
					sheetsService = null;
				}
				else
				{
					GoogleCredential credential;
					using (FileStream stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
					{
						credential = GoogleCredential.FromStream(stream).CreateScoped(new string[] { SheetsService.Scope.Spreadsheets });
					}
					sheetsService = new SheetsService(new BaseClientService.Initializer
					{
						HttpClientInitializer = credential,
						ApplicationName = "WinFormsApp"
					});
				}
			}
			catch
			{
				sheetsService = null;
			}
			return sheetsService;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024E8 File Offset: 0x000006E8
		private string ReadNameFromJson()
		{
			try
			{
				bool flag = File.Exists(this.configFilePath);
				if (flag)
				{
					string jsonContent = File.ReadAllText(this.configFilePath);
					JObject jsonObj = JObject.Parse(jsonContent);
					JToken jtoken = jsonObj["name"];
					return ((jtoken != null) ? jtoken.ToString() : null) ?? "";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi đọc file JSON: " + ex.Message);
			}
			return "";
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002578 File Offset: 0x00000778
		private async Task CheckKey()
		{
			try
			{
				Form1.<>c__DisplayClass50_0 CS$<>8__locals1 = new Form1.<>c__DisplayClass50_0();
				CS$<>8__locals1.<>4__this = this;
				ValueTuple<bool, bool, int, DateTime?, bool> valueTuple = await Task.Run<ValueTuple<bool, bool, int, DateTime?, bool>>(() => this.GetRemainingDays(Form1.IP + this.currentCode + Form1.Tool));
				ValueTuple<bool, bool, int, DateTime?, bool> valueTuple2 = valueTuple;
				bool isSuccess = valueTuple2.Item1;
				CS$<>8__locals1.isValid = valueTuple2.Item2;
				CS$<>8__locals1.remainingDays = valueTuple2.Item3;
				CS$<>8__locals1.endDate = valueTuple2.Item4;
				CS$<>8__locals1.keyExists = valueTuple2.Item5;
				valueTuple = default(ValueTuple<bool, bool, int, DateTime?, bool>);
				valueTuple2 = default(ValueTuple<bool, bool, int, DateTime?, bool>);
				Console.WriteLine(string.Format("[DEBUG] Kiểm tra lại key {0}: isSuccess={1}, isValid={2}, remainingDays={3}, keyExists={4}", new object[] { this.currentCode, isSuccess, CS$<>8__locals1.isValid, CS$<>8__locals1.remainingDays, CS$<>8__locals1.keyExists }));
				if (isSuccess)
				{
					base.Invoke(new MethodInvoker(delegate
					{
						bool isValid = CS$<>8__locals1.isValid;
						if (isValid)
						{
							CS$<>8__locals1.<>4__this.Text = string.Format("Chào mừng {0} | {1} | HSD: {2} ngày", Form1.KhachHang, Form1.NameTool, CS$<>8__locals1.remainingDays);
						}
						else
						{
							bool keyExists = CS$<>8__locals1.keyExists;
							if (keyExists)
							{
								CS$<>8__locals1.<>4__this.Text = string.Format("Tool đã hết hạn vào ngày {0:dd/MM/yyyy}, vui lòng ib fb tienthanh để gia hạn!!!", CS$<>8__locals1.endDate);
								CS$<>8__locals1.<>4__this._timer.Stop();
								MessageBox.Show(" Vui lòng ib fb TienThanh để gia hạn key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								CS$<>8__locals1.<>4__this.Close();
							}
							else
							{
								CS$<>8__locals1.<>4__this.Text = "Không tồn tại key trên hệ thống, vui lòng ib fb tienthanh để mua!!!";
								CS$<>8__locals1.<>4__this._timer.Stop();
								MessageBox.Show("Bạn chưa mua tool! Vui lòng ib fb TienThanh để kick hoạt key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								CS$<>8__locals1.<>4__this.Close();
							}
						}
					}));
				}
				else
				{
					Form1.lastcheck++;
					this.label9.Text = Form1.lastcheck.ToString();
				}
				CS$<>8__locals1 = null;
			}
			catch (Exception ex)
			{
				Console.WriteLine("[ERROR] Lỗi khi kiểm tra key: " + ex.Message);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025BC File Offset: 0x000007BC
		[return: TupleElementNames(new string[] { "isSuccess", "isValid", "remainingDays", "endDate", "keyExists" })]
		private ValueTuple<bool, bool, int, DateTime?, bool> GetRemainingDays(string code)
		{
			ValueTuple<bool, bool, int, DateTime?, bool> valueTuple;
			try
			{
				SheetsService service = this.GetSheetsService();
				bool flag = service == null;
				if (flag)
				{
					valueTuple = new ValueTuple<bool, bool, int, DateTime?, bool>(false, false, 0, null, false);
				}
				else
				{
					SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(this.spreadsheetId, this.sheetRange);
					ValueRange response = request.Execute();
					bool flag2 = response.Values == null || response.Values.Count == 0;
					if (flag2)
					{
						valueTuple = new ValueTuple<bool, bool, int, DateTime?, bool>(true, false, 0, null, false);
					}
					else
					{
						DateTime now = DateTime.Now;
						foreach (IList<object> row in response.Values)
						{
							bool flag3 = row.Count < 3;
							if (!flag3)
							{
								string sheetCode = row[0].ToString().Trim();
								string endDateStr = row[2].ToString().Trim();
								string kh = row[3].ToString().Trim();
								Form1.KhachHang = kh;
								DateTime endDate;
								bool flag4 = sheetCode == code && DateTime.TryParse(endDateStr, out endDate);
								if (flag4)
								{
									int remainingDays = (endDate - now).Days;
									return new ValueTuple<bool, bool, int, DateTime?, bool>(true, now <= endDate, remainingDays, new DateTime?(endDate), true);
								}
							}
						}
						valueTuple = new ValueTuple<bool, bool, int, DateTime?, bool>(true, false, 0, null, false);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("[ERROR] Lỗi khi lấy dữ liệu từ Google Sheets: " + ex.Message);
				valueTuple = new ValueTuple<bool, bool, int, DateTime?, bool>(false, false, 0, null, false);
			}
			return valueTuple;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027B4 File Offset: 0x000009B4
		public static bool TimeBaoTri()
		{
			DateTime now = DateTime.Now;
			return (now.Hour == 3 && now.Minute >= 10) || (now.Hour == 4 && now.Minute == 0) || File.Exists("baotri");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000280C File Offset: 0x00000A0C
		private async void Timer_Tick3(object sender, EventArgs e)
		{
			bool flag = Form1.TimeBaoTri();
			if (!flag)
			{
				bool flag2 = this.isLoggingIn == null || this.isLoggingIn.Length != this.dataGridView1.Rows.Count;
				if (flag2)
				{
					this.isLoggingIn = new bool[this.dataGridView1.Rows.Count];
				}
				List<int> selectedRows = new List<int>();
				int num;
				for (int i = 0; i < this.dataGridView1.Rows.Count; i = num + 1)
				{
					DataGridViewRow row = this.dataGridView1.Rows[i];
					bool flag3 = row.Cells[0].Value == null;
					if (!flag3)
					{
						bool flag4 = row.DefaultCellStyle.BackColor == global::System.Drawing.Color.LightGreen;
						if (!flag4)
						{
							bool isChecked = false;
							object cellValue = row.Cells[8].Value;
							bool parsedChecked;
							bool flag5 = cellValue != null && bool.TryParse(cellValue.ToString(), out parsedChecked);
							if (flag5)
							{
								isChecked = parsedChecked;
							}
							bool flag6 = isChecked;
							if (flag6)
							{
								selectedRows.Add(i);
							}
							row = null;
							cellValue = null;
						}
					}
					num = i;
				}
				foreach (int j in selectedRows)
				{
					DataGridViewRow row2 = this.dataGridView1.Rows[j];
					bool flag7 = this.loggedInPIDs.ContainsKey(j);
					if (flag7)
					{
						Form1.<>c__DisplayClass53_0 CS$<>8__locals1 = new Form1.<>c__DisplayClass53_0();
						CS$<>8__locals1.pid = this.loggedInPIDs[j];
						bool flag8 = !Process.GetProcesses().Any((Process p) => p.Id == CS$<>8__locals1.pid);
						if (flag8)
						{
							this.RemovePIDFromList(CS$<>8__locals1.pid);
						}
						CS$<>8__locals1 = null;
					}
					bool flag9 = !this.loggedInPIDs.ContainsKey(j) && !this.isLoggingIn[j];
					if (flag9)
					{
						this.isLoggingIn[j] = true;
						int num2 = await this.LoginIDAsync(j);
						int pid = num2;
						if (pid > 0)
						{
							this.loggedInPIDs[j] = pid;
						}
						this.isLoggingIn[j] = false;
					}
					if (this.isLoggingIn[j] && this.loggedInPIDs.ContainsKey(j))
					{
						Form1.<>c__DisplayClass53_1 CS$<>8__locals2 = new Form1.<>c__DisplayClass53_1();
						CS$<>8__locals2.pid = this.loggedInPIDs[j];
						if (!Process.GetProcesses().Any((Process p) => p.Id == CS$<>8__locals2.pid))
						{
							this.RemovePIDFromList(CS$<>8__locals2.pid);
						}
						CS$<>8__locals2 = null;
					}
				}
				List<int>.Enumerator enumerator = default(List<int>.Enumerator);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002854 File Offset: 0x00000A54
		private async void Timer_Tick(object sender, EventArgs e)
		{
			bool flag = Form1.TimeBaoTri();
			if (!flag)
			{
				int luong;
				bool flag2 = !int.TryParse(this.txtLuong.Text, out luong) || luong <= 0;
				if (flag2)
				{
					Console.WriteLine("Số lượng luồng không hợp lệ.");
				}
				else
				{
					bool flag3 = this.isLoggingIn == null || this.isLoggingIn.Length != this.dataGridView1.Rows.Count;
					if (flag3)
					{
						this.isLoggingIn = new bool[this.dataGridView1.Rows.Count];
					}
					List<int> selectedRows = new List<int>();
					int num;
					for (int i = 0; i < this.dataGridView1.Rows.Count; i = num + 1)
					{
						DataGridViewRow row = this.dataGridView1.Rows[i];
						bool flag4 = row.Cells[0].Value == null;
						if (!flag4)
						{
							bool flag5 = row.DefaultCellStyle.BackColor == global::System.Drawing.Color.LightGreen;
							if (!flag5)
							{
								bool isChecked = false;
								object cellValue = row.Cells[8].Value;
								bool parsedChecked;
								bool flag6 = cellValue != null && bool.TryParse(cellValue.ToString(), out parsedChecked);
								if (flag6)
								{
									isChecked = parsedChecked;
								}
								bool flag7 = isChecked;
								if (flag7)
								{
									selectedRows.Add(i);
								}
								row = null;
								cellValue = null;
							}
						}
						num = i;
					}
					selectedRows = selectedRows.Take(luong).ToList<int>();
					foreach (int j in selectedRows)
					{
						DataGridViewRow row2 = this.dataGridView1.Rows[j];
						bool flag8 = this.loggedInPIDs.ContainsKey(j);
						if (flag8)
						{
							Form1.<>c__DisplayClass54_0 CS$<>8__locals1 = new Form1.<>c__DisplayClass54_0();
							CS$<>8__locals1.pid = this.loggedInPIDs[j];
							bool flag9 = !Process.GetProcesses().Any((Process p) => p.Id == CS$<>8__locals1.pid);
							if (flag9)
							{
								this.RemovePIDFromList(CS$<>8__locals1.pid);
							}
							CS$<>8__locals1 = null;
						}
						bool flag10 = !this.loggedInPIDs.ContainsKey(j) && !this.isLoggingIn[j];
						if (flag10)
						{
							this.isLoggingIn[j] = true;
							int num2 = await this.LoginIDAsync(j);
							int pid = num2;
							if (pid > 0)
							{
								this.loggedInPIDs[j] = pid;
							}
							this.isLoggingIn[j] = false;
						}
						if (this.isLoggingIn[j] && this.loggedInPIDs.ContainsKey(j))
						{
							Form1.<>c__DisplayClass54_1 CS$<>8__locals2 = new Form1.<>c__DisplayClass54_1();
							CS$<>8__locals2.pid = this.loggedInPIDs[j];
							if (!Process.GetProcesses().Any((Process p) => p.Id == CS$<>8__locals2.pid))
							{
								this.RemovePIDFromList(CS$<>8__locals2.pid);
							}
							CS$<>8__locals2 = null;
						}
					}
					List<int>.Enumerator enumerator = default(List<int>.Enumerator);
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000289C File Offset: 0x00000A9C
		private void RemovePIDFromList(int pid)
		{
			int keyToRemove = this.loggedInPIDs.FirstOrDefault((KeyValuePair<int, int> x) => x.Value == pid).Key;
			bool flag = keyToRemove != -1;
			if (flag)
			{
				this.loggedInPIDs.Remove(keyToRemove);
				this.isLoggingIn[keyToRemove] = false;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000028FC File Offset: 0x00000AFC
		private async Task<int> LoginIDAsync(int id)
		{
			return await Task.Run<int>(delegate
			{
				DataGridViewCellCollection cell = this.dataGridView1.Rows[id].Cells;
				ProcessStartInfo startInfo = new ProcessStartInfo
				{
					FileName = Form1.AppGameExe,
					Arguments = string.Format("--acc {0} --pass {1} --server {2} --team {3} --type {4} --prx {5} --id {6}", new object[]
					{
						cell[1].Value,
						cell[2].Value,
						cell[3].Value,
						cell[4].Value,
						cell[5].Value,
						cell[6].Value,
						cell[0].Value
					})
				};
				int num;
				try
				{
					Process prc = Process.Start(startInfo);
					bool flag = prc != null;
					if (flag)
					{
						while (prc.MainWindowHandle == IntPtr.Zero)
						{
							Thread.Sleep(100);
						}
						string type = cell[5].Value.ToString();
						string[] tp = type.Split(new char[] { '.' });
						string get = tp[0];
						string windowTitle = "[" + get + "]BROLY: " + cell[0].Value.ToString();
						Form1.SetWindowText(prc.MainWindowHandle, windowTitle);
						num = prc.Id;
					}
					else
					{
						num = -1;
					}
				}
				catch (Exception ex)
				{
					Form1.WriteLog("Lỗi khi khởi chạy tiến trình: " + ex.Message);
					num = -1;
				}
				return num;
			});
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002948 File Offset: 0x00000B48
		private static void WriteLog(string message)
		{
			string filePath = "Nro_244_Data//Resources//log.txt";
			try
			{
				using (StreamWriter writer = new StreamWriter(filePath, true))
				{
					writer.WriteLine(string.Format("[{0:yyyy-MM-dd HH:mm:ss}] {1}", DateTime.Now, message));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Không thể ghi log vào file: " + ex.Message);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000029CC File Offset: 0x00000BCC
		private void Form1_Load(object sender, EventArgs e)
		{
			this.Text = "QLTK";
			new Data(this.dataGridView1, this.txtAcc).LoadFile();
			this.isLoggingIn = new bool[this.dataGridView1.Rows.Count];
			this.InitializeTimers();
			bool flag = File.Exists("Port");
			if (flag)
			{
				this.txtPort.Text = File.ReadAllText("Port");
			}
			bool flag2 = File.Exists(Form1.size);
			if (flag2)
			{
				this.txtX.Text = File.ReadAllText(Form1.size).Split(new char[] { 'x' })[0];
				this.txtY.Text = File.ReadAllText(Form1.size).Split(new char[] { 'x' })[1];
			}
			this.checkBox1.Checked = File.Exists(Form1.dokhu);
			this.checkBox3.Checked = File.Exists("Port");
			this.checkBox2.Checked = File.Exists(Form1.tdlt);
			bool flag3 = this.dataGridView1.Rows.Count >= 0;
			if (flag3)
			{
				this.txtLuong.Text = this.dataGridView1.Rows.Count.ToString();
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B28 File Offset: 0x00000D28
		private async Task UpdateRowColorsAsync()
		{
			await Task.Run(delegate
			{
				bool invokeRequired = this.dataGridView1.InvokeRequired;
				if (invokeRequired)
				{
					this.dataGridView1.Invoke(new Action(this.UpdateRowColors));
				}
				else
				{
					this.UpdateRowColors();
				}
			});
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B6C File Offset: 0x00000D6C
		private void UpdateRowColors()
		{
			foreach (object obj in ((IEnumerable)this.dataGridView1.Rows))
			{
				DataGridViewRow row = (DataGridViewRow)obj;
				bool flag = row.Cells[0].Value == null;
				if (!flag)
				{
					string id = row.Cells[0].Value.ToString();
					string filePath = "Nro_244_Data/Resources/Status/xong" + id;
					bool flag2 = File.Exists(filePath);
					if (flag2)
					{
						row.DefaultCellStyle.BackColor = global::System.Drawing.Color.LightGreen;
					}
					else
					{
						row.DefaultCellStyle.BackColor = global::System.Drawing.Color.White;
					}
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002C44 File Offset: 0x00000E44
		private async void LoadTrangThai(object sender, EventArgs e)
		{
			await this.LoadStatusAsync();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002C8C File Offset: 0x00000E8C
		private async Task LoadStatusAsync()
		{
			bool invokeRequired = this.dataGridView1.InvokeRequired;
			if (invokeRequired)
			{
				this.dataGridView1.Invoke(new Action(async delegate
				{
					await this.LoadStatusAsync();
				}));
			}
			else
			{
				bool flag = File.Exists("Nro_244_Data/Resources/thongbao");
				if (flag)
				{
					this.label9.Text = File.ReadAllText("Nro_244_Data/Resources/thongbao");
				}
				foreach (object obj in ((IEnumerable)this.dataGridView1.Rows))
				{
					DataGridViewRow row = (DataGridViewRow)obj;
					bool flag2 = row.Index == this.dataGridView1.NewRowIndex;
					if (!flag2)
					{
						bool flag3 = row.Cells[0].Value != null;
						if (flag3)
						{
							Form1.<>c__DisplayClass63_0 CS$<>8__locals1 = new Form1.<>c__DisplayClass63_0();
							string id = row.Cells[0].Value.ToString();
							CS$<>8__locals1.filePath = "Nro_244_Data/Resources/Status/acc" + id;
							bool flag4 = File.Exists(CS$<>8__locals1.filePath);
							if (flag4)
							{
								try
								{
									string text = await Task.Run<string>(() => File.ReadAllText(CS$<>8__locals1.filePath));
									string content = text;
									text = null;
									row.Cells[7].Value = content;
									content = null;
								}
								catch (Exception ex)
								{
									Console.WriteLine("Lỗi đọc file " + CS$<>8__locals1.filePath + ": " + ex.Message);
								}
							}
							else
							{
								row.Cells[7].Value = "Chưa có thông tin";
							}
							CS$<>8__locals1 = null;
							id = null;
						}
						row = null;
					}
				}
				IEnumerator enumerator = null;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002CD0 File Offset: 0x00000ED0
		private async void Timer_Tick1()
		{
			await this.FixCrashAsync();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002D0C File Offset: 0x00000F0C
		private void InitializeTimers()
		{
			this._timer = new global::System.Windows.Forms.Timer
			{
				Interval = 10000
			};
			this._timer.Tick += this.Timer_Tick;
			this.timer = new global::System.Windows.Forms.Timer
			{
				Interval = 1000
			};
			this.timer.Tick += async delegate(object s, EventArgs ev)
			{
				await Task.Run(delegate
				{
					this.Timer_Tick1();
				});
			};
			this.timer.Start();
			this.___timer = new global::System.Windows.Forms.Timer
			{
				Interval = 3000
			};
			this.___timer.Tick += async delegate(object s, EventArgs ev)
			{
				await this.CheckTabNotRespondingAsync();
			};
			this.___timer.Start();
			this.__timer = new global::System.Windows.Forms.Timer
			{
				Interval = 30000
			};
			this.__timer.Tick += async delegate(object s, EventArgs ev)
			{
				await this.SapXepAsync();
			};
			this.__timer.Start();
			this.____timer = new global::System.Windows.Forms.Timer
			{
				Interval = 1000
			};
			this.____timer.Tick += this.LoadTrangThai;
			this.____timer.Tick += async delegate(object s, EventArgs e)
			{
				await this.UpdateRowColorsAsync();
			};
			this.____timer.Start();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002E50 File Offset: 0x00001050
		public async Task FixCrashAsync()
		{
			try
			{
				await Task.Run(delegate
				{
					string processNameToKill = "WerFault";
					Process[] processesToKill = Process.GetProcessesByName(processNameToKill);
					List<Process> failedToKill = new List<Process>();
					Parallel.ForEach<Process>(processesToKill, delegate(Process process)
					{
						try
						{
							bool flag2 = !process.HasExited;
							if (flag2)
							{
								bool success = Form1.TerminateProcess(process.Handle, 0U);
								bool flag3 = success;
								if (!flag3)
								{
									throw new Exception("TerminateProcess thất bại.");
								}
								Form1.WriteLog(string.Format("Đã tắt tiến trình: {0} (ID: {1})", process.ProcessName, process.Id));
							}
						}
						catch (Exception ex2)
						{
							failedToKill.Add(process);
							Form1.WriteLog(string.Format("Không thể tắt tiến trình {0} (ID: {1}): {2}", process.ProcessName, process.Id, ex2.Message));
						}
					});
					bool flag = failedToKill.Any<Process>();
					if (flag)
					{
						Form1.WriteLog("Các tiến trình không tắt được:");
						foreach (Process process2 in failedToKill)
						{
							Form1.WriteLog(string.Format("- {0} (ID: {1})", process2.ProcessName, process2.Id));
						}
					}
				});
			}
			catch (Exception ex)
			{
				Form1.WriteLog("Lỗi xảy ra khi kiểm tra và tắt tiến trình: " + ex.Message);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002E94 File Offset: 0x00001094
		private async Task CheckTabNotRespondingAsync()
		{
			await Task.Run(async delegate
			{
				Process[] processes = Process.GetProcessesByName("Nro_244");
				foreach (Process process in processes)
				{
					try
					{
						bool flag = process != null;
						if (flag)
						{
							bool flag2 = !process.Responding;
							if (flag2)
							{
								process.Kill();
								Form1.WriteLog(string.Format("Đã tắt tab không phản hồi: {0}({1})", process.ProcessName, process.Id));
							}
							else
							{
								double num = await this.GetCpuUsageAsync(process);
								double cpuUsage = num;
								long memoryUsage = process.WorkingSet64 / 1024L / 1024L;
								if (cpuUsage < 0.1 && memoryUsage < 25L)
								{
									process.Kill();
									Form1.WriteLog(string.Format("Đã tắt tab không sử dụng tài nguyên: {0} ({1})", process.ProcessName, process.Id));
								}
							}
						}
					}
					catch (Exception ex)
					{
						Form1.WriteLog("Lỗi khi kiểm tra process " + process.ProcessName + ": " + ex.Message);
					}
					process = null;
				}
				Process[] array = null;
			});
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002ED8 File Offset: 0x000010D8
		private async Task<double> GetCpuUsageAsync(Process process)
		{
			double num;
			try
			{
				TimeSpan startCpuTime = process.TotalProcessorTime;
				DateTime startTime = DateTime.UtcNow;
				await Task.Delay(500);
				TimeSpan endCpuTime = process.TotalProcessorTime;
				DateTime endTime = DateTime.UtcNow;
				double cpuUsedMs = (endCpuTime - startCpuTime).TotalMilliseconds;
				double totalMs = (endTime - startTime).TotalMilliseconds;
				double cpuUsage = cpuUsedMs / totalMs * 100.0 / (double)Environment.ProcessorCount;
				num = Math.Round(cpuUsage, 2);
			}
			catch
			{
				num = 0.0;
			}
			return num;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002F24 File Offset: 0x00001124
		public bool CheckAdd(bool Add)
		{
			bool flag = string.IsNullOrEmpty(this.txtAcc.Text);
			bool check;
			if (flag)
			{
				MessageBox.Show("Chưa nhập tài khoản ", "Lỗi Thêm Tài Khoản ", MessageBoxButtons.OK);
				this.txtAcc.Focus();
				check = false;
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(this.txtPass.Text);
				if (flag2)
				{
					MessageBox.Show("Chưa nhập mật khẩu ", "Lỗi Nhập Mật Khẩu ", MessageBoxButtons.OK);
					this.txtPass.Focus();
					check = false;
				}
				else
				{
					bool flag3 = string.IsNullOrEmpty(this.cboSv.Text);
					if (flag3)
					{
						MessageBox.Show("Chưa nhập Server ", "Lỗi Thêm Server ", MessageBoxButtons.OK);
						this.cboSv.Focus();
						check = false;
					}
					else
					{
						check = true;
					}
				}
			}
			return check;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002FE4 File Offset: 0x000011E4
		private void button1_Click(object sender, EventArgs e)
		{
			bool flag = this.CheckAdd(true);
			if (flag)
			{
				string accPattern = this.txtAcc.Text.Trim();
				bool flag2 = accPattern.Contains("[") && accPattern.Contains("]");
				if (flag2)
				{
					int startIndex = accPattern.IndexOf("[") + 1;
					int endIndex = accPattern.IndexOf("]");
					string range = accPattern.Substring(startIndex, endIndex - startIndex);
					string[] rangeParts = range.Split(new char[] { '-' });
					int start;
					int end;
					bool flag3 = rangeParts.Length == 2 && int.TryParse(rangeParts[0], out start) && int.TryParse(rangeParts[1], out end);
					if (flag3)
					{
						for (int i = start; i <= end; i++)
						{
							string account = accPattern.Substring(0, startIndex - 1) + i.ToString();
							this.dataGridView1.Rows.Add(new object[]
							{
								this.dataGridView1.Rows.Count,
								account,
								this.txtPass.Text,
								this.cboSv.Text,
								this.txtTeam.Text,
								this.cboType.Text,
								string.IsNullOrEmpty(this.txtPrx.Text) ? "none" : this.txtPrx.Text
							});
						}
					}
					else
					{
						MessageBox.Show("Phạm vi không hợp lệ! Định dạng yêu cầu là [start-end].", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				else
				{
					this.dataGridView1.Rows.Add(new object[]
					{
						this.dataGridView1.Rows.Count,
						accPattern,
						this.txtPass.Text,
						this.cboSv.Text,
						this.txtTeam.Text,
						this.cboType.Text,
						string.IsNullOrEmpty(this.txtPrx.Text) ? "none" : this.txtPrx.Text
					});
				}
				new Data(this.dataGridView1, this.txtAcc).ExporFile();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003238 File Offset: 0x00001438
		private void button2_Click(object sender, EventArgs e)
		{
			bool flag = this.CheckAdd(false);
			if (flag)
			{
				int index = this.dataGridView1.CurrentRow.Index;
				this.dataGridView1.Rows[index].Cells[1].Value = this.txtAcc.Text;
				this.dataGridView1.Rows[index].Cells[2].Value = this.txtPass.Text;
				this.dataGridView1.Rows[index].Cells[3].Value = this.cboSv.Text;
				this.dataGridView1.Rows[index].Cells[4].Value = this.txtTeam.Text;
				this.dataGridView1.Rows[index].Cells[5].Value = this.cboType.Text;
				this.dataGridView1.Rows[index].Cells[6].Value = (string.IsNullOrEmpty(this.txtPrx.Text) ? "none" : this.txtPrx.Text);
				new Data(this.dataGridView1, this.txtAcc).ExporFile();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000033A8 File Offset: 0x000015A8
		private void button3_Click(object sender, EventArgs e)
		{
			bool flag = this.dataGridView1.Rows.Count > 0;
			if (flag)
			{
				this.dataGridView1.Rows.RemoveAt(this.dataGridView1.CurrentRow.Index);
				new Data(this.dataGridView1, this.txtAcc).ExporFile();
			}
			else
			{
				MessageBox.Show("Chưa có tài khoản ", "Lỗi xóa tài khoản ", MessageBoxButtons.OK);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000341C File Offset: 0x0000161C
		private void button4_Click(object sender, EventArgs e)
		{
			this._timer.Start();
			this.button4.Enabled = false;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003438 File Offset: 0x00001638
		private void button5_Click(object sender, EventArgs e)
		{
			this._timer.Stop();
			this.button4.Enabled = true;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003454 File Offset: 0x00001654
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.checkBox1.Checked;
			if (@checked)
			{
				File.Create(Form1.dokhu).Close();
			}
			else
			{
				bool flag = File.Exists(Form1.dokhu);
				if (flag)
				{
					File.Delete(Form1.dokhu);
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000034A4 File Offset: 0x000016A4
		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.checkBox3.Checked;
			if (@checked)
			{
				File.WriteAllText("Port", this.txtPort.Text);
				this.txtPort.Enabled = false;
			}
			else
			{
				this.txtPort.Enabled = true;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000034F8 File Offset: 0x000016F8
		private void button9_Click(object sender, EventArgs e)
		{
			string folderPath = "Nro_244_Data/Resources/Status/";
			string searchPattern = "xong*";
			try
			{
				string[] files = Directory.GetFiles(folderPath, searchPattern);
				foreach (string file in files)
				{
					File.Delete(file);
				}
				MessageBox.Show("Reset data successfully", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi xóa file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000358C File Offset: 0x0000178C
		private void button7_Click(object sender, EventArgs e)
		{
			string titleToClose = "BROLY: ";
			DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn tắt tất cả các tab game không?", "Xác nhận tắt tab", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			bool flag = dialogResult == DialogResult.Yes;
			if (flag)
			{
				Process[] processes = Process.GetProcesses();
				foreach (Process process in processes)
				{
					try
					{
						bool flag2 = process.MainWindowTitle.Contains(titleToClose);
						if (flag2)
						{
							process.Kill();
							Form1.WriteLog(string.Format("Đã tắt tiến trình: {0} (ID: {1})", process.ProcessName, process.Id));
						}
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000363C File Offset: 0x0000183C
		private void button8_Click(object sender, EventArgs e)
		{
			string localLowPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\LocalLow\\Team\\ragonboy244");
			DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa dữ liệu game không?", "Xóa dữ liệu Game???", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			bool flag = dialogResult == DialogResult.Yes;
			if (flag)
			{
				try
				{
					bool flag2 = Directory.Exists(localLowPath);
					if (flag2)
					{
						string[] files = Directory.GetFileSystemEntries(localLowPath);
						foreach (string file in files)
						{
							bool flag3 = File.Exists(file);
							if (flag3)
							{
								File.Delete(file);
							}
							else
							{
								bool flag4 = Directory.Exists(file);
								if (flag4)
								{
									Directory.Delete(file, true);
								}
							}
						}
						MessageBox.Show("Đã xóa các tệp không cần thiết thành công.");
					}
					else
					{
						MessageBox.Show("Thư mục không tồn tại.");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
				}
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003730 File Offset: 0x00001930
		private async void button6_Click(object sender, EventArgs e)
		{
			await this.LoginIDAsync(this.dataGridView1.CurrentRow.Index);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003778 File Offset: 0x00001978
		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			int index = this.dataGridView1.CurrentRow.Index;
			this.txtAcc.Text = this.dataGridView1.Rows[index].Cells[1].Value.ToString();
			this.txtPass.Text = this.dataGridView1.Rows[index].Cells[2].Value.ToString();
			this.cboSv.Text = this.dataGridView1.Rows[index].Cells[3].Value.ToString();
			this.txtTeam.Text = this.dataGridView1.Rows[index].Cells[4].Value.ToString();
			this.cboType.Text = this.dataGridView1.Rows[index].Cells[5].Value.ToString();
			this.txtPrx.Text = this.dataGridView1.Rows[index].Cells[6].Value.ToString();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000038C4 File Offset: 0x00001AC4
		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.checkBox2.Checked;
			if (@checked)
			{
				File.Create(Form1.tdlt).Close();
			}
			else
			{
				bool flag = File.Exists(Form1.tdlt);
				if (flag)
				{
					File.Delete(Form1.tdlt);
				}
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003914 File Offset: 0x00001B14
		private void SaveTextToFile(string filePath, string text)
		{
			try
			{
				File.WriteAllText(filePath, text);
			}
			catch
			{
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003944 File Offset: 0x00001B44
		private void button10_Click(object sender, EventArgs e)
		{
			try
			{
				this.SaveTextToFile(Form1.size, this.txtX.Text + "x" + this.txtY.Text);
				MessageBox.Show("Ok");
			}
			catch
			{
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000039A4 File Offset: 0x00001BA4
		private void button11_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "Text files (*.txt)|*.txt";
				openFileDialog.Title = "Chọn tệp TXT";
				bool flag = openFileDialog.ShowDialog() == DialogResult.OK;
				if (flag)
				{
					string[] lines = File.ReadAllLines(openFileDialog.FileName);
					foreach (string line in lines)
					{
						string[] parts = line.Split(new char[] { '|' });
						bool flag2 = parts.Length == 3;
						if (flag2)
						{
							this.dataGridView1.Rows.Add(new object[]
							{
								"",
								parts[0],
								parts[1],
								parts[2],
								"1",
								"3.Clone - sơ sinh",
								"none"
							});
							new Data(this.dataGridView1, this.txtAcc).ExporFile();
						}
					}
				}
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003AB8 File Offset: 0x00001CB8
		private void button12_Click(object sender, EventArgs e)
		{
			foreach (object obj in this.dataGridView1.SelectedRows)
			{
				DataGridViewRow row = (DataGridViewRow)obj;
				bool flag = !row.IsNewRow;
				if (flag)
				{
					row.Cells[8].Value = true;
				}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003B3C File Offset: 0x00001D3C
		private void button13_Click(object sender, EventArgs e)
		{
			foreach (object obj in this.dataGridView1.SelectedRows)
			{
				DataGridViewRow row = (DataGridViewRow)obj;
				bool flag = !row.IsNewRow;
				if (flag)
				{
					row.Cells[8].Value = false;
				}
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003BC0 File Offset: 0x00001DC0
		private async Task<string> ReadFileTextAsync(string path)
		{
			string text2;
			using (StreamReader reader = new StreamReader(path))
			{
				string text = await reader.ReadToEndAsync();
				text2 = text;
			}
			return text2;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003C0C File Offset: 0x00001E0C
		private async Task SapXepAsync()
		{
			Form1.<>c__DisplayClass91_0 CS$<>8__locals1 = new Form1.<>c__DisplayClass91_0();
			int screenWidth = Screen.PrimaryScreen.Bounds.Width;
			int screenHeight = Screen.PrimaryScreen.Bounds.Height;
			CS$<>8__locals1.tabWidth = 150;
			CS$<>8__locals1.tabHeight = 150;
			List<Process> processes = (from p in Process.GetProcesses()
				where p.MainWindowTitle.Contains("[3]BROLY: ")
				orderby Regex.Match(p.MainWindowTitle, "[3]BROLY:\\s*(\\d+)").Groups[1].Value
				select p).ToList<Process>();
			CS$<>8__locals1.columns = screenWidth / CS$<>8__locals1.tabWidth;
			int rows = screenHeight / (CS$<>8__locals1.tabHeight + 30);
			CS$<>8__locals1.x = 0;
			CS$<>8__locals1.y = 0;
			using (List<Process>.Enumerator enumerator = processes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Form1.<>c__DisplayClass91_1 CS$<>8__locals2 = new Form1.<>c__DisplayClass91_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					CS$<>8__locals2.process = enumerator.Current;
					await Task.Run(delegate
					{
						IntPtr hwnd = CS$<>8__locals2.process.MainWindowHandle;
						bool flag = hwnd == IntPtr.Zero || !Form1.IsWindow(hwnd);
						if (!flag)
						{
							Form1.RECT rect = default(Form1.RECT);
							Form1.GetWindowRect(hwnd, ref rect);
							int xPosition = CS$<>8__locals2.CS$<>8__locals1.x * CS$<>8__locals2.CS$<>8__locals1.tabWidth;
							int yPosition = CS$<>8__locals2.CS$<>8__locals1.y * (CS$<>8__locals2.CS$<>8__locals1.tabHeight + 30);
							bool flag2 = rect.Left != xPosition || rect.Top != yPosition;
							if (flag2)
							{
								Form1.MoveWindow(hwnd, xPosition, yPosition, CS$<>8__locals2.CS$<>8__locals1.tabWidth, CS$<>8__locals2.CS$<>8__locals1.tabHeight + 30, true);
							}
							int num = CS$<>8__locals2.CS$<>8__locals1.x;
							CS$<>8__locals2.CS$<>8__locals1.x = num + 1;
							bool flag3 = CS$<>8__locals2.CS$<>8__locals1.x >= CS$<>8__locals2.CS$<>8__locals1.columns;
							if (flag3)
							{
								CS$<>8__locals2.CS$<>8__locals1.x = 0;
								num = CS$<>8__locals2.CS$<>8__locals1.y;
								CS$<>8__locals2.CS$<>8__locals1.y = num + 1;
							}
						}
					});
					CS$<>8__locals2 = null;
				}
			}
			List<Process>.Enumerator enumerator = default(List<Process>.Enumerator);
			await this.SapXepAsyncPhu();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003C50 File Offset: 0x00001E50
		private async Task SapXepAsyncPhu()
		{
			Form1.<>c__DisplayClass92_0 CS$<>8__locals1 = new Form1.<>c__DisplayClass92_0();
			int screenWidth = Screen.PrimaryScreen.Bounds.Width;
			int screenHeight = Screen.PrimaryScreen.Bounds.Height;
			CS$<>8__locals1.tabWidth = 150;
			CS$<>8__locals1.tabHeight = 150;
			List<Process> processes = (from p in Process.GetProcesses()
				where p.MainWindowTitle.Contains("[2]BROLY: ")
				orderby Regex.Match(p.MainWindowTitle, "[2]BROLY:\\s*(\\d+)").Groups[1].Value
				select p).ToList<Process>();
			CS$<>8__locals1.columns = screenWidth / CS$<>8__locals1.tabWidth;
			int rows = screenHeight / (CS$<>8__locals1.tabHeight + 30);
			CS$<>8__locals1.x = 0;
			CS$<>8__locals1.y = 0;
			using (List<Process>.Enumerator enumerator = processes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Form1.<>c__DisplayClass92_1 CS$<>8__locals2 = new Form1.<>c__DisplayClass92_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					CS$<>8__locals2.process = enumerator.Current;
					await Task.Run(delegate
					{
						IntPtr hwnd = CS$<>8__locals2.process.MainWindowHandle;
						bool flag = hwnd == IntPtr.Zero || !Form1.IsWindow(hwnd);
						if (!flag)
						{
							Form1.RECT rect = default(Form1.RECT);
							Form1.GetWindowRect(hwnd, ref rect);
							int xPosition = CS$<>8__locals2.CS$<>8__locals1.x * CS$<>8__locals2.CS$<>8__locals1.tabWidth;
							int yPosition = CS$<>8__locals2.CS$<>8__locals1.y * (CS$<>8__locals2.CS$<>8__locals1.tabHeight + 30);
							bool flag2 = rect.Left != xPosition || rect.Top != yPosition;
							if (flag2)
							{
								Form1.MoveWindow(hwnd, xPosition, yPosition + 180, CS$<>8__locals2.CS$<>8__locals1.tabWidth, CS$<>8__locals2.CS$<>8__locals1.tabHeight + 30, true);
							}
							int num = CS$<>8__locals2.CS$<>8__locals1.x;
							CS$<>8__locals2.CS$<>8__locals1.x = num + 1;
							bool flag3 = CS$<>8__locals2.CS$<>8__locals1.x >= CS$<>8__locals2.CS$<>8__locals1.columns;
							if (flag3)
							{
								CS$<>8__locals2.CS$<>8__locals1.x = 0;
								num = CS$<>8__locals2.CS$<>8__locals1.y;
								CS$<>8__locals2.CS$<>8__locals1.y = num + 1;
							}
						}
					});
					CS$<>8__locals2 = null;
				}
			}
			List<Process>.Enumerator enumerator = default(List<Process>.Enumerator);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003C94 File Offset: 0x00001E94
		private async void button14_Click(object sender, EventArgs e)
		{
			await this.SapXepAsync();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003CDC File Offset: 0x00001EDC
		private void button15_Click(object sender, EventArgs e)
		{
			bool flag = !File.Exists("proxy.txt");
			if (flag)
			{
				MessageBox.Show("Không tìm thấy file.");
			}
			else
			{
				int soLanLap;
				bool flag2 = !int.TryParse(this.textBox1.Text, out soLanLap) || soLanLap <= 0;
				if (flag2)
				{
					MessageBox.Show("Ghi số dòng chuẩn chưa >>>> \n Ghi 10 dòng thì thêm dòng đầu cho 10 acc...\n :D");
					this.textBox1.Focus();
				}
				else
				{
					string[] lines = File.ReadAllLines("proxy.txt");
					bool flag3 = lines.Length == 0;
					if (flag3)
					{
						MessageBox.Show("File không có nội dung.");
					}
					else
					{
						int currentRowIndex = 0;
						int lineIndex = 0;
						while (currentRowIndex < this.dataGridView1.Rows.Count)
						{
							bool isNewRow = this.dataGridView1.Rows[currentRowIndex].IsNewRow;
							if (isNewRow)
							{
								currentRowIndex++;
							}
							else
							{
								int i = 0;
								while (i < soLanLap && currentRowIndex < this.dataGridView1.Rows.Count)
								{
									bool flag4 = !this.dataGridView1.Rows[currentRowIndex].IsNewRow;
									if (flag4)
									{
										this.dataGridView1.Rows[currentRowIndex].Cells[6].Value = lines[lineIndex];
									}
									currentRowIndex++;
									i++;
								}
								lineIndex++;
								bool flag5 = lineIndex >= lines.Length;
								if (flag5)
								{
									lineIndex = 0;
								}
							}
						}
						new Data(this.dataGridView1, this.txtAcc).ExporFile();
						MessageBox.Show("Hoàn tất.");
					}
				}
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003E74 File Offset: 0x00002074
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003EAC File Offset: 0x000020AC
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
			this.dataGridView1 = new DataGridView();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.Column3 = new DataGridViewTextBoxColumn();
			this.Column4 = new DataGridViewTextBoxColumn();
			this.Column5 = new DataGridViewTextBoxColumn();
			this.Column6 = new DataGridViewTextBoxColumn();
			this.Column7 = new DataGridViewTextBoxColumn();
			this.Column8 = new DataGridViewTextBoxColumn();
			this.Column9 = new DataGridViewCheckBoxColumn();
			this.groupBox1 = new GroupBox();
			this.button3 = new Button();
			this.button2 = new Button();
			this.button1 = new Button();
			this.txtTeam = new TextBox();
			this.label6 = new Label();
			this.label5 = new Label();
			this.txtPrx = new TextBox();
			this.label4 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.cboType = new ComboBox();
			this.cboSv = new ComboBox();
			this.txtPass = new TextBox();
			this.txtAcc = new TextBox();
			this.groupBox2 = new GroupBox();
			this.label11 = new Label();
			this.txtLuong = new TextBox();
			this.label7 = new Label();
			this.textBox1 = new TextBox();
			this.button15 = new Button();
			this.button14 = new Button();
			this.button13 = new Button();
			this.button12 = new Button();
			this.button11 = new Button();
			this.label10 = new Label();
			this.button10 = new Button();
			this.txtY = new TextBox();
			this.txtX = new TextBox();
			this.checkBox2 = new CheckBox();
			this.button9 = new Button();
			this.checkBox3 = new CheckBox();
			this.txtPort = new DomainUpDown();
			this.label8 = new Label();
			this.panel1 = new Panel();
			this.label9 = new Label();
			this.button8 = new Button();
			this.button7 = new Button();
			this.button6 = new Button();
			this.button5 = new Button();
			this.button4 = new Button();
			this.checkBox1 = new CheckBox();
			this.timer1 = new global::System.Windows.Forms.Timer(this.components);
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.AllowUserToResizeColumns = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.Column1, this.Column2, this.Column3, this.Column4, this.Column5, this.Column6, this.Column7, this.Column8, this.Column9 });
			this.dataGridView1.EnableHeadersVisualStyles = false;
			this.dataGridView1.Location = new Point(12, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new Size(668, 248);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.CellClick += this.dataGridView1_CellClick;
			this.Column1.HeaderText = "ID";
			this.Column1.Name = "Column1";
			this.Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Column1.Width = 30;
			this.Column2.HeaderText = "Account";
			this.Column2.Name = "Column2";
			this.Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Column2.Width = 130;
			this.Column3.HeaderText = "PassWord";
			this.Column3.Name = "Column3";
			this.Column3.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Column3.Visible = false;
			this.Column4.HeaderText = "Server";
			this.Column4.Name = "Column4";
			this.Column4.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Column4.Width = 60;
			this.Column5.HeaderText = "Team";
			this.Column5.Name = "Column5";
			this.Column5.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Column5.Width = 60;
			this.Column6.HeaderText = "Type";
			this.Column6.Name = "Column6";
			this.Column6.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Column6.Width = 80;
			this.Column7.HeaderText = "Proxy";
			this.Column7.Name = "Column7";
			this.Column7.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Column7.Width = 70;
			this.Column8.HeaderText = "Trạng Thái ";
			this.Column8.Name = "Column8";
			this.Column8.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Column8.Width = 180;
			this.Column9.HeaderText = "X";
			this.Column9.Name = "Column9";
			this.Column9.Width = 40;
			this.groupBox1.Controls.Add(this.button3);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.txtTeam);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.txtPrx);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.cboType);
			this.groupBox1.Controls.Add(this.cboSv);
			this.groupBox1.Controls.Add(this.txtPass);
			this.groupBox1.Controls.Add(this.txtAcc);
			this.groupBox1.Location = new Point(13, 268);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(277, 270);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Account";
			this.button3.Location = new Point(184, 226);
			this.button3.Name = "button3";
			this.button3.Size = new Size(75, 30);
			this.button3.TabIndex = 26;
			this.button3.Text = "Xóa";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += this.button3_Click;
			this.button2.Location = new Point(103, 226);
			this.button2.Name = "button2";
			this.button2.Size = new Size(75, 31);
			this.button2.TabIndex = 25;
			this.button2.Text = "Sửa ";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += this.button2_Click;
			this.button1.Location = new Point(22, 226);
			this.button1.Name = "button1";
			this.button1.Size = new Size(75, 30);
			this.button1.TabIndex = 24;
			this.button1.Text = "Thêm";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += this.button1_Click;
			this.txtTeam.Location = new Point(72, 128);
			this.txtTeam.Name = "txtTeam";
			this.txtTeam.Size = new Size(174, 20);
			this.txtTeam.TabIndex = 23;
			this.label6.AutoSize = true;
			this.label6.Location = new Point(17, 131);
			this.label6.Name = "label6";
			this.label6.Size = new Size(34, 13);
			this.label6.TabIndex = 22;
			this.label6.Text = "Team";
			this.label5.AutoSize = true;
			this.label5.Location = new Point(19, 191);
			this.label5.Name = "label5";
			this.label5.Size = new Size(33, 13);
			this.label5.TabIndex = 21;
			this.label5.Text = "Proxy";
			this.txtPrx.Location = new Point(72, 188);
			this.txtPrx.Name = "txtPrx";
			this.txtPrx.Size = new Size(174, 20);
			this.txtPrx.TabIndex = 12;
			this.label4.AutoSize = true;
			this.label4.Location = new Point(19, 164);
			this.label4.Name = "label4";
			this.label4.Size = new Size(31, 13);
			this.label4.TabIndex = 20;
			this.label4.Text = "Type";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(13, 102);
			this.label3.Name = "label3";
			this.label3.Size = new Size(48, 13);
			this.label3.TabIndex = 19;
			this.label3.Text = "Máy chủ";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(10, 62);
			this.label2.Name = "label2";
			this.label2.Size = new Size(52, 13);
			this.label2.TabIndex = 18;
			this.label2.Text = "Mật khẩu";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(7, 26);
			this.label1.Name = "label1";
			this.label1.Size = new Size(55, 13);
			this.label1.TabIndex = 17;
			this.label1.Text = "Tài khoản";
			this.cboType.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboType.FormattingEnabled = true;
			this.cboType.Items.AddRange(new object[] { "1.Acc chính ", "2.Acc phụ", "3.Clone - sơ sinh" });
			this.cboType.Location = new Point(72, 156);
			this.cboType.Name = "cboType";
			this.cboType.Size = new Size(174, 21);
			this.cboType.TabIndex = 16;
			this.cboSv.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboSv.FormattingEnabled = true;
			this.cboSv.Items.AddRange(new object[]
			{
				"1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
				"11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
				"21", "22", "23", "24", "25", "26", "27", "28", "29", "30"
			});
			this.cboSv.Location = new Point(72, 94);
			this.cboSv.Name = "cboSv";
			this.cboSv.Size = new Size(174, 21);
			this.cboSv.TabIndex = 15;
			this.txtPass.Location = new Point(72, 55);
			this.txtPass.Name = "txtPass";
			this.txtPass.Size = new Size(174, 20);
			this.txtPass.TabIndex = 14;
			this.txtPass.UseSystemPasswordChar = true;
			this.txtAcc.Location = new Point(72, 19);
			this.txtAcc.Name = "txtAcc";
			this.txtAcc.Size = new Size(174, 20);
			this.txtAcc.TabIndex = 13;
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.txtLuong);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.textBox1);
			this.groupBox2.Controls.Add(this.button15);
			this.groupBox2.Controls.Add(this.button14);
			this.groupBox2.Controls.Add(this.button13);
			this.groupBox2.Controls.Add(this.button12);
			this.groupBox2.Controls.Add(this.button11);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.button10);
			this.groupBox2.Controls.Add(this.txtY);
			this.groupBox2.Controls.Add(this.txtX);
			this.groupBox2.Controls.Add(this.checkBox2);
			this.groupBox2.Controls.Add(this.button9);
			this.groupBox2.Controls.Add(this.checkBox3);
			this.groupBox2.Controls.Add(this.txtPort);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.panel1);
			this.groupBox2.Controls.Add(this.button8);
			this.groupBox2.Controls.Add(this.button7);
			this.groupBox2.Controls.Add(this.button6);
			this.groupBox2.Controls.Add(this.button5);
			this.groupBox2.Controls.Add(this.button4);
			this.groupBox2.Controls.Add(this.checkBox1);
			this.groupBox2.Location = new Point(297, 268);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(383, 270);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Setting";
			this.label11.AutoSize = true;
			this.label11.Location = new Point(23, 62);
			this.label11.Name = "label11";
			this.label11.Size = new Size(37, 13);
			this.label11.TabIndex = 29;
			this.label11.Text = "Luồng";
			this.txtLuong.Location = new Point(74, 59);
			this.txtLuong.Name = "txtLuong";
			this.txtLuong.Size = new Size(58, 20);
			this.txtLuong.TabIndex = 28;
			this.label7.AutoSize = true;
			this.label7.Location = new Point(346, 208);
			this.label7.Name = "label7";
			this.label7.Size = new Size(31, 13);
			this.label7.TabIndex = 27;
			this.label7.Text = "dòng";
			this.textBox1.Location = new Point(315, 201);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(29, 20);
			this.textBox1.TabIndex = 26;
			this.button15.Location = new Point(201, 198);
			this.button15.Name = "button15";
			this.button15.Size = new Size(107, 23);
			this.button15.TabIndex = 25;
			this.button15.Text = "Them Proxy Nhanh";
			this.button15.UseVisualStyleBackColor = true;
			this.button15.Click += this.button15_Click;
			this.button14.Location = new Point(23, 233);
			this.button14.Name = "button14";
			this.button14.Size = new Size(75, 23);
			this.button14.TabIndex = 24;
			this.button14.Text = "Sắp Xếp";
			this.button14.UseVisualStyleBackColor = true;
			this.button14.Click += this.button14_Click;
			this.button13.Location = new Point(315, 57);
			this.button13.Name = "button13";
			this.button13.Size = new Size(54, 23);
			this.button13.TabIndex = 23;
			this.button13.Text = "Bỏ tick Tài khỏa đã chọn";
			this.button13.UseVisualStyleBackColor = true;
			this.button13.Click += this.button13_Click;
			this.button12.Location = new Point(168, 57);
			this.button12.Name = "button12";
			this.button12.Size = new Size(140, 23);
			this.button12.TabIndex = 22;
			this.button12.Text = "Tick Tài Khoản đã chọn";
			this.button12.UseVisualStyleBackColor = true;
			this.button12.Click += this.button12_Click;
			this.button11.Location = new Point(120, 198);
			this.button11.Name = "button11";
			this.button11.Size = new Size(75, 23);
			this.button11.TabIndex = 21;
			this.button11.Text = "Thêm File";
			this.button11.UseVisualStyleBackColor = true;
			this.button11.Click += this.button11_Click;
			this.label10.AutoSize = true;
			this.label10.Location = new Point(260, 239);
			this.label10.Name = "label10";
			this.label10.Size = new Size(14, 13);
			this.label10.TabIndex = 19;
			this.label10.Text = "X";
			this.button10.Location = new Point(331, 235);
			this.button10.Name = "button10";
			this.button10.Size = new Size(38, 23);
			this.button10.TabIndex = 18;
			this.button10.Text = "Size";
			this.button10.UseVisualStyleBackColor = true;
			this.button10.Click += this.button10_Click;
			this.txtY.Location = new Point(280, 237);
			this.txtY.Name = "txtY";
			this.txtY.Size = new Size(45, 20);
			this.txtY.TabIndex = 17;
			this.txtX.Location = new Point(216, 237);
			this.txtX.Name = "txtX";
			this.txtX.Size = new Size(42, 20);
			this.txtX.TabIndex = 16;
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new Point(122, 175);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new Size(54, 17);
			this.checkBox2.TabIndex = 15;
			this.checkBox2.Text = "TDLT";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.CheckedChanged += this.checkBox2_CheckedChanged;
			this.button9.Location = new Point(300, 24);
			this.button9.Name = "button9";
			this.button9.Size = new Size(75, 23);
			this.button9.TabIndex = 14;
			this.button9.Text = "Reset";
			this.button9.UseVisualStyleBackColor = true;
			this.button9.Click += this.button9_Click;
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new Point(205, 177);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new Size(45, 17);
			this.checkBox3.TabIndex = 13;
			this.checkBox3.Text = "Port";
			this.checkBox3.UseVisualStyleBackColor = true;
			this.checkBox3.CheckedChanged += this.checkBox3_CheckedChanged;
			this.txtPort.Location = new Point(262, 174);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new Size(93, 20);
			this.txtPort.TabIndex = 12;
			this.label8.AutoSize = true;
			this.label8.Location = new Point(22, 94);
			this.label8.Name = "label8";
			this.label8.Size = new Size(59, 13);
			this.label8.TabIndex = 0;
			this.label8.Text = "Thông báo";
			this.panel1.BackColor = SystemColors.AppWorkspace;
			this.panel1.Controls.Add(this.label9);
			this.panel1.Location = new Point(23, 102);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(332, 67);
			this.panel1.TabIndex = 10;
			this.label9.AutoSize = true;
			this.label9.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 163);
			this.label9.ForeColor = global::System.Drawing.Color.Snow;
			this.label9.Location = new Point(3, 15);
			this.label9.Name = "label9";
			this.label9.Size = new Size(27, 16);
			this.label9.TabIndex = 0;
			this.label9.Text = "null";
			this.button8.Location = new Point(25, 198);
			this.button8.Name = "button8";
			this.button8.Size = new Size(89, 23);
			this.button8.TabIndex = 9;
			this.button8.Text = "Xóa dl game";
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Click += this.button8_Click;
			this.button7.Location = new Point(113, 233);
			this.button7.Name = "button7";
			this.button7.Size = new Size(75, 23);
			this.button7.TabIndex = 8;
			this.button7.Text = "Close all";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += this.button7_Click;
			this.button6.Location = new Point(23, 21);
			this.button6.Name = "button6";
			this.button6.Size = new Size(109, 28);
			this.button6.TabIndex = 7;
			this.button6.Text = "Mở tab đang chọn";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += this.button6_Click;
			this.button5.Location = new Point(219, 24);
			this.button5.Name = "button5";
			this.button5.Size = new Size(75, 23);
			this.button5.TabIndex = 6;
			this.button5.Text = "Tắt";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += this.button5_Click;
			this.button4.Location = new Point(138, 24);
			this.button4.Name = "button4";
			this.button4.Size = new Size(75, 23);
			this.button4.TabIndex = 5;
			this.button4.Text = "Bật";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += this.button4_Click;
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new Point(23, 175);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new Size(91, 17);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Text = "Dò + báo khu";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += this.checkBox1_CheckedChanged;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(696, 548);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.dataGridView1);
			base.Icon = (Icon)resources.GetObject("$this.Icon");
			base.Name = "Form1";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "QLTK - SP";
			base.Load += this.Form1_Load;
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000003 RID: 3
		private const int SW_MINIMIZE = 6;

		// Token: 0x04000004 RID: 4
		private const int SW_RESTORE = 9;

		// Token: 0x04000005 RID: 5
		private const uint WM_CLOSE = 16U;

		// Token: 0x04000006 RID: 6
		public static string FileResource = "Nro_244_Data//Resources//Data";

		// Token: 0x04000007 RID: 7
		public static string AppGameExe = "Nro_244.exe";

		// Token: 0x04000008 RID: 8
		public static int PORT;

		// Token: 0x04000009 RID: 9
		private global::System.Windows.Forms.Timer timer;

		// Token: 0x0400000A RID: 10
		private global::System.Windows.Forms.Timer _timer;

		// Token: 0x0400000B RID: 11
		private global::System.Windows.Forms.Timer __timer;

		// Token: 0x0400000C RID: 12
		private global::System.Windows.Forms.Timer ___timer;

		// Token: 0x0400000D RID: 13
		private global::System.Windows.Forms.Timer ____timer;

		// Token: 0x0400000E RID: 14
		private bool[] isLoggingIn;

		// Token: 0x0400000F RID: 15
		private Dictionary<int, int> loggedInPIDs = new Dictionary<int, int>();

		// Token: 0x04000010 RID: 16
		private SocketOutPut listener = new SocketOutPut();

		// Token: 0x04000011 RID: 17
		private global::System.Windows.Forms.Timer checkTimer;

		// Token: 0x04000012 RID: 18
		private string spreadsheetId = "1__3pRCfIXpfo_UzqVhTHH9wxGVeVPNUkMlftrFACLHE";

		// Token: 0x04000013 RID: 19
		private string sheetRange = "KeyTools";

		// Token: 0x04000014 RID: 20
		private string serviceAccountKey = "credentials.json";

		// Token: 0x04000015 RID: 21
		private string currentCode;

		// Token: 0x04000016 RID: 22
		private string configFilePath = "sevice-setup.json";

		// Token: 0x04000017 RID: 23
		public static string IP;

		// Token: 0x04000018 RID: 24
		public static string Tool;

		// Token: 0x04000019 RID: 25
		public static string Ver;

		// Token: 0x0400001A RID: 26
		public static string KhachHang;

		// Token: 0x0400001B RID: 27
		public static string NameTool = "QLTK - SP Broly";

		// Token: 0x0400001C RID: 28
		public static int lastcheck = 0;

		// Token: 0x0400001D RID: 29
		public static string size = "Nro_244_Data//Resources//Data//size.txt";

		// Token: 0x0400001E RID: 30
		public static string dokhu = "Nro_244_Data//Resources//dokhu";

		// Token: 0x0400001F RID: 31
		public static string tdlt = "Nro_244_Data//Resources//tdlt";

		// Token: 0x04000020 RID: 32
		private IContainer components = null;

		// Token: 0x04000021 RID: 33
		private DataGridView dataGridView1;

		// Token: 0x04000022 RID: 34
		private GroupBox groupBox1;

		// Token: 0x04000023 RID: 35
		private Label label5;

		// Token: 0x04000024 RID: 36
		private TextBox txtPrx;

		// Token: 0x04000025 RID: 37
		private Label label4;

		// Token: 0x04000026 RID: 38
		private Label label3;

		// Token: 0x04000027 RID: 39
		private Label label2;

		// Token: 0x04000028 RID: 40
		private Label label1;

		// Token: 0x04000029 RID: 41
		private ComboBox cboType;

		// Token: 0x0400002A RID: 42
		private ComboBox cboSv;

		// Token: 0x0400002B RID: 43
		private TextBox txtPass;

		// Token: 0x0400002C RID: 44
		private TextBox txtAcc;

		// Token: 0x0400002D RID: 45
		private Button button3;

		// Token: 0x0400002E RID: 46
		private Button button2;

		// Token: 0x0400002F RID: 47
		private Button button1;

		// Token: 0x04000030 RID: 48
		private TextBox txtTeam;

		// Token: 0x04000031 RID: 49
		private Label label6;

		// Token: 0x04000032 RID: 50
		private GroupBox groupBox2;

		// Token: 0x04000033 RID: 51
		private CheckBox checkBox1;

		// Token: 0x04000034 RID: 52
		private Panel panel1;

		// Token: 0x04000035 RID: 53
		private Button button8;

		// Token: 0x04000036 RID: 54
		private Button button7;

		// Token: 0x04000037 RID: 55
		private Button button6;

		// Token: 0x04000038 RID: 56
		private Button button5;

		// Token: 0x04000039 RID: 57
		private Button button4;

		// Token: 0x0400003A RID: 58
		private Label label8;

		// Token: 0x0400003B RID: 59
		private Label label9;

		// Token: 0x0400003C RID: 60
		private DomainUpDown txtPort;

		// Token: 0x0400003D RID: 61
		private CheckBox checkBox3;

		// Token: 0x0400003E RID: 62
		private global::System.Windows.Forms.Timer timer1;

		// Token: 0x0400003F RID: 63
		private Button button9;

		// Token: 0x04000040 RID: 64
		private Button button10;

		// Token: 0x04000041 RID: 65
		private TextBox txtY;

		// Token: 0x04000042 RID: 66
		private TextBox txtX;

		// Token: 0x04000043 RID: 67
		private CheckBox checkBox2;

		// Token: 0x04000044 RID: 68
		private Label label10;

		// Token: 0x04000045 RID: 69
		private Button button11;

		// Token: 0x04000046 RID: 70
		private Button button13;

		// Token: 0x04000047 RID: 71
		private Button button12;

		// Token: 0x04000048 RID: 72
		private Button button14;

		// Token: 0x04000049 RID: 73
		private Button button15;

		// Token: 0x0400004A RID: 74
		private Label label7;

		// Token: 0x0400004B RID: 75
		private TextBox textBox1;

		// Token: 0x0400004C RID: 76
		private DataGridViewTextBoxColumn Column1;

		// Token: 0x0400004D RID: 77
		private DataGridViewTextBoxColumn Column2;

		// Token: 0x0400004E RID: 78
		private DataGridViewTextBoxColumn Column3;

		// Token: 0x0400004F RID: 79
		private DataGridViewTextBoxColumn Column4;

		// Token: 0x04000050 RID: 80
		private DataGridViewTextBoxColumn Column5;

		// Token: 0x04000051 RID: 81
		private DataGridViewTextBoxColumn Column6;

		// Token: 0x04000052 RID: 82
		private DataGridViewTextBoxColumn Column7;

		// Token: 0x04000053 RID: 83
		private DataGridViewTextBoxColumn Column8;

		// Token: 0x04000054 RID: 84
		private DataGridViewCheckBoxColumn Column9;

		// Token: 0x04000055 RID: 85
		private Label label11;

		// Token: 0x04000056 RID: 86
		private TextBox txtLuong;

		// Token: 0x02000008 RID: 8
		private struct RECT
		{
			// Token: 0x0400005C RID: 92
			public int Left;

			// Token: 0x0400005D RID: 93
			public int Top;

			// Token: 0x0400005E RID: 94
			public int Right;

			// Token: 0x0400005F RID: 95
			public int Bottom;
		}

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x06000062 RID: 98
		private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
	}
}
using System;
using System.Windows.Forms;

namespace QLTK___SP
{
	// Token: 0x02000004 RID: 4
	internal static class Program
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00006004 File Offset: 0x00004204
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLTK___SP
{
	// Token: 0x02000005 RID: 5
	internal class SocketOutPut
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00006020 File Offset: 0x00004220
		public void Start(DataGridView dataGridView1)
		{
			this.listenThread = new Thread(delegate
			{
				this.listener = new TcpListener(IPAddress.Any, 8888);
				this.listener.Start();
				for (;;)
				{
					try
					{
						TcpClient client = this.listener.AcceptTcpClient();
						NetworkStream stream = client.GetStream();
						byte[] buffer = new byte[1024];
						int bytesRead = stream.Read(buffer, 0, buffer.Length);
						string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
						bool flag = !string.IsNullOrWhiteSpace(message);
						if (flag)
						{
							List<int> ports = new List<int>();
							dataGridView1.Invoke(new Action(delegate
							{
								int startPort = 1000;
								for (int i = 0; i < dataGridView1.Rows.Count; i++)
								{
									ports.Add(startPort + i);
								}
							}));
							Task.Run(async delegate
							{
								foreach (int port in ports)
								{
									this.SendBackToExe(message, port);
									await Task.Delay(100);
								}
								List<int>.Enumerator enumerator = default(List<int>.Enumerator);
							});
						}
						client.Close();
					}
					catch
					{
					}
				}
			});
			this.listenThread.IsBackground = true;
			this.listenThread.Start();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00006074 File Offset: 0x00004274
		private void SendBackToExe(string message, int port)
		{
			try
			{
				using (TcpClient client = new TcpClient("127.0.0.1", port))
				{
					NetworkStream stream = client.GetStream();
					byte[] data = Encoding.UTF8.GetBytes(message);
					stream.Write(data, 0, data.Length);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000060E4 File Offset: 0x000042E4
		public void Stop()
		{
			TcpListener tcpListener = this.listener;
			if (tcpListener != null)
			{
				tcpListener.Stop();
			}
			Thread thread = this.listenThread;
			if (thread != null)
			{
				thread.Abort();
			}
		}

		// Token: 0x04000057 RID: 87
		private TcpListener listener;

		// Token: 0x04000058 RID: 88
		private Thread listenThread;
	}
}
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace QLTK___SP.Properties
{
	// Token: 0x02000006 RID: 6
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00006114 File Offset: 0x00004314
		internal Resources()
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00006120 File Offset: 0x00004320
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager temp = new ResourceManager("QLTK___SP.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = temp;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00006168 File Offset: 0x00004368
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000617F File Offset: 0x0000437F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x04000059 RID: 89
		private static ResourceManager resourceMan;

		// Token: 0x0400005A RID: 90
		private static CultureInfo resourceCulture;
	}
}
using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace QLTK___SP.Properties
{
	// Token: 0x02000007 RID: 7
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00006188 File Offset: 0x00004388
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x0400005B RID: 91
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}

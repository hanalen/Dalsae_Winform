using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;

namespace TwitterClient
{
	public partial class PinForm : Form
	{
		private bool isPin = false;
		public PinForm(bool isPin)
		{
			InitializeComponent();
			this.isPin = isPin;

			if(isPin)
			{
				label1.Text = "웹 페이지에 나와있는 7자리 숫자를 입력 해주세요.";
				this.Text = "Pin 입력";
				button1.Text = "확인";
			}
			else
			{
				label1.Text = "검색 할 아이디를 입력 해주세요";
				this.Text = "아이디 검색";
				button1.Text = "검색";
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (isPin)//핀 발급시
			{
				InputPin();
			}
			else//아이디 검색
			{
				SearchID();
			}
		}

		private void InputPin()
		{
			int pin;
			if (int.TryParse(textBox1.Text, out pin) && textBox1.Text.Length == 7)
			{
				ClientInctence.UpdatePin(textBox1.Text);
				this.Close();
			}
			else
			{
				MessageBox.Show(this, "웹 페이지에서 발급 받은.\r7자리 숫자를 제대로 입력 해주세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void SearchID()
		{
			//ParameterUserTimeLine parameter = new ParameterUserTimeLine();
			//parameter.screen_name = textBox1.Text;
			ClientInctence.LoadTweet(MainForm.eSelectMenu.USER,textBox1.Text);
			this.Close();
		}

		private void pinForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				e.Handled = true;
				if (isPin)
					InputPin();
				else
					SearchID();
			}
		}
	}
}

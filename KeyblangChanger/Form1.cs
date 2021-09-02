using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace KeyblangChanger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.textBox1.GotFocus += TextBox1OnGotFocus;
            this.textBox1.Click += TextBox1OnGotFocus;
        }

        private void TextBox1OnGotFocus(object sender, EventArgs e)
        {
            this.textBox1.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = GetLayoutId();
        }

        [DllImport("user32.dll")]
        static extern bool GetKeyboardLayoutName([Out] StringBuilder pwszKLID);

        static string GetLayoutId()
        {
            var sb = new StringBuilder(10);
            GetKeyboardLayoutName(sb);
            return sb.ToString();
        }
    }
}
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        [DllImport("winmm.dll")]
        public static extern int mciSendString(string command, string buffer, int bufferSize, IntPtr hwndCallback);

        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes)
            {
                listBox1.Items.Add(p.ProcessName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string shortcutPath = @"C:\Users\Dzmitry\Desktop\Новый текстовый документ.txt"; 
            try
            {
                Process.Start(shortcutPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при запуске ярлыка: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string shortcutPath = @"C:\Users\Dzmitry\Desktop\Новый текстовый документ.txt"; 
            try
            {
                Process[] processes = Process.GetProcesses();
                foreach (Process p in processes)
                {
                    if (p.MainModule.FileName.ToLower() == shortcutPath.ToLower())
                    {
                        p.Kill();
                        return;
                    }
                }
                MessageBox.Show("Ярлык не был найден.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при закрытии ярлыка: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mciSendString("set cdaudio door open", null, 0, IntPtr.Zero);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mciSendString("set cdaudio door closed", null, 0, IntPtr.Zero);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MONITORPOWER, MONITOR_OFF);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MONITORPOWER, MONITOR_ON);
        }

        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MONITORPOWER = 0xF170;
        private const int MONITOR_ON = -1;
        private const int MONITOR_OFF = 2;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    }
}

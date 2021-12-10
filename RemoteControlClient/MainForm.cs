using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteRunClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            server = new HttpServer(ip, port);
            wsclient = new WebSocketClient($"ws://{ip}:{port}");
            wsclient.OnOpen += websocket_OnOpen;
            wsclient.OnError += websocket_OnError;
            wsclient.OnMessage += websocket_OnMessage;
            wsclient.OnClose += websocket_OnClose;
            //timer = new System.Timers.Timer();
            //timer.Interval = 20;
            //timerIsRunning = false;
            //timer.Elapsed += SetPos;
            board = new Board();
        }



        private HttpServer server;
        private WebSocketClient wsclient;
        private System.Timers.Timer timer;
        private bool IsWSClient => serverCheckBox.Checked;
        private string ip = "127.0.0.1";
        private int port = 8088;
        private string exePath = "";
        private string args = "";
        private Board board;
        private bool timerIsRunning;
        //private Win32Utils.POINT point;

        private void ToggleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsWSClient)
                {
                    if (!wsclient.IsConnecting)
                    {
                        var url = IPtextBox.Text;
                        if (!string.IsNullOrWhiteSpace(url))
                        {
                            ip = url.Split(':')[0];
                            port = Convert.ToInt32(url.Split(':')[1]);
                            server.SetListenIPPort(ip, port);
                        }
                        wsclient.SetUri($"ws://{ip}:{port}");
                        wsclient.Open();

                    }
                    else
                    {
                        wsclient.Close();

                    }
                }
                else
                {
                    if (server.IsListening)
                    {
                        server.Stop();
                        toggleBtn.Text = "启动服务器";
                        MessageBox.Show("服务器停止");
                    }
                    else
                    {
                        server.SetExePath(exePath);
                        var url = IPtextBox.Text;
                        if (!string.IsNullOrWhiteSpace(url))
                        {
                            ip = url.Split(':')[0];
                            port = Convert.ToInt32(url.Split(':')[1]);
                            server.SetListenIPPort(ip, port);
                        }
                        server.Start();
                        toggleBtn.Text = "停止服务器";
                        if (string.IsNullOrWhiteSpace(exePath))
                        {
                            MessageBox.Show($"服务器启动，监听 {ip}:{port}");
                        }
                        else
                        {
                            MessageBox.Show($"服务器启动，监听 {ip}:{port}, 执行程序: {exePath}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.GetType().Name);
                MessageBox.Show("出错了!请检查IP和端口，如果没问题也许需要管理员权限？", "出错了!");
            }
        }

        private void selectExeBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "可执行文件|*.exe|所有文件|*.*";
            ofd.Title = "请选择可执行程序";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName.IndexOf(":") < 0)
                {
                    return;
                }
                exePath = ofd.FileName;
                exeLabel.Text= $"程序: {exePath}";
            }
            else
            {
                MessageBox.Show("未选择文件，退出修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void HideBtn_Click(object sender, EventArgs e)
        {
            if (server.IsListening || wsclient.IsConnecting)
            {
                Hide();
            }
            else
            {
                MessageBox.Show("没启动服务不可隐藏窗口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void websocket_OnOpen(object sender, EventArgs e)
        {
            //statusPoint.Fill = greenBrush;
            //logger.AddLog($"Connect to Server successful.");
            toggleBtn.Invoke(new Action(() => { toggleBtn.Text = "断开连接"; }));
            serverCheckBox.Invoke(new Action(() => { serverCheckBox.Enabled = false; }));
            wsclient.Send("/rc");
        }

        private void websocket_OnError(object sender, Exception ex)
        {
            //logger.AddLog($"Error occurred! {ex.Message}");
            //MessageBox.Show($"error: {ex.Message}", "出错了！", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void websocket_OnMessage(object sender, string data)
        {
            //logger.AddLog($"Receive {data}");
            try
            {

                if (data == "/run")
                {
                    if (string.IsNullOrWhiteSpace(exePath))
                    {
                        //returnObj = $"<h1>错误！未设置执行文件地址!</h1>";
                        wsclient.Send("/info 错误！未设置执行文件地址!");
                        return;
                    }
                    Task.Run(() =>
                    {
                        var process = new Process
                        {
                            StartInfo =
                            {
                                //UseShellExecute = true,
                                FileName = exePath,
                                Arguments = args,
                                CreateNoWindow = true,
                                //Verb = "runas"
                            }
                        };
                        process.Start();
                        
                        //var proc = Process.Start();
                    });
                }
                else if (data == "/ok")
                {

                }
                else if (data == "/hide")
                {
                    if(timerIsRunning)
                    {
                        timerIsRunning = false;
                        //timer.Stop();
                        //Win32Utils.ShowCur();
                        board.Hide();
                        wsclient.Send("/info 屏幕已释放");
                    }
                    else
                    {
                        //point = Win32Utils.GetPos();
                        //timer.Start();
                        //Win32Utils.HideCur();
                        board.Show();
                        timerIsRunning = true;
                        wsclient.Send("/info 屏幕已挟持");
                    }
                }
                else if (data == "/exit")
                {
                    wsclient.Send("/info 2秒后退出");
                    Task.Run(() =>
                    {
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                    });
                }
                else
                {
                    wsclient.Send($"/info 不能识别的指令: {data}");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void websocket_OnClose(object sender, EventArgs e)
        {
            toggleBtn.Invoke(new Action(() => { toggleBtn.Text = "连接WS服务器"; }));
            serverCheckBox.Invoke(new Action(() => { serverCheckBox.Enabled = true; }));
        }

        private void serverCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (IsWSClient)
            {
                toggleBtn.Text = "连接WS服务器";
            }
            else
            {
                toggleBtn.Text = "启动";
            }
        }

        private void argBox_TextChanged(object sender, EventArgs e)
        {
            args = argBox.Text;
        }

        private void SetPos(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Win32Utils.SetPos(point.X, point.Y);
            if(timerIsRunning)
            {
                timer.Start();
            }
        }
    }
}

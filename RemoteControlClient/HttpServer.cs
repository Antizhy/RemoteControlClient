using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteRunClient
{
    class HttpServer
    {
        public HttpServer(string url, int port)
        {
            //endPoint = new IPEndPoint(IPAddress.Parse(url), port);
            listener = new HttpListener();
            listener.Prefixes.Add($"http://{url}:{port}/");
            
            //listener.Prefixes.Add($"http://+:{port}/");

        }
        private string execvPath = "";
        //private IPEndPoint endPoint;
        private HttpListener listener;
        private static Encoding UTF8 = new UTF8Encoding(false);
        private bool isListening = false;
        public bool IsListening => isListening;
        public void Start()
        {
            listener.Start();
            isListening = true;
            listener.BeginGetContext(Result, null);
        }

        public void Stop()
        {
            listener.Stop();
            isListening = false;
        }

        public void SetExePath(string path)
        {
            execvPath = path;
        }

        public void SetListenIPPort(string ip, int port)
        {
            if(!isListening)
            {
                listener.Prefixes.Clear();
                listener.Prefixes.Add($"http://{ip}:{port}/");
            }
            else
            {
                MessageBox.Show("服务器运行时不可修改", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void Result(IAsyncResult ar)
        {
            var guid = Guid.NewGuid().ToString();
            try
            {
                listener.BeginGetContext(Result, null);
                var ctx = listener.EndGetContext(ar);
                ctx.Response.StatusCode = (int)HttpStatusCode.OK;
                ctx.Response.ContentType = "text/html;charset=UTF-8";
                //ctx.Response.AddHeader("Content-Type", "text/html");
                ctx.Response.ContentEncoding = UTF8;
                string returnObj = "";
                //定义返回客户端的信息
                //if (ctx.Request.HttpMethod == "POST" && ctx.Request.InputStream != null)
                //{
                //    //处理客户端发送的请求并返回处理信息
                //    returnObj = HandleRequest(ctx.Request, ctx.Response);
                //}
                //else 
                if (ctx.Request.HttpMethod == "GET")
                {
                    //MessageBox.Show(ctx.Request.RawUrl);
                    var rawurl = ctx.Request.RawUrl;
                    if (rawurl == "/run")
                    {
                        if (string.IsNullOrWhiteSpace(execvPath))
                        {
                            returnObj = $"<h1>错误！未设置执行文件地址!</h1>";
                        }
                        else
                        {
                            returnObj = $"执行程序{execvPath}";
                            Task.Run(() => 
                            {
                                var proc = Process.Start(execvPath);
                            });
                        }
                    }
                    else if(rawurl == "/about")
                    {
                        returnObj = $"<h1 style=\"color: #007acc;\">RemoteRun v1.2</h1><h2>designed by ?</h2>";
                    }
                    else if (rawurl == "/exit")
                    {
                        returnObj = $"<h1>2秒后退出</h1>";
                        Task.Run(() =>
                        {
                            Thread.Sleep(2000);
                            Environment.Exit(0);
                        });
                    }
                    else
                    {
                        returnObj = $"<h1>{ctx.Request.RawUrl}</h1>";
                    }
                }
                else
                {
                    returnObj = $"不支持的请求";
                }
                var returnByteArr = UTF8.GetBytes(returnObj);//设置客户端返回信息的编码
                using (var stream = ctx.Response.OutputStream)
                {
                    //把处理信息返回到客户端
                    stream.Write(returnByteArr, 0, returnByteArr.Length);
                }
                //MessageBox.Show($"请求处理完成：{guid}, 时间：{ DateTime.Now }");
            }
            catch (InvalidOperationException ioe)
            {
                //
            }
            catch (Exception ex)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //MessageBox.Show(ex.ToString());
                //Console.WriteLine($"出错：{ex.ToString()}");
            }
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine($"请求处理完成：{guid},时间：{ DateTime.Now.ToString()}\r\n");
            
        }
        private static string HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string data = null;
            try
            {
                var byteList = new List<byte>();
                var byteArr = new byte[2048];
                int readLen = 0;
                int len = 0;
                //接收客户端传过来的数据并转成字符串类型
                do
                {
                    readLen = request.InputStream.Read(byteArr, 0, byteArr.Length);
                    len += readLen;
                    byteList.AddRange(byteArr);
                } while (readLen != 0);
                data = UTF8.GetString(byteList.ToArray(), 0, len);

                //获取得到数据data可以进行其他操作
            }
            catch (Exception ex)
            {
                response.StatusDescription = "404";
                response.StatusCode = 404;
                //MessageBox.Show($"在接收数据时发生错误:{ex}");
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine($"在接收数据时发生错误:{ex.ToString()}");
                return $"500";//把服务端错误信息直接返回可能会导致信息不安全，此处仅供参考
            }
            response.StatusDescription = "200";//获取或设置返回给客户端的 HTTP 状态代码的文本说明。
            response.StatusCode = 200;// 获取或设置返回给客户端的 HTTP 状态代码。
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"接收数据完成:{data.Trim()},时间：{DateTime.Now.ToString()}");
            return $"接收数据完成";
        }
    }
}


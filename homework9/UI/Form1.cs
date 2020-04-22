using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using SimpleCrawler;

namespace UI
{
    public partial class Form1 : Form
    {
        string startUrl="http://www.baidu.com";
        
        public Form1()
        {
            InitializeComponent();
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            startUrl = textBox1.Text;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            urls.Clear();
            count = 0;
            urls.Add(startUrl, false);
            richTextBox1.AppendText( "开始爬行了...\n");
            Crawl();
            richTextBox1.AppendText("爬行结束\n"); 

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public System.Collections.Hashtable urls = new Hashtable();
        private int count = 0;
        // public string startUrl { set; get; }
        /*static void Main(string[] args)
        {
            SimpleCrawler myCrawler = new SimpleCrawler();
           
            //if (args.Length >= 1) startUrl = args[0];
            myCrawler.urls.Add(startUrl, false);//加入初始页面
            new Thread(myCrawler.Crawl).Start();
        }*/

        private void Crawl()
        {

            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)
                {
                    if ((bool)urls[url]) continue;
                    current = url;
                }

                if (current == null || count > 20) break;
                richTextBox1.AppendText("爬行" + current + "页面!\n");
                string html = DownLoad(current); // 下载
                urls[current] = true;
                count++;
                Parse(html, ref current);//解析,并加入新的链接

            }
        }


        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                return html;
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText(ex.Message+"\n");
                return "";
            }
        }

        private void Parse(string html, ref string current)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
            if (current.Substring(current.Length - 1, 1) == "/") current = current.Substring(0, current.Length - 1);

            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>');
                if (strRef.Length <= 1) continue;
                if (strRef.Substring(0, 2).Equals("//")) strRef = "http:"+strRef;
                if (strRef.Length >= 4 && strRef.Substring(0, 4) == "http" && current.Length <= strRef.Length)
                {
                    if (current == strRef.Substring(0, current.Length)) { }
                    else continue;
                }
                
                if (strRef.Substring(0, 1) == "/") strRef = current + strRef;
                if (strRef.Substring(0, 4) == ".../") strRef = current.Substring(0, current.LastIndexOf("/")) + strRef.Substring(3, strRef.Length - 3);
                if (urls[strRef] == null) urls[strRef] = false;
            }
        }
    }
}

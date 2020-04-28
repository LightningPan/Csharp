using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawlerForm {
  class Crawler {
    public event Action<Crawler> CrawlerStopped;
    public event Action<Crawler, string, string> PageDownloaded;

    //所有已下载和待下载URL，key是URL，value表示是否下载成功
    private Dictionary<string, bool> urls = new Dictionary<string, bool>();

    //待下载队列
    private Queue<string> pending = new Queue<string>();

    //URL检测表达式，用于在HTML文本中查找URL
    private readonly string urlDetectRegex = @"(href|HREF)\s*=\s*[""'](?<url>[^""'#>]+)[""']";

    //URL解析表达式
    public static readonly string urlParseRegex = @"^(?<site>https?://(?<host>[\w\d.]+)(:\d+)?($|/))([\w\d]+/)*(?<file>[^#?]*)";
    public string HostFilter { get; set; } //主机过滤规则
    public string FileFilter { get; set; } //文件过滤规则
    public int MaxPage { get; set; } //最大下载数量
    public string StartURL { get; set; } //起始网址
    public Encoding HtmlEncoding { get; set; } //网页编码
    public Dictionary<string, bool> DownloadedPages { get => urls; }
    

    public Crawler() {
      MaxPage = 100;
      HtmlEncoding = Encoding.UTF8;
      
    }
        
    public void Start(){
       urls.Clear();
       pending.Clear();
       pending.Enqueue(StartURL);
       int count = 0;
       List<String> html=new List<string>();
       List<Task> tasks = new List<Task>();
      List<string> url = new List<string>();
       while ((count < MaxPage && pending.Count > 0)||tasks.Count==3){
        if (tasks.Count != 3) { url.Add(pending.Dequeue()); }
        try{
                    if (tasks.Count < 3)
                    {
                        tasks.Add(Task.Run(() =>
                        {
                            html.Add(DownLoad(url[url.Count - 1]));
                            urls[url[url.Count - 1]] = true;
                            PageDownloaded(this, url[url.Count - 1], "success");
                        }));
                        continue;
                    }
                    else {
                        Task.WaitAll(tasks.ToArray());
                        for (int i = 0; i < tasks.Count; i++) {
                            Parse(html[i], url[i]);
                        }
                        html.Clear();
                        tasks.Clear();
                        url.Clear();
                        
                    }
                    if ((tasks.Count + pending.Count) < 3) {
                        url.Add(pending.Dequeue());
                        tasks.Add(Task.Run(() =>
                        {
                            html.Add(DownLoad(url[url.Count - 1]));
                            urls[url[url.Count - 1]] = true;
                            PageDownloaded(this, url[url.Count - 1], "success");
                        }));
                        Task.WaitAll(tasks.ToArray());
                        for (int i = 0; i < tasks.Count; i++)
                        {
                            Parse(html[i], url[i]);
                        }
                        html.Clear();
                        tasks.Clear();
                        url.Clear();
                    }
            
            //Parse(html, url);//解析,并加入新的链接
          }
          catch (Exception ex) {
            PageDownloaded(this, url[url.Count-1], "  Error:" + ex.Message);
          }
          count++;
         }


        CrawlerStopped(this);
     }


    private string DownLoad(string url) {
      WebClient webClient = new WebClient();
      webClient.Encoding = Encoding.UTF8;
      string html = webClient.DownloadString(url);
      string fileName = urls.Count.ToString();
      File.WriteAllText(fileName, html, Encoding.UTF8);
      return html;
    }

    private void Parse(string html, string pageUrl) {

      var matches = new Regex(urlDetectRegex).Matches(html);
      foreach (Match match in matches) {
        string linkUrl = match.Groups["url"].Value;
        if (linkUrl == null || linkUrl == "") continue;
        linkUrl = FixUrl(linkUrl, pageUrl);//转绝对路径

        //解析出host和file两个部分，进行过滤
        Match linkUrlMatch = Regex.Match(linkUrl, urlParseRegex);
        string host = linkUrlMatch.Groups["host"].Value;
        string file = linkUrlMatch.Groups["file"].Value;
        if (file == "") file = "index.html";

        if (Regex.IsMatch(host, HostFilter) && Regex.IsMatch(file, FileFilter)
          && !urls.ContainsKey(linkUrl)) {
            pending.Enqueue(linkUrl);
            urls.Add(linkUrl, false);
        }
      }
    }


    //将相对路径转为绝对路径
    static private string FixUrl(string url, string baseUrl) {
      if (url.Contains("://")) {
        return url;
      }
      if (url.StartsWith("//")) {
        return "http:" + url;
      }
      if (url.StartsWith("/")) {
        Match urlMatch = Regex.Match(baseUrl, urlParseRegex);
        String site = urlMatch.Groups["site"].Value;
        return site.EndsWith("/") ? site + url.Substring(1) : site + url;
      }

      if (url.StartsWith("../")) {
        url = url.Substring(3);
        int idx = baseUrl.LastIndexOf('/');
        return FixUrl(url, baseUrl.Substring(0, idx));
      }

      if (url.StartsWith("./")) {
        return FixUrl(url.Substring(2), baseUrl);
      }

      int end = baseUrl.LastIndexOf("/");
      return baseUrl.Substring(0, end) + "/" + url;
    }
  }

   
}

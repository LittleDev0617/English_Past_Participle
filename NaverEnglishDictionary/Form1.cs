using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace NaverEnglishDictionary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://endic.naver.com/search.nhn?sLn=kr&isOnlyViewEE=N&query=" + EnWord.Text);
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
            HtmlDocument doc = webBrowser1.Document;
            HtmlElementCollection trs = doc.GetElementsByTagName("a");

            foreach (HtmlElement el in trs)
            {
                string cn = el.GetAttribute("className");
                if (cn.Contains("N=a:wrd.entry,r:1,"))
                {
                    string de = el.GetAttribute("href");
                    webBrowser1.Navigate(de);
                    while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                    {
                        Application.DoEvents();
                    }
                    HtmlDocument doc2 = webBrowser1.Document;
                    HtmlElementCollection c = doc2.GetElementsByTagName("span");
                    foreach (HtmlElement d in c)
                    {
                        if (d.InnerText == "과거분사")
                        {
                            HtmlElement par = d.Parent;
                            HtmlElementCollection child = par.Children;
                            foreach(HtmlElement pp in child)
                            {
                                if(pp.GetAttribute("className") == "fnt_e07")
                                {
                                    label1.Text = pp.InnerText;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Classadress
{
    public class WebAPI
    {
        public bool SelectListView(string url, ListView listView)
        {
            listView.Items.Clear();
            try
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url);
                StreamReader sr = new StreamReader(stream);
                string result = sr.ReadToEnd();
                ArrayList list = JsonConvert.DeserializeObject<ArrayList>(result);
                for (int i = 0; i < list.Count; i++)
                {
                    JArray j = (JArray)list[i];
                    string[] arr = new string[j.Count];
                    for (int k = 0; k < j.Count; k++)
                    {
                        arr[k] = j[k].ToString();
                    }
                    listView.Items.Add(new ListViewItem(arr));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Selectpic(string url, int hNum, PictureBox pictureBox, string tb8)
        {
            try
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url);
                StreamReader sr = new StreamReader(stream);
                string result = sr.ReadToEnd();
                ArrayList list = JsonConvert.DeserializeObject<ArrayList>(result);
                for (int i = hNum - 1; i < hNum; i++)
                {
                    JArray j = (JArray)list[i];
                    string[] arr = new string[j.Count];
                    for (int k = 0; k < j.Count; k++)
                    {
                        if (k == 2)
                        {
                            arr[k] = j[k].ToString();
                            tb8 = arr[k];
                            pictureBox.Load(arr[k]);
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Post(string url, Hashtable ht)
        {
            try
            {
                WebClient wc = new WebClient();
                NameValueCollection param = new NameValueCollection();

                foreach (DictionaryEntry data in ht)
                {
                    param.Add(data.Key.ToString(), data.Value.ToString());
                }
                byte[] result = wc.UploadValues(url, "POST", param);
                string resultstr = Encoding.UTF8.GetString(result);
                if ("1" == resultstr)
                {
                    // MessageBox.Show("성공");
                }
                else
                {
                    // MessageBox.Show("실패");
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

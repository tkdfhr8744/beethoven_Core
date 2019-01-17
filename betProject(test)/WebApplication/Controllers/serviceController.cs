using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using betProject.module;
using System.Data.SqlClient;
using System.IO;
using MySql.Data.MySqlClient;

namespace WebApplication.Controllers
{
    [ApiController]
    public class serviceController : ControllerBase
    {
        [Route("select")]
        [HttpGet]
        public ActionResult<ArrayList> select()
        {
            Database db = new Database();
            MySqlDataReader sdr = db.Reader("p_tools_select");
            ArrayList list = new ArrayList();
            while (sdr.Read())
            {
                string[] arr = new string[sdr.FieldCount];
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    arr[i] = sdr.GetValue(i).ToString();
                }
                list.Add(arr);
            }
            db.ReaderClose(sdr);
            db.ConnectionClose();
            return list;
        }

        [Route("select_img")]
        [HttpGet]
        public ActionResult<ArrayList> select_img()
        {
            Database db = new betProject.module.Database();
            MySqlDataReader sdr = db.Reader("p_tools_selectimg");
            ArrayList list = new ArrayList();
            while (sdr.Read())
            {
                string[] arr = new string[sdr.FieldCount];
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    arr[i] = sdr.GetValue(i).ToString();
                }
                list.Add(arr);
            }
            db.ReaderClose(sdr);
            db.ConnectionClose();
            return list;
        }

        [Route("insert")]
        [HttpPost]
        public ActionResult<string> insert([FromForm] string hName, [FromForm] string cpName, [FromForm]string weight, [FromForm]string EA, [FromForm] string filename, [FromForm]string filedata)
        {
            string hUrl = "";
            string path = System.IO.Directory.GetCurrentDirectory();//현재프로젝트의 위치를 나타냄 여기는 sevice의 위치값
            path += "\\wwwroot";
            if (!System.IO.Directory.Exists(path))//위치가 있는지 없는지 파악할때 이용
            {
                System.IO.Directory.CreateDirectory(path);//path에 주소가 없을경우 폴더를 만들어 경로 생성
            }

            byte[] data = Convert.FromBase64String(filedata);//스트링 바이트로 변환하여 넣기

            try
            {
                string ext = filename.Substring(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf("."));
                Guid savename = Guid.NewGuid();
                string fullname = savename + ext;//파일명 만들기
                string fullpath = string.Format("{0}\\{1}", path, fullname); //경로 + 파일명
                FileInfo fi = new FileInfo(fullpath);
                FileStream fs = fi.Create();
                fs.Write(data, 0, data.Length);
                fs.Close();

                hUrl = string.Format("http://192.168.3.12:5000/{0}", fullname);
            }
            catch
            {
                Console.WriteLine("저장실패");
            }

            Hashtable ht = new Hashtable();
            ht.Add("_hName", hName);
            ht.Add("_cpName", cpName);
            ht.Add("_hUrl", hUrl);
            ht.Add("_weight", weight);
            ht.Add("_EA", EA);
            Database db = new Database();
            if (db.NonQuery("p_tools_insert", ht))
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        [Route("update")]
        [HttpPost]
        public ActionResult<string> update([FromForm]string hNo, [FromForm] string hName, [FromForm] string cpName, [FromForm]string weight, [FromForm]string EA, [FromForm] string filename, [FromForm]string filedata)
        {
            /*
            string path=System.IO.Directory.GetCurrentDirectory();
            path+="\\wwwroot";
            FileInfo fi=new FileInfo(path);
            fi.Delete();*/

            string hUrl = "";
            string path = System.IO.Directory.GetCurrentDirectory();//현재프로젝트의 위치를 나타냄 여기는 sevice의 위치값
            path += "\\wwwroot";
            if (!System.IO.Directory.Exists(path))//위치가 있는지 없는지 파악할때 이용
            {
                System.IO.Directory.CreateDirectory(path);//path에 주소가 없을경우 폴더를 만들어 경로 생성
            }

            byte[] data = Convert.FromBase64String(filedata);//스트링 바이트로 변환하여 넣기

            try
            {
                string ext = filename.Substring(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf("."));
                Guid savename = Guid.NewGuid();
                string fullname = savename + ext;//파일명 만들기
                string fullpath = string.Format("{0}\\{1}", path, fullname); //경로 + 파일명
                FileInfo fi = new FileInfo(fullpath);
                FileStream fs = fi.Create();
                fs.Write(data, 0, data.Length);
                fs.Close();

                hUrl = string.Format("http://192.168.3.12:5000/{0}", fullname);
            }
            catch
            {
                Console.WriteLine("저장실패");
            }
            Hashtable ht = new Hashtable();
            ht.Add("_hNo", hNo);
            ht.Add("_hName", hName);
            ht.Add("_cpName", cpName);
            ht.Add("_hUrl", hUrl);
            ht.Add("_weight", weight);
            ht.Add("_EA", EA);
            Database db = new Database();
            if (db.NonQuery("p_tools_update", ht))
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
        [Route("delete")]
        [HttpPost]
        public ActionResult<string> delete([FromForm] string hNo)
        {
            Hashtable ht = new Hashtable();
            ht.Add("_hNo", hNo);
            Database db = new Database();
            if (db.NonQuery("p_tools_delete", ht))
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }


    }
}

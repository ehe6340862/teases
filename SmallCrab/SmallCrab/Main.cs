using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmallCrab
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        //bool->true 文件夹Folder
        static Dictionary<string, bool> _list = new Dictionary<string, bool>();//定义list变量，存放获取到的路径
        private void btnStart_Click(object sender, EventArgs e)
        {
            //lsvConsole.Items.Clear();
            //Thread thread = new Thread(new ThreadStart(Start));
            //thread.IsBackground = true;
            //thread.Start();

            string basePath = txtPath.Text.ToString();
            BuildOCFile(basePath, "hahahaha");
        }
        public void Start()
        {

            string basePath = txtPath.Text.ToString();
            if (string.IsNullOrWhiteSpace(basePath))
            {
                SetLabelText("<-------------------------------path error------------------------------------->");
            }
            else
            {
                _list.Clear();
                getPath(basePath);
                Tease();
                //BuildFile(basePath);
                SetLabelText("<------------------------------------------------------------------------------->");
                SetLabelText("<------------------------------------------------------------------------------->");
                SetLabelText("<----------------------------- mission success!--------------------------------->");
                SetLabelText("<------------------------------------------------------------------------------->");
                SetLabelText("<------------------------------------------------------------------------------->");
            }

        }
        delegate void labDelegate(string str);
        private void SetLabelText(string str)
        {
            if (lsvConsole.InvokeRequired)
            {
                Invoke(new labDelegate(SetLabelText), new string[] { str });
            }
            else
            {
                lsvConsole.Items.Add(str);
                lsvConsole.TopIndex = lsvConsole.Items.Count - (int)(lsvConsole.Height / lsvConsole.ItemHeight);
            }


        }
        /// <summary>
        /// 获取文件夹下面所有的文件和文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Dictionary<string, bool> getPath(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fil = dir.GetFiles();
            DirectoryInfo[] dii = dir.GetDirectories();
            foreach (FileInfo f in fil)
            {
                if (f.Name.Contains("crabs_img"))
                {
                    File.Delete(f.FullName);
                }
                else
                {
                    _list.Add(f.FullName, false);//添加文件的路径到列表
                }

            }
            //获取子文件夹内的文件列表，递归遍历
            foreach (DirectoryInfo d in dii)
            {
                getPath(d.FullName);
                _list.Add(d.FullName, true);//添加文件夹的路径到列表
            }
            return _list;
        }
        /// <summary>
        /// 混淆路的代码
        /// </summary>
        public void Tease()
        {
            foreach (string path in _list.Keys)
            {

                if (!_list[path])
                {
                    string suffix = getSuffix(path);
                    switch (suffix)
                    {
                        case ".css":
                            TeaseCss(path);
                            break;
                        case ".js":
                            TeaseJavaScript(path);
                            break;
                        case ".html":
                            TeaseHtml(path);
                            break;
                        case ".htm":
                            TeaseHtml(path);
                            break;
                        case ".png":
                            TeaseImg(path);
                            break;
                        case ".jpg":
                            TeaseImg(path);
                            break;
                        case ".jpeg":
                            TeaseImg(path);
                            break;
                        case ".mp3":
                            TeaseMedia(path);
                            break;
                        case ".m4a":
                            TeaseMedia(path);
                            break;
                        case ".ogg":
                            TeaseMedia(path);
                            break;
                        case ".wav":
                            TeaseMedia(path);
                            break;
                        default:
                            break;
                    }
                }

            }
        }
        public void TeaseHtml(string path)
        {
            string content = ReadFile(path) ?? "";
            SetLabelText("混淆javascript文件：" + path);
            content = "<!--" + getNow() + ":" + getGuid() + getGuid() + "-->" + content;
            WriteFile(path, content);
        }
        public void TeaseJavaScript(string path)
        {
            string content = ReadFile(path) ?? "";
            SetLabelText("混淆javascript文件：" + path);
            content = "/*" + getNow() + ":" + getGuid() + "*/" + content;
            //data.js
            WriteFile(path, content);
        }
        public void TeaseCss(string path)
        {
            string content = ReadFile(path) ?? "";
            SetLabelText("混淆css文件：" + path);
            content = "/*" + getNow() + ":" + getGuid() + "*/" + content;
            WriteFile(path, content);

        }
        public void TeaseImg(string path)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                SetLabelText("混淆image文件：" + path);
                writer.Write(getGuid());
            }

        }
        public void TeaseMedia(string path)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                SetLabelText("混淆media文件：" + path);
                writer.Write(getGuid());
            }
        }
        public void BuildOCFile(string basePath, string className)
        {
            //生成.h文件
            string htemp = string.Empty;
            string mtemp = string.Empty;

            //生成注释
            htemp += "/*\n";
            mtemp += "/*\n";
            int rd = getRandom().Next(5, 20);
            for (int i = 0; i < rd; i++)
            {
                htemp += string.Format("   {0}\n", GetLongStr());
            }
            htemp += "*/\n";
            mtemp += "*/\n";

            htemp += string.Format("   //{0}\n", GetLongStr());
            htemp += string.Format("   \n", "");
            htemp += "#import <Cordova/CDVViewController.h>" + "\n";
            htemp += "@interface {0}{{" + "\n";
            htemp += string.Format("    //{0}\n", GetLongStr());
            htemp += "}}" + "\n";

            mtemp += string.Format("   //{0}\n", GetLongStr());
            mtemp += string.Format("   \n", "");
            mtemp += "#import {0}.h\"" + "\n";
            mtemp += "@interface {1}()" + "\n";
            mtemp += "@end" + "\n";
            mtemp += "@implementation {2}" + "\n";

            htemp = string.Format(htemp, className);
            mtemp = string.Format(mtemp, className, className, className);
            List<Entry<string, string>> func = BuildOCFuncs();
            foreach (Entry<string,string> item in func)
            {
                htemp += item.key;
                mtemp += item.value;

            }
            htemp += "@end" + "\n";
            mtemp += "@end" + "\n";
            WriteFile(Path.Combine(basePath, className + ".h"), htemp);
            WriteFile(Path.Combine(basePath, className + ".m"), mtemp);
        }
        public List<Entry<string, string>> BuildOCFuncs()
        {
            List<Entry<string, string>> rt = new List<Entry<string, string>>();
            string[] tp = new string[] { "NSString*", "NSNumber*", "NSDictionary*", "NSInteger", "NSArray*" };

            int cnt = getRandom().Next(10,20);
            for (int c = 0; c < cnt; c++)
            {
                string key = string.Empty;
                string val = string.Empty;
                key += string.Format("   //{0}\n", getNow() + GetLongStr() + GetLongStr());
                key += "-(void) ";
                int rd = getRandom().Next(1, 9);
                for (int i = 0; i < rd; i++)
                {
                    key += GetRandomStr() + ":" + "(" + tp[getRandom().Next(0, tp.Length - 1)] + ") " + GetRandomStr() + " ";
                }
                val = key + "{" + "\n";
                key = key + ";" + "\n";
                //生成方法体
                val += BuildOCFuncBodys();
                val += "}\n";
                rt.Add(new Entry<string, string>(key, val));
            }
            return rt;
        }
        public string BuildOCFuncBodys()
        {
            string body = string.Empty;
            int rd = getRandom().Next(1, 10);
            for (int i = 0; i < rd; i++)
            {
                body += string.Format("   //{0}\n", GetLongStr());
                int t = getRandom().Next(1, 6);
                string tp = GetRandomStr();
                string tp2 = GetRandomStr(); 
                switch (t)
                {

                    case 1:
                        body += string.Format("   NSString *{0}=@\"{1}\";\n", tp, GetLongStr());
                        body += string.Format("   \n", "");
                        body += string.Format("   NSLog(@\"%@\",[{0} uppercaseString]);\n", tp);
                        body += string.Format("   \n", "");
                        body += string.Format("   NSString *{0}=@\"{1}\";\n", tp2, GetLongStr());
                        body += string.Format("   \n", "");
                        body += string.Format("   BOOL {0} =[{1} isEqualToString:{2}];\n", GetRandomStr(), tp, tp2);
                        break;
                    case 2:
                        body += string.Format("   NSNumber *{0} = [[NSNumber alloc] initWithInt:{1}];\n", tp,getRandom().Next(0,9999));
                        body += string.Format("   NSNumber *{0} = [[NSNumber alloc] initWithInt:{1}];\n", tp2, getRandom().Next(0, 9999));
                        body += string.Format("   \n", "");
                        body += string.Format("   BOOL ret = [{0} isEqualToNumber:{1}];\n", tp,tp2);
                        body += string.Format("   NSLog(@\"%d\", [{0} intValue]);\n", tp);
                        break;
                    case 3:
                        body += string.Format("   NSDictionary *{0} = [NSDictionary dictionaryWithObjectsAndKeys:@\"{1}\",@\"{2}\",@\"{3}\",@\"{4}\",@\"{5}\",@\"{6}\",@\"{7}\", nil];\n", tp, GetRandomStr(), GetRandomStr(), GetRandomStr(), GetRandomStr(), GetRandomStr(), GetRandomStr(), GetRandomStr());
                        body += string.Format("   NSLog(@\"%@\", {0}.allKeys);\n", tp);
                        body += string.Format("   \n", "");
                        body += string.Format("   [{0} setObject:@\"{1}\" forKey:@\"{2}\"];\n", tp, GetRandomStr(),GetRandomStr());
                        body += string.Format("   NSLog(@\"%@\", {0}.allKeys);\n", tp);
                        break;
                    case 4:
                        body += string.Format("   NSArray *{0}=[NSArray arrayWithObjects:@\"{1}\",@\"{2}\",@\"{3}\",@\"{4}\",@\"{5}\",@\"{6}\", nil];\n", tp, GetRandomStr(), GetRandomStr(), GetRandomStr(), GetRandomStr(), GetRandomStr(), GetRandomStr());
                        body += string.Format("   int {0} = [array count];\n", tp2);
                        body += string.Format("   \n", "");
                        body += string.Format("   NSLog(@\"%i\", {0});\n",  tp2);
                        body += string.Format("   if ([{0} containsObject:@\"{1}\"]) {{\n", tp,GetRandomStr());
                        body += string.Format("      NSLog(@\"{0}\");\n",  GetRandomStr());
                        body += string.Format("   \n", "");
                        body += string.Format("   }}","");
                        break;
                    case 5:
                        body += string.Format("   int {0} = pow({1}, {2}) - 1;\n", GetRandomStr(), getRandom().Next(0, 9999), getRandom().Next(0, 9999));
                        body += string.Format("   \n", null);
                        body += string.Format("   long {0} = powl({1}, {2}) - 1;\n", GetRandomStr(), getRandom().Next(0, 9999), getRandom().Next(0, 9999));
                        body += string.Format("   printf(\"{0}\");\n", GetLongStr());
                        break;
                    default:
                        body += string.Format("   //{0}\n", GetLongStr());
                        break;
                }
            }
            body += "\n";
            return body;
        }
        public string GetRandomStr()
        {
            string[] str = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            int len = getRandom().Next(6, 20);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                sb.Append(str[getRandom().Next(0, str.Length - 1)]);
            }
            return sb.ToString();
        }
        public string GetLongStr()
        {
            int len = getRandom().Next(1, 3);
            string rt = string.Empty;
            for (int i = 0; i < len; i++)
            {
                rt += getNow() + "+" + getGuid();
            }
            return rt;
        }
        public string getNow()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmss");
        }
        public Random getRandom()
        {
            return new Random(Guid.NewGuid().GetHashCode());
        }
        public string getGuid()
        {
            return Guid.NewGuid().ToString();
        }
        public string ReadFile(string path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string rt = sr.ReadToEnd();
                sr.Close();
                return rt;
            }
        }

        public void CreateFile(string path, string content)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                //获得字节数组
                byte[] data = System.Text.Encoding.Default.GetBytes(content);
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();

            }
        }
        //获得字节数组

        public void WriteFile(string path, string content, bool append = false)
        {
            using (StreamWriter sw = new StreamWriter(path, append))
            {
                sw.Write(content);
                sw.Flush();
                sw.Close();
            }
        }
        public string getSuffix(string path)
        {
            if (path.IndexOf(".") < 0) return string.Empty;
            return path.Substring(path.LastIndexOf(".")).ToLower();
        }
    }
}

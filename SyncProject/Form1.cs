using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectTransferService;
using  Newtonsoft.Json.Linq;
using System.IO;
using System.Configuration;

namespace SyncProject
{
    public partial class Form1 : Form
    {
        static string resultpath = ConfigurationManager.AppSettings["ResultPath"].ToString();
        public Form1()
        {
            InitializeComponent();
            this.resultText.Multiline = true;//将Multiline属性设为true，实现显示多行
            this.resultText.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.btnExport.Enabled = false;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            // test callAPI = new test();
            //int result= callAPI.CreateProjectTask();
            //if (result > 0)
            //{
            //    MessageBox.Show("成功创建task:"+result.ToString());
            //}
            //else
            //    MessageBox.Show("调用API创建任务失败");
            this.btnStart.Enabled = false;
            StringBuilder reuslt = new StringBuilder();
           ProjectTransfer projectTransfer = new ProjectTransfer();
            reuslt.Append("开始分析需要处理的项目，该过程需要几分钟，请耐心等待.....");
            resultText.Text = reuslt.ToString(); 
           List<Project> tobeSyncedProjects=    projectTransfer.GetToBeSyncedProject();
            reuslt.Append($"\r\n经过分析，需要增加项目属性来维护项目的信息共有{ tobeSyncedProjects.Count.ToString()}个.");
            resultText.Text = reuslt.ToString();


            for (int i = 0; i < tobeSyncedProjects.Count; i++)
            {
                reuslt.Append($"\r\n开始处理第{i + 1}个项目: {tobeSyncedProjects[i].ProjectSpaceTitle}");
                resultText.Text = reuslt.ToString();
              JToken   apireturn = projectTransfer.GenerateProjectTask(tobeSyncedProjects[i]);
                // if (apireturn > 0)
                if (apireturn["Success"].ToString().ToLower() == "true")
                {
                    reuslt.Append($" .....结果： 成功生成项目属性条目{ apireturn["Data"]}");
                    resultText.Text = reuslt.ToString();

                }
                else
                {
                   // { "Success":false,"Error":{ "ErrorCode":-10,"ErrorMessage":"未将对象引用设置到对象的实例。"} }
                    reuslt.Append($".....结果：调用api调用失败，相应项目属性条目未创建,api返回：错误代码{apireturn["Error"]["ErrorCode"]}；信息为{apireturn["Error"]["ErrorMessage"]}");
                    resultText.Text = reuslt.ToString();
                }

            }

            //int apireturn = projectTransfer.GenerateProjectTask(tobeSyncedProjects.First());
            //if (apireturn > 0)
            //{
            //    MessageBox.Show("成功创建task:" + apireturn.ToString());
            //}
            //else
            //    MessageBox.Show("调用API创建任务失败");
            this.btnStart.Enabled = true;
            this.btnExport.Enabled = true;
        }

        /// <summary>
        /// 导出结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExport_Click(object sender, EventArgs e)
        {
            string fileName = resultpath + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";
            try
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
                streamWriter.Write(this.resultText.Text);

                streamWriter.Close();
                fileStream.Close();
                MessageBox.Show($"已导出结果到{fileName}");
               
            }
            catch (Exception ex)
            {

                MessageBox.Show($"导出出错：{ex.Message}");
            }
           
         
        }
    }
}

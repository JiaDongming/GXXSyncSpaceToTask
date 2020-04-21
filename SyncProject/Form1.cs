using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectTransferService;

namespace SyncProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.resultText.Multiline = true;//将Multiline属性设为true，实现显示多行
            this.resultText.ScrollBars = RichTextBoxScrollBars.Vertical;
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
            reuslt.Append($"\r\n 经过分析，需要增加项目属性来维护项目的信息共有{ tobeSyncedProjects.Count.ToString()}个.");
            resultText.Text = reuslt.ToString();


            for (int i = 0; i < tobeSyncedProjects.Count; i++)
            {
                reuslt.Append($"\r\n开始处理第{i + 1}个项目{tobeSyncedProjects[i].ProjectTtile}");
                resultText.Text = reuslt.ToString();
                int apireturn = projectTransfer.GenerateProjectTask(tobeSyncedProjects[i]);
                if (apireturn > 0)
                {
                    reuslt.Append($" .....; 成功生成项目属性条目{ apireturn.ToString()}");
                    resultText.Text = reuslt.ToString();

                }
                else
                {
                    reuslt.Append($".....; 调用api调用失败，相应项目属性条目未创建");
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
        }
    }
}

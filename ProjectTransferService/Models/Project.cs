using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ProjectTransferService
{
    /// <summary>
    /// 项目实体类
    /// </summary>
   public class Project
    {
        [DisplayName("项目编码")]
        public string ProjectCode { get; set; } //项目编码
        [DisplayName("项目名称")]
        public string ProjectTtile { get; set; }//项目名称
        [DisplayName("产品代码")]
        public string ProductCode { get; set; } //产品代码
        [DisplayName("产品名称")]
        public string ProductName { get; set; }//产品名称
        [DisplayName("所属单位")]
        public string BelongTo { get; set; }//所属单位
        [DisplayName("项目状态")]
        public string ProjectStatus { get; set; }//项目状态
        [DisplayName("计划开始时间")]
        public string PlanStartDate { get; set; }
        [DisplayName("计划完成时间")]
        public string PlanFinishDate { get; set; }
        [DisplayName("项目级别")]
        public string ProjectLevel { get; set; }
        [DisplayName("项目经理")]
        public string ProjectManager { get; set; } //项目经理

        public int ProjectManagerID { get; set; }//项目经理编号

        [DisplayName("开发方式")]
        public string DevWay { get; set; }
        [DisplayName("产品经理")]
        public string ProductManager { get; set; }//产品经理
        public int ProductManagerID { get; set; }//产品经理编号

        [DisplayName("合同额（万）")]
        public string ContractMoney { get; set; }
        [DisplayName("开发模式")]
        public string DevModle { get; set; }
        [DisplayName("开发负责人")]
        public string DevManager { get; set; }//开发负责人

        public int DevManagerID { get; set; }//开发负责人编号
        public List<int> DevManagerIDs { get; set; }
        [DisplayName("贡献值")]
        public string ScoreMoney { get; set; }//贡献值
        [DisplayName("项目类型")]
        public string ProjectType { get; set; }
        [DisplayName("测试负责人")]
        public string TestManager { get; set; }//测试负责人
        public int TestManagerID { get; set; }//测试负责人编号
        public List<int> TestManagerIDs { get; set; }

        [DisplayName("需求分析师")]
        public string RequirementAnalyst { get; set; }
        public List<int> RequirementAnalystIDs { get; set; }


        [DisplayName("开发工程师")]
        public string DevEngineer { get; set; }
        public List<int> DevEngineerIDs { get; set; }


        [DisplayName("测试工程师")]
        public string TestEngineer { get; set; }
        public List<int> TestEngineerIDs { get; set; }

        [DisplayName("质量管理员")]
        public string QualityManager { get; set; }
        public List<int> QualityManagerIDs { get; set; }

        [DisplayName("配置管理员")]
        public string ConfigManager { get; set; }
        public List<int> ConfigManagerIDs { get; set; }


        [DisplayName("转产项目")]
        public string TransfterProject { get; set; }//转产项目
        [DisplayName("重要程度")]
        public string Level { get; set; }//重要程度
        [DisplayName("批量发货")]
        public string PatchDeliver { get; set; }//批量发货
        [DisplayName("其他成员")]
        public string OtherMembers { get; set; } //项目下拉成员的文本(其他成员)
        public List<int> OtherMemberList { get; set; }


        public List<LogIn> SelectedProjecMembers;


        public string ProjectResourceText { get; set; } //项目资源（合并所有人员）
        public List<int> ProjectResourceIDs { get; set; }
        public List<LogIn> ProjectResourceMembers;


        [DisplayName("项目目标")]
        public string ProjectGole { get; set; }//项目目标
        [DisplayName("项目描述")]
        public string ProjectDesc { get; set; }//项目描述
        [DisplayName("项目团队成员文本框")]
        public string ProjectMembersText { get; set; }//项目团队成员文本框
        [DisplayName("商机编号")]
        public string ShangJiID { get; set; }//商机编号
        [DisplayName("结束时间")]
        public string EndDate { get; set; }//结束时间


        public int HiddenTaskID { get; set; }
        [DisplayName("当前Techexcel的项目编号")]
        public int? ProjectSpaceID { get; set; }

        //项目团队资源
        public List<int> SubProjectOwners { get; set; }

        public int? ProjectInfoTaskID { get; set; }
        public string ProjectSpaceTitle { get; set; }

        public Project()
        {
            SelectedProjecMembers = new List<LogIn>();
            OtherMemberList = new List<int>();
            DevManagerIDs = new List<int>();
            TestManagerIDs = new List<int>();
            RequirementAnalystIDs = new List<int>();
            DevEngineerIDs = new List<int>();
            TestEngineerIDs = new List<int>();
            QualityManagerIDs = new List<int>();
            ConfigManagerIDs = new List<int>();
            ProjectResourceIDs = new List<int>();
            ProjectResourceMembers = new List<LogIn>();
        }

        public Project(int spaceId):this()
        {
            this.ProjectSpaceID = spaceId;
            using (GXX_DS_0403Entities dbcontext = new GXX_DS_0403Entities())
            {
                //hiddent task
                var negativeID= -1500000001 - spaceId;
                this.HiddenTaskID = (from c in dbcontext.Bug where c.ProjectID == 502 && c.SubProjectID == negativeID select c.BugID).SingleOrDefault<int>();


                var info = from c in dbcontext.CustomerFieldTrackExt2 where c.ProjectID == 502 && c.IssueID == this.HiddenTaskID select c;
                var page1001 = (from c in info where c.PageNumber == 1001 select c).SingleOrDefault();
                var page1002 = (from c in info where c.PageNumber == 1002 select c).SingleOrDefault();
                var page1003 = (from c in info where c.PageNumber == 1003 select c).SingleOrDefault();
                var page1004 = (from c in info where c.PageNumber == 1004 select c).SingleOrDefault();
                var login = from l in dbcontext.LogIn select new {FullName= l.FName +" "+ l.LName , PersonID=l.PersonID};
                var bugselection = from c in dbcontext.BugSelectionInfo where c.ProjectID == 502 && c.BugID == this.HiddenTaskID select c;
                var resource = from m in dbcontext.SubProjectOwners where m.ProjectID == 502 && m.SubProjectID == spaceId select m;
                var accounttype = from a in dbcontext.AccountTypes where a.ProjectID == 502 select a;
                var projectMemberList = from a in dbcontext.ProjectMembers where a.ProjectID == 502 select a;
                if (page1001!=null)
                {
                    // 项目编码[CustomerFieldTrackExt2].[Custom_3],pageNumber = 1001
                    this.ProjectCode = page1001.Custom_3;

                    //名称[CustomerFieldTrackExt2].[Custom_2],pageNumber = 1001
                    this.ProjectTtile = page1001.Custom_2;

                    //项目状态[CustomerFieldTrackExt2].[Custom_9],pageNumber = 1001
                    this.ProjectStatus = page1001.Custom_9;

                    // 项目级别[CustomerFieldTrackExt2].[Custom_7],pageNumber = 1001
                    this.ProjectLevel = page1001.Custom_7;

                    //开发方式[CustomerFieldTrackExt2].[Custom_8],pageNumber = 1001
                    this.DevWay = page1001.Custom_8;

                    //项目类型[CustomerFieldTrackExt2].[Custom_6],pageNumber=1001
                    this.ProjectType = page1001.Custom_6;

                    //描述[CustomerFieldTrackExt2].[Custom_12],pageNumber = 1001
                    this.ProjectDesc = page1001.Custom_12;

                    //项目目标[CustomerFieldTrackExt2].[Custom_11],pageNumber = 1001
                    this.ProjectGole = page1001.Custom_11;

                    //商机编号[CustomerFieldTrackExt2].[Custom_4],pageNumber = 1001
                    this.ShangJiID = page1001.Custom_4;

                }
                if (page1002 != null)
                {
                    //产品名称[CustomerFieldTrackExt2].[Custom_4],pageNumber = 1002
                    this.ProductName = page1002.Custom_4;

                    //计划开始时间[CustomerFieldTrackExt2].[Custom_2],pageNumber = 1002
                    this.PlanStartDate = page1002.Custom_2;

                    //计划完成时间[CustomerFieldTrackExt2].[Custom_3],pageNumber = 1002
                    this.PlanFinishDate = page1002.Custom_3;

                    //项目经理[CustomerFieldTrackExt2].[Custom_1],pageNumber = 1002
                    this.ProjectManager = page1002.Custom_1;
                    this.ProjectManagerID = (from c in login where c.FullName == this.ProjectManager select c.PersonID).SingleOrDefault();

                    //产品经理(PM)[CustomerFieldTrackExt2].[Custom_5],pageNumber = 1002
                    this.ProductManager = page1002.Custom_5;
                    this.ProductManagerID = (from c in login where c.FullName == this.ProductManager select c.PersonID).SingleOrDefault();

                    //合同额（万）	[CustomerFieldTrackExt2].[Custom_9],pageNumber=1002
                    this.ContractMoney = page1002.Custom_9;
                }

                if (page1003 != null)
                {
                    //产品代码[CustomerFieldTrackExt2].[Custom_2],pageNumber = 1003
                    this.ProductCode = page1003.Custom_2;
                    // 所属单位[CustomerFieldTrackExt2].[Custom_9],pageNumber = 1003
                    this.BelongTo = page1003.Custom_9;

                    //开发模式[CustomerFieldTrackExt2].[Custom_4],pageNumber=1003
                    this.DevModle = page1003.Custom_4;
                    //贡献值（万）		[CustomerFieldTrackExt2].[Custom_7],pageNumber=1003
                    this.ScoreMoney = page1003.Custom_7;
                    //转产项目[CustomerFieldTrackExt2].[Custom_5],pageNumber=1003
                    this.TransfterProject = page1003.Custom_5;

                    //重要程度	[CustomerFieldTrackExt2].[Custom_3],pageNumber=1003
                    this.Level = page1003.Custom_3;

                    //批量发货[CustomerFieldTrackExt2].[Custom_6],pageNumber=1003
                    this.PatchDeliver = page1003.Custom_6;
                    //结束时间 [CustomerFieldTrackExt2].[Custom_8],pageNumber = 1003
                    this.EndDate = page1003.Custom_8;
                }

                if (page1004!=null)
                {
                    //开发负责人 (多选FieldiD=1000301)[CustomerFieldTrackExt2].[Custom_1],pageNumber=1004
                    this.DevManager = page1004.Custom_1;
                    this.DevManagerIDs = (from c in bugselection where c.FieldID == 1000301 select c.FieldSelectionID).ToList<int>();

                    //测试负责人(多选FieldID=1000302)	[CustomerFieldTrackExt2].[Custom_2],pageNumber=1004
                    this.TestManager = page1004.Custom_2;
                    this.TestManagerIDs = (from c in bugselection where c.FieldID == 1000302 select c.FieldSelectionID).ToList<int>();
                }
                //项目团队资源（subprojectowners）
                this.SubProjectOwners = (from c in resource select c.TeamMemberID).ToList<int>();
                Dictionary<int, string> dicAccountType = new Dictionary<int, string>();

                dicAccountType.Add((from c in accounttype where c.AccountTypeName == "测试工程师" select c.AccountTypeID).SingleOrDefault(), "测试工程师");
                dicAccountType.Add((from c in accounttype where c.AccountTypeName == "开发工程师" select c.AccountTypeID).SingleOrDefault(), "开发工程师");
                dicAccountType.Add((from c in accounttype where c.AccountTypeName == "配置管理员" select c.AccountTypeID).SingleOrDefault(), "配置管理员");
                dicAccountType.Add((from c in accounttype where c.AccountTypeName == "QA" select c.AccountTypeID).SingleOrDefault(), "质量管理员");
                dicAccountType.Add((from c in accounttype where c.AccountTypeName == "需求分析师" select c.AccountTypeID).SingleOrDefault(), "需求分析师");
                if (this.SubProjectOwners.Count > 0)
                {
                    foreach (var item in this.SubProjectOwners)
                    {
                        var accountID = (from c in projectMemberList where c.MemberID == item select c.AccountTypeID).SingleOrDefault();
                        if (dicAccountType.ContainsKey(Convert.ToInt32(accountID) ))
                        {
                            switch (dicAccountType[Convert.ToInt32(accountID)])
                            {
                                case "测试工程师":
                                    this.TestEngineerIDs.Add(Convert.ToInt32(accountID));
                                    break;
                                case "开发工程师":
                                    this.DevEngineerIDs.Add(Convert.ToInt32(accountID));
                                    break;
                                case "配置管理员":
                                    this.ConfigManagerIDs.Add(Convert.ToInt32(accountID));
                                    break;
                                case "质量管理员":
                                    this.QualityManagerIDs.Add(Convert.ToInt32(accountID));
                                    break;
                                case "需求分析师":
                                    this.RequirementAnalystIDs.Add(Convert.ToInt32(accountID));
                                    break;
                                default:
                                    this.OtherMemberList.Add(Convert.ToInt32(accountID));
                                    break;
                            }
                        }
                        else
                            this.OtherMemberList.Add(Convert.ToInt32(accountID));

                    }
                }


            }
        }

        public override string ToString()
        {
            return $"当前更新的项目信息是: 项目编码" + this.ProjectCode + "...其他信息待补充";
        }
    }
}

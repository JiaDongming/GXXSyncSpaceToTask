using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Untility;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace ProjectTransferService
{
    /// <summary>
    /// 项目转化
    /// </summary>
  public  class ProjectTransfer
    {
        List<Project> tobeSyncedProjects = null;
        static string ServerName = ConfigurationManager.AppSettings["ServerName"];
        static string UserToken = ConfigurationManager.AppSettings["UserToken"];
        public ProjectTransfer()
        {
            tobeSyncedProjects = new List<Project>();
        }

        #region 准备要同步的项目数据
        /// <summary>
        /// 获取所有待同步的项目
        /// </summary>
        /// <returns></returns>
        public List<Project> GetToBeSyncedProject()
        {
          
            using (GXX_DS_0403Entities dbcontext= new GXX_DS_0403Entities())
            {
                //1. 遍历SubProject表，获取type=98，并且名下没有任务类型（issuetypes）为"项目属性"的任务的所有space id
                //  select SubProjectType,*from subproject where projectid = 502 and SubProjectID in (
                // select ChildID from SubProjectTree where projectid = 502 and ParentID = 0 )
                //and SubProjectID not in (select SubProjectID from Bug where ProjectID = 502 and CrntBugTypeID = 227)

                if (tobeSyncedProjects.Count==0)
                {
                    //取根目录为0的所有子目录
                    var rootSpaces = from s in dbcontext.SubProjectTree where s.ProjectID == 502 && s.ParentID == 0 select s;

                    //获取"项目属性"的所有任务
                    var existSpaces = from b in dbcontext.Bug where b.ProjectID == 502 && b.CrntBugTypeID == 227 select b; //227 代表"项目属性"类型任务

                    //获取类型为98的所有目录
                    var allSpaces = from s in dbcontext.SubProject where s.ProjectID == 502 && s.SubProjectType == 98 select s;

                    //Get to be synced project space
                    var tobeSyncProject = from s in allSpaces
                                          where
                      (from c in rootSpaces select c.ChildID).Contains(s.SubProjectID) &&
                      !(from t in existSpaces select t.SubProjectID).Contains(s.SubProjectID)
                                          select s.SubProjectID;

                    
                    //2. 遍历这些space，获取它的hidden task， 以及项目相关其他信息，封装到泛型中，返回
                    foreach (var item in tobeSyncProject)
                    {
                        tobeSyncedProjects.Add(new Project(item));
                    }
                    // tobeSyncedProjects.Add(new Project(21917));
                }


            }

            return tobeSyncedProjects;
        }

        #endregion

        #region 项目数据同步（创建+更新）
        /// <summary>
        /// 在对应项目下创建出"项目属性" 任务，并把除成员之外的属性通过api调用生成到任务中
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public JToken GenerateProjectTask(Project project)
        {
            //根据project里的信息准备调用参数

            //调用HttpHelp类来调用api生成task

            //获取返回task id，，存入到项目实体类中
            //string token = "668CBA7B-5B82-456d-8409-6E257AEC4607";
            string ApiTaskUrl = "http://"+ ServerName + "/DevTrackApi/api/Task/Create?languageid=2&token=" + UserToken;


            var json_req = new
            {
                ProjectId = 502,
                TemplateId = 0,
                FieldValues = new List<APIParam> {
                new APIParam
                {
                    FieldId = 122,//subproject
                    Option = 0,
                    FieldValue = project.ProjectSpaceID==null?0:project.ProjectSpaceID
                },
                 new APIParam
                 {
                     FieldId = 101,//title
                     Option = 1,
                     FieldValue = project.ProjectSpaceTitle==null?string.Empty:project.ProjectSpaceTitle
                 },
                   new APIParam
                   {
                       FieldId = 601,//status
                       Option = 0,
                       FieldValue = 1174
                   },
                     new APIParam
                     {
                         FieldId = 108,//owner
                         Option = 0,
                         FieldValue = project.PrimaryOwnerID==null?0:project.PrimaryOwnerID
                     },
                     new APIParam
                     {
                         FieldId = 103,//type
                         Option = 0,
                         FieldValue = 227
                     },
                       new APIParam
                     {
                         FieldId = 12208,//项目编码
                         Option = 1,
                         FieldValue = project.ProjectCode==null?string.Empty:project.ProjectCode
                     },
                          new APIParam
                     {
                         FieldId = 12302,//产品名称
                         Option = 1,
                         FieldValue = project.ProductName==null?string.Empty:project.ProductName
                     },
                    new APIParam
                     {
                         FieldId = 12301,//产品代码
                         Option = 1,
                         FieldValue = project.ProductCode==null?string.Empty:project.ProductCode
                     },
                      new APIParam
                     {
                         FieldId = 12304,//所属单位（下拉）
                         Option = 1,
                         FieldValue = project.BelongTo==null?string.Empty:project.BelongTo
                     },
                         new APIParam
                     {
                         FieldId = 12506,//商机编号
                         Option = 1,
                         FieldValue = project.ShangJiID==null?string.Empty:project.ShangJiID
                     },
                       new APIParam
                     {
                         FieldId = 12405,//计划开始
                         Option = 1,
                         FieldValue = project.PlanStartDate==null?string.Empty:project.PlanStartDate.Substring(0,10)
                     },
                         new APIParam
                     {
                         FieldId = 12406,//计划结束
                         Option = 1,
                         FieldValue = project.PlanFinishDate==null?string.Empty:project.PlanFinishDate.Substring(0,10)
                     },
                          new APIParam
                     {
                         FieldId = 12306,//项目级别（下拉）
                         Option = 1,
                         FieldValue = project.ProjectLevel==null?string.Empty:project.ProjectLevel
                     },
                          new APIParam
                     {
                         FieldId = 12403,//项目状态（下拉）
                         Option = 1,
                         FieldValue = project.ProjectStatus==null?string.Empty:project.ProjectStatus
                     },
                          new APIParam
                     {
                         FieldId = 12402,//开发方式（下拉）
                         Option = 1,
                         FieldValue = project.DevWay==null?string.Empty:project.DevWay
                     },
                          new APIParam
                     {
                         FieldId = 12305,//项目类型（下拉）
                         Option = 1,
                         FieldValue = project.ProjectType==null?string.Empty:project.ProjectType
                     },
                          new APIParam
                     {
                         FieldId = 12401,//预估贡献值（万）
                         Option = 1,
                         FieldValue = project.ScoreMoney==null?string.Empty:project.ScoreMoney
                     },
                          new APIParam
                     {
                         FieldId = 12502,//开发模式（下拉）
                         Option = 1,
                         FieldValue =project.DevModle==null?string.Empty:project.DevModle
                     },
                          new APIParam
                     {
                         FieldId = 12501,//重要程度（下拉）
                         Option = 1,
                         FieldValue = project.Level==null?string.Empty:project.Level
                     },
                          new APIParam
                     {
                         FieldId = 12504,//合同额（万）
                         Option = 1,
                         FieldValue = project.ContractMoney==null?string.Empty:project.ContractMoney
                     },
                          new APIParam
                     {
                         FieldId = 12308,//批量发货（下拉）
                         Option = 1,
                         FieldValue = project.PatchDeliver==null?string.Empty:project.PatchDeliver
                     },
                          new APIParam
                     {
                         FieldId = 12307,//转产项目（下拉）
                         Option = 1,
                         FieldValue = project.TransfterProject==null?string.Empty:project.TransfterProject
                     },
                          new APIParam
                     {
                         FieldId = 12507,//结束时间
                         Option = 1,
                         FieldValue = project.EndDate==null?string.Empty:project.EndDate.Substring(0,10)
                     },
                          new APIParam
                     {
                         FieldId = 11910,//项目目标
                         Option = 1,
                         FieldValue = project.ProjectGole==null?string.Empty:project.ProjectGole
                     },
                          new APIParam
                     {
                         FieldId = 102,//描述
                         Option = 1,
                         FieldValue =project.ProjectDesc==null?string.Empty:project.ProjectDesc
                     },
                           new APIParam
                     {
                         FieldId = 12111,//描述
                         Option = 1,
                         FieldValue =project.ProjectResourceText==null?string.Empty:project.ProjectResourceText
                     },
              },
            };
            #region 参数异常未空时判断
            //for (int i = 0; i < json_req.FieldValues.Count ; i++)
            //{
               
            //    if (json_req.FieldValues[i].FieldValue == null || json_req.FieldValues[i].FieldValue.ToString() == "")
            //    {
            //        json_req.FieldValues.Remove(json_req.FieldValues[i]);
            //    }
            //}

            List<APIParam> newjson_req = (from c in json_req.FieldValues where c.FieldValue != "" || c.FieldValue.ToString() != string.Empty select c).ToList<APIParam>();
            var json_req2 = new
            {
                ProjectId = 502,
                TemplateId = 0,
                FieldValues = newjson_req
            };

            //if (project.EndDate == null)
            //{
            //    json_req.FieldValues.Remove(new
            //    {
            //        FieldId = 12507,//结束时间
            //        Option = 1,
            //        FieldValue = project.EndDate == null ? "" : project.EndDate.Substring(0, 10)
            //    });
            //}

                //if (project.PlanStartDate == null)
                //{
                //    json_req.FieldValues.Remove(new
                //    {
                //        FieldId = 12405,//计划开始
                //        Option = 1,
                //        FieldValue = project.PlanStartDate == null ? "" : project.PlanStartDate.Substring(0, 10)
                //    });
                //}

                //if (project.PlanFinishDate == null)
                //{
                //    json_req.FieldValues.Remove(new
                //    {
                //        FieldId = 12406,//计划结束
                //        Option = 1,
                //        FieldValue = project.PlanFinishDate == null ? "" : project.PlanFinishDate.Substring(0, 10)
                //    });
                //}

                //if (project.ScoreMoney == null)
                //{
                //    json_req.FieldValues.Remove(new
                //    {
                //        FieldId = 12401,//预估贡献值（万）
                //        Option = 1,
                //        FieldValue = project.ScoreMoney
                //    });
                //}

                //if (project.ContractMoney == null)
                //{
                //    json_req.FieldValues.Remove(new
                //    {
                //        FieldId = 12504,//合同额（万）
                //        Option = 1,
                //        FieldValue = project.ContractMoney
                //    });
                //}

                //if (project.ShangJiID == null)
                //{
                //    json_req.FieldValues.Remove(new
                //         {
                //             FieldId = 12506,//商机编号
                //             Option = 1,
                //             FieldValue = project.ShangJiID == null ? "" : project.ShangJiID
                //         });
                //}

                //if (project.ProductCode == null)
                //{
                //    json_req.FieldValues.Remove(new
                //    {
                //        FieldId = 12301,//产品代码
                //        Option = 1,
                //        FieldValue = project.ProductCode
                //    });
                //}
                #endregion

             string jsonRequest = JsonConvert.SerializeObject(json_req2);//将对象转换为json

            string result = HttpHelp.HttpPost(ApiTaskUrl, jsonRequest);
            Newtonsoft.Json.Linq.JToken json = Newtonsoft.Json.Linq.JToken.Parse(result);

            if (json["Success"].ToString().ToLower() == "true")
            {
                project.ProjectInfoTaskID = Convert.ToInt32(json["Data"]);
                UpdateMembers(project);
                //tobeSyncedProjects.Remove(project);
                //return Convert.ToInt32(json["Data"]);
                return json;
            }

            //else return 0;
            else return json;

        }
         
        /// <summary>
        /// 更新当前项目下的"项目属性"中的任务里的成员下拉字段值
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public void UpdateMembers(Project project)
        {
            //根据project获取相应的"项目属性"任务编号

            //把项目的成员分类，按不同的账户类型，插入到不同的人员下拉字段中
            using (GXX_DS_0403Entities dbcontext= new GXX_DS_0403Entities())
            {
                //项目经理 id=2
                dbcontext.Database.ExecuteSqlCommand($"insert into BugSelectionInfo values(502,{project.ProjectInfoTaskID},2,{project.ProjectManagerID})");
                //产品经理 id=1603
                dbcontext.Database.ExecuteSqlCommand($"insert into BugSelectionInfo values(502,{project.ProjectInfoTaskID},1603,{project.ProductManagerID})");
                //开发负责人 id=12407
                PatchAddUser(12407, project, project.DevManagerIDs);
                //测试负责人 id=12408
                PatchAddUser(12408, project, project.TestManagerIDs);
                //需求分析师 id=12508
                PatchAddUser(12508, project, project.RequirementAnalystIDs);
                //开发工程师 id=12601
                PatchAddUser(12601, project, project.DevEngineerIDs);
                //测试工程师 id=12602
                PatchAddUser(12602, project, project.TestEngineerIDs);
                //质量管理员 id=12603
                PatchAddUser(12603, project, project.QualityManagerIDs);
                //配置管理员 id=12604
                PatchAddUser(12604, project, project.ConfigManagerIDs);
                //其他成员  id=12505
                PatchAddUser(12505, project, project.OtherMemberList);

            }

        
        }

        private void PatchAddUser(int FieldID, Project project, List<int> members)
        {
            using (GXX_DS_0403Entities dbcontext= new GXX_DS_0403Entities())
            {
                string sqlstr = string.Empty;
                foreach (var item in members)
                {
                    if (dbcontext.BugSelectionInfo.Where(c=>c.ProjectID==502&&c.BugID== project.ProjectInfoTaskID&&c.FieldID== FieldID&&c.FieldSelectionID==item).Count()==0)
                    {
                        sqlstr = "insert into BugSelectionInfo values(@ProjectID,@IssueID,@FieldID,@MemberID)";
                        SqlParameter[] sqlparams = new SqlParameter[]
                        {
                        new SqlParameter("@ProjectID",502),
                        new SqlParameter("@IssueID",project.ProjectInfoTaskID),
                        new SqlParameter("@FieldID",FieldID),
                        new SqlParameter("@MemberID",item),

                        };

                        dbcontext.Database.ExecuteSqlCommand(sqlstr, sqlparams);
                    }
                
                }
            }
          
        }


        #endregion

        #region 根据项目找到hideen task id

        /// <summary>
        /// 获取项目对应的hidden task
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private Project GetHiddenTask(Project project)
        {
            return null;
        }

        #endregion

        #region 把项目资源转换到具体人员字段中
        private Project SubProjectOwnerTransfer(Project project)
        {
            return null;
        }

        
        #endregion
    }
}

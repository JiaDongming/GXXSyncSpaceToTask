using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Untility;

namespace ProjectTransferService
{
    /// <summary>
    /// 项目转化
    /// </summary>
  public  class ProjectTransfer
    {

        #region 准备要同步的项目数据
        /// <summary>
        /// 获取所有待同步的项目
        /// </summary>
        /// <returns></returns>
        public List<Project> GetToBeSyncedProject()
        {
            List<Project> tobeSyncedProjects = new List<Project>();
            using (GXX_DS_0403Entities dbcontext= new GXX_DS_0403Entities())
            {
                //1. 遍历SubProject表，获取type=98，并且名下没有任务类型（issuetypes）为"项目属性"的任务的所有space id
                //  select SubProjectType,*from subproject where projectid = 502 and SubProjectID in (
                // select ChildID from SubProjectTree where projectid = 502 and ParentID = 0 )
                //and SubProjectID not in (select SubProjectID from Bug where ProjectID = 502 and CrntBugTypeID = 227)

                //取根目录为0的所有子目录
                var rootSpaces = from s in dbcontext.SubProjectTree where s.ProjectID == 502 && s.ParentID == 0 select s;

                //获取"项目属性"的所有任务
                var existSpaces = from b in dbcontext.Bug where b.ProjectID == 502 && b.CrntBugTypeID == 227 select b; //227 代表"项目属性"类型任务

                //获取类型为98的所有目录
                var allSpaces = from s in dbcontext.SubProject where s.ProjectID == 502 && s.SubProjectType == 98 select s;

                //Get to be synced project space
                var tobeSyncProject = from s in allSpaces where 
                                      (from c in rootSpaces select c.ChildID).Contains(s.SubProjectID) && 
                                      !(from t in existSpaces select t.SubProjectID).Contains(s.SubProjectID)
                                      select s;

                //2. 遍历这些space，获取它的hidden task， 以及项目相关其他信息，封装到泛型中，返回
                foreach (var item in tobeSyncProject)
                {
                    tobeSyncedProjects.Add(new Project(item.SubProjectID));
                }
               // tobeSyncedProjects.Add(new Project(21917));

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
        public int GenerateProjectTask(Project project)
        {
            //根据project里的信息准备调用参数

            //调用HttpHelp类来调用api生成task

            //获取返回task id，，存入到项目实体类中
            string token = "668CBA7B-5B82-456d-8409-6E257AEC4607";
            string ApiTaskUrl = "http://localhost/DevTrackApi/api/Task/Create?languageid=2&token=" + token;


            var json_req = new
            {
                ProjectId = 502,
                TemplateId = 0,
                FieldValues = new ArrayList  {
                new
                {
                    FieldId = 122,
                    Option = 0,
                    FieldValue = 40274
                },
                 new
                 {
                     FieldId = 101,
                     Option = 1,
                     FieldValue = "V3"
                 },
                   new
                   {
                       FieldId = 601,
                       Option = 0,
                       FieldValue = 1174
                   },
                     new
                     {
                         FieldId = 108,
                         Option = 0,
                         FieldValue = 611
                     },
                     new
                     {
                         FieldId = 103,
                         Option = 0,
                         FieldValue = 227
                     },
              },
            };
            string jsonRequest = JsonConvert.SerializeObject(json_req);//将对象转换为json

            string result = HttpHelp.HttpPost(ApiTaskUrl, jsonRequest);
            Newtonsoft.Json.Linq.JToken json = Newtonsoft.Json.Linq.JToken.Parse(result);

            if (json["Success"].ToString() == "true")
            {
                project.ProjectInfoTaskID = Convert.ToInt32(json["Data"]);
                return Convert.ToInt32(json["Data"]);
            }
             
            else return 0;

        }
         
        /// <summary>
        /// 更新当前项目下的"项目属性"中的任务里的成员下拉字段值
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public int UpdateMembers(Project project)
        {
            //根据project获取相应的"项目属性"任务编号

            //把项目的成员分类，按不同的账户类型，插入到不同的人员下拉字段中

            return 1;
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

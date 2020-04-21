using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Untility;

namespace ProjectTransferService
{
    public class test
    {
        public int CreateProjectTask()
        {
            string token = "668CBA7B-5B82-456d-8409-6E257AEC4607";
            string ApiTaskUrl = "http://localhost/DevTrackApi/api/Task/Create?languageid=2&token=" + token;


            var json_req = new
            {
                ProjectId = 502,
                TemplateId = 0,
                FieldValues = new ArrayList  {
                new
                {
                    FieldId = 122,//subproject
                    Option = 0,
                    FieldValue = 40274
                },
                 new
                 {
                     FieldId = 101,//title
                     Option = 1,
                     FieldValue = "V3"
                 },
                   new
                   {
                       FieldId = 601,//status
                       Option = 0,
                       FieldValue = 1174
                   },
                     new
                     {
                         FieldId = 108,//owner
                         Option = 0,
                         FieldValue = 1
                     },
                     new
                     {
                         FieldId = 103,//type
                         Option = 0,
                         FieldValue = 227
                     },
                       new
                     {
                         FieldId = 12208,//项目编码
                         Option = 1,
                         FieldValue = "项目编码"
                     },
                          new
                     {
                         FieldId = 12302,//产品名称
                         Option = 1,
                         FieldValue = "产品名称"
                     },
                    new
                     {
                         FieldId = 12301,//产品代码
                         Option = 1,
                         FieldValue = "产品代码"
                     },
                      new
                     {
                         FieldId = 12304,//所属单位（下拉）
                         Option = 1,
                         FieldValue = "AR产品线"
                     },
                         new
                     {
                         FieldId = 12506,//商机编号
                         Option = 1,
                         FieldValue = "商机编号"
                     },
                       new
                     {
                         FieldId = 12405,//计划开始
                         Option = 1,
                         FieldValue = "2020-03-05"
                     },
                         new
                     {
                         FieldId = 12406,//计划结束
                         Option = 1,
                         FieldValue = "2020-04-30"
                     },
                          new
                     {
                         FieldId = 12306,//项目级别（下拉）
                         Option = 1,
                         FieldValue = "A"
                     },
                          new
                     {
                         FieldId = 12403,//项目状态（下拉）
                         Option = 1,
                         FieldValue = "未启动"
                     },
                          new
                     {
                         FieldId = 12402,//开发方式（下拉）
                         Option = 1,
                         FieldValue = "敏捷"
                     },
                          new
                     {
                         FieldId = 12305,//项目类型（下拉）
                         Option = 1,
                         FieldValue = "产品项目"
                     },
                          new
                     {
                         FieldId = 12401,//预估贡献值（万）
                         Option = 1,
                         FieldValue = "35"
                     },
                          new
                     {
                         FieldId = 12502,//开发模式（下拉）
                         Option = 1,
                         FieldValue = "独立开发"
                     },
                          new
                     {
                         FieldId = 12501,//重要程度（下拉）
                         Option = 1,
                         FieldValue = "重点项目"
                     },
                          new
                     {
                         FieldId = 12504,//合同额（万）
                         Option = 1,
                         FieldValue = "55"
                     },
                          new
                     {
                         FieldId = 12308,//批量发货（下拉）
                         Option = 1,
                         FieldValue = "否"
                     },
                          new
                     {
                         FieldId = 12307,//转产项目（下拉）
                         Option = 1,
                         FieldValue = "否"
                     },
                          new
                     {
                         FieldId = 12507,//结束时间
                         Option = 1,
                         FieldValue = "2020-04-09"
                     },
                          new
                     {
                         FieldId = 11910,//项目目标
                         Option = 1,
                         FieldValue = "项目目标"
                     },
                          new
                     {
                         FieldId = 102,//描述
                         Option = 1,
                         FieldValue = "描述"
                     },
              },
            };
            string jsonRequest = JsonConvert.SerializeObject(json_req);//将对象转换为json

            string result = HttpHelp.HttpPost(ApiTaskUrl, jsonRequest);
            Newtonsoft.Json.Linq.JToken json = Newtonsoft.Json.Linq.JToken.Parse(result);

            if (json["Success"].ToString().ToLower() == "true") return Convert.ToInt32(json["Data"]);
            else return 0;
        }

        //第三步：根据AgentID，UserID，定义一个SendMessage方法【这里可以定义一个实体类，定义发送的内容】
        /// <summary>
        /// 发送消息到钉钉
        /// </summary>
        /// <param name="tobeSent"></param>
        //public void SendMessage(List<Message> tobeSent)
        //{
        //    string MessageUrl = "https://oapi.dingtalk.com/message/send?access_token=" + GetAccessToken();
        //    foreach (Message item in tobeSent)
        //    {
        //        if (item.UserID is null || item.UserID.Length == 0)//如果用户匹配的钉钉账号不存在则
        //        {
        //            new DevSuite().Update(new UpdateTag
        //            {
        //                SMSId = item.SMSID,
        //                SystemFlag = IntegratedSystem.DingDing,
        //                MyFlag = UpdateFlag.NoMapUser
        //            });
        //        }
        //        else
        //        {
        //            var json_req = new
        //            {
        //                touser = item.UserID,  //接受推送userid，不同用户用|分割
        //                toparty = "",   //接受推送部门id
        //                agentid = item.DingAgentID,
        //                msgtype = "text", //推送类型
        //                text = new
        //                {
        //                    content = item.SMSContent
        //                }
        //            };
        //            string jsonRequest = JsonConvert.SerializeObject(json_req);//将对象转换为json

        //            string result = HttpHelp.HttpPost(MessageUrl, jsonRequest);
        //            Newtonsoft.Json.Linq.JToken json = Newtonsoft.Json.Linq.JToken.Parse(result);

        //            //判断执行的返回结果是否发送成功
        //            // if ((json["errmsg"].ToString() == "ok" || json["errcode"].ToString() == "0") && json["forbiddenUserId"].ToString() != item.UserID)//api返回时ok并且当前用户没有被限制
        //            if ((json["errmsg"].ToString() == "ok" || json["errcode"].ToString() == "0"))//api返回时ok并且当前用户没有被限制
        //            {
        //                if (json["forbiddenUserId"] == null)
        //                {
        //                    //更新已发送成功的消息为已发送
        //                    new DevSuite().Update(new UpdateTag
        //                    {
        //                        SMSId = item.SMSID,
        //                        SystemFlag = IntegratedSystem.DingDing,
        //                        MyFlag = UpdateFlag.ok
        //                    });

        //                }



        //        }


        //    }

        //}
    }
}

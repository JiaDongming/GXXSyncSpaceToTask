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
            string ApiTaskUrl = "http://localhost/DevTrackApi/api/Task/Create?languageid=2&token="+ token;
           
           
            var json_req = new
            {
                ProjectId = 502,
                TemplateId=0,
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

            if (json["Success"].ToString()=="true") return Convert.ToInt32(json["Data"]);
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

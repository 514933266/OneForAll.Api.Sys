using Microsoft.IdentityModel.Tokens;
using OneForAll.Core;
using OneForAll.Core.Extension;
using Sys.Domain.Enums;
using Sys.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.AggregateRoots
{
    /// <summary>
    /// 微信公众号回复设置
    /// </summary>
    public class SysWxgzhReplySetting
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        [Required]
        [StringLength(200)]
        public string AppId { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [Required]
        public SysWxgzhMsgTypeEnum MsgType { get; set; }

        /// <summary>
        /// 消息类型的key值（用于菜单点击事件）
        /// </summary>
        [Required]
        [StringLength(20)]
        public string MsgKey { get; set; } = "";

        /// <summary>
        /// 消息类型
        /// </summary>
        [Required]
        public SysWxgzhReplyTypeEnum ReplyType { get; set; }

        /// <summary>
        /// 内容的xml模板
        /// </summary>
        [Required]
        public string XmlContent { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public string ContentJson { get; set; }

        /// <summary>
        /// 设置Xml模板内容
        /// </summary>
        public void SetXmlContent()
        {
            switch (ReplyType)
            {
                case SysWxgzhReplyTypeEnum.Text: XmlContent = GetTextXmlContent(); break;
                case SysWxgzhReplyTypeEnum.Image: XmlContent = GetImageXmlContent(); break;
                case SysWxgzhReplyTypeEnum.Voice: XmlContent = GetVoiceXmlContent(); break;
                case SysWxgzhReplyTypeEnum.Video: XmlContent = GetVideoXmlContent(); break;
                case SysWxgzhReplyTypeEnum.Music: XmlContent = GetMusicXmlContent(); break;
                case SysWxgzhReplyTypeEnum.Article: XmlContent = GetArticleXmlContent(); break;
                case SysWxgzhReplyTypeEnum.CustomerService: XmlContent = GetCustomerServiceJsonContent(); break;
            }
        }

        /// <summary>
        /// 文本消息模板
        /// </summary>
        /// <returns></returns>
        public string GetTextXmlContent()
        {
            return @"<xml>
                     <ToUserName><![CDATA[toUser]]></ToUserName>
                     <FromUserName><![CDATA[fromUser]]></FromUserName>
                     <CreateTime>[createtime]</CreateTime>
                     <MsgType><![CDATA[text]]></MsgType>
                     <Content><![CDATA[content]]></Content>
                     </xml>";
        }

        /// <summary>
        /// 图片消息模板
        /// </summary>
        /// <returns></returns>
        public string GetImageXmlContent()
        {
            return @"<xml>
                      <ToUserName><![CDATA[toUser]]></ToUserName>
                      <FromUserName><![CDATA[fromUser]]></FromUserName>
                      <CreateTime>[createtime]</CreateTime>
                      <MsgType><![CDATA[image]]></MsgType>
                      <Image>
                        <MediaId><![CDATA[media_id]]></MediaId>
                      </Image>
                    </xml>";
        }

        /// <summary>
        /// 语音消息模板
        /// </summary>
        /// <returns></returns>
        public string GetVoiceXmlContent()
        {
            return @"<xml>
                      <ToUserName><![CDATA[toUser]]></ToUserName>
                      <FromUserName><![CDATA[fromUser]]></FromUserName>
                      <CreateTime>[createtime]</CreateTime>
                      <MsgType><![CDATA[voice]]></MsgType>
                      <Voice>
                        <MediaId><![CDATA[media_id]]></MediaId>
                      </Voice>
                    </xml>";
        }

        /// <summary>
        /// 视频消息模板
        /// </summary>
        /// <returns></returns>
        public string GetVideoXmlContent()
        {
            return @"<xml>
                      <ToUserName><![CDATA[toUser]]></ToUserName>
                      <FromUserName><![CDATA[fromUser]]></FromUserName>
                      <CreateTime>[createtime]</CreateTime>
                      <MsgType><![CDATA[video]]></MsgType>
                      <Video>
                        <MediaId><![CDATA[media_id]]></MediaId>
                        <Title><![CDATA[title]]></Title>
                        <Description><![CDATA[description]]></Description>
                      </Video>
                    </xml>";
        }

        /// <summary>
        /// 音乐消息模板
        /// </summary>
        /// <returns></returns>
        public string GetMusicXmlContent()
        {
            return @"<xml>
                      <ToUserName><![CDATA[toUser]]></ToUserName>
                      <FromUserName><![CDATA[fromUser]]></FromUserName>
                      <CreateTime>[createtime]</CreateTime>
                      <MsgType><![CDATA[music]]></MsgType>
                      <Music>
                        <Title><![CDATA[title]]></Title>
                        <Description><![CDATA[description]]></Description>
                        <MusicUrl><![CDATA[music_url]]></MusicUrl>
                        <HQMusicUrl><![CDATA[hq_music_url]]></HQMusicUrl>
                        <ThumbMediaId><![CDATA[media_id]]></ThumbMediaId>
                      </Music>
                    </xml>";
        }

        /// <summary>
        /// 图文消息模板
        /// </summary>
        /// <returns></returns>
        public string GetArticleXmlContent()
        {
            return @"<xml>
                      <ToUserName><![CDATA[toUser]]></ToUserName>
                      <FromUserName><![CDATA[fromUser]]></FromUserName>
                      <CreateTime>[createtime]</CreateTime>
                      <MsgType><![CDATA[news]]></MsgType>
                      <ArticleCount>1</ArticleCount>
                      <Articles>
                        <item>
                          <Title><![CDATA[title]]></Title>
                          <Description><![CDATA[description]]></Description>
                          <PicUrl><![CDATA[picurl]]></PicUrl>
                          <Url><![CDATA[url]]></Url>
                        </item>
                      </Articles>
                    </xml>";
        }

        /// <summary>
        /// 客服消息
        /// </summary>
        /// <returns></returns>
        public string GetCustomerServiceJsonContent()
        {
            return "[{\"touser\": \"[toUser]\",\"msgtype\": \"text\",\"text\": {\"content\": \"[content]\"}}]";
        }

        /// <summary>
        /// 获取Xml回复内容
        /// </summary>
        /// <param name="toUser"></param>
        /// <param name="fromUser"></param>
        /// <returns></returns>
        public string GetXmlReplyContent(string toUser, string fromUser)
        {
            var result = string.Empty;
            if (!ContentJson.IsNullOrEmpty() && !XmlContent.IsNullOrEmpty())
            {
                var xmlContent = XmlContent;
                var contents = ContentJson.FromJson<IEnumerable<SysWxgzhReplySettingContentVo>>();
                contents.ForEach(e =>
                {
                    xmlContent = xmlContent.Replace($"[{e.Name}]", $"[{e.Value}]");
                });
                xmlContent = xmlContent.Replace($"[createtime]", $"[{DateTime.Now.ToString("yyyyMMddHHmmss")}]");
                xmlContent = xmlContent.Replace($"[toUser]", $"[{toUser}]");
                xmlContent = xmlContent.Replace($"[fromUser]", $"[{fromUser}]");
                result = xmlContent;
            }
            return result;
        }

        /// <summary>
        /// 获取Xml回复内容
        /// </summary>
        /// <param name="toUser"></param>
        /// <param name="fromUser"></param>
        /// <returns></returns>
        public List<T> GetJsonReplyContent<T>(string toUser, string fromUser)
        {
            // 使用xml对内容做替换
            var result = string.Empty;
            if (!ContentJson.IsNullOrEmpty() && !XmlContent.IsNullOrEmpty())
            {
                var xmlContent = XmlContent;
                var contents = ContentJson.FromJson<IEnumerable<SysWxgzhReplySettingContentVo>>();
                contents.ForEach(e =>
                {
                    xmlContent = xmlContent.Replace($"[{e.Name}]", $"{e.Value}");
                });
                xmlContent = xmlContent.Replace($"[createtime]", $"{DateTime.Now.ToString("yyyyMMddHHmmss")}");
                xmlContent = xmlContent.Replace($"[toUser]", $"{toUser}");
                xmlContent = xmlContent.Replace($"[fromUser]", $"{fromUser}");
                result = xmlContent;
            }
            return result.FromJson<List<T>>();
        }

        /// <summary>
        /// 替换MsgType = Text 的响应的内容值
        /// </summary>
        /// <param name="value">回复内容</param>
        /// <returns></returns>
        public void ReplaceReplyTextContentJsonValue(string value)
        {
            ReplaceReplyContentJsonValue("content", value);
        }

        /// <summary>
        /// 替换MsgType = Text 的响应的内容值
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public void ReplaceReplyContentJsonValue(string name, string value)
        {
            var contents = ContentJson.FromJson<IEnumerable<SysWxgzhReplySettingContentVo>>();
            contents.ForEach(e =>
            {
                if (e.Name == name)
                {
                    e.Value = value;
                }
            });
            ContentJson = contents.ToJson();
        }
    }
}

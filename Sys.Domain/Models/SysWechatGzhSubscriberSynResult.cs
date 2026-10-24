namespace Sys.Domain.Models
{
    /// <summary>
    /// 微信公众号关注用户关联结果
    /// </summary>
    public class SysWechatGzhSubscriberSynResult
    {
        /// <summary>
        /// 待关联的关注用户总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 关联成功的数量
        /// </summary>
        public int UpdatedCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Public.Models
{
    /// <summary>
    /// 统一的权限
    /// </summary>
    public static class ConstPermission
    {
        /// <summary>
        /// 查看页面
        /// </summary>
        public const string VIEW = "EnterView";

        /// <summary>
        /// 添加
        /// </summary>
        public const string ADD = "Add";

        /// <summary>
        /// 修改
        /// </summary>
        public const string UPDATE = "Update";

        /// <summary>
        /// 删除
        /// </summary>
        public const string DELETE = "Delete";
    }
}

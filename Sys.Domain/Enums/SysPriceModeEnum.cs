using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Enums
{
    /// <summary>
    /// 价格模式
    /// </summary>
    public enum SysPriceModeEnum
    {
        /// <summary>
        /// 不收费
        /// </summary>
        None = -1,

        /// <summary>
        /// 一次性付费
        /// </summary>
        Once = 0,

        /// <summary>
        /// 按日付费
        /// </summary>
        Day = 1,

        /// <summary>
        /// 按月付费
        /// </summary>
        Month = 2,

        /// <summary>
        /// 按年付费
        /// </summary>
        Year = 3,

        /// <summary>
        /// 按量收费
        /// </summary>
        Quantity = 4
    }
}

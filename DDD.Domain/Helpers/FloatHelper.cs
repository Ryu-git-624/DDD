using System;

namespace DDD.Domain.Helpers
{///
    public static class FloatHelper
    {
        /// <summary>
        /// 小数点以下2桁で指定桁数で四捨五入します
        /// </summary>
        /// <param name="value"></param>
        /// <param name="descimalPoint"></param>
        /// <returns></returns>
        public static string RoundString(this float value, int descimalPoint)
        {
            var temp = Convert.ToSingle(Math.Round(value, descimalPoint));
            return temp.ToString("F" + descimalPoint);
        }
    }
}

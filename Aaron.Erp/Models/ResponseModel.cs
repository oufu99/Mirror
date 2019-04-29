using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ResponseModel
    {
        /// <summary>
        /// 返回参数  保持跟html状态码相同
        /// </summary>
        public int Code { get; set; }

        public string Msg { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        [HttpGet(nameof(Send_Test))]
        public string Send_Test()
        {
            return ($"通过QQ邮件接口向");
            Console.WriteLine($"通过QQ邮件接口向");
        }

        [HttpPost(nameof(Send_Test2))]
        public string Send_Test2()
        {
            return ($"通过QQ邮件接口向");
            Console.WriteLine($"通过QQ邮件接口向");
        }
        [HttpPost(nameof(Send_QQ))]
        public string Send_QQ(SendRequest model)
        {
            return ($"通过QQ邮件接口向{model.Email}发送邮件");
        }

        [HttpPost(nameof(Send_163))]
        public void Send_163(SendEmailRequest model)
        {
            Console.WriteLine($"通过网易邮件接口向{model.Email}发送邮件，标题{model.Title}，内容：{model.Body}");
        }

        [HttpPost(nameof(Send_Sohu))]
        public void Send_Sohu(SendEmailRequest model)
        {
            Console.WriteLine($"通过Sohu邮件接口向{model.Email}发送邮件，标题{model.Title}，内容：{model.Body}");
        }
    }

    public class SendEmailRequest
    {
        public string Email { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
    public class SendRequest
    {
        public string Email { get; set; }

    }
}
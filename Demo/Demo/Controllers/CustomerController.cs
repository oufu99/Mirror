using BLL;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Controllers
{
    public class CustomerController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public string GetCustomer()
        {
            var res = CustomerBLL.Instance.GetCustomer();
            return JsonConvert.SerializeObject(res);
        }

        public string UpdateCustomer(Customer model)
        {
            var res = CustomerBLL.Instance.UpdateCustomer(model);
            return JsonConvert.SerializeObject(res);
        }
        public string DeleteCustomer(List<Customer> list)
        {
            var res = CustomerBLL.Instance.DeleteCustomer(list);
            return JsonConvert.SerializeObject(res);
        }
        public string AddCustomer(Customer model)
        {
            var res = CustomerBLL.Instance.AddCustomer(model);
            return JsonConvert.SerializeObject(res);
        }
    }
}
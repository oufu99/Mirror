using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CustomerDAL
    {
        DemoContext context = new DemoContext();
        public List<Customer> GetCustomers()
        {
            return context.Customers.Where(c => true).ToList();
        }

        public ResponseModel UpdateCustomer(Customer model)
        {
            var targetModel = context.Customers.FirstOrDefault(c => c.Id == model.Id);
            if (model == null)
            {
                return ResponseHelper.GetFailModel("更新失败,查不到这条记录");
            }
            targetModel = ResponseHelper.ConvertCustomerModel(model, targetModel);
            context.Entry<Customer>(targetModel).State = EntityState.Modified;
            var res = context.SaveChanges();
            if (res == 0)
            {
                return ResponseHelper.GetFailModel("更新失败");
            }
            return ResponseHelper.GetSuccessModel("更新成功");
        }

        public ResponseModel DeleteCustomer(List<Customer> list)
        {
            try
            {
                foreach (var item in list)
                {
                    var model = context.Customers.FirstOrDefault(c => c.Id == item.Id);
                    context.Customers.Remove(model);
                }

                var res = context.SaveChanges();
                return ResponseHelper.GetSuccessModel("删除成功");

            }
            catch (Exception)
            {
                return ResponseHelper.GetFailModel("删除失败");
                throw;
            }

        }

        public ResponseModel AddCustomer(Customer model)
        {
            DbEntityEntry<Customer> entityEntry = context.Entry<Customer>(model);
            entityEntry.State = EntityState.Added;
            var res = context.SaveChanges();
            if (res == 0)
            {
                return ResponseHelper.GetFailModel("添加失败");
            }
            return ResponseHelper.GetSuccessModel("添加成功");
        }


    }
}

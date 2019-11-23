using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ResponseHelper
    {

        public static ResponseModel GetFailModel(string msg)
        {
            return new ResponseModel() { Success = false, Msg = msg };
        }

        public static ResponseModel GetSuccessModel(string msg)
        {
            return new ResponseModel() { Success = true, Msg = msg };
        }

        public static Customer ConvertCustomerModel(Customer model, Customer targerModel)
        {
            targerModel.Name = model.Name;
            targerModel.MiniName = model.MiniName;
            targerModel.Type = model.Type;
            targerModel.Province = model.Province;
            targerModel.City = model.City;
            targerModel.Address = model.Address;
            targerModel.Remark = model.Remark;
            targerModel.IsOpen = model.IsOpen;
            return targerModel;
        }

    }
}

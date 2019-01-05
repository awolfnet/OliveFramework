using OliveFramework.Model.Datatable;
using OliveFramework.Model.Request;
using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// NewCargoOrder 的摘要说明
    /// </summary>
    public class NewCargoOrder : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                CargoOrderDetail orderDetail = getModel<CargoOrderDetail>("model");

                /*
                CargoOrderDetail orderDetail = new CargoOrderDetail();
                orderDetail.amount = 1234;
                orderDetail.buyerid = 11;
                orderDetail.comment = "按时发货";
                orderDetail.cargoid = 1;
                orderDetail.orderdate = DateTime.Now;
                */
                

                string OrderID = new Controller.Order().NewCargoOrder(orderDetail);

                string message = string.Format("您有新的订单：\r\n日期：{0}\r\n留言：{1}\r\n价格：{2}元",
                    DateTime.Now, orderDetail.comment, orderDetail.amount);

                CargoModel cargoInfo = new Controller.Cargo().GetCargoInfo((uint)orderDetail.cargoid);

                new Controller.Message().AddMessage(cargoInfo.aid, message, orderDetail.buyerid, Controller.Message.MESSAGE_STATUS.UNREACH, Controller.Message.MESSAGE_STYLE.INFORMATION, "订单", Controller.Message.MESSAGE_TYPE.CHAT);


                WriteSuccess<string>(OrderID);
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }
        }
    }
}
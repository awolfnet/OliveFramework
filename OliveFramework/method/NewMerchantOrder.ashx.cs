using OliveFramework.Page;
using OliveFramework.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OliveFramework.Model.Datatable;

namespace OliveFramework.method
{
    /// <summary>
    /// NewMerchantOrder 的摘要说明
    /// </summary>
    public class NewMerchantOrder : BasePage
    {
        protected override void OnRequest()
        {
            base.OnRequest();

            try
            {
                MerchantOrderDetail orderDetail = getModel<MerchantOrderDetail>("model");

                /*
                MerchantOrderDetail orderDetail = new MerchantOrderDetail();
                orderDetail.amount = 1234;
                orderDetail.amounttype = 0;
                orderDetail.buyerid = 11;
                orderDetail.comment = "按时发货";
                orderDetail.merchantid = 1;
                orderDetail.orderdate = DateTime.Now;
                orderDetail.orderlist = "圆钢1吨，角钢1吨";
                */

                string OrderID= new Controller.Order().NewMerchantOrder(orderDetail);

                string message = string.Format("您有新的订单：\r\n日期：{0}\r\n清单：{1}\r\n价格({2})：{3}元", DateTime.Now, orderDetail.orderlist,orderDetail.amounttype==0?"理论":"实际", orderDetail.amount);

                MerchantModel merchantInfo = new Controller.Merchant().GetMerchantInfo((uint)orderDetail.merchantid);

                new Controller.Message().AddMessage(merchantInfo.aid, message, orderDetail.buyerid, Controller.Message.MESSAGE_STATUS.UNREACH, Controller.Message.MESSAGE_STYLE.INFORMATION, "订单", Controller.Message.MESSAGE_TYPE.CHAT);

                WriteSuccess<string>(OrderID);
            }catch(Exception ex)
            {
                WriteException(ex);
            }

        }



    }
}
using OliveFramework.Model;
using OliveFramework.Model.Datatable;
using OliveFramework.Model.Request;
using OliveFramework.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Controller
{
    public class Order: BaseController
    {
        public string NewMerchantOrder(MerchantOrderDetail orderDetail)
        {
            MerchantOrderModel orderModel = new MerchantOrderModel();

            orderModel.amount = orderDetail.amount;
            orderModel.amounttype = orderDetail.amounttype;
            orderModel.buyerid = orderDetail.buyerid;
            orderModel.comment = orderDetail.comment;
            orderModel.merchantid = orderDetail.merchantid;
            orderModel.orderdate = DateTime.Now;
            orderModel.orderlist = orderDetail.orderlist;

            //db.BeginTransaction();

            int insertID = new Random().Next(0, 9999999);
            int typeCode = MakeOrderTypeCode(0);
            string OrderID = string.Format("0{0}{1:0000000}{2}", DateTime.Now.ToString("yyMMdd"), insertID, typeCode);

            orderModel.orderid = OrderID;

            db.InsertSingleLine<MerchantOrderModel>("merchantorder", orderModel);

            //解决事务问题前用随机数方式
            //ulong insertID=db.GetLastInsertID();
            //int typeCode = MakeOrderTypeCode(0);

            //string OrderID = string.Format("0{0}{1:0000000}{2}", DateTime.Now.ToString("yyMMdd"), insertID, typeCode);

            //orderModel.orderid = OrderID;
            //string where = string.Format("merchantorder.id = {0}", insertID);
            //db.UpdateSingleLine<MerchantOrderModel>("merchantorder", orderModel, where);

            //db.CommitTransaction();

            return OrderID;
        }

        public string NewCargoOrder(CargoOrderDetail orderDetail)
        {
            CargoOrderModel orderModel = new CargoOrderModel();

            orderModel.amount = orderDetail.amount;
            orderModel.buyerid = orderDetail.buyerid;
            orderModel.cargoid = orderDetail.cargoid;
            orderModel.comment = orderDetail.comment;
            orderModel.orderdate = DateTime.Now;

            int insertID = new Random().Next(0, 9999999);
            int typeCode = MakeOrderTypeCode(1);
            string OrderID = string.Format("1{0}{1:0000000}{2}", DateTime.Now.ToString("yyMMdd"), insertID, typeCode);

            orderModel.orderid = OrderID;

            //db.BeginTransaction();

            db.InsertSingleLine<CargoOrderModel>("cargoorder", orderModel);

            //解决事务问题前采用随机数方式
            //ulong insertID = db.GetLastInsertID();
            //int insertID = new Random().Next(0, 9999999);
            //int typeCode = MakeOrderTypeCode(1);

            //string OrderID = string.Format("1{0}{1:0000000}{2}", DateTime.Now.ToString("yyMMdd"), insertID, typeCode);

            //orderModel.orderid = OrderID;
            //string where = string.Format("cargoorder.id = {0}", insertID);
            //db.UpdateSingleLine<CargoOrderModel>("cargoorder", orderModel, where);

            //db.CommitTransaction();

            return OrderID;
        }

        public OrderHistoryModel GetBuyHistory(uint? buyerID)
        {
            return GetOrderHistoryList(buyerID, null);
        }

        public OrderHistoryModel GetSellHistory(uint? merchantOwnerID)
        {
            return GetOrderHistoryList(null, merchantOwnerID);
        }

        private OrderHistoryModel GetOrderHistoryList(uint? buyerID,uint? merchantOwnerID)
        {
            string where = "";
            if(buyerID!=null)
            {
                where = string.Format("buyer_id={0}", buyerID);
            }else
            {
                where = string.Format("merchant_owner_id={0}", merchantOwnerID);
            }

            List<ViewOrderHistoryListModel> list = db.SelectData<ViewOrderHistoryListModel>("view_orderhistory", where);

            if (list == null)
            {
                throw new UnfulfilException("/language/database/no_record");
            }

            OrderHistoryModel historyList = new OrderHistoryModel();
            foreach(ViewOrderHistoryListModel orderHistory in list)
            {
                historyList.Add(orderHistory);
            }

            return historyList;

        }

        public CargoOrderHistoryModel GetBuyCargoHistory(uint? buyerID)
        {
            return GetCargoOrderHistoryList(buyerID, null);
        }

        public CargoOrderHistoryModel GetSellCargoHistory(uint? cargoOwnerID)
        {
            return GetCargoOrderHistoryList(null, cargoOwnerID);
        }
        private CargoOrderHistoryModel GetCargoOrderHistoryList(uint? buyerID,uint? cargoOwnerID)
        {
            string where = "";
            if (buyerID != null)
            {
                where = string.Format("buyer_id={0}", buyerID);
            }
            else
            {
                where = string.Format("cargo_owner_id={0}", cargoOwnerID);
            }

            List<ViewCargoOrderHistoryListModel> list = db.SelectData<ViewCargoOrderHistoryListModel>("view_cargoorderhistory", where);

            if (list == null)
            {
                throw new UnfulfilException("/language/database/no_record");
            }

            CargoOrderHistoryModel historyList = new CargoOrderHistoryModel();
            foreach (ViewCargoOrderHistoryListModel orderHistory in list)
            {
                historyList.Add(orderHistory);
            }

            return historyList;
        }

        public int MakeOrderTypeCode(int orderType)
        {
            int[] merchantCode = { 0, 2, 4, 6, 8 };
            int[] cargoCode = { 1, 3, 5, 7, 9 };

            int index = new Random().Next(0,4);

            if(orderType==0)
            {
                return merchantCode[index];
            }
            else
            {
                return cargoCode[index];
            }
        }



    }
}
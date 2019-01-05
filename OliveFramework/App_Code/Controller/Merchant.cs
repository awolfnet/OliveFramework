using OliveFramework.Model.Datatable;
using OliveFramework.Model.Request;
using OliveFramework.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Controller
{
    public class Merchant: BaseController
    {
        public MerchantListModel GetMerchantList(String productName,String marketName)
        {

            return GetMerchantList(null, null, null, marketName, productName, null);
        }

        public MerchantListModel GetMerchantList(String productName, String marketName, uint merchantType)
        {

            return GetMerchantList(null, null, null, marketName, productName, merchantType);
        }

        public MerchantListModel GetMerchantList(string merchantName,string merchantContact,string merchantDescription,string marketName,string productName,uint? merchantType)
        {
            string where = "";
            if (!String.IsNullOrWhiteSpace(productName))
            {
                where += string.Format("view_merchantlist.product_name = '{0}'", productName);
            }


            if (!String.IsNullOrWhiteSpace(marketName))
            {
                if (!String.IsNullOrWhiteSpace(where))
                    where += " AND ";

                where += string.Format("view_merchantlist.market_name = '{0}'", marketName);
            }

            if (!String.IsNullOrWhiteSpace(merchantDescription))
            {
                if (!String.IsNullOrWhiteSpace(where))
                    where += " AND ";

                where += string.Format("view_merchantlist.merchant_description like '%{0}%'", merchantDescription);
            }

            if (!String.IsNullOrWhiteSpace(merchantContact))
            {
                if (!String.IsNullOrWhiteSpace(where))
                    where += " AND ";

                where += string.Format("view_merchantlist.merchant_contact like '%{0}%'", merchantContact);
            }

            if (!String.IsNullOrWhiteSpace(merchantName))
            {
                if (!String.IsNullOrWhiteSpace(where))
                    where += " AND ";

                where += string.Format("view_merchantlist.merchant_name like '%{0}%'", merchantName);
            }

            if (merchantType!=null)
            {
                if (!String.IsNullOrWhiteSpace(where))
                    where += " AND ";

                where += string.Format("view_merchantlist.merchant_type_id = '{0}'", merchantType);
            }

            //string.Format("view_merchantlist.product_name = '{0}' AND view_merchantlist.market_name = '{1}'", productName, marketName);

            List<ViewMerchantListModel> list = db.SelectData<ViewMerchantListModel>("view_merchantlist", null, where, true, "view_merchantlist.merchant_name DESC");

            if (list == null)
            {
                throw new UnfulfilException("/language/database/no_record");
            }

            MerchantListModel merchantList = new MerchantListModel();

            foreach (ViewMerchantListModel merchant in list)
            {
                merchantList.Add(merchant);
            }
            return merchantList;


        }

        public MerchantModel GetMerchantInfo(uint merchantID)
        {
            string where = string.Format(" `merchantid`={0}", merchantID);

            MerchantModel merchantInfo= db.SelectData<MerchantModel>("merchant", where)[0];

            return merchantInfo;
        }

        public void AddMerchant(string merchantName,string merchantContact,string merchantWebsite,string merchantDescription, List<IDText> merchantSellProductList, uint marketID,uint? ownerID,int? typeID)
        {
            
            MerchantModel merchant = new MerchantModel();
            merchant.contact = merchantContact;
            merchant.description = merchantDescription;
            merchant.name = merchantName;
            merchant.website = merchantWebsite;
            merchant.marketid = marketID;
            merchant.aid = ownerID;
            merchant.mtypeid = typeID;

            db.BeginTransaction();
            db.InsertSingleLine<MerchantModel>("merchant", merchant);

            uint merchantID=db.GetLastInsertID();

            foreach(IDText idtext in merchantSellProductList)
            {
                MerchantSellModel merchantSell = new MerchantSellModel();
                merchantSell.merchantid = merchantID;
                merchantSell.productid = idtext.id;
                db.InsertSingleLine<MerchantSellModel>("merchantsell", merchantSell);
            }

            db.CommitTransaction();
        }

        public void ModifyMerchant(uint merchantID, string merchantName, string merchantContact, string merchantWebsite, string merchantDescription, List<IDText> merchantSellProductList, uint marketID, uint? ownerID)
        {
            
            MerchantModel merchant = new MerchantModel();
            merchant.contact = merchantContact;
            merchant.description = merchantDescription;
            merchant.name = merchantName;
            merchant.website = merchantWebsite;
            merchant.marketid = null;
            merchant.aid = ownerID;

            string where = string.Format("merchant.merchantid={0}", merchantID);

            db.BeginTransaction();
            db.UpdateSingleLine<MerchantModel>("merchant", merchant, where);

            //uint merchantID = db.GetLastInsertID();

            /*
            foreach (IDText idtext in merchantSellProductList)
            {
                MerchantSellModel merchantSell = new MerchantSellModel();
                merchantSell.merchantid = merchantID;
                merchantSell.productid = idtext.id;
                db.InsertSingleLine<MerchantSellModel>("merchantsell", merchantSell);
            }
            */

            db.CommitTransaction();
        }

        public void DeleteMerchant(uint merchantID)
        {
            string where = "";
            where = string.Format("merchantid = {0}", merchantID);

            db.BeginTransaction();
            db.DeleteRecord("merchant", where);
            db.DeleteRecord("merchantsell", where);
            db.DeleteRecord("merchantfavorite", where);
            db.CommitTransaction();
        }

        public List<IDText> GetMerchantSell(uint merchantID)
        {
            string where = string.Format("merchantid={0}", merchantID);
            List<ViewMerchantSell> list = db.SelectData<ViewMerchantSell>("view_merchantsell", where);
            if(list==null)
            {
                throw new UnfulfilException("/language/database/no_record");
            }

            List<IDText> sellList = new List<IDText>();
            foreach(ViewMerchantSell sell in list)
            {
                IDText it = new IDText();
                it.id = (uint)sell.product_id;
                it.text = sell.product_name;
                sellList.Add(it);
            }
            return sellList;
        }

        public List<MerchantTypeModel> GetMerchantTypeList()
        {
            List<MerchantTypeModel> list = db.SelectData<MerchantTypeModel>("merchanttype");
            if (list == null)
            {
                throw new UnfulfilException("/language/database/no_record");
            }

            return list;
        }
    }
}
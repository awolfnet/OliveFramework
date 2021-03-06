﻿using OliveFramework.Model.Datatable;
using OliveFramework.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Controller
{
    public class Market: BaseController
    {
        public MarketListModel GetMarketList(String marketLocation,String marketType)
        {
            string where = "";
            if(!String.IsNullOrWhiteSpace(marketLocation))
            {
                where += String.Format("view_marketlist.market_location = '{0}'", marketLocation);
            }


            string where_marketType = "";
            if(!String.IsNullOrWhiteSpace(marketType))
            {
                if (where.Length > 0)
                    where += " AND ";

                where += String.Format("view_marketlist.market_typename = '{0}'", marketType);
            }

            List<ViewMarketListModel> list = db.SelectData<ViewMarketListModel>("view_marketlist", null,where,false, "view_marketlist.market_name DESC");
            if(list==null)
            {
                throw new UnfulfilException("/language/database/no_record");
            }

            MarketListModel marketList = new MarketListModel();
            foreach(ViewMarketListModel market in list)
            {
                marketList.Add(market);
            }
            return marketList;
        }

        public ProductListModel GetProductList(String marketType)
        {
            string where = string.Format("markettype.name = '{0}'", marketType);
            string join = "INNER JOIN markettype ON product.markettypeid = markettype.typeid";

            List<ProductModel> list = db.SelectData<ProductModel>("product", join, where,false, "product.`name` DESC");

            ProductListModel productList = new ProductListModel();
            foreach(ProductModel product in list)
            {
                productList.Add(product);
            }

            return productList;
        }

        public void AddMarket(string marketProvince,string marketName,uint marketType)
        {
            MarketModel market = new MarketModel();
            market.name = marketName;
            market.location = marketProvince;
            market.markettypeid = marketType;

            db.InsertSingleLine<MarketModel>("market", market);

        }

        public void DeleteMarket(uint marketID)
        {
            string where = string.Format(" `marketid`={0}", marketID);
            db.DeleteRecord("market", where);
        }

        public void AddProduct(string productName,uint marketTypeID)
        {
            ProductModel product = new ProductModel();
            product.name = productName;
            product.markettypeid = marketTypeID;

            db.InsertSingleLine<ProductModel>("product", product);
        }

        public void DeleteProduct(uint productID,string productName, uint marketTypeID)
        {
            string where = string.Format(" `productid`={0}", productID);
            db.DeleteRecord("product", where);
        }
    }
}
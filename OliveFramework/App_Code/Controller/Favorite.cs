﻿using OliveFramework.Model.Datatable;
using OliveFramework.Model.Response;
using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Controller
{
    public class Favorite: BaseController
    {
        public void AddCargo(uint userID, uint cargoID)
        {
            CargoFavoriteModel favoriteInfo = new CargoFavoriteModel();
            favoriteInfo.aid = userID;
            favoriteInfo.cargoid = cargoID;
            try
            {
                db.InsertSingleLine<CargoFavoriteModel>("cargofavorite", favoriteInfo);
            }
            catch (Database.Exception ex)
            {
                switch (ex.Code)
                {
                    case 1062:
                        throw new UnfulfilException("/language/database/duplicate_entry");
                    default: throw (ex);
                }
            }
        }

        public void AddMerchant(uint userID,uint merchantID)
        {
            MerchantFavoriteModel favoriteInfo = new MerchantFavoriteModel();
            favoriteInfo.aid = userID;
            favoriteInfo.merchantid = merchantID;
            try
            {
                db.InsertSingleLine<MerchantFavoriteModel>("merchantfavorite", favoriteInfo);
            }catch(Database.Exception ex)
            {
                switch(ex.Code)
                {
                    case 1062:
                        throw new UnfulfilException("/language/database/duplicate_entry");
                    default:throw(ex);
                }
            }
            
        }

        public void RemoveMerchant(uint userID,uint favoriteID)
        {

        }

        public MerchantFavoriteListModel GetMerchantFavoriteList(uint userID)
        {
            List<ViewMerchantFavoriteListModel> list;

            String where = String.Format("view_merchantfavoritelist.aid={0}", userID);

            try
            {
                list = db.SelectData<ViewMerchantFavoriteListModel>("view_merchantfavoritelist", where);
            }catch (Database.Exception ex)
            {
                switch (ex.Code)
                {
                    case 1062:
                        throw new UnfulfilException("/language/database/duplicate_entry");
                    default: throw (ex);
                }
            }

            MerchantFavoriteListModel favoriteList = new MerchantFavoriteListModel();
            foreach(ViewMerchantFavoriteListModel favorite in list)
            {
                favoriteList.Add(favorite);
            }
            return favoriteList;
        }

        public CargoFavoriteListModel GetCargoFavoriteList(uint userID)
        {
            List<ViewCargoFaoriteListModel> list;

            String where = String.Format("view_cargofavoritelist.aid={0}", userID);

            try
            {
                list = db.SelectData<ViewCargoFaoriteListModel>("view_cargofavoritelist", where);
            }catch (Database.Exception ex)
            {
                switch (ex.Code)
                {
                    case 1062:
                        throw new UnfulfilException("/language/database/duplicate_entry");
                    default: throw (ex);
                }
            }

            CargoFavoriteListModel favoriteList = new CargoFavoriteListModel();
            foreach(ViewCargoFaoriteListModel favorite in list)
            {
                favoriteList.Add(favorite);
            }

            return favoriteList;
        }
    }
}
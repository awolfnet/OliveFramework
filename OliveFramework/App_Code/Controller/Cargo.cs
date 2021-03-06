﻿using OliveFramework.Model.Datatable;
using OliveFramework.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.Controller
{
    public class Cargo: BaseController
    {
        public CargoListModel GetCargoList(string cargoPlate, string cargoDriver,string cargoLicence,string cargoContact,string cargoDescription,string marketName)
        {
            string where = "";
            if (!String.IsNullOrWhiteSpace(cargoPlate))
            {
                where += string.Format("view_cargolist.cargo_plate LIKE '%{0}%'", cargoPlate);
            }

            if (!String.IsNullOrWhiteSpace(cargoDriver))
            {
                if (!String.IsNullOrWhiteSpace(where))
                    where += " AND ";

                where += string.Format("view_cargolist.cargo_driver LIKE '%{0}%'", cargoDriver);
            }

            if (!String.IsNullOrWhiteSpace(cargoLicence))
            {
                if (!String.IsNullOrWhiteSpace(where))
                    where += " AND ";

                where += string.Format("view_cargolist.driver_licence LIKE '%{0}%'", cargoLicence);
            }

            if (!String.IsNullOrWhiteSpace(cargoContact))
            {
                if (!String.IsNullOrWhiteSpace(where))
                    where += " AND ";

                where += string.Format("view_cargolist.driver_contact LIKE '%{0}%'", cargoContact);
            }

            if (!String.IsNullOrWhiteSpace(cargoDescription))
            {
                if (!String.IsNullOrWhiteSpace(where))
                    where += " AND ";

                where += string.Format("view_cargolist.cargo_description LIKE '%{0}%'", cargoDescription);
            }


            if (!String.IsNullOrWhiteSpace(marketName))
            {
                if (!String.IsNullOrWhiteSpace(where))
                    where += " AND ";

                where += string.Format("view_cargolist.market_name = '{0}'", marketName);
            }


            List<ViewCargoListModel> list = db.SelectData<ViewCargoListModel>("view_cargolist", null, where, true, "view_cargolist.cargo_plate DESC");

            if (list == null)
            {
                throw new UnfulfilException("/language/database/no_record");
            }

            CargoListModel cargoList = new CargoListModel();

            foreach (ViewCargoListModel cargo in list)
            {
                cargoList.Add(cargo);
            }
            return cargoList;
        }
        public CargoListModel GetCargoList(String marketName,string cargoDescription)
        {
            return GetCargoList(null, null, null, null, cargoDescription, marketName);
        }

        public CargoModel GetCargoInfo(uint cargoID)
        {
            string where = string.Format("`cargo`.`cargoid`={0}", cargoID);

            CargoModel merchantInfo = db.SelectData<CargoModel>("cargo", where)[0];

            return merchantInfo;
        }

        public void DeleteCargo(uint cargoID)
        {
            string where = "";
            where = string.Format("cargoid = {0}", cargoID);

            db.BeginTransaction();
            db.DeleteRecord("cargo", where);
            db.DeleteRecord("cargofavorite", where);
            db.CommitTransaction();
        }

        public void AddCargo(string cargoPlate,string cargoDriver,string driverLicence,string cargoContact,string cargoDescription,uint marketID,uint? cargoOwnerID)
        {
            CargoModel cargo = new CargoModel();

            cargo.plate = cargoPlate;
            cargo.driver = cargoDriver;
            cargo.licence = driverLicence;
            cargo.contact = cargoContact;
            cargo.description = cargoDescription;
            cargo.marketid = marketID;

                cargo.aid = cargoOwnerID;


            db.InsertSingleLine<CargoModel>("cargo", cargo);

        }
    }
}
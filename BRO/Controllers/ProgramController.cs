using BRO.Models;
using BRO.MyClass;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Configuration;

namespace BRO.Controllers
{
    public class ProgramController : Controller
    {
        private static ConDB conn1 = new ConDB("MySQLConn1");
        static string sSQL;

        public ActionResult Program()
        {
            sSQL = " SELECT * FROM mainpath";
            using (MySqlDataReader dr = conn1.ExecuteReader(sSQL))
            {
                if (dr.Read()) // If you're expecting more than one line, change this to while(reader.Read()).
                {
                    ProgramModel rec = new ProgramModel
                    {
                        txtCoName = dr["CONAME"].ToString(),
                        txtAdd1 = dr["ADD1"].ToString(),
                        txtLengthMenu = dr["LENGTHMENU"].ToString(),
                    };

                    ViewBag.FieldValue = rec;
                    return View();
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Program( ProgramModel viewModel )
        {
            sSQL =  " UPDATE mainpath set " +
                " CONAME=" + viewModel.txtCoName +
                " ADD1 = " + viewModel.txtAdd1 + " , " +
                " LENGTHMENU = " + viewModel.txtLengthMenu + " , " +
                " EDIT_ID= " + Session["USER_ID"] + " , " +
                " DT_EDIT= " + DateTime.Now +
                " WHERE CONAME = " + viewModel.txtCoName;

            conn1.ExecuteQuery(sSQL);
            conn1.Close();

            Session["LENGTH1"] = viewModel.txtLengthMenu;

            return Json(new { status = "updated", message = "Successfully updated" });
        }
    }
}
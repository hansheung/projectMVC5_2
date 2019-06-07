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
        public ConDB conn1 = new ConDB("bronet");
        static string sSQL;

        public ActionResult Program()
        {
            try
            {
                sSQL = " SELECT * FROM mainpath";
                MySqlDataReader dr = conn1.ExecuteReader(sSQL);

                if (dr.Read())
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Program( ProgramModel viewModel )
        {
            sSQL =  " UPDATE mainpath set " +
                    " CONAME='" + viewModel.txtCoName + "', " +
                    " ADD1 = '" + viewModel.txtAdd1 + "', " +
                    " LENGTHMENU = '" + viewModel.txtLengthMenu + "', " +
                    " EDIT_ID= '" + Session["USER_ID"] + "', " +
                    " DT_EDIT= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                    " WHERE CONAME = '" + viewModel.txtCoName + "'";
                    conn1.execute(sSQL);

            Session["LENGTH1"] = viewModel.txtLengthMenu;

            return Json(new { status = "updated", message = "Successfully updated" });
        }
    }
}
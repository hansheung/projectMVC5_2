using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BRO.MyClass;
using BRO.Models;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Net;
using System.Web.Security;

namespace BRO.Controllers
{
    public class HomeController : Controller
    {
        public Proc proc = new Proc();
        static string sSQL;

        public ActionResult Company()
        {
            return View();
        }

        public ActionResult Applications()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["USER_ID"] = null; //it's my session variable
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut(); //you write this when you use FormsAuthentication
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ConDB conn1 = new ConDB("MySQLConn1"); //===Correct

                var sLOGIN_ID = Request.Form["txtLOGIN_ID"];
                var sPASSWORD = Request.Form["txtPASSWORD"];

                sSQL = " SELECT * FROM mainpass where LOGIN_ID ='" + sLOGIN_ID + "'";
                using (MySqlDataReader dr = conn1.ExecuteReader(sSQL)) {   //=== Correct

                    if (dr.Read())
                    {
                        double ddtLastUse;
                        string sdtLastUse = dr["DATELASTUSE"].ToString();
                        string sPassword = dr["PASSWORD"].ToString();

                        int iPassword = proc.pPassConv(sPASSWORD);

                        if (DBNull.Value.Equals(dr["DATELASTUSE"]))
                        {
                            DateTime d2 = new DateTime(1980, 1, 1, 0, 0, 0);
                            ddtLastUse = (double)(0 - (d2.ToOADate()));
                        }
                        else
                        {
                            DateTime d1 = DateTime.Parse(sdtLastUse);
                            DateTime d2 = new DateTime(1980, 1, 1, 0, 0, 0);

                            ddtLastUse = (double)(d1.ToOADate() - d2.ToOADate());
                        }

                        int iCheckPass = iPassword + (int)Math.Round(ddtLastUse);

                        if (sPassword == iCheckPass.ToString())
                        {
                            DateTime d1 = DateTime.Now;
                            DateTime d2 = new DateTime(1980, 1, 1, 0, 0, 0);

                            double dUpdate = (double)(d1.ToOADate() - d2.ToOADate());
                            int iUpdatedPass = iPassword + (int)Math.Round(dUpdate);

                            Session["USER_ID"] = dr["LOGIN_ID"].ToString();
                            Session["USER_NAME"] = dr["NAME"].ToString();

                            sSQL = " UPDATE mainpass set" +
                                    " DATELASTUSE='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' , " +
                                    " PASSWORD='" + iUpdatedPass.ToString() + "'" +
                                    " WHERE LOGIN_ID='" + viewModel.txtLOGIN_ID + "'";
                            //conn1.Close(); //=== Correct
                            conn1.ExecuteQuery(sSQL); //== Correct

                            sSQL = " SELECT * FROM mainpath";
                            using (MySqlDataReader drpath = conn1.ExeReader(sSQL))
                            {
                                if (drpath.Read())
                                {
                                    Session["LENGTH1"] = drpath["LENGTHMENU"].ToString();
                                }
                            }
                            return Json(new { status = "success", message = "Login Successful" });

                        }
                        else
                        {
                            return Json(new { status = "fail", message = "Password is incorrect", fieldname = "PASSWORD" });
                        }
                    }
                    else
                    {
                        return Json(new { status = "fail", message = "Invalid Login ID", fieldname = "LOGIN_ID" });
                    }

                    //conn1.Close();
                }
            }

            return Json(viewModel, "json");
        }

        
    }
}
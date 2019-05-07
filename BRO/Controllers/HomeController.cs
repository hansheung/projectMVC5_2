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
        private ConDB conn = new ConDB();
        public Proc proc = new Proc();

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
            Session["USERID"] = null; //it's my session variable
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
                string sSQL = " SELECT * FROM mainpass where ID ='" + viewModel.txtLoginID + "'";
                DataTable dt = conn.GetData(sSQL);

                if (dt.Rows.Count > 0)
                {
                    double ddtLastUse;

                    string sdtLastUse = dt.Rows[0]["DATELASTUSE"].ToString();
                    string sPassword = dt.Rows[0]["PASSWORD"].ToString();

                    int iPassword = proc.pPassConv(viewModel.txtPassword);

                    if (DBNull.Value.Equals(dt.Rows[0]["DATELASTUSE"]))
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

                        string constr = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
                        using (MySqlConnection con = new MySqlConnection(constr))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("UPDATE mainpass " +
                                "set DATELASTUSE = @DateLastUse, " +
                                "PASSWORD=@Password " +
                                "WHERE ID = @LoginID"))
                            {
                                using (MySqlDataAdapter sda = new MySqlDataAdapter())
                                {
                                    cmd.Parameters.AddWithValue("@DateLastUse", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@Password", iUpdatedPass.ToString());
                                    cmd.Parameters.AddWithValue("@LoginID", viewModel.txtLoginID);
                                    cmd.Connection = con;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }

                        Session["USERID"] = dt.Rows[0]["ID"].ToString();
                        Session["USERNAME"] = dt.Rows[0]["NAME"].ToString();

                        string sSQL2 = " SELECT * FROM mainpath";
                        DataTable dt2 = conn.GetData(sSQL2);
                        if (dt2.Rows.Count > 0)
                        {
                            Session["LENGTH1"] = dt2.Rows[0]["LENGTHMENU"].ToString();
                        }

                            return Json(new { status = "success", message = "Login Successful" });

                    }
                    else
                    {
                        return Json(new { status = "fail", message = "Password is incorrect", fieldname = "Password" });
                    }
                }
                else
                {
                    return Json(new { status = "fail", message = "Invalid Login ID", fieldname = "LoginID" });
                }
            }

            return Json(viewModel, "json");
        }

        
    }
}
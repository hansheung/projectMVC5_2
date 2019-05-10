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
        public ActionResult Program()
        {
            ConDB conn = new ConDB();
            //Proc proc = new Proc();

            string sSQL = " SELECT * FROM mainpath ";
            DataTable dt = conn.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                ProgramModel rec = new ProgramModel
                {
                    txtCoName = dt.Rows[0]["CONAME"].ToString(),
                    txtAdd1 = dt.Rows[0]["ADD1"].ToString(),
                    txtLengthMenu = dt.Rows[0]["LENGTHMENU"].ToString(),
                };

                ViewBag.FieldValue = rec;
                return View();

            }

            return View();
        }

        [HttpPost]
        public ActionResult Program( ProgramModel viewModel )
        {
            string constr = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE mainpath " +
                "set CONAME=@CoName, " +
                "ADD1 = @Add1, " +
                "LENGTHMENU = @LengthMenu, " +
                "EDIT_ID=@EditID, " +
                "DT_EDIT=@DtEdit " +
                "WHERE CONAME = @CoName"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@CoName", viewModel.txtCoName);
                        cmd.Parameters.AddWithValue("@Add1", viewModel.txtAdd1);
                        cmd.Parameters.AddWithValue("@LengthMenu", viewModel.txtLengthMenu);
                        cmd.Parameters.AddWithValue("@EditID", Session["USER_ID"]);
                        cmd.Parameters.AddWithValue("@DtEdit", DateTime.Now);
                        System.Diagnostics.Debug.WriteLine(" sSQL : ");
                        System.Diagnostics.Debug.WriteLine(cmd);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Session["LENGTH1"] = viewModel.txtLengthMenu;

                        return Json(new { status = "updated", message = "Successfully updated" });
                    }
                }
            }
        }
    }
}
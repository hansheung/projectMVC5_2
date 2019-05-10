using BRO.Models;
using BRO.MyClass;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Mvc;


namespace BRO.Controllers
{
    public class PasswordController : Controller
    {
        private ConDB conn = new ConDB();
        public Proc proc = new Proc();

        static int TOTAL_ROWS;

        static readonly List<DataItem> _data = CreateData();

        public class DataItem
        {
            public string AUTOINC { get; set; }
            public string LOGIN_ID { get; set; }
            public string NAME { get; set; }
            public string DT_CREATE { get; set; }
        }

        public class DataTableData
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<DataItem> data { get; set; }
        }

        private static List<DataItem> CreateData()
        {
            List<DataItem> list = new List<DataItem>();

            string mainconn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sSQL = " SELECT * FROM mainpass ";
            MySqlCommand comm = new MySqlCommand(sSQL);
            comm.Connection = mysqlconn;

            MySqlDataAdapter adapter = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            TOTAL_ROWS = dt.Rows.Count;

            mysqlconn.Open();
            MySqlDataReader dr = comm.ExecuteReader();

            while (dr.Read())
            {
                DataItem item = new DataItem();

                item.AUTOINC = dr["AUTOINC"].ToString();
                item.LOGIN_ID = dr["LOGIN_ID"].ToString();
                item.NAME = dr["NAME"].ToString();
                item.DT_CREATE = dr["DT_CREATE"].ToString();
                list.Add(item);
            }
            mysqlconn.Close();

            return list;
        }

        private int SortString(string s1, string s2, string sortDirection)
        {
            return sortDirection == "asc" ? s1.CompareTo(s2) : s2.CompareTo(s1);
        }

        private int SortInteger(string s1, string s2, string sortDirection)
        {
            int i1 = int.Parse(s1);
            int i2 = int.Parse(s2);
            return sortDirection == "asc" ? i1.CompareTo(i2) : i2.CompareTo(i1);
        }

        private int SortDateTime(string s1, string s2, string sortDirection)
        {
            DateTime d1 = DateTime.Parse(s1);
            DateTime d2 = DateTime.Parse(s2);
            return sortDirection == "asc" ? d1.CompareTo(d2) : d2.CompareTo(d1);
        }

        private List<DataItem> FilterData(ref int recordFiltered, int start, int length, string search, int sortColumn, string sortDirection)
        {
            List<DataItem> _data = CreateData();

            List<DataItem> list = new List<DataItem>();
            if (search == null)
            {
                list = _data;
            }
            else
            {
                // simulate search
                foreach (DataItem dataItem in _data)
                {
                    if (
                        dataItem.LOGIN_ID.ToUpper().Contains(search.ToUpper()) ||
                        dataItem.NAME.ToString().Contains(search.ToUpper()) ||
                        dataItem.DT_CREATE.ToString().Contains(search.ToUpper())
                        )
                    {
                        list.Add(dataItem);
                    }
                }
            }

            // simulate sort
            //=== sortColumn need to change additional column
            if (sortColumn == 1)
            {   // sort LOGIN_ID
                list.Sort((x, y) => SortString(x.LOGIN_ID, y.LOGIN_ID, sortDirection));
            }
            else if (sortColumn == 2)
            {   // sort NAME
                list.Sort((x, y) => SortString(x.NAME, y.NAME, sortDirection));
            }
            else if (sortColumn == 3)
            {   // sort DT_CREATE
                list.Sort((x, y) => SortDateTime(x.DT_CREATE, y.DT_CREATE, sortDirection));
            }

            recordFiltered = list.Count;

            // get just one page of data
            list = list.GetRange(start, Math.Min(length, list.Count - start));

            return list;
        }

        // this ajax function is called by the client for each draw of the information on the page (i.e. when paging, ordering, searching, etc.). 
        public ActionResult AjaxGetJsonData(int draw, int start, int length)
        {
            System.Diagnostics.Debug.WriteLine(" Draw : " + draw);

            var searchParam = Request.QueryString["columns[1]search[value]"];
            System.Diagnostics.Debug.WriteLine(" RequestQueryString : " + searchParam);

            string search = Request.Form["search[value]"];
            System.Diagnostics.Debug.WriteLine(" RequestForm : " + search);
            
            string search2 = Request.QueryString["search[value]"];
            System.Diagnostics.Debug.WriteLine(" RequestQueryString2 : " + search2);
            
            int sortColumn = -1;
            string sortDirection = "asc";
            if (length == -1)
            {
                length = TOTAL_ROWS;
            }

            // note: we only sort one column at a time
            if (Request.QueryString["order[0][column]"] != null)
            {
                //sortColumn = int.Parse(Request.QueryString["columns[indexOfColumnToSearch]search[value]"]);

                sortColumn = int.Parse(Request["order[0][column]"]);
            }
            if (Request.QueryString["order[0][dir]"] != null)
            {
                sortDirection = Request.QueryString["order[0][dir]"];
            }

            DataTableData dataTableData = new DataTableData();
            dataTableData.draw = draw;
            dataTableData.recordsTotal = TOTAL_ROWS;
            int recordsFiltered = 0;
            dataTableData.data = FilterData(ref recordsFiltered, start, length, search, sortColumn, sortDirection);
            dataTableData.recordsFiltered = recordsFiltered;

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Password()
        {
            return View();
        }

        public ActionResult PasswordDet(string AUTOINC)
        {
            if (AUTOINC is null)
            {

                PasswordModel rec = new PasswordModel
                {
                    txtLOGIN_ID = "",
                    txtNAME = "",
                    txtPASSWORD = "",
                };

                ViewBag.FieldValue = rec;
                return View();
            }
            else
            {
                ConDB conn = new ConDB();
                //Proc proc = new Proc();

                string sSQL = " SELECT * FROM mainpass where AUTOINC ='" + AUTOINC + "'";
                DataTable dt = conn.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    PasswordModel rec = new PasswordModel
                    {
                        txtLOGIN_ID = dt.Rows[0]["LOGIN_ID"].ToString(),
                        txtNAME = dt.Rows[0]["NAME"].ToString(),
                        txtPASSWORD = dt.Rows[0]["PASSWORD"].ToString(),
                    };

                    ViewBag.FieldValue = rec;
                    return View();

                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult PasswordDet(string id, PasswordModel viewModel)
        {
            //ConDB conn = new ConDB();
            //Proc proc = new Proc();
            if (id == "Save")
            {
                int iPassword = proc.pPassConv(viewModel.txtPASSWORD.ToString());

                DateTime d1 = DateTime.Now;
                DateTime d2 = new DateTime(1980, 1, 1, 0, 0, 0);

                double dUpdate = (double)(d1.ToOADate() - d2.ToOADate());
                int iUpdatedPass = iPassword + (int)Math.Round(dUpdate);

                string sSQL = " SELECT * FROM mainpass where LOGIN_ID ='" + viewModel.txtLOGIN_ID + "'";
                DataTable dt = conn.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    return Json(new { status = "fail", message = "Login ID already exists", fieldname = "LOGIN_ID" });

                }
                else
                {
                    string constr = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
                    using (MySqlConnection con = new MySqlConnection(constr))
                    {
                        using (MySqlCommand cmd = new MySqlCommand(
                                " INSERT into mainpass" +
                                " (LOGIN_ID, NAME, PASSWORD,DATELASTUSE,EDIT_ID,DT_EDIT,CREATE_ID,DT_CREATE)" +
                                " values " +
                                " (@LOGIN_ID,@NAME, @PASSWORD, @DATELASTUSE, @EDIT_ID, @DT_EDIT, @CREATE_ID, @DT_CREATE)"
                            ))
                        {
                            using (MySqlDataAdapter sda = new MySqlDataAdapter())
                            {
                                cmd.Parameters.AddWithValue("@LOGIN_ID", viewModel.txtLOGIN_ID);
                                cmd.Parameters.AddWithValue("@NAME", viewModel.txtNAME);
                                cmd.Parameters.AddWithValue("@PASSWORD", iUpdatedPass);
                                cmd.Parameters.AddWithValue("@DATELASTUSE", DateTime.Now);
                                cmd.Parameters.AddWithValue("@EDIT_ID", Session["USER_ID"]);
                                cmd.Parameters.AddWithValue("@DT_EDIT", DateTime.Now);
                                cmd.Parameters.AddWithValue("@CREATE_ID", Session["USER_ID"]);
                                cmd.Parameters.AddWithValue("@DT_CREATE", DateTime.Now);

                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                return Json(new { status = "saved", message = "Successfully saved" });

                            }
                        }
                    }
                }

            }
            else
            {
                string constr = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    if (string.IsNullOrEmpty(viewModel.txtPASSWORD))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("UPDATE mainpass " +
                        "set NAME=@NAME, " +
                        "DATELASTUSE = @DATELASTUSE, " +
                        "EDIT_ID=@EDIT_ID, " +
                        "DT_EDIT=@DT_EDIT " +
                        "WHERE LOGIN_ID = @LOGIN_ID"))
                        {
                            using (MySqlDataAdapter sda = new MySqlDataAdapter())
                            {
                                cmd.Parameters.AddWithValue("@NAME", viewModel.txtNAME);
                                cmd.Parameters.AddWithValue("@DATELASTUSE", DateTime.Now);
                                cmd.Parameters.AddWithValue("@EDIT_ID", Session["USER_ID"]);
                                cmd.Parameters.AddWithValue("@DT_EDIT", DateTime.Now);
                                cmd.Parameters.AddWithValue("@LOGIN_ID", viewModel.txtLOGIN_ID);

                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                return Json(new { status = "updated", message = "Successfully updated" });
                            }
                        }
                    }
                    else
                    {
                        int iPassword = proc.pPassConv(viewModel.txtPASSWORD.ToString());

                        DateTime d1 = DateTime.Now;
                        DateTime d2 = new DateTime(1980, 1, 1, 0, 0, 0);

                        double dUpdate = (double)(d1.ToOADate() - d2.ToOADate());
                        int iUpdatedPass = iPassword + (int)Math.Round(dUpdate);

                        using (MySqlCommand cmd = new MySqlCommand("UPDATE mainpass " +
                        "set NAME=@NAME, " +
                        "PASSWORD=@PASSWORD, " +
                        "DATELASTUSE = @DATELASTUSE, " +
                        "EDIT_ID=@EDIT_ID, " +
                        "DT_EDIT=@DT_EDIT " +
                        "WHERE LOGIN_ID = @LOGIN_ID"))
                        {
                            using (MySqlDataAdapter sda = new MySqlDataAdapter())
                            {
                                cmd.Parameters.AddWithValue("@NAME", viewModel.txtNAME);
                                cmd.Parameters.AddWithValue("@PASSWORD", iUpdatedPass);
                                cmd.Parameters.AddWithValue("@DATELASTUSE", DateTime.Now);
                                cmd.Parameters.AddWithValue("@EDIT_ID", Session["USER_ID"]);
                                cmd.Parameters.AddWithValue("@DT_EDIT", DateTime.Now);
                                cmd.Parameters.AddWithValue("@LOGIN_ID", viewModel.txtLOGIN_ID);

                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                return Json(new { status = "updated", message = "Successfully updated" });

                            }
                        }
                    }
                }
            }

            //return Json(viewModel, "json");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(PasswordModel model)
        {
            if (ModelState.IsValid)
            {
                string sSQL = " SELECT * FROM mainpass where LOGIN_ID ='" + model.txtLOGIN_ID + "'";
                DataTable dt = conn.GetData(sSQL);

                if (dt.Rows.Count > 0)
                {
                    return Json(new { status = "success", message = "Successfully saved" });

                }
                else
                {
                    return Json(new { status = "fail", message = "Invalid Login ID", fieldname = "LOGIN_ID" });
                }
            }

            return Json(model, "json");
        }
    }
}

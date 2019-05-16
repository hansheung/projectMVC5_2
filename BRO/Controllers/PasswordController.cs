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
        private static ConDB conn1 = new ConDB("MySQLConn1");
        public Proc proc = new Proc();

        static string sSQL;
        static int TOTAL_ROWS;

        //static readonly List<DataItem> _data = CreateData();

        public class DataItem
        {
            public string AUTOINC { get; set; }
            public string LOGIN_ID { get; set; }
            public string NAME { get; set; }
            public string DT_EDIT { get; set; }
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

            sSQL = " SELECT * FROM mainpass ";
            DataTable dt = conn1.GetData(sSQL);
            TOTAL_ROWS = dt.Rows.Count;

            using (MySqlDataReader dr = conn1.ExecuteReader(sSQL))
            {
                while (dr.Read())
                {
                    DataItem item = new DataItem();

                    item.AUTOINC = dr["AUTOINC"].ToString();
                    item.LOGIN_ID = dr["LOGIN_ID"].ToString();
                    item.NAME = dr["NAME"].ToString();
                    item.DT_EDIT = dr["DT_EDIT"].ToString();
                    list.Add(item);
                }
            }
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

        // here we simulate SQL search, sorting and paging operations
        // !!!! DO NOT DO THIS IN REAL APPLICATION !!!!
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
                        dataItem.DT_EDIT.ToString().Contains(search.ToUpper())
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
                list.Sort((x, y) => SortDateTime(x.DT_EDIT, y.DT_EDIT, sortDirection));
            }

            recordFiltered = list.Count;

            // get just one page of data
            list = list.GetRange(start, Math.Min(length, list.Count - start));

            return list;
        }

        // this ajax function is called by the client for each draw of the information on the page (i.e. when paging, ordering, searching, etc.). 
        public ActionResult AjaxGetJsonData(int draw, int start, int length)
        {
            //string search4 = Request.Form.GetValues("search[value]")[0];
            //string draw4 = Request.Form.GetValues("draw")[0];
            //string order4 = Request.Form.GetValues("order[0][column]")[0];
            //string orderDir4 = Request.Form.GetValues("order[0][dir]")[0];
            //System.Diagnostics.Debug.WriteLine(" search4 : " + search4);
            //System.Diagnostics.Debug.WriteLine(" draw4 : " + draw4);
            //System.Diagnostics.Debug.WriteLine(" oreder4 : " + order4);
            //System.Diagnostics.Debug.WriteLine(" direction4 : " + orderDir4);

            System.Diagnostics.Debug.WriteLine(" Draw : " + draw);

            var searchParam = Request.QueryString["columns[1]search[value]"];
            System.Diagnostics.Debug.WriteLine(" RequestQueryString : " + searchParam);

            string search1 = Request.Form["search[value]"];
            System.Diagnostics.Debug.WriteLine(" RequestForm1 : " + search1);

            string search2 = Request.QueryString["search[value]"];
            System.Diagnostics.Debug.WriteLine(" RequestQueryString2 : " + search2);

            string whichcolumn = Request.QueryString["order[0][column]"];
            System.Diagnostics.Debug.WriteLine(" Which Column : " + whichcolumn);

            string search = Request.QueryString["search[value]"];
            System.Diagnostics.Debug.WriteLine(" RequestQueryString : " + search);

            int sortColumn = -1;
            string sortDirection = "asc";
            if (length == -1)
            {
                length = TOTAL_ROWS;
            }

            // note: we only sort one column at a time
            if (Request.QueryString["order[0][column]"] != null)
            {
                sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
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

        public ActionResult PasswordDet(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View();
            }
            else
            {
                sSQL = " SELECT * FROM mainpass where AUTOINC ='" + id + "'";
                using (MySqlDataReader reader = conn1.ExecuteReader(sSQL))
                {
                    if (reader.Read()) // If you're expecting more than one line, change this to while(reader.Read()).
                    {
                        PasswordModel rec = new PasswordModel
                        {
                            txtLOGIN_ID = reader["LOGIN_IN"].ToString(),
                            txtNAME = reader["NAME"].ToString(),
                            txtPASSWORD = reader["PASSWORD"].ToString(),
                        };

                        ViewBag.FieldValue = rec;
                        return View();
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult PasswordDet(string id, PasswordModel viewModel)
        {
            int iPassword = proc.pPassConv(viewModel.txtPASSWORD.ToString());

            DateTime d1 = DateTime.Now;
            DateTime d2 = new DateTime(1980, 1, 1, 0, 0, 0);

            double dUpdate = (double)(d1.ToOADate() - d2.ToOADate());
            int iUpdatedPass = iPassword + (int)Math.Round(dUpdate);

            if (id == "Save")
            {
                sSQL = " SELECT * FROM mainpass where LOGIN_ID ='" + viewModel.txtLOGIN_ID + "'";
                using (MySqlDataReader reader = conn1.ExecuteReader(sSQL))
                {
                    if (reader.Read())
                    {
                        return Json(new { status = "fail", message = "Login ID already exists", fieldname = "LOGIN_ID" });
                    }
                    else
                    {
                        sSQL =  " INSERT into mainpass" +
                                " (LOGIN_ID, NAME, PASSWORD,DATELASTUSE,EDIT_ID,DT_EDIT,CREATE_ID,DT_CREATE)" +
                                " values " +
                                  viewModel.txtLOGIN_ID + " , " +
                                  viewModel.txtNAME + " , " +
                                  iUpdatedPass + " , " +
                                  Session["USER_ID"] + " , " +
                                  DateTime.Now + " , " +
                                  Session["USER_ID"] + " , " +
                                  DateTime.Now + " ) ";

                        conn1.ExecuteQuery(sSQL);
                        conn1.Close();

                        return Json(new { status = "saved", message = viewModel.txtLOGIN_ID });
                    }
                }
            }
            else if (id == "Update")
            {
                string sUpdatePass;

                if (!string.IsNullOrEmpty(viewModel.txtPASSWORD))
                {
                    sUpdatePass = "PASSWORD=" + iUpdatedPass + ",";
                }
                else
                {
                    sUpdatePass = "";
                }

                sSQL =  " UPDATE mainpass set " +
                        " NAME=" + viewModel.txtNAME + " , " + sUpdatePass +
                        " EDIT_ID= " + Session["USER_ID"] + " , " +
                        " DT_EDIT= " + DateTime.Now +
                        " WHERE LOGIN_ID = " + viewModel.txtLOGIN_ID;

                    conn1.ExecuteQuery(sSQL);
                    conn1.Close();
                    return Json(new { status = "updated", message = viewModel.txtLOGIN_ID });
            }
            else
            {
                sSQL = " DELETE FROM mainpass" +
                        " WHERE LOGIN_ID = " + viewModel.txtLOGIN_ID;

                conn1.ExecuteQuery(sSQL);
                conn1.Close();

                return Json(new { status = "deleted", message = viewModel.txtLOGIN_ID });

            }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(PasswordModel model)
        {
            sSQL = " SELECT * FROM mainpass where LOGIN_ID ='" + model.txtLOGIN_ID + "'";
            using (MySqlDataReader reader = conn1.ExecuteReader(sSQL))
            {
                if (reader.Read())
                {
                    return Json(new { status = "success", message = "Successfully saved" });

                }
                else
                {
                    return Json(new { status = "fail", message = "Invalid Login ID", fieldname = "LOGIN_ID" });
                }
            }
        }
    }
}

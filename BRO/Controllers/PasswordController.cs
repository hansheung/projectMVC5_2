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
        private static ConDB conn1 = new ConDB("bronet");
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

            try
            {
                sSQL = "SELECT * FROM mainpass";

                DataTable dt = conn1.GetData(sSQL);
                TOTAL_ROWS = dt.Rows.Count;

                MySqlDataReader dr = conn1.ExecuteReader(sSQL);
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
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

        private List<DataItem> FilterData(ref int recordFiltered, int start, int length, string search, string searchLOGIN_ID, string searchNAME, string searchDT_EDIT, int sortColumn, string sortDirection)
        {
            List<DataItem> _data = CreateData();

            List<DataItem> list = new List<DataItem>();

            if (!string.IsNullOrEmpty(searchLOGIN_ID))
            {
                foreach (DataItem dataItem in _data)
                {
                    if (
                        dataItem.LOGIN_ID.ToUpper().Contains(searchLOGIN_ID.ToUpper())
                        )
                    {
                        list.Add(dataItem);
                    }
                }
            }

            if (!string.IsNullOrEmpty(searchNAME))
            {
                foreach (DataItem dataItem in _data)
                {
                    if (
                        dataItem.NAME.ToUpper().Contains(searchNAME.ToUpper())
                        )
                    {
                        list.Add(dataItem);
                    }
                }
            }

            if (!string.IsNullOrEmpty(searchDT_EDIT))
            {
                foreach (DataItem dataItem in _data)
                {
                    if (
                        dataItem.DT_EDIT.ToUpper().Contains(searchDT_EDIT.ToUpper())
                        )
                    {
                        list.Add(dataItem);
                    }
                }
            }

            if (!string.IsNullOrEmpty(searchLOGIN_ID) && !string.IsNullOrEmpty(searchNAME) && !string.IsNullOrEmpty(searchDT_EDIT))
            {
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

            if (!string.IsNullOrEmpty(searchLOGIN_ID) && !string.IsNullOrEmpty(searchNAME))
            {
                foreach (DataItem dataItem in _data)
                {
                    if (
                        dataItem.LOGIN_ID.ToUpper().Contains(searchLOGIN_ID.ToUpper()) ||
                        dataItem.NAME.ToUpper().Contains(searchNAME.ToUpper())
                        )
                    {
                        list.Add(dataItem);
                    }
                }
            }

            if (!string.IsNullOrEmpty(searchLOGIN_ID) && !string.IsNullOrEmpty(searchDT_EDIT))
            {
                foreach (DataItem dataItem in _data)
                {
                    if (
                        dataItem.LOGIN_ID.ToUpper().Contains(searchLOGIN_ID.ToUpper()) ||
                        dataItem.DT_EDIT.ToUpper().Contains(searchDT_EDIT.ToUpper())
                        )
                    {
                        list.Add(dataItem);
                    }
                }
            }

            if (!string.IsNullOrEmpty(searchNAME) && !string.IsNullOrEmpty(searchDT_EDIT))
            {

                foreach (DataItem dataItem in _data)
                {
                    if (
                        dataItem.NAME.ToUpper().Contains(searchNAME.ToUpper()) ||
                        dataItem.DT_EDIT.ToUpper().Contains(searchDT_EDIT.ToUpper())
                        )
                    {
                        list.Add(dataItem);
                    }
                }
            }

            if (!string.IsNullOrEmpty(search))
            {
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
            
            if(string.IsNullOrEmpty(search) && string.IsNullOrEmpty(searchLOGIN_ID) && string.IsNullOrEmpty(searchNAME) && string.IsNullOrEmpty(searchDT_EDIT))
            {
                list = _data;
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
            System.Diagnostics.Debug.WriteLine(" Draw : " + draw);

            var searchLOGIN_ID = Request.QueryString["columns[1][search][value]"];
            System.Diagnostics.Debug.WriteLine(" RequestParam1 : " + searchLOGIN_ID);

            var searchNAME = Request.QueryString["columns[2][search][value]"];
            System.Diagnostics.Debug.WriteLine(" RequestParam2 : " + searchNAME);

            var searchDT_EDIT = Request.QueryString["columns[3][search][value]"];
            System.Diagnostics.Debug.WriteLine(" RequestParam3 : " + searchDT_EDIT);

            //string whichcolumn = Request.QueryString["order[0][column]"];
            //System.Diagnostics.Debug.WriteLine(" Sort by what Column : " + whichcolumn);

            //string whichdirection = Request.QueryString["order[0][dir]"];
            //System.Diagnostics.Debug.WriteLine(" Sort by which Direction : " + whichdirection);

            string search = Request.QueryString["search[value]"];
            System.Diagnostics.Debug.WriteLine(" search : " + search);

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
            dataTableData.data = FilterData(ref recordsFiltered, start, length, search, searchLOGIN_ID, searchNAME, searchDT_EDIT, sortColumn, sortDirection);
            dataTableData.recordsFiltered = recordsFiltered;

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Password()
        {
            return View();
        }

        public ActionResult PasswordDet(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                PasswordModel rec = new PasswordModel
                {
                    txtLOGIN_ID = "",
                    txtNAME = "",
                    //txtPASSWORD = "",
                };

                ViewBag.FieldValue = rec;
                return View();
            }
            else
            {
                try
                {
                    sSQL = " SELECT * FROM mainpass where AUTOINC ='" + id + "'";
                    MySqlDataReader dr = conn1.ExecuteReader(sSQL);
                    if (dr.Read())
                    {
                        PasswordModel rec = new PasswordModel
                        {
                            txtLOGIN_ID = dr["LOGIN_ID"].ToString(),
                            txtNAME = dr["NAME"].ToString(),
                           // txtPASSWORD = dr["PASSWORD"].ToString(),
                        };

                        ViewBag.FieldValue = rec;
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult PasswordDet(string id, PasswordModel viewModel)
        {
            if (id == "Save")
            {

                int iPassword = proc.pPassConv(viewModel.txtPASSWORD.ToString());

                DateTime d1 = DateTime.Now;
                DateTime d2 = new DateTime(1980, 1, 1, 0, 0, 0);

                double dUpdate = (double)(d1.ToOADate() - d2.ToOADate());
                int iUpdatedPass = iPassword + (int)Math.Round(dUpdate);

                try
                {
                    sSQL = " SELECT * FROM mainpass where LOGIN_ID ='" + viewModel.txtLOGIN_ID + "'";
                    MySqlDataReader dr = conn1.ExecuteReader(sSQL);
                    if (dr.Read())
                    {
                        return Json(new { status = "fail", message = "Login ID already exists", fieldname = "LOGIN_ID" });
                    }
                    else
                    {
                        sSQL = " INSERT into mainpass" +
                                " (LOGIN_ID, NAME, PASSWORD,DATELASTUSE,EDIT_ID,DT_EDIT,CREATE_ID,DT_CREATE)" +
                                " values ( " +
                                "'" +  viewModel.txtLOGIN_ID + "', " +
                                "'" + viewModel.txtNAME + "', " +
                                "'" + iUpdatedPass + "', " +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                "'" + Session["USER_ID"] + "' , " +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                                "'" + Session["USER_ID"] + "', " +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' ) ";
                        conn1.execute(sSQL);

                        return Json(new { status = "saved", message = viewModel.txtLOGIN_ID });
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }

                return View();
            }
            else if (id == "Update")
            {
                string sUpdatePass;

                if (!string.IsNullOrEmpty(viewModel.txtPASSWORD))
                {
                    int iPassword = proc.pPassConv(viewModel.txtPASSWORD.ToString());

                    DateTime d1 = DateTime.Now;
                    DateTime d2 = new DateTime(1980, 1, 1, 0, 0, 0);

                    double dUpdate = (double)(d1.ToOADate() - d2.ToOADate());
                    int iUpdatedPass = iPassword + (int)Math.Round(dUpdate);

                    sUpdatePass = "PASSWORD=" + iUpdatedPass + ",";
                }
                else
                {
                    sUpdatePass = "";
                }

                sSQL =  " UPDATE mainpass set" +
                        " NAME= '" + viewModel.txtNAME + "'," +
                            sUpdatePass +
                        " DATELASTUSE='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                        " WHERE LOGIN_ID='" + viewModel.txtLOGIN_ID + "'";
                        conn1.execute(sSQL);

                return Json(new { status = "updated", message = viewModel.txtLOGIN_ID });
            }
            else
            {
                sSQL = " DELETE FROM mainpass" +
                        " WHERE LOGIN_ID = '" + viewModel.txtLOGIN_ID + "'";
                conn1.execute(sSQL);

                return Json(new { status = "deleted", message = viewModel.txtLOGIN_ID });
            }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(PasswordModel viewModel)
        {
            try
            {
                sSQL = " SELECT * FROM mainpass where LOGIN_ID ='" + viewModel.txtLOGIN_ID + "'";
                MySqlDataReader dr = conn1.ExecuteReader(sSQL);
                if (dr.Read())
                {
                    return Json(new { status = "success", message = "Successfully saved" });
                }
                else
                {
                    return Json(new { status = "fail", message = "Invalid Login ID", fieldname = "LOGIN_ID" });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return View();
        }
    }
}

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
    public class ICController : Controller
    {
        private ConDB conn = new ConDB();
        public Proc proc = new Proc();

        static int TOTAL_ROWS;

        static readonly List<DataItem> _data = CreateData();
       // static readonly List<DataItem> _dataGrp = CreateDataGrp();


        // GET: IC
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dash()
        {
            return View();
        }

        public ActionResult Group()
        {
            return View();
        }

        public ActionResult Location()
        {
            return View();
        }

        public ActionResult Stock()
        {
            return View();
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

        public class DataItem
        {
            public string AUTOINC { get; set; }
            public string STKCODE { get; set; }
            public string PART { get; set; }
            public string PART1 { get; set; }
            public string PART2 { get; set; }
            public string GRP_CODE { get; set; }
            public string GRP_PART { get; set; }
            public string LOC_CODE { get; set; }
            public string LOC_PART { get; set; }
            public string UNIT { get; set; }
            public string TAR_CODE { get; set; }
            public string TAR_PART { get; set; }
            public string COST { get; set; }
            public string S_PRICE { get; set; }
            public string AVG_COST { get; set; }
            public string REORDER { get; set; }
            public string MAXIMUM { get; set; }
            public string MINIMUM { get; set; }
            public string REMARK { get; set; }
            public string BALQTY { get; set; }
            public string BALVALUE { get; set; }
            public string DT_EDIT { get; set; }
            public string EDIT_ID { get; set; }
            public string VEN_CODE { get; set; }
            public string VEN_NAME { get; set; }
            public string DEB_CODE { get; set; }
            public string DEB_NAME { get; set; }
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

            string mainconn = ConfigurationManager.ConnectionStrings["MySQLConnection2"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sSQL = " SELECT * FROM icstk ";
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
                item.STKCODE = dr["STKCODE"].ToString();
                item.PART = dr["PART"].ToString();
                item.PART1 = dr["PART1"].ToString();
                list.Add(item);
            }
            mysqlconn.Close();

            return list;
        }

        private static List<DataItem> CreateDataGrp()
        {
            List<DataItem> listGrp = new List<DataItem>();

            string mainconn = ConfigurationManager.ConnectionStrings["MySQLConnection2"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sSQL = " SELECT * FROM icgrp ";
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
                item.GRP_CODE = dr["GRP_CODE"].ToString();
                item.GRP_PART = dr["GRP_PART"].ToString();
                item.DT_EDIT = dr["DT_EDIT"].ToString();
                listGrp.Add(item);
            }
            mysqlconn.Close();

            return listGrp;
        }

        private static List<DataItem> CreateDataLoc()
        {
            List<DataItem> listLoc = new List<DataItem>();

            string mainconn = ConfigurationManager.ConnectionStrings["MySQLConnection2"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sSQL = " SELECT * FROM icloc ";
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
                item.LOC_CODE = dr["LOC_CODE"].ToString();
                item.LOC_PART = dr["LOC_PART"].ToString();
                item.DT_EDIT = dr["DT_EDIT"].ToString();
                listLoc.Add(item);
            }
            mysqlconn.Close();

            return listLoc;
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
                        dataItem.STKCODE.ToUpper().Contains(search.ToUpper()) ||
                        dataItem.PART.ToString().Contains(search.ToUpper()) ||
                        dataItem.PART1.ToString().Contains(search.ToUpper())
                        )
                    {
                        list.Add(dataItem);
                    }
                }
            }

            // simulate sort
            //=== sortColumn need to change additional column
            if (sortColumn == 1)
            {   // sort LoginID
                list.Sort((x, y) => SortString(x.STKCODE, y.STKCODE, sortDirection));
            }
            else if (sortColumn == 2)
            {   // sort Name
                list.Sort((x, y) => SortString(x.PART, y.PART, sortDirection));
            }
            else if (sortColumn == 3)
            {   // sort DateCreated
                list.Sort((x, y) => SortString(x.PART1, y.PART1, sortDirection));
            }

            recordFiltered = list.Count;

            // get just one page of data
            list = list.GetRange(start, Math.Min(length, list.Count - start));

            return list;
        }

        private List<DataItem> FilterDataGrp(ref int recordFiltered, int start, int length, string search, int sortColumn, string sortDirection)
        {
            List<DataItem> _dataGrp = CreateDataGrp();

            List<DataItem> listGrp = new List<DataItem>();

            if (search == null)
            {
                listGrp = _dataGrp;
            }
            else
            {
                // simulate search
                foreach (DataItem dataItem in _dataGrp)
                {
                    if (
                        dataItem.GRP_CODE.ToUpper().Contains(search.ToUpper()) ||
                        dataItem.GRP_PART.ToString().Contains(search.ToUpper()) ||
                        dataItem.DT_EDIT.ToString().Contains(search.ToUpper())
                        )
                    {
                        listGrp.Add(dataItem);
                    }
                }
            }

            // simulate sort
            //=== sortColumn need to change additional column
            if (sortColumn == 1)
            {   // sort LoginID
                listGrp.Sort((x, y) => SortString(x.GRP_CODE, y.GRP_CODE, sortDirection));
            }
            else if (sortColumn == 2)
            {   // sort Name
                listGrp.Sort((x, y) => SortString(x.GRP_PART, y.GRP_PART, sortDirection));
            }
            else if (sortColumn == 3)
            {   // sort DateCreated
                listGrp.Sort((x, y) => SortString(x.DT_EDIT, y.DT_EDIT, sortDirection));
            }

            recordFiltered = listGrp.Count;

            // get just one page of data
            listGrp = listGrp.GetRange(start, Math.Min(length, listGrp.Count - start));

            return listGrp;
        }

        private List<DataItem> FilterDataLoc(ref int recordFiltered, int start, int length, string search, int sortColumn, string sortDirection)
        {
            List<DataItem> _dataLoc = CreateDataLoc();

            List<DataItem> listLoc = new List<DataItem>();

            if (search == null)
            {
                listLoc = _dataLoc;
            }
            else
            {
                // simulate search
                foreach (DataItem dataItem in _dataLoc)
                {
                    if (
                        dataItem.LOC_CODE.ToUpper().Contains(search.ToUpper()) ||
                        dataItem.LOC_PART.ToString().Contains(search.ToUpper()) ||
                        dataItem.DT_EDIT.ToString().Contains(search.ToUpper())
                        )
                    {
                        listLoc.Add(dataItem);
                    }
                }
            }

            // simulate sort
            //=== sortColumn need to change additional column
            if (sortColumn == 1)
            {   // sort LoginID
                listLoc.Sort((x, y) => SortString(x.LOC_CODE, y.LOC_CODE, sortDirection));
            }
            else if (sortColumn == 2)
            {   // sort Name
                listLoc.Sort((x, y) => SortString(x.LOC_PART, y.LOC_PART, sortDirection));
            }
            else if (sortColumn == 3)
            {   // sort DateCreated
                listLoc.Sort((x, y) => SortString(x.DT_EDIT, y.DT_EDIT, sortDirection));
            }

            recordFiltered = listLoc.Count;

            // get just one page of data
            listLoc = listLoc.GetRange(start, Math.Min(length, listLoc.Count - start));

            return listLoc;
        }

        public ActionResult AjaxGetJsonDataGrp(int draw, int start, int length)
        {
            var searchParam = Request.QueryString["columns[1]search[value]"];
            System.Diagnostics.Debug.WriteLine(" Search Param : ");
            System.Diagnostics.Debug.WriteLine(searchParam);
            System.Diagnostics.Debug.WriteLine(" Search : ");
            string search = Request["search[value]"];

            System.Diagnostics.Debug.WriteLine(search);
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
            dataTableData.data = FilterDataGrp(ref recordsFiltered, start, length, search, sortColumn, sortDirection);
            dataTableData.recordsFiltered = recordsFiltered;

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxGetJsonDataLoc(int draw, int start, int length)
        {
            string search = Request["search[value]"];

            System.Diagnostics.Debug.WriteLine(search);
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
            dataTableData.data = FilterDataLoc(ref recordsFiltered, start, length, search, sortColumn, sortDirection);
            dataTableData.recordsFiltered = recordsFiltered;

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        // this ajax function is called by the client for each draw of the information on the page (i.e. when paging, ordering, searching, etc.). 
        public ActionResult AjaxGetJsonData(int draw, int start, int length)
        {
            var searchParam = Request.QueryString["columns[1]search[value]"];
            System.Diagnostics.Debug.WriteLine(" Search Param : ");
            System.Diagnostics.Debug.WriteLine(searchParam);
            System.Diagnostics.Debug.WriteLine(" Search : ");
            string search = Request["search[value]"];

            System.Diagnostics.Debug.WriteLine(search);
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

        public ActionResult GroupDet(string id)
        {
            if (id is null)
            {

                ICModel rec = new ICModel
                {
                    GRP_CODE = "",
                    GRP_PART = "",
                    DT_EDIT = "",
                };

                ViewBag.FieldValue = rec;
                return View();
            }
            else
            {
                ConDB2 conn = new ConDB2();
                string sSQL = " SELECT * FROM icgrp where AUTOINC ='" + id + "'";
                DataTable dt = conn.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    ICModel rec = new ICModel
                    {
                        GRP_CODE = dt.Rows[0]["GRP_CODE"].ToString(),
                        GRP_PART = dt.Rows[0]["GRP_PART"].ToString(),
                        DT_EDIT = dt.Rows[0]["DT_EDIT"].ToString(),
                    };

                    ViewBag.FieldValue = rec;
                    return View();

                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult GroupDet(string id, ICModel viewModel)
        {
            ConDB2 conn = new ConDB2();
            
            if (id == "Save")
            {
                string sSQL = " SELECT * FROM icgrp where GRP_CODE ='" + viewModel.GRP_CODE + "'";
                DataTable dt = conn.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    return Json(new { status = "fail", message = "Group Code already exists", fieldname = "GRP_CODE" });

                }
                else
                {
                    string constr = ConfigurationManager.ConnectionStrings["MySQLConnection2"].ConnectionString;
                    using (MySqlConnection con = new MySqlConnection(constr))
                    {
                        using (MySqlCommand cmd = new MySqlCommand(
                                " INSERT into icgrp" +
                                " (GRP_CODE, GRP_PART, EDIT_ID,DT_EDIT,CREATE_ID,DT_CREATE)" +
                                " values " +
                                " (@GRP_CODE,@GRP_PART,@EDIT_ID, @DT_EDIT, @CREATE_ID, @DT_CREATE)"
                            ))
                        {
                            using (MySqlDataAdapter sda = new MySqlDataAdapter())
                            {
                                cmd.Parameters.AddWithValue("@GRP_CODE", viewModel.GRP_CODE);
                                cmd.Parameters.AddWithValue("@GRP_PART", viewModel.GRP_PART);
                                cmd.Parameters.AddWithValue("@EDIT_ID", Session["USER_ID"]);
                                cmd.Parameters.AddWithValue("@DT_EDIT", DateTime.Now);
                                cmd.Parameters.AddWithValue("@CREATE_ID", Session["USER_ID"]);
                                cmd.Parameters.AddWithValue("@DT_CREATE", DateTime.Now);

                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                return Json(new { status = "saved", message = viewModel.GRP_CODE });

                            }
                        }
                    }
                }

            }
            else if (id == "Update")
            {
                string constr = ConfigurationManager.ConnectionStrings["MySQLConnection2"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand("UPDATE icgrp " +
                    "set GRP_PART=@GRP_PART, " +
                    "EDIT_ID=@EDIT_ID, " +
                    "DT_EDIT=@DT_EDIT " +
                    "WHERE GRP_CODE = @GRP_CODE"))
                    {
                        using (MySqlDataAdapter sda = new MySqlDataAdapter())
                        {
                            cmd.Parameters.AddWithValue("@GRP_PART", viewModel.GRP_PART);
                            cmd.Parameters.AddWithValue("@EDIT_ID", Session["USER_ID"]);
                            cmd.Parameters.AddWithValue("@DT_EDIT", DateTime.Now);
                            cmd.Parameters.AddWithValue("@GRP_CODE", viewModel.GRP_CODE);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            return Json(new { status = "updated", message = viewModel.GRP_CODE });

                        }
                    }
                    
                }
            }

            else
            {
                string constr = ConfigurationManager.ConnectionStrings["MySQLConnection2"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand("DELETE from icgrp " +
                            "WHERE GRP_CODE = @GRP_CODE"))
                    {
                        using (MySqlDataAdapter sda = new MySqlDataAdapter())
                        {
                            cmd.Parameters.AddWithValue("@GRP_CODE", viewModel.GRP_CODE);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            return Json(new { status = "deleted", message = viewModel.GRP_CODE });
                        }
                    }
                }
            }
        }

        public ActionResult LocationDet(string id)
        {
            if (id is null)
            {

                ICModel rec = new ICModel
                {
                    LOC_CODE = "",
                    LOC_PART = "",
                    DT_EDIT = "",
                };

                ViewBag.FieldValue = rec;
                return View();
            }
            else
            {
                ConDB2 conn = new ConDB2();
                string sSQL = " SELECT * FROM icloc where AUTOINC ='" + id + "'";
                DataTable dt = conn.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    ICModel rec = new ICModel
                    {
                        LOC_CODE = dt.Rows[0]["LOC_CODE"].ToString(),
                        LOC_PART = dt.Rows[0]["LOC_PART"].ToString(),
                        DT_EDIT = dt.Rows[0]["DT_EDIT"].ToString(),
                    };

                    ViewBag.FieldValue = rec;
                    return View();

                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult LocationDet(string id, ICModel viewModel)
        {
            ConDB2 conn = new ConDB2();

            if (id == "Save")
            {
                string sSQL = " SELECT * FROM icloc where LOC_CODE ='" + viewModel.LOC_CODE + "'";
                DataTable dt = conn.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    return Json(new { status = "fail", message = "Location Code already exists", fieldname = "LOC_CODE" });

                }
                else
                {
                    string constr = ConfigurationManager.ConnectionStrings["MySQLConnection2"].ConnectionString;
                    using (MySqlConnection con = new MySqlConnection(constr))
                    {
                        using (MySqlCommand cmd = new MySqlCommand(
                                " INSERT into icloc" +
                                " (LOC_CODE, LOC_PART, EDIT_ID,DT_EDIT,CREATE_ID,DT_CREATE)" +
                                " values " +
                                " (@LOC_CODE,@LOC_PART,@EDIT_ID, @DT_EDIT, @CREATE_ID, @DT_CREATE)"
                            ))
                        {
                            using (MySqlDataAdapter sda = new MySqlDataAdapter())
                            {
                                cmd.Parameters.AddWithValue("@LOC_CODE", viewModel.LOC_CODE);
                                cmd.Parameters.AddWithValue("@LOC_PART", viewModel.LOC_PART);
                                cmd.Parameters.AddWithValue("@EDIT_ID", Session["USER_ID"]);
                                cmd.Parameters.AddWithValue("@DT_EDIT", DateTime.Now);
                                cmd.Parameters.AddWithValue("@CREATE_ID", Session["USER_ID"]);
                                cmd.Parameters.AddWithValue("@DT_CREATE", DateTime.Now);

                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                return Json(new { status = "saved", message = viewModel.LOC_CODE });

                            }
                        }
                    }
                }

            }
            else if (id == "Update")
            {
                string constr = ConfigurationManager.ConnectionStrings["MySQLConnection2"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand("UPDATE icloc " +
                    "set LOC_PART=@LOC_PART, " +
                    "EDIT_ID=@EDIT_ID, " +
                    "DT_EDIT=@DT_EDIT " +
                    "WHERE LOC_CODE = @LOC_CODE"))
                    {
                        using (MySqlDataAdapter sda = new MySqlDataAdapter())
                        {
                            cmd.Parameters.AddWithValue("@LOC_PART", viewModel.LOC_PART);
                            cmd.Parameters.AddWithValue("@EDIT_ID", Session["USER_ID"]);
                            cmd.Parameters.AddWithValue("@DT_EDIT", DateTime.Now);
                            cmd.Parameters.AddWithValue("@LOC_CODE", viewModel.LOC_CODE);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            return Json(new { status = "updated", message = viewModel.LOC_CODE });

                        }
                    }

                }
            }

            else
            {
                string constr = ConfigurationManager.ConnectionStrings["MySQLConnection2"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand("DELETE from icloc " +
                            "WHERE LOC_CODE = @LOC_CODE"))
                    {
                        using (MySqlDataAdapter sda = new MySqlDataAdapter())
                        {
                            cmd.Parameters.AddWithValue("@LOC_CODE", viewModel.LOC_CODE);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            return Json(new { status = "deleted", message = viewModel.LOC_CODE });
                        }
                    }
                }
            }
        }

        public ActionResult StockDet(string id)
        {
            if (id is null)
            {

                ICModel rec = new ICModel
                {   AUTOINC = "",                    
                    STKCODE = "",
                    PART = "",
                    PART1 = "",
                    PART2 = "",
                    GRP_CODE = "",
                    GRP_PART = "",
                    LOC_CODE ="",
                    LOC_PART = "",
                };

                ViewBag.FieldValue = rec;
                return View();
            }
            else
            {
                //ConDB conn = new ConDB();
                ////Proc proc = new Proc();

                //string sSQL = " SELECT * FROM mainpass where AUTOINC ='" + id + "'";
                //DataTable dt = conn.GetData(sSQL);
                //if (dt.Rows.Count > 0)
                //{
                //    PasswordModel rec = new PasswordModel
                //    {
                //        txtLoginID = dt.Rows[0]["ID"].ToString(),
                //        txtName = dt.Rows[0]["NAME"].ToString(),
                //        txtPassword = dt.Rows[0]["PASSWORD"].ToString(),
                //    };

                //    ViewBag.FieldValue = rec;
                //    return View();

                //}
            }

            return View();
        }
    }
}
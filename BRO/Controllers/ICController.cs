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
        //private ConDB conn = new ConDB();
        private ConDB conn1 = new ConDB("MySQLConn1");
        private ConDB conn2 = new ConDB("MySQLConn2");
        //public Proc proc = new Proc();

        static int TOTAL_ROWS;
        static string TABLENAME;

        //static readonly List<DataItem> _data = CreateData();
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

        public ActionResult Currency(string id)
        {
            if (id == "Deb")
            {
                var sTableName = "arcurr";
                ViewBag.sTableName = sTableName;
                TABLENAME = sTableName;
                ViewBag.sHeaderName = "Debtor";
            }
            else
            {
                var sTableName = "apcurr";
                ViewBag.sTableName = sTableName;
                TABLENAME = sTableName;
                ViewBag.sHeaderName = "Vendor";
            }

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

        private int SortDouble(string s1, string s2, string sortDirection)
        {
            double d1 = double.Parse(s1);
            double d2 = double.Parse(s2);
            return sortDirection == "asc" ? d1.CompareTo(d2) : d2.CompareTo(d1);
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
            public string CURR_CODE { get; set; }
            public string CURR_PART { get; set; }
            public string RATE { get; set; }
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

            ConDB conn = new ConDB("MySQLConn2");
            string sSQL = " SELECT * FROM icstk ";
            DataTable dt = conn.GetData(sSQL);
            TOTAL_ROWS = dt.Rows.Count;

            using (MySqlDataReader dr = conn.ExecuteReader(sSQL))
            {
                while (dr.Read())
                {
                    DataItem item = new DataItem();

                    item.AUTOINC = dr["AUTOINC"].ToString();
                    item.STKCODE = dr["STKCODE"].ToString();
                    item.PART = dr["PART"].ToString();
                    item.PART1 = dr["PART1"].ToString();
                    list.Add(item);
                }
            }
            conn.Close();

            return list;
        }

        private static List<DataItem> CreateDataGrp()
        {
            List<DataItem> listGrp = new List<DataItem>();

            ConDB conn = new ConDB("MySQLConn1");
            string sSQL = " SELECT * FROM icgrp ";

            //public DataTable GetData(string sSQL)
            //{


            //    MySqlCommand command = new MySqlCommand(sSQL, connection);
            //    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            //    DataTable dt = new DataTable();
            //    adapter.Fill(dt);
            //    return dt;
            //}

            DataTable dt = conn.GetData(sSQL);

            TOTAL_ROWS = dt.Rows.Count;

            //mysqlconn.Open();
            //MySqlDataReader dr = conn.ExecuteQuery(sSQL);

            //string constr = ConfigurationManager.ConnectionStrings["MySQLConnection2"].ConnectionString;
            //using (MySqlConnection con = new MySqlConnection(constr))
            //{
            //    using (MySqlCommand cmd = new MySqlCommand("UPDATE " + TABLENAME +
            //    "set CURR_PART=@CURR_PART, " +
            //    "RATE = @RATE" +
            //    "EDIT_ID = @EDIT_ID, " +
            //    "DT_EDIT = @DT_EDIT " +
            //    "WHERE CURR_CODE = @CURR_CODE"))
            //    {
            //        using (MySqlDataAdapter sda = new MySqlDataAdapter())
            //        {
            //            cmd.Parameters.AddWithValue("@CURR_PART", viewModel.CURR_PART);
            //            cmd.Parameters.AddWithValue("@RATE", viewModel.RATE);
            //            cmd.Parameters.AddWithValue("@EDIT_ID", Session["USER_ID"]);
            //            cmd.Parameters.AddWithValue("@DT_EDIT", DateTime.Now);
            //            cmd.Parameters.AddWithValue("@CURR_CODE", viewModel.CURR_CODE);

            //            cmd.Connection = con;
            //            con.Open();
            //            cmd.ExecuteNonQuery();
            //            con.Close();

            //conn.Open();
            //MySqlDataReader dr = new MySqlDataAdapter();

            //String connectionString = "<THE CONNECTION STRING HERE>";
            //String sql = "SELECT * FROM students";
            //SqlCommand cmd = new SqlCommand(sql, conn);

            //var model = new List<Student>();
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    SqlDataReader rdr = cmd.ExecuteReader();
            //    while (rdr.Read())

            //using (MySqlConnection connection = new MySqlConnection(...))
            //{
            //    connection.Open();
            //    using (MySqlCommand cmd = new MySqlCommand("select product_price from product where product_name='@pname';", connection))
            //    {
            //        cmd.Parameters.AddWithValue("@pname", x);
            //        using (MySqlDataReader reader = cmd.ExecuteReader())
            //        {
            //            StringBuilder sb = new StringBuilder();
            //            while (reader.Read())
            //                sb.Append(reader.GetInt32(0).ToString());

            //            Price_label.Content = sb.ToString();
            //        }
            //    }
            //}
            //MySqlDataReader dr = conn.ExecuteReader(sSQL);
            //DataTable dt = conn.GetData(sSQL);
            using (MySqlDataReader dr = conn.ExecuteReader(sSQL))
            {
                while (dr.Read())
                {
                    DataItem item = new DataItem();

                    item.AUTOINC = dr["AUTOINC"].ToString();
                    item.GRP_CODE = dr["GRP_CODE"].ToString();
                    item.GRP_PART = dr["GRP_PART"].ToString();
                    item.DT_EDIT = dr["DT_EDIT"].ToString();
                    listGrp.Add(item);
                }
            }

            return listGrp;
        }

        private static List<DataItem> CreateDataLoc()
        {
            List<DataItem> listLoc = new List<DataItem>();

            ConDB conn = new ConDB("MySQLConn2");
            string sSQL = " SELECT * FROM icloc ";
            DataTable dt = conn.GetData(sSQL);
            TOTAL_ROWS = dt.Rows.Count;

            using (MySqlDataReader dr = conn.ExecuteReader(sSQL))
            {
                while (dr.Read())
                {
                    DataItem item = new DataItem();

                    item.AUTOINC = dr["AUTOINC"].ToString();
                    item.LOC_CODE = dr["LOC_CODE"].ToString();
                    item.LOC_PART = dr["LOC_PART"].ToString();
                    item.DT_EDIT = dr["DT_EDIT"].ToString();
                    listLoc.Add(item);
                }
            }
            conn.Close();

            return listLoc;
        }

        private static List<DataItem> CreateDataCurr(string sTableName)
        {
            List<DataItem> listCurr = new List<DataItem>();

            ConDB conn = new ConDB("MySQLConn2");
            string sSQL = " SELECT * FROM " + sTableName;
            DataTable dt = conn.GetData(sSQL);
            TOTAL_ROWS = dt.Rows.Count;

            using (MySqlDataReader dr = conn.ExecuteReader(sSQL))
            {
                DataItem item = new DataItem();

                item.AUTOINC = dr["AUTOINC"].ToString();
                item.CURR_CODE = dr["CURR_CODE"].ToString();
                item.CURR_PART = dr["CURR_PART"].ToString();
                item.RATE = dr["RATE"].ToString();
                item.DT_EDIT = dr["DT_EDIT"].ToString();
                listCurr.Add(item);
            }
            conn.Close();

            return listCurr;
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

        private List<DataItem> FilterDataCurr(ref int recordFiltered, int start, int length, string search, int sortColumn, string sortDirection, string sTableName)
        {
            List<DataItem> _dataCurr = CreateDataCurr(sTableName);

            List<DataItem> listCurr = new List<DataItem>();

            if (search == null)
            {
                listCurr = _dataCurr;
            }
            else
            {
                // simulate search
                foreach (DataItem dataItem in _dataCurr)
                {
                    if (
                        dataItem.CURR_CODE.ToUpper().Contains(search.ToUpper()) ||
                        dataItem.CURR_PART.ToString().Contains(search.ToUpper()) ||
                        dataItem.RATE.ToString().Contains(search.ToUpper()) ||
                        dataItem.DT_EDIT.ToString().Contains(search.ToUpper())
                        )
                    {
                        listCurr.Add(dataItem);
                    }
                }
            }

            // simulate sort
            //=== sortColumn need to change additional column
            if (sortColumn == 1)
            {   // sort LoginID
                listCurr.Sort((x, y) => SortString(x.CURR_CODE, y.CURR_CODE, sortDirection));
            }
            else if (sortColumn == 2)
            {   // sort Name
                listCurr.Sort((x, y) => SortString(x.CURR_PART, y.CURR_PART, sortDirection));
            }
            else if (sortColumn == 3)
            {   // sort Double
                listCurr.Sort((x, y) => SortDouble(x.RATE, y.RATE, sortDirection));
            }
            else if (sortColumn == 4)
            {   // sort DateCreated
                listCurr.Sort((x, y) => SortString(x.DT_EDIT, y.DT_EDIT, sortDirection));
            }

            recordFiltered = listCurr.Count;

            // get just one page of data
            listCurr = listCurr.GetRange(start, Math.Min(length, listCurr.Count - start));

            return listCurr;
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

        public ActionResult AjaxGetJsonDataCurr(int draw, int start, int length, string sTableName)
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
            dataTableData.data = FilterDataCurr(ref recordsFiltered, start, length, search, sortColumn, sortDirection, TABLENAME);
            dataTableData.recordsFiltered = recordsFiltered;

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GroupDet(string id)
        {
            if (string.IsNullOrEmpty(id))
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
                ConDB conn = new ConDB("MySQLConn2");
                string sSQL = " SELECT * FROM icgrp where AUTOINC ='" + id + "'";
                using (MySqlDataReader reader = conn.ExecuteReader(sSQL))
                {
                    if(reader.Read()) // If you're expecting more than one line, change this to while(reader.Read()).
                    {
                        ICModel rec = new ICModel
                        {
                            GRP_CODE = reader["GRP_CODE"].ToString(),
                            GRP_PART = reader["GRP_PART"].ToString(),
                            DT_EDIT = reader["DT_EDIT"].ToString(),
                        };

                        ViewBag.FieldValue = rec;
                        return View();
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult GroupDet(string id, ICModel viewModel)
        {
            if (id == "Save")
            {
                string sSQL = " SELECT * FROM icgrp where GRP_CODE ='" + viewModel.GRP_CODE + "'";
                using (MySqlDataReader reader = conn2.ExecuteReader(sSQL))
                {
                    if (reader.Read())
                    {
                        return Json(new { status = "fail", message = "Group Code already exists", fieldname = "GRP_CODE" });
                    }
                    else
                    {
                        sSQL =  " INSERT into icgrp" +
                                " (GRP_CODE, GRP_PART, EDIT_ID,DT_EDIT,CREATE_ID,DT_CREATE)" +
                                " values " +
                                " (@GRP_CODE,@GRP_PART,@EDIT_ID, @DT_EDIT, @CREATE_ID, @DT_CREATE)";
                        public void CreateMySqlCommand(string myExecuteQuery, MySqlConnection myConnection)
                        {
                            MySqlCommand myCommand = new MySqlCommand(myExecuteQuery, myConnection);
                            myCommand.Connection.Open();
                            myCommand.ExecuteNonQuery();
                            myConnection.Close();
                        }
                    }
                }
               
                if (dt.Rows.Count > 0)
                {
                    

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
            if (string.IsNullOrEmpty(id))
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
                ConDB conn = new ConDB("MySQLConn2");
                string sSQL = " SELECT * FROM icloc where AUTOINC ='" + id + "'";
                using (MySqlDataReader reader = conn.ExecuteReader(sSQL))
                {
                    if (reader.Read()) // If you're expecting more than one line, change this to while(reader.Read()).
                    {
                        ICModel rec = new ICModel
                        {
                            LOC_CODE = reader["LOC_CODE"].ToString(),
                            LOC_PART = reader["LOC_PART"].ToString(),
                            DT_EDIT = reader["DT_EDIT"].ToString(),
                        };

                        ViewBag.FieldValue = rec;
                        return View();
                    }
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

        public ActionResult CurrencyDet(string id, string tableName)
        {
            TABLENAME = tableName;

            if (TABLENAME == "arcurr")
            {
                ViewBag.Title = "Debtor Currency Details";
            }
            else
            {
                ViewBag.Title = "Vendor Currency Details";
                //ViewBag.sDebOrVen = "Ven";
            }

            if (string.IsNullOrEmpty(id))
            {

                ICModel rec = new ICModel
                {
                    CURR_CODE = "",
                    CURR_PART = "",
                    RATE = "",
                    DT_EDIT = "",
                };

                ViewBag.FieldValue = rec;
                return View();
            }
            else
            {
                ConDB conn = new ConDB("MySQLConn2");
                string sSQL = " SELECT * FROM " + TABLENAME +
                                "where AUTOINC ='" + id + "'";
                using (MySqlDataReader reader = conn.ExecuteReader(sSQL))
                {
                    if (reader.Read()) // If you're expecting more than one line, change this to while(reader.Read()).
                    {
                        ICModel rec = new ICModel
                        {
                            CURR_CODE = reader["CURR_CODE"].ToString(),
                            CURR_PART = reader["CURR_PART"].ToString(),
                            RATE = reader["CURR_PART"].ToString(),
                            DT_EDIT = reader["DT_EDIT"].ToString(),
                        };

                        ViewBag.FieldValue = rec;
                        return View();
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult CurrencyDet(string id, ICModel viewModel)
        //public ActionResult CurrencyDet(string id, string sTableName, ICModel viewModel)
        {
            ConDB2 conn = new ConDB2();
            string sDebOrVen;

            if (TABLENAME == "arcurr")
            {
                sDebOrVen = "Deb";
            }
            else
            {
                sDebOrVen = "Ven";
            }

            if (id == "Save")
            {
                string sSQL = " SELECT * FROM " + TABLENAME +
                    " where CURR_CODE ='" + viewModel.CURR_CODE + "'";
                DataTable dt = conn.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    return Json(new { status = "fail", message = "Currency Code already exists", fieldname = "CURR_CODE" });

                }
                else
                {
                    string constr = ConfigurationManager.ConnectionStrings["MySQLConnection2"].ConnectionString;
                    using (MySqlConnection con = new MySqlConnection(constr))
                    {
                        using (MySqlCommand cmd = new MySqlCommand(
                                " INSERT into " + TABLENAME +
                                " (CURR_CODE, CURR_PART, RATE, EDIT_ID,DT_EDIT,CREATE_ID,DT_CREATE)" +
                                " values " +
                                " (@CURR_CODE,@CURR_PART,@RATE, @EDIT_ID, @DT_EDIT, @CREATE_ID, @DT_CREATE)"
                            ))
                        {
                            using (MySqlDataAdapter sda = new MySqlDataAdapter())
                            {
                                cmd.Parameters.AddWithValue("@CURR_CODE", viewModel.CURR_CODE);
                                cmd.Parameters.AddWithValue("@CURR_PART", viewModel.CURR_PART);
                                cmd.Parameters.AddWithValue("@RATE", double.Parse(viewModel.RATE));
                                cmd.Parameters.AddWithValue("@EDIT_ID", Session["USER_ID"]);
                                cmd.Parameters.AddWithValue("@DT_EDIT", DateTime.Now);
                                cmd.Parameters.AddWithValue("@CREATE_ID", Session["USER_ID"]);
                                cmd.Parameters.AddWithValue("@DT_CREATE", DateTime.Now);

                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                return Json(new { status = "saved", message = viewModel.CURR_CODE, DebOrVen=sDebOrVen});

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
                    using (MySqlCommand cmd = new MySqlCommand("UPDATE " + TABLENAME +
                    "set CURR_PART=@CURR_PART, " +
                    "RATE = @RATE" +
                    "EDIT_ID = @EDIT_ID, " +
                    "DT_EDIT = @DT_EDIT " +
                    "WHERE CURR_CODE = @CURR_CODE"))
                    {
                        using (MySqlDataAdapter sda = new MySqlDataAdapter())
                        {
                            cmd.Parameters.AddWithValue("@CURR_PART", viewModel.CURR_PART);
                            cmd.Parameters.AddWithValue("@RATE", viewModel.RATE);
                            cmd.Parameters.AddWithValue("@EDIT_ID", Session["USER_ID"]);
                            cmd.Parameters.AddWithValue("@DT_EDIT", DateTime.Now);
                            cmd.Parameters.AddWithValue("@CURR_CODE", viewModel.CURR_CODE);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            return Json(new { status = "updated", message = viewModel.CURR_CODE, DebOrVen = sDebOrVen });

                        }
                    }

                }
            }

            else
            {
                string constr = ConfigurationManager.ConnectionStrings["MySQLConnection2"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand("DELETE from " + TABLENAME +
                                                                "WHERE CURR_CODE = @CURR_CODE"))
                    {
                        using (MySqlDataAdapter sda = new MySqlDataAdapter())
                        {
                            cmd.Parameters.AddWithValue("@CURR_CODE", viewModel.CURR_CODE);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            return Json(new { status = "deleted", message = viewModel.CURR_CODE, DebOrVen = sDebOrVen });
                        }
                    }
                }
            }
        }


        public ActionResult StockDet(string id)
        {
            if (string.IsNullOrEmpty(id))
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
                ConDB conn = new ConDB("MySQLConn2");
                string sSQL = " SELECT * FROM icstk " +
                                "where AUTOINC ='" + id + "'";
                using (MySqlDataReader reader = conn.ExecuteReader(sSQL))
                {
                    if (reader.Read()) // If you're expecting more than one line, change this to while(reader.Read()).
                    {
                        ICModel rec = new ICModel
                        {
                            STKCODE = reader["STKCODE"].ToString(),
                            PART = reader["PART"].ToString(),
                            DT_EDIT = reader["DT_EDIT"].ToString(),
                        };

                        ViewBag.FieldValue = rec;
                        return View();
                    }
                }
            }

            return View();
        }
    }
}
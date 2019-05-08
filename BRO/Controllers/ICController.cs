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

        // GET: IC
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dash()
        {
            return View();
        }

        public ActionResult Stock()
        {
            return View();
        }

        public class DataItem
        {
            public string AUTOINC { get; set; }
            public string STKCODE { get; set; }
            public string PART { get; set; }
            public string PART1 { get; set; }
            public string PART2 { get; set; }
            public string UNIT { get; set; }
            public string LOC { get; set; }
            public string GRP { get; set; }
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

        public ActionResult StockDet(string id)
        {
            if (id is null)
            {

                ICModel rec = new ICModel
                {
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
                ConDB conn = new ConDB();
                //Proc proc = new Proc();

                string sSQL = " SELECT * FROM mainpass where AUTOINC ='" + id + "'";
                DataTable dt = conn.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    PasswordModel rec = new PasswordModel
                    {
                        txtLoginID = dt.Rows[0]["ID"].ToString(),
                        txtName = dt.Rows[0]["NAME"].ToString(),
                        txtPassword = dt.Rows[0]["PASSWORD"].ToString(),
                    };

                    ViewBag.FieldValue = rec;
                    return View();

                }
            }

            return View();
        }

        public ActionResult viewGRP()
        {
            return View();
        }
    }
}
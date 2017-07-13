using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ActionController : Controller
    {
        // GET: Action
        public ActionResult Index()
        {
            List<NameValueData> per = new List<NameValueData>();
            ViewBag.City = GetList();
            per = GetList();
            return View("ActionList",per);
        }

        public List<NameValueData> GetList()
        {
            string connstring = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            var listOfPerson = new List<NameValueData>();

                using (var connection = new SqlConnection(connstring))
                {
                    connection.Open();
                    string sql = "SELECT CityID AS Value,CityName AS Name FROM dbo.city";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var person = new NameValueData();
                                person.Value = Convert.ToInt16(reader["Value"].ToString());
                                person.Name = reader["Name"].ToString();

                            listOfPerson.Add(person);
                            }
                        }
                    }
                }

                return listOfPerson;
        }


        public JsonResult Save(string cityName,int stateID,int cityID)
        {
            string conncetionstring = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            using(var conn= new SqlConnection(conncetionstring))
            {
                conn.Open();

                string query = "INSERT INTO dbo.city (CityID,CityName,StateID)";
                query += " VALUES (@CityID,@CityName, @StateID)";

                SqlCommand myCommand = new SqlCommand(query, conn);

                myCommand.Parameters.AddWithValue("@CityID", cityID);
                myCommand.Parameters.AddWithValue("@CityName",cityName);
                myCommand.Parameters.AddWithValue("@StateID",stateID);
                // ... other parameters
                myCommand.ExecuteNonQuery();
            }
            
            //var savedSuccess = "City Saved Successfully";
            return Json(new { responseText = "The city saved successfully." }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// delete the selected record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int cityID)
        {
            string connstring = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            using(var conn= new SqlConnection(connstring))
            {
                conn.Open();
                string sqlQuery = "Delete from dbo.city where CityID=@cityID";
                using (var command = new SqlCommand(sqlQuery, conn))
                {
                    command.Parameters.AddWithValue("@cityID",cityID);
                    command.ExecuteNonQuery();
                }
            }
            return Json(new { responseText = "The city deleted successfully." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int cityID)
        {
            ViewBag.City = GetList();
            string connstring = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            var listOfPerson = new List<City>();
            var person = new City();
            using (var connection = new SqlConnection(connstring))
            {
                connection.Open();
                
                string sql = "SELECT CityID,CityName,StateID FROM dbo.city where CityID=@CityID";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CityID", cityID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            person.CityID = Convert.ToInt16(reader["CityID"].ToString());
                            person.CityName = reader["CityName"].ToString();
                            person.StateID = Convert.ToInt16(reader["StateID"].ToString());
                        }
                    }
                }
            }

            return Json(person,JsonRequestBehavior.AllowGet);
        }
    }
}
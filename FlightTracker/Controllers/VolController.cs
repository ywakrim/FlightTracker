using FlightTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FlightTracker.Controllers
{
    public class VolController : Controller
    {
        // GET: Vol
        public ActionResult Index()
        {
            return View(GetVols());
        }

        // GET: Vol/Details/5
        public ActionResult Details(int id)
        {
            var volToGet = GetVols().Where(v => v.id == id).FirstOrDefault();
            var c_depart = volToGet.coordonneesGPS_Dep.Split(';');
            var c_destination = volToGet.coordonneesGPS_Des.Split(';');
            ViewBag.message = 0;
            if (volToGet.coordonneesGPS_Dep != string.Empty && volToGet.coordonneesGPS_Des != string.Empty)
            {
                double distance = utility.GeoDataTools.distance(double.Parse(c_depart[0]), double.Parse(c_depart[1]),
                                                                        double.Parse(c_destination[1]), double.Parse(c_destination[1]), 'K');
                ViewBag.message = (distance * volToGet.consomation_Avion) + volToGet.effort_decolage;
            }
            return View(volToGet);
        }

        // GET: Vol/Create
        public ActionResult Create(Vol newVol)
        {
            
            return View(new Vol());
        }

        // POST: Vol/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Vol newVol)
        {
            try
            {
                // TODO: Add insert logic here
                addVol(newVol);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vol/Edit/5
        public ActionResult Edit(int id, Vol volToEdit)
        {
            var listVol = GetVols();

            return View(listVol.Where(v => v.id == id).FirstOrDefault());

            //return View();
        }

        // POST: Vol/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Vol editedVol)
        {
            try
            {
                // TODO: Add update logic here

                updateVol(editedVol);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void updateVol(Vol newVol)
        {
            var volToEditLine = "\r\n" + newVol.id + "|" + newVol.name + "|" + newVol.aereport_Dep + "|" + newVol.coordonneesGPS_Dep + "|" + newVol.aereport_Des + "|" + newVol.coordonneesGPS_Des + "|" + newVol.consomation_Avion + "|" + newVol.effort_decolage;
            StringBuilder sb = new StringBuilder();
            string filePath = HttpContext.Server.MapPath(@"~/App_Data/VolData.csv");
            using (var streamReader = new System.IO.StreamReader(filePath))
            {
                string line;
                while (( line = streamReader.ReadLine())!=null  )
                {
                    if (line.Split('|')[0] == newVol.id.ToString())
                        sb.Append(volToEditLine);
                    else
                        sb.Append("\r\n").Append(line);
                }
            }
            System.IO.File.WriteAllText(filePath, sb.ToString());
        }

        // GET: Vol/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Vol/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private IEnumerable<Vol> GetVols()
        {
            var csvConfig = new CsvHelper.Configuration.Configuration();
            csvConfig.Delimiter = "|";
            using (var fileReader = System.IO.File.OpenText(HttpContext.Server.MapPath(@"~/App_Data/VolData.csv")))
            using (var csvReader = new CsvHelper.CsvReader(fileReader, csvConfig))
            {
                return csvReader.GetRecords<Vol>().ToList();
            }
        }

        private void addVol(Vol newVol)
        {
            int newVolId = GetVols().OrderByDescending(v => v.id).First().id + 1;
            var newVolLine = "\r\n" + newVolId +  "|" + newVol.name + "|" + newVol.aereport_Dep + "|" + newVol.coordonneesGPS_Dep + "|" + newVol.aereport_Des + "|" + newVol.coordonneesGPS_Des + "|" + newVol.consomation_Avion + "|" + newVol.effort_decolage;
            var filePath = HttpContext.Server.MapPath(@"~/App_Data/VolData.csv");
            var oldFileText = System.IO.File.ReadAllText(filePath);
            var newFileText = oldFileText + newVolLine;
            System.IO.File.WriteAllText(filePath, newFileText);
        }
    }
}

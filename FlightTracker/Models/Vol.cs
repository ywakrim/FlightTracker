using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlightTracker.Models
{
    public class Vol
    {
        /// <summary>
        /// identifiant du Vol
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Nom du Vol
        /// </summary> 
        [Display(Name = "Nom du Vol")]
        public string name { get; set; }
        /// <summary>
        /// Aereport de départ
        /// </summary>
        [Display(Name = "Aereport de départ")]
        public string aereport_Dep { get; set; }
        /// <summary>
        /// Coordonnées GPS de l'aereport de départ
        /// format : Latitude;Longitude
        /// </summary>
        [Display(Name = "Coordonnées GPS de l'aereport de départ")]
        public string coordonneesGPS_Dep { get; set; }
        /// <summary>
        /// Aereport de destination
        /// </summary>
        [Display(Name = "Aereport de destination")]
        public string aereport_Des { get; set; }
        /// <summary>
        /// Coordonnées GPS de l'aereport de destination
        /// format : Latitude;Longitude
        /// </summary>
        [Display(Name = "Coordonnées GPS de l'aereport de destination (Latitude;Longitude)")]
        public string coordonneesGPS_Des { get; set; }
        /// <summary>
        /// Consomation par kilometre de l'avion
        /// </summary>
        [Display(Name = "Consomation de l'avion (L/Km)")]
        public int consomation_Avion { get; set; }
        /// <summary>
        /// Effort de décolage de l'avion 
        /// nombre de litre nécessaire pour le décollag de l'avion
        /// </summary>
        [Display(Name = "Effort de décolage de l'avion (Litre)")]
        public int effort_decolage { get; set; }
    }
}
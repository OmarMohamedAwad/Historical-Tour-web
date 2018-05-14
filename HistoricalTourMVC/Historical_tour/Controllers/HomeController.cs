using Historical_tour.Models;
using HistoricTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Historical_tour.Controllers
{
    public class HomeController : Controller
    {
        HistoricTourDataContext DB = new HistoricTourDataContext();

        [HttpGet]
        public ActionResult Index()
        {

            List<Object> Models = new List<object>();

            List<placeClass> places = placeClass.ConvertToList(DB.Places.Where(p => p.GovernorateID == 1)).OrderBy(o=>o.Rate).ToList();

            List<placeClass> placesFour = new List<placeClass>();

            for (int i = 0; i < 4; i++)
            {
                try
                {
                    placesFour.Add(places.ElementAt(i));
                }
                catch
                {
                    break;
                }
            }
            

            for (int i = 0; i < placesFour.Count; i++)
            {
                int placeID = placesFour.ElementAt(i).Id;

                placesFour.ElementAt(i).Reviews = DB.PlaceReviews.Where(r => r.PlaceID == placeID).ToList();
                placesFour.ElementAt(i).WorkingDays = DB.PlaceDays.Where(r => r.PlaceID == placeID).ToList();
                placesFour.ElementAt(i).Images = DB.PlaceImages.Where(r => r.PlaceID == placeID).ToList();
            }

            List<hotelClass> hotels = hotelClass.ConvertToList(DB.Hotels).OrderBy(o => o.Rate).ToList();

            List<hotelClass> hotelsFive = new List<hotelClass>();

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    hotelsFive.Add(hotels.ElementAt(i));
                }
                catch
                {
                    break;

                }
            }

            for (int i = 0; i < hotelsFive.Count; i++)
            {
                int hotelId = hotelsFive.ElementAt(i).Id;

                hotelsFive.ElementAt(i).Reviews = DB.HotelReviews.Where(r => r.HotelID == hotelId).ToList();
                hotelsFive.ElementAt(i).Images = DB.HotelImages.Where(r => r.HotelID == hotelId).ToList();
            }


            List<TourGuide> tourguides = DB.TourGuides.ToList();

            List<TourGuide> tourguidesFour = new List<TourGuide>();

            for (int i = 0; i < 4; i++)
            {
                try
                {
                    tourguidesFour.Add(tourguides.ElementAt(i));
                }
                catch
                {
                    break;

                }
            }

            Models.Add(placesFour);
            Models.Add(hotelsFive);
            Models.Add(tourguidesFour);

            return View(Models);

        }


    }
}
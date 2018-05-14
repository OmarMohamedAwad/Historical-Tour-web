using Historical_tour.Models;
using HistoricTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Historical_tour.Controllers
{
    public class PlaceController : Controller
    {

        public static HistoricTourDataContext DB = new HistoricTourDataContext();

        public static List<placeClass> places = placeClass.ConvertToList(DB.Places.Where(p => p.GovernorateID == 1));

        List<List<placeClass>> placesArr = new List<List<placeClass>>();

        [HttpGet]
        public ActionResult GetPlace(int id)
        {
            placeClass place = placeClass.ConvertToObject(DB.Places.Where(p => p.ID == id).SingleOrDefault());

            place.Reviews = DB.PlaceReviews.Where(r => r.PlaceID == id).ToList();
            place.WorkingDays = DB.PlaceDays.Where(r => r.PlaceID == id).ToList();
            place.Images = DB.PlaceImages.Where(r => r.PlaceID == id).ToList();

            return View(place);

        }


        [HttpGet]
        public ActionResult GetPlaces(int id, int? type)
        {
            List<placeClass> places = placeClass.ConvertToList(DB.Places.Where(p => p.GovernorateID == id));

            for (int i = 0; i < places.Count; i++)
            {
                int placeID = places.ElementAt(i).Id;

                places.ElementAt(i).Reviews = DB.PlaceReviews.Where(r => r.PlaceID == placeID).ToList();
                places.ElementAt(i).WorkingDays = DB.PlaceDays.Where(r => r.PlaceID == placeID).ToList();
                places.ElementAt(i).Images = DB.PlaceImages.Where(r => r.PlaceID == placeID).ToList();
            }


            int counter = 0;

            for (int i = 0; i < Math.Ceiling((decimal)places.Count / 4); i++)
            {
                List<placeClass> arr = new List<placeClass>();

                for (int j = 0; j < 4; j++)
                {
                    try
                    {
                        arr.Add(places.ElementAt(counter));
                        counter++;
                    }
                    catch
                    {
                        break;
                    }

                }

                placesArr.Add(arr);
            }



            if (type == 1 ||type==null)
            {
                return View(placesArr);
            }
            else
            {
                return Json(placesArr, JsonRequestBehavior.AllowGet);
            }

        }
        //sort
        public ActionResult Sorting(int id)
        {

            List<placeClass> places = placeClass.ConvertToList(DB.Places);


            for (int i = 0; i < places.Count; i++)
            {
                int placeId = places.ElementAt(i).Id;
                places.ElementAt(i).Reviews = DB.PlaceReviews.Where(r => r.PlaceID == placeId).ToList();
                places.ElementAt(i).Images = DB.PlaceImages.Where(r => r.PlaceID == placeId).ToList();
            }

            List<placeClass> OrderdPlaces;

            if (id == 1)
            {
                OrderdPlaces = places.OrderBy(e => e.Id).ToList();
            }
            else if (id == 2)
            {
                OrderdPlaces = places.OrderByDescending(e => e.Id).ToList();
            }
            else if (id == 3)
            {
                OrderdPlaces = places.OrderBy(e => e.Rate).ToList();
            }
            else
            {
                OrderdPlaces = places.OrderByDescending(e => e.Rate).ToList();

            }



            int counter = 0;

            for (int i = 0; i < Math.Ceiling((decimal)OrderdPlaces.Count / 4); i++)
            {
                List<placeClass> arr = new List<placeClass>();

                for (int j = 0; j < 4; j++)
                {
                    try
                    {
                        arr.Add(OrderdPlaces.ElementAt(counter));
                        counter++;
                    }
                    catch
                    {
                        break;
                    }

                }

                placesArr.Add(arr);
            }


            return View("Getplaces", placesArr);


        }

    }
}
using Historical_tour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Historical_tour.Controllers
{
    public class TourGuideController : Controller
    {
        private HistoricTourDataContext DB = new HistoricTourDataContext();

        [HttpGet]
        public ActionResult GetTourguide(int id)
        {
            TourGuide tourguide = DB.TourGuides.Where(t => t.ID == id).SingleOrDefault();

            return View(tourguide);
        }


        [HttpGet]
        public ActionResult GetTourguides(int? type)
        {
            List<TourGuide> tourguides = DB.TourGuides.ToList();

            List<List<TourGuide>> toursArr = new List<List<TourGuide>>();

            int counter = 0;

            for (int i = 0; i < Math.Ceiling((decimal)tourguides.Count / 4); i++)
            {
                List<TourGuide> arr = new List<TourGuide>();

                for (int j = 0; j < 4; j++)
                {
                    try
                    {
                        arr.Add(tourguides.ElementAt(counter));
                        counter++;
                    }
                    catch
                    {
                        break;
                    }

                }

                toursArr.Add(arr);
            }



            if (type == 1 || type == null)
            {
                return View(toursArr);
            }
            else
            {
                return Json(toursArr, JsonRequestBehavior.AllowGet);
            }

            //if (type == 1)
            //{
            //    return View(tourguides);
            //}
            //else
            //{
            //    return Json(tourguides, JsonRequestBehavior.AllowGet);
            //}
            
        }

        [HttpGet]
        public ActionResult booking(int id)
        {
            TourGuide tourguide = DB.TourGuides.Where(t => t.ID == id).SingleOrDefault();

            return View(tourguide);
        }

        [HttpGet]
        public ActionResult sorting(int typeOrder)
        {
             List<TourGuide> tourguides = DB.TourGuides.ToList();

            List<List<TourGuide>> toursArr = new List<List<TourGuide>>();

            

            List<TourGuide> OrderdTourguides;

            int counter = 0;

            if (typeOrder == 1)
            {
                OrderdTourguides = tourguides.OrderBy(r => r.Rate).ToList();
            }
            else
            {
                OrderdTourguides = tourguides.OrderByDescending(r => r.Rate).ToList();
            }

            for (int i = 0; i < Math.Ceiling((decimal)OrderdTourguides.Count / 4); i++)
            {
                List<TourGuide> arr = new List<TourGuide>();

                for (int j = 0; j < 4; j++)
                {
                    try
                    {
                        arr.Add(OrderdTourguides.ElementAt(counter));
                        counter++;
                    }
                    catch
                    {
                        break;
                    }

                }

                toursArr.Add(arr);
            }


            return View("GetTourguides", toursArr);

        }

    }
}
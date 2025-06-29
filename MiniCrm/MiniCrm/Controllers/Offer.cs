using Dapper;
using Microsoft.AspNetCore.Mvc;
using MiniCrm.Models;

namespace MiniCrm.Controllers
{
    public class Offer : Controller
    {
        public IActionResult Index()
        {
            return View(DP.Listeleme<OfferModel>("OfferViewAll"));
        }

        public IActionResult EY(int id = 0)
        {
            if (id == 0)
                return View(new OfferModel());
            else
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
                var offer = DP.Listeleme<OfferModel>("OfferViewById", param).FirstOrDefault();
                if (offer == null)
                    offer = new OfferModel();
                return View(offer);
            }
        }

        [HttpPost]
        public IActionResult EY(OfferModel offer)
        {
            var param = new DynamicParameters();
            param.Add("@Id", offer.Id);
            param.Add("@CustomerId", offer.CustomerId);
            param.Add("@Title", offer.Title);
            param.Add("@Amount", offer.Amount);
            param.Add("@OfferDate", offer.OfferDate);
            param.Add("@Status", offer.Status);
            DP.ExecuteReturn("OfferUpsert", param);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id);
            DP.ExecuteReturn("OfferDelete", param);
            return RedirectToAction("Index");
        }
    }
}

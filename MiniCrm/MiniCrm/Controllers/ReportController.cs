using Dapper;
using Microsoft.AspNetCore.Mvc;
using MiniCrm.Models;

namespace MiniCrm.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult OfferSuccessRate()
        {
            var result = DP.Listeleme<dynamic>("GetOfferSuccessRate").FirstOrDefault();
            ViewBag.SuccessRate = result?.SuccessRate ?? 0;
            return View();
        }

        public IActionResult TotalSalesAndRevenue(DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue)
            {
                ViewBag.TotalSales = null;
                ViewBag.TotalRevenue = null;
                return View();
            }
            var param = new DynamicParameters();
            param.Add("@StartDate", startDate.Value);
            param.Add("@EndDate", endDate.Value);
            var result = DP.Listeleme<dynamic>("GetTotalSalesAndRevenue", param).FirstOrDefault();
            ViewBag.TotalSales = result?.TotalSales ?? 0;
            ViewBag.TotalRevenue = result?.TotalRevenue ?? 0;
            return View();
        }

        public IActionResult CustomerSales()
        {
            var result = DP.Listeleme<dynamic>("GetCustomerSales").ToList();
            return View(result);
        }
    }
}

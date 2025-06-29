using Dapper;
using Microsoft.AspNetCore.Mvc;
using MiniCrm.Models;

namespace MiniCrm.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View(DP.Listeleme<CustomerModel>("CustomerViewAll"));
        }

        public IActionResult EY(int id = 0)
        {
            if (id == 0)
                return View(new CustomerModel());
            else
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
                var customer = DP.Listeleme<CustomerModel>("CustomerViewById", param).FirstOrDefault();
                if (customer == null)
                    customer = new CustomerModel();
                return View(customer);
            }
        }

        [HttpPost]
        public IActionResult EY(CustomerModel customer)
        {
            var param = new DynamicParameters();
            param.Add("@Id", customer.Id);
            param.Add("@FirstName", customer.FirstName);
            param.Add("@LastName", customer.LastName);
            param.Add("@Company", customer.Company);
            param.Add("@Email", customer.Email);
            param.Add("@Phone", customer.Phone);
            DP.ExecuteReturn("CustomerUpsert", param);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id);
            DP.ExecuteReturn("CustomerDelete", param);
            return RedirectToAction("Index");
        }
    }
}

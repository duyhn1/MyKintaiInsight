using Mykintai_Indentỉy.AuthenticationFilters;
using Mykintai_Indentỉy.Models.Home;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mykintai_Indentỉy.Controllers
{
    [JwtAuthorize]
    public class HomeController : Controller
    {
        private readonly string getWorkplaceUrl = "https://api.fjpservice.com/api/dakoku/workplace";

        public async Task<ActionResult> Dashboard()
        {
            var accessToken = Request.Cookies["access_token"]?.Value;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync(getWorkplaceUrl);
            var result = await response.Content.ReadAsStringAsync();
            var workplace = JsonConvert.DeserializeObject<WorkplaceModel>(result);
            return View(workplace);
        }

        public ActionResult Overtime()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Welcome to my page";

            return View();
        }


    }
}
using Mykintai_Indentỉy.AuthenticationFilters;
using Mykintai_Indentỉy.Models.Home;
using Mykintai_Indentỉy.Models.HomeApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Mykintai_Indentỉy.Controllers
{
    public class HomeApiController : ApiController
    {
        private readonly string checkInOutUrl = "https://api.fjpservice.com/api/dakoku";
        private readonly string searchOvertimeUrl = "https://api.fjpservice.com/api/otrequest/search";
        private readonly string editOvertimeUrl = "https://api.fjpservice.com/api/otrequest/resubmit";

        [HttpPost]
        [Route("api/checkout")]
        public async Task<string> CheckOut(GeographicModel geo)
        {
            var authorization = Request.Headers.Authorization;
            var accessToken = authorization.Parameter;
            var data = new Dictionary<string, object>
            {
                { "canModify", false },
                { "checkinLatitude", 0 },
                { "checkinLocation", "" },
                { "checkinLongitute", 0 },
                { "checkinType", 0 },
                { "checkoutLatitude", geo.latitude },
                { "checkoutLocation", "" },
                { "checkoutLongitute", geo.longtitude },
                { "checkoutType", 1 },
                { "displayEditEndWorkingTime", "" },
                { "displayEditStartWorkingTime", "" },
                { "displayEndWorkingTime", "" },
                { "displayFixEndWorkingTime", "" },
                { "displayFixStartWorkingTime", "" },
                { "displayOrgEditEndWorkingTime", "" },
                { "displayOrgEditStartWorkingTime", "" },
                { "displayStartWorkingTime", "" },
                { "errorClass", "" },
                { "reason", "" }
            };
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsync(checkInOutUrl, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        [HttpPost]
        [Route("api/checkin")]
        public async Task<string> CheckIn(GeographicModel geo)
        {
            var authorization = Request.Headers.Authorization;
            var accessToken = authorization.Parameter;
            var data = new Dictionary<string, object>
            {
                { "canModify", false },
                { "checkinLatitude", geo.latitude },
                { "checkinLocation", "" },
                { "checkinLongitute", geo.longtitude },
                { "checkinType", 1 },
                { "checkoutLatitude", 0 },
                { "checkoutLocation", "" },
                { "checkoutLongitute", 0 },
                { "checkoutType", 0 },
                { "displayEditEndWorkingTime", "" },
                { "displayEditStartWorkingTime", "" },
                { "displayEndWorkingTime", "" },
                { "displayFixEndWorkingTime", "" },
                { "displayFixStartWorkingTime", "" },
                { "displayOrgEditEndWorkingTime", "" },
                { "displayOrgEditStartWorkingTime", "" },
                { "displayStartWorkingTime", "" },
                { "errorClass", "" },
                { "reason", "" }
            };
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsync(checkInOutUrl, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        [HttpPost]
        [Route("api/search")]
        public async Task<List<OvertimeModel>> Search(SearchOvertimeModel search)
        {
            var authorization = Request.Headers.Authorization;
            var accessToken = authorization.Parameter;
            var data = new
            {
                FromDate = search.dateStart,
                Status = search.status,
                ToDate = search.dateEnd
            };
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsync(searchOvertimeUrl, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsAsync<List<OvertimeModel>>();
            return result;
        }

        [HttpPut]
        [Route("api/edit")]
        public async Task<HttpResponseMessage> EditRequest(OvertimeModel edit)
        {
            var authorization = Request.Headers.Authorization;
            var accessToken = authorization.Parameter;
            var data = new
            {
                employeeId = edit.employeeId,
                endTime = edit.endTimeEdit,
                id = edit.id,
                reason = edit.reason,
                requestDate = edit.requestDate,
                startTime = edit.startTimeEdit,
                status = edit.status
            };
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsync(editOvertimeUrl, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            return response;
            //return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
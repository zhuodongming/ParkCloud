using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Park.Entity;
using Park.Rep;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Park.ParkApi.Filters
{
    public class AuthorizationAttribute : IAsyncAuthorizationFilter
    {
        private readonly ParkRep _parkRep;
        public AuthorizationAttribute(ParkRep parkRep)
        {
            _parkRep = parkRep;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;

            string authorization = request.Headers["Authorization"];

            string method = request.Method;
            string rawUrl = request.Path.Value + request.QueryString.Value;
            string date = request.Headers["Date"];
            string contentMD5 = String.Empty;
            using (StreamReader sr = new StreamReader(request.Body))
            {
                contentMD5 = Crypto.GetMD5(await sr.ReadToEndAsync());
            }

            if (authorization.StartsWith("PKC"))
            {
                string credential = authorization.Substring(authorization.LastIndexOf(' '));
                string[] split = credential.Split(':');
                if (split.Length == 2)
                {
                    string parkID = split[0];
                    string signingRequest = split[1];
                    ParkModel parkModel = await _parkRep.SingleOrDefaultByIdAsync(parkID);
                    if (parkModel != null)
                    {
                        string preSignstring = method + "\n"
                                            + contentMD5 + "\n"
                                            + date + "\n"
                                            + rawUrl + "\n";
                        string sign = Crypto.GetHMACSHA1(preSignstring, parkModel.park_key);
                        if (sign == signingRequest)
                        {
                            context.HttpContext.Items.Add("parkID", parkID);

                        }
                    }
                }
            }

            context.Result = new UnauthorizedResult();
        }
    }
}

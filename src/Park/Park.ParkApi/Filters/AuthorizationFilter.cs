using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Park.Entity;
using Park.Repository;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Park.ParkApi.Filters
{
    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;

            string method = request.Method;
            string rawUrl = request.Path.Value + request.QueryString.Value;
            string date = request.Headers["Date"];
            string content = null;
            if (request.Body.CanSeek)
            {
                request.Body.Position = 0;
                content = await new StreamReader(request.Body).ReadToEndAsync();
                request.Body.Position = 0;
            }

            if (AuthenticationHeaderValue.TryParse(request.Headers["Authorization"], out AuthenticationHeaderValue authorization))
            {
                if (String.Equals("PKC", authorization.Scheme, StringComparison.CurrentCultureIgnoreCase))
                {
                    string[] split = authorization.Parameter.Split(':');
                    string parkID = split[0];
                    string sign = split[1];
                    var parkEntity = await new ParkRep().SingleOrDefaultByIdAsync(parkID);
                    if (parkEntity != null)
                    {
                        string preString = method + "\n"
                                            + rawUrl + "\n"
                                            + date + "\n"
                                            + content + "\n";

                        string sha1 = Crypto.GetHMACSHA1(preString, parkEntity.park_key);
                        if (String.Equals(sign, sha1, StringComparison.CurrentCultureIgnoreCase))
                        {
                            context.HttpContext.Items.Add("ParkUser", parkEntity);
                            return;
                        }
                        else
                        {
                            Log.Warn($"{parkID}签名计算不通过");
                        }
                    }
                    else
                    {
                        Log.Warn($"{parkID}应用不存在");
                    }
                }
                else
                {
                    Log.Warn($"Authorization头格式不合法:{request.Headers["Authorization"]}");
                }
            }
            else
            {
                Log.Warn($"Authorization头格式不合法:{request.Headers["Authorization"]}");
            }

            context.Result = new UnauthorizedResult();
        }
    }
}

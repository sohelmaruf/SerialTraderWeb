using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Reflection;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net;
using WaterCons.Helpers;

using WaterCons.Library.Interfaces;
using WaterCons.Library.DataServices;
using WaterCons.Library.Business;
using WaterCons.Library.Entity;
using WaterCons.Library.Models;
using WaterCons.Library.Common;

namespace WaterCons.Filters
{
    /// <summary>
    /// Authenicate User
    /// </summary>
    public class WebApiAuthenicationAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
    public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
    {
      var request = actionContext.Request;
      var headers = request.Headers;
      if (!headers.Contains("X-Requested-With") || headers.GetValues("X-Requested-With").FirstOrDefault() != "XMLHttpRequest")
      {
        TransactionalInformation transactionInformation = new TransactionalInformation();
        transactionInformation.ReturnMessage.Add("Access has been denied.");
        transactionInformation.ReturnStatus = false;
        actionContext.Response = request.CreateResponse<TransactionalInformation>(HttpStatusCode.BadRequest, transactionInformation);
      }
      else
      {
        HttpContext ctx = default(HttpContext);
        ctx = HttpContext.Current;
        if (ctx.User.Identity.IsAuthenticated == false)
        {
          TransactionalInformation transactionInformation = new TransactionalInformation();
          transactionInformation.ReturnMessage.Add("Your session has expired.");
          transactionInformation.ReturnStatus = false;
          actionContext.Response = request.CreateResponse<TransactionalInformation>(HttpStatusCode.BadRequest, transactionInformation);
        }

      }
    }

  }
              
}
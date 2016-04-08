using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace AzureWebAppComponent
{
	public class BasicAuthAttribute : ActionFilterAttribute, IAuthenticationFilter
	{
		public void OnAuthentication(AuthenticationContext filterContext)
		{
		}

		public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
		{
			var user = filterContext.HttpContext.User;

			if (user == null || !user.Identity.IsAuthenticated)
			{
				filterContext.Result = new HttpUnauthorizedResult();
			}
		}
	}
}
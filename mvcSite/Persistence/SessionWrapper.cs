using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace mvcSite.Persistence
{
    public class SessionWrapper : ISessionWrapper
    {
        public HttpSessionState GetSession()
        {
            return HttpContext.Current.Session;
        }
    }

}
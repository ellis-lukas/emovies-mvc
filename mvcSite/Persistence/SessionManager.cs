using mvcSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace mvcSite.Persistence
{
    public class SessionManager
    {
        private readonly ISessionWrapper sessionWrapper;

        public SessionManager(ISessionWrapper sessionWrapper)
        {
            this.sessionWrapper = sessionWrapper;
        }

        public IEnumerable<OrderLine> NonZeroOrderLines
        {
            get { return sessionWrapper.GetSession()["NonZeroOrderLines"] as IEnumerable<OrderLine>; }
            set { sessionWrapper.GetSession()["NonZeroOrderLines"] = value; }
        }

        public Customer CustomerData
        {
            get { return sessionWrapper.GetSession()["CustomerData"] as Customer; }
            set { sessionWrapper.GetSession()["CustomerData"] = value; }
        }
    }
}
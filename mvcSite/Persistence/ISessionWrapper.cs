using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;

namespace mvcSite.Persistence
{
    public interface ISessionWrapper
    {
        HttpSessionState GetSession();
    }
}

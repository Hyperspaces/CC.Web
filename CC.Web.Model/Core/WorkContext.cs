using CC.Web.Model.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Model.Core
{
    public class WorkContext : IWorkContext
    {
        public User CurUser { get; set; }
    }
}

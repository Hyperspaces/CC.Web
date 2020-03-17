using CC.Web.Model.System;

namespace CC.Web.Model.Core
{
    public interface IWorkContext
    {
        User CurUser { get; set; }
    }
}
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CC.Web.Api.Core
{
    public class AutofacConfig
    {
        public static void RegisterObj(ContainerBuilder builder)
        {
            //注册Service中的组件,Service中的类要以Service结尾，否则注册失败
            builder.RegisterAssemblyTypes(GetAssemblyByName("CC.Web.Service")).Where(a => a.Name.EndsWith("Service")).AsImplementedInterfaces().PropertiesAutowired();
            //注册Dao中的组件,Dao中的类要以Dao结尾，否则注册失败
            builder.RegisterAssemblyTypes(GetAssemblyByName("CC.Web.Dao")).Where(a => a.Name.EndsWith("Dao")).AsImplementedInterfaces().PropertiesAutowired();
        }
        /// <summary>
        /// 根据程序集名称获取程序集
        /// </summary>
        /// <param name="AssemblyName">程序集名称</param>
        /// <returns></returns>
        public static Assembly GetAssemblyByName(String AssemblyName)
        {
            return Assembly.Load(AssemblyName);
        }
    }
}

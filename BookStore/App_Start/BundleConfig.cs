using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Using necessário para usarmos os bundles
using System.Web.Optimization;

namespace BookStore.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles
            (BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/site")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/js/site")
                .Include("~/Scripts/jquery-1.10.2.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/respond.js"));
        }
    }
}
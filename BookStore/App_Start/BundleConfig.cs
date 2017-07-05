using System.Web.Optimization;
//Using necessário para usarmos os bundles

namespace BookStore
{
    public class BundleConfig
    {
        public static void RegisterBundles
            (BundleCollection bundles)
        {
            //bundles.Add(new StyleBundle("~/css/site")
            //    .Include("~/Public/css/bootstrap.css")
            //    .Include("~/Public/css/app/app.css"));

            //bundles.Add(new ScriptBundle("~/js/site")
            //    .Include("~/Public/js/jquery-1.10.2.js")
            //    .Include("~/Public/js/bootstrap.js")
            //    .Include("~/Public/js/jquery.validate.js")
            //    .Include("~/Public/js/respond.js"));
        }
    }
}
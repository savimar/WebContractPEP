﻿using System.Web.Optimization;

namespace WebContractPEP
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // готово к выпуску, используйте средство сборки по адресу https://modernizr.com, чтобы выбрать только необходимые тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/node_modules/respond.js"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css",
                "~/Content/css/font-awesome.css," +
                "~/Scripts/summernote/summernote.css"));


            bundles.Add(new ScriptBundle("~/bundle/base").Include(
                "~/Scripts/jquery-{version}",
                "~/Scripts/bootstrap.js"
            ));
            bundles.Add(new StyleBundle("~/fonts/summernote").Include(
                "~/Content/summernote/summernote.ttf",
                "~/Content/summernote/summernote.woff",
                "~/Content/summernote/summernote.woff2",
                 "~/Content/summernote/summernote.eot"
            ));
            /*
                        bundles.Add(new ScriptBundle("~/bundle/summernote").Include(
                            "~/Scripts/summernote/summernote.js"

                        ));*/
            /*
                        bundles.Add(new ScriptBundle("~/bundle/summernote/summernote.").Include(
                            "~/Content/summernote/summernote.js"
                        ));
            */

            /*
                        bundles.Add(new ScriptBundle("~/bundle/summernote/summernote.js").Include(
                            "~/Content/summernote.js"
                        ));

                        bundles.Add(new StyleBundle("~/Scripts/summernote-lite.css").Include(
                            "~/Content/summernote-lite.css"
                        ));
                    }
            */
        }
    }

}

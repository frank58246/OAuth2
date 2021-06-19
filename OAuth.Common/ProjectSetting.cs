using System;
using System.Collections.Generic;
using System.Text;

namespace OAuth.Common
{
    public class ProjectSetting
    {
        private static string _domain =>
            Environment.GetEnvironmentVariable("DOMAIN") ?? "localhost";

        public static class Url
        {
            public static string OAuthServer = $"https://{_domain}:49001";

            public static string ResourceApi = $"https://{_domain}:49003";
        }
    }
}
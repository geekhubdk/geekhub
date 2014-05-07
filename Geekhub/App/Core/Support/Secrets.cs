using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Geekhub.App.Core.Support
{
    public static class Secrets
    {
        private static readonly NameValueCollection _items = new NameValueCollection(); 
        static Secrets()
        {
            var path = HttpContext.Current.Server.MapPath("~/secrets.json");

            if (File.Exists(path)) {
                var jsonFile = File.ReadAllText(path);
                dynamic result = JsonConvert.DeserializeObject(jsonFile);

                foreach (var i in result) {
                    _items.Add((string)i[0], (string)i[1]);
                }
            }
            
        }

        public static string Get(string name)
        {
            var value = _items[name];

            if (value == null) {
                value = ConfigurationManager.AppSettings[name];
            }

            if (value == null) {
                throw new Exception("Could not get secret: " + name);
            }

            return value;
        }
    }
}
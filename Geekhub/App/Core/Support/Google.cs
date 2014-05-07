using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Xml;

namespace Geekhub.App.Core.Support
{
    public class Google
    {
        public static GoogleLocation GetLocationFromAddress(string s)
        {
            var client = new WebClient();
            var xml = client.DownloadString("http://maps.google.com/maps/api/geocode/xml?address=" + HttpUtility.UrlEncode(s) + "&sensor=false");

            return new GoogleLocation(xml);
        }

        public class GoogleLocation
        {
            private XmlDocument _doc;

            public GoogleLocation(string xml)
            {
                _doc = new XmlDocument();
                _doc.LoadXml(xml);

                var status = _doc["GeocodeResponse"]["status"].InnerText;
                if (status != "OK") {
                    throw new Exception("Google Location Failed. Status: " + status);
                }
            }

            public string Address
            {
                get
                {
                    return Result["formatted_address"].InnerText;
                }
            }

            public double Lat
            {
                get
                {
                    return double.Parse(Result["geometry"]["location"]["lat"].InnerText, new CultureInfo("en-us"));
                }
            }

            public double Lng
            {
                get
                {
                    return double.Parse(Result["geometry"]["location"]["lat"].InnerText, new CultureInfo("en-us"));
                }
            }

            public string City
            {
                get
                {
                    foreach (XmlNode e in Result.ChildNodes) {
                        if (e.Name == "address_component") {
                            foreach(XmlNode e2 in e.ChildNodes) {
                                if (e2.InnerText == "locality") {
                                    return e["long_name"].InnerText;
                                }
                            }
                        }
                    }

                    throw new Exception("Could not find city");
                }
            }

            private XmlElement Result
            {
                get
                {
                    return _doc["GeocodeResponse"]["result"];
                }
            }
        }
    }
}
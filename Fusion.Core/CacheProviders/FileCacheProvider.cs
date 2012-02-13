using System;
using System.IO;
using System.Web;
using System.Xml.Linq;

namespace Fusion.Core.CacheProviders
{
    public class FileCacheProvider : ICacheProvider
    {
        public FileCacheProvider(string cacheDirectory)
        {
            if (string.IsNullOrEmpty(cacheDirectory))
                throw new Exception("No cache directory provided");

            if (!cacheDirectory.EndsWith("/"))
                cacheDirectory += "/";

            CacheDirectory = !Path.IsPathRooted(cacheDirectory) ? HttpContext.Current.Server.MapPath(cacheDirectory) : cacheDirectory;

            if (!Directory.Exists(CacheDirectory))
                throw new Exception(string.Format("The FileCacheProvider directory '{0}' could not be found or does not exist", CacheDirectory));
        }

        private string CacheDirectory { get; set; }

        #region ICacheProvider Members

        public void Add(string url, XDocument document)
        {
            //if (Exists(url))
            //    return;
            var cachePath = GetCachePath(url);

            // Add the server's local time to the document
            var localTime = new XElement("localTime") {Value = DateTime.UtcNow.ToString("yyyy-MM-dd H:mm:ss")};
            document.Root.AddFirst(localTime);

            if (File.Exists(cachePath))
                File.Delete(cachePath);
            using (var fileStream = File.CreateText(cachePath))
            {
                fileStream.Write(document.ToString());
                fileStream.Close();
            }
            //TextWriter textWriter = new StreamWriter(cachePath);
            //textWriter.WriteLine(document.ToString());
            //textWriter.Close();
        }

        public bool Exists(string url)
        {
            // Get the server time to compare cache
            var document = Get(url);
            if (document == null)
                return false;

            if (document.Root != null)
            {
                var apiCurrentTime = DateTime.Parse(document.Root.Element("currentTime").Value);
                var apiCachedUntil = DateTime.Parse(document.Root.Element("cachedUntil").Value);
                var apiDifference = apiCachedUntil - apiCurrentTime;

                //var cacheDuration = apiCachedUntil - apiCurrentTime;
                var localTimeSaved = DateTime.Parse(document.Root.Element("localTime").Value);
                var localCurrentTime = DateTime.UtcNow;
                var localDifference = localCurrentTime - localTimeSaved;

                //var timeDifference = localCurrentTime - apiCurrentTime;
                //var localCachedUntil = apiCachedUntil.Add(timeDifference);

                if (apiDifference < localDifference)
                {
                    Delete(url);
                    return false;
                }
            }
            return true;
        }

        public XDocument Get(string url)
        {
            var cachePath = GetCachePath(url);
            if (File.Exists(cachePath))
            {
                var document = XDocument.Load(cachePath);
                return document;
            }
            return null;
        }

        public void Delete(string url)
        {
            var cachePath = GetCachePath(url);
            if (File.Exists(cachePath))
                File.Delete(cachePath);
        }

        #endregion

        private string GetCachePath(string url)
        {
            var cacheFilePath = string.Format("{0}{1}.cache.xml", CacheDirectory, Helpers.MD5(url));
            return cacheFilePath;
        }
    }
}
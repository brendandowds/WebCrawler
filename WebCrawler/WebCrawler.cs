using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace WebCrawler
{
    public class WebCrawler
    {
        /// <summary>
        /// Crawl a domain given by the URL and extract all links to pages on the same domain, external URLs and static content.
        /// </summary>
        /// <param name="url">The URL used as the starting domain.</param>
        /// <returns>A collection of links, or an error message if the URL is not valid.</returns>
        public static IEnumerable<string> Crawl(string url)
        {
            List<string> links = new List<string>();
            try
            {
                if (!WebCrawler.IsValidUrl(url))
                    return new List<string>() { "The URL entered is not valid. Please try a different one." };

                // Get the domain from the given URL
                string domain = GetDomain(url);
                
                // Get all the links from the domain
                string linksString;
                using (WebClient webClient = new WebClient())
                {
                    linksString = webClient.DownloadString(domain);
                }

                // Add the current domain
                links.Add(domain);

                // Add each to the list avoiding duplicates
                foreach (string item in Finder(linksString).Where(x => !links.Contains(x)))
                {
                    links.Add(item);
                }
            }
            catch (Exception ex)
            {
                links.Add(string.Format("An error occured in crawling the domain {0}:", url));
                links.Add(ex.Message);
            }

            return links;
        }

        /// <summary>
        /// Check if the URL entered is valid (for both http and https)
        /// </summary>
        /// <param name="url">The URL to be tested.</param>
        /// <returns>True if the URL is valid; false otherwise.</returns>
        private static bool IsValidUrl(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// Get the domain from a URL
        /// </summary>
        /// <param name="url">The input URL</param>
        /// <returns>The domain as a string</returns>
        private static string GetDomain(string url)
        {
            return new Uri(url).GetLeftPart(UriPartial.Authority);
        }

        /// <summary>
        /// Find the URL links within a string
        /// </summary>
        /// <param name="file">The string to be checked</param>
        /// <returns>A list of links.</returns>
        private static List<string> Finder(string file)
        {
            List<string> list = new List<string>();

            // Find all matches in file.
            MatchCollection m1 = Regex.Matches(file, @"(<a.*?>.*?</a>)", RegexOptions.Singleline);

            // Loop over each match extracting the href
            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;
                
                // Get href attribute.
                Match m2 = Regex.Match(value, @"href=\""(.*?)\""", RegexOptions.Singleline);
                if (m2.Success)
                {
                    list.Add(m2.Groups[1].Value.TrimEnd('/'));
                }
            }

            return list;
        }
    }
}

# WebCrawler
A very, very basic webcrawler to extract the links from a domain, including but not descending into external links.

It should also capture static content e.g. images, but it does not.

As this is written in C# using the .NET framework you shall need to compile it using the .NET compiler. It was written in VisualStudio 2010, using the inbuilt unit testing.

Note on catching all exceptions.
Issues/notes:

1. WebClient is quick and simple to use (instantiate, initialise and use - other methods make require multiple objects e.g. a HttpWebRequest, WebResponse, Stream and StreamReader). But when compared to HttpWebRequest whilst good for 'quick and dirty' simple requests, but not for when you would like more control over the requests.

2. WebClient does only has a default timeout of 100 seconds (there is no property to change it), this is a very long time to wait if there is no internet connection.

3. Downsides: There is not multi-threading or asynchronous calls implemented here. Once started you just have to wait for it to finish. You can not cancel the operation. By using DownloadStringAsync instead of DownloadString, then is would be asynchronous and we'd have the ability to cancel it (via CancelAsync).

4. Not using events which could allow updates on progress and completion.

5. Can only find static content by the appropriate HTML tag, if the tag is missing or incorrect (in this program) then the content is missed. i.e. if there was a typo in the Finder method  e.g. <igm> instead of <img> then all images would be missed. 
If the typo was in the domain being crawled then that is not the reposibility of this program to find and (using the above image sceanrio) any images with the wrong tag would not be displayed nor found by the crawler.

6. Web permissions/security have not been considered in this simple code.

7. A general try-catch round the publically exposed Crawl method, should prevent it from crashing, however it also only allows for a general and equal treatment of all exceptions and their root causes.

8. UTs to test we would like our own fixed web pages as far as I'm aware WebClient does not implement it's own interface (it implements IComponent and IDisposable but they don't help us in this regard) that would allow us to inject it into the WebCrawler class and provide a seam for testing (whereby would could provide our own implementation/substitute configured to give a known and wholly in our control data to requests without the need for and actual domain).

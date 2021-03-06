﻿using System;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Cache;

namespace Pers_uchet_org
{
    internal class MyHttpUtility
    {
        #region Поля

        private HttpWebRequest _request;
        private HttpWebResponse _response;
        private CookieCollection _cookies;
        //static string responseResult = string.Empty;
        //static string responseUrl = string.Empty;
        private string _accept =
            "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/x-shockwave-flash, application/msword, application/vnd.ms-excel, application/vnd.ms-powerpoint, */*";

        private string _acceptLanguage = "ru-RU";
        private string _acceptEncoding;


        private string _userAgent =
            "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; Tablet PC 2.0; OfficeLiveConnector.1.4; OfficeLivePatch.1.3)";

        private string _contentType = "application/x-www-form-urlencoded";
        private bool _allowAutoRedirect = true;

        private bool _expect100Continue = true;
        private bool _keepAlive;
        private RequestCachePolicy _cachePolicy;

        private WebProxy _proxy = new WebProxy();
        private int _timeout = 5000;
        private NetworkCredential _netCredential;
        private string _errorString = string.Empty;

        #endregion

        #region Свойства

        public HttpWebRequest Request
        {
            get { return _request; }
            set { _request = value; }
        }

        public HttpWebResponse Response
        {
            get { return _response; }
            set { _response = value; }
        }

        public CookieCollection Cookie
        {
            get { return _cookies; }
            set { _cookies = value; }
        }

        //public static string ResponseResult
        //{
        //    get { return responseResult; }
        //    set { responseResult = value; }
        //}

        //public static string ResponseUrl
        //{
        //    get { return responseUrl; }
        //    //set { responseUrl = value; }
        //}

        public string Accept
        {
            get { return _accept; }
            set { _accept = value; }
        }

        public string AcceptLanguage
        {
            get { return _acceptLanguage; }
            set { _acceptLanguage = value; }
        }

        public string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; }
        }

        public string ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }

        public bool AllowAutoRedirect
        {
            get { return _allowAutoRedirect; }
            set { _allowAutoRedirect = value; }
        }

        public WebProxy Proxy
        {
            get { return _proxy; }
            set { _proxy = value; }
        }

        public int Timeout
        {
            get { return _timeout; }
            set
            {
                if (_timeout > -2) _timeout = value;
                else _timeout = -1;
            }
        }

        public string ErrorString
        {
            get { return _errorString; }
            set { _errorString = value; }
        }

        public NetworkCredential NetCredential
        {
            get { return _netCredential; }
            set { _netCredential = value; }
        }

        public string AcceptEncoding
        {
            get { return _acceptEncoding; }
            set { _acceptEncoding = value; }
        }

        public bool Expect100Continue
        {
            get { return _expect100Continue; }
            set { _expect100Continue = value; }
        }

        public bool KeepAlive
        {
            get { return _keepAlive; }
            set { _keepAlive = value; }
        }

        public RequestCachePolicy CachePolicy
        {
            get { return _cachePolicy; }
            set { _cachePolicy = value; }
        }

        #endregion

        #region Методы

        public HttpWebRequest CreateGetRequest(string url)
        {
            //try
            //{
            //if (string.IsNullOrEmpty(url))
            //    return request;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.Proxy = _proxy;
            request.Credentials = _netCredential;
            request.KeepAlive = _keepAlive;
            request.ServicePoint.Expect100Continue = _expect100Continue;

            if (!string.IsNullOrEmpty(_accept))
                request.Accept = _accept;
            request.Headers.Add("Accept-Language", !string.IsNullOrEmpty(_acceptLanguage) ? _acceptLanguage : "us-US");
            if (!string.IsNullOrEmpty(_acceptEncoding))
                request.Headers.Add("Accept-Encoding", _acceptEncoding);
            if (!string.IsNullOrEmpty(_userAgent))
                request.UserAgent = _userAgent;
            request.AllowAutoRedirect = _allowAutoRedirect;
            request.ContentType = _contentType;
            if (_timeout > -1)
                request.Timeout = _timeout;
            request.CookieContainer = new CookieContainer();
            if (_cookies != null)
                request.CookieContainer.Add(_cookies);

            //using (response = (HttpWebResponse)request.GetResponse())
            //{
            //    StreamReader reader = new StreamReader(response.GetResponseStream());
            //    sourcePage = reader.ReadToEnd();
            //    cookies = response.Cookies;
            //    responseUrl = response.ResponseUri.AbsoluteUri;

            //    reader.Close();
            //    response.GetResponseStream().Close();
            //    response.Close();
            //}
            return request;
            //}
            //catch (Exception ex)
            //{
            //    errorString = ex.Message;
            //    return null;
            //}
        }

        public string CreateGetRequestAndExec(string url)
        {
            //try
            //{
            HttpWebRequest request = CreateGetRequest(url);
            //if (string.IsNullOrEmpty(url))
            //    return request;
            //request = (HttpWebRequest)HttpWebRequest.Create(url);
            //request.Method = "GET";
            //if (!string.IsNullOrEmpty(accept))
            //    request.Accept = accept;
            //if (!string.IsNullOrEmpty(accept))
            //    request.Headers.Add("Accept-Language", acceptLanguage);
            //else
            //    request.Headers.Add("Accept-Language", "us-US");
            //if (!string.IsNullOrEmpty(userAgent))
            //    request.UserAgent = userAgent;
            //request.AllowAutoRedirect = allowAutoRedirect;
            //request.Proxy = proxy;
            //if (timeout > -1)
            //    request.Timeout = timeout;
            //request.CookieContainer = new CookieContainer();
            //if (cookies != null)
            //    request.CookieContainer.Add(cookies);

            string result;
            ExecRequest(request, out result);
            //using (response = (HttpWebResponse)request.GetResponse())
            //{
            //    StreamReader reader = new StreamReader(response.GetResponseStream());
            //    sourcePage = reader.ReadToEnd();
            //    cookies = response.Cookies;
            //    responseUrl = response.ResponseUri.AbsoluteUri;

            //    reader.Close();
            //    response.GetResponseStream().Close();
            //    response.Close();
            //}
            return result;
            //}
            //catch (Exception ex)
            //{
            //    errorString = ex.Message;
            //    return null;
            //}
        }

        public HttpWebRequest CreatePostRequest(string url, string postData)
        {
            //try
            //{
            //if (string.IsNullOrEmpty(url))
            //    return false;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.Proxy = _proxy;
            request.Credentials = _netCredential;
            request.KeepAlive = _keepAlive;
            request.ServicePoint.Expect100Continue = _expect100Continue;

            if (!string.IsNullOrEmpty(_accept))
                request.Accept = _accept;
            request.Headers.Add("Accept-Language", !string.IsNullOrEmpty(_acceptLanguage) ? _acceptLanguage : "us-US");
            if (!string.IsNullOrEmpty(_acceptEncoding))
                request.Headers.Add("Accept-Encoding", _acceptEncoding);
            if (!string.IsNullOrEmpty(_userAgent))
                request.UserAgent = _userAgent;
            request.AllowAutoRedirect = _allowAutoRedirect;
            request.ContentType = _contentType;
            if (_timeout > -1)
                request.Timeout = _timeout;
            request.CookieContainer = new CookieContainer();
            if (_cookies != null)
                request.CookieContainer.Add(_cookies);

            SetPostData(request, postData);

            //using (response = (HttpWebResponse)request.GetResponse())
            //{
            //    StreamReader reader = new StreamReader(response.GetResponseStream());
            //    sourcePage = reader.ReadToEnd();
            //    cookies = response.Cookies;
            //    responseUrl = response.ResponseUri.AbsoluteUri;

            //    reader.Close();
            //    response.GetResponseStream().Close();
            //    response.Close();
            //}
            return request;
            //}
            //catch (Exception ex)
            //{
            //    errorString = ex.Message;
            //    return false;
            //}
        }

        public string CreatePostRequestAndExec(string url, string postData)
        {
            //try
            //{
            HttpWebRequest request = CreatePostRequest(url, postData);
            //if (string.IsNullOrEmpty(url))
            //    return false;
            //request = (HttpWebRequest)HttpWebRequest.Create(url);
            //request.Method = "POST";
            //if (!string.IsNullOrEmpty(accept))
            //    request.Accept = accept;
            //if (!string.IsNullOrEmpty(accept))
            //    request.Headers.Add("Accept-Language", acceptLanguage);
            //else
            //    request.Headers.Add("Accept-Language", "us-US");
            //if (!string.IsNullOrEmpty(userAgent))
            //    request.UserAgent = userAgent;
            //if (timeout > -1)
            //    request.Timeout = timeout;
            //request.AllowAutoRedirect = allowAutoRedirect;
            //request.Proxy = proxy;
            //request.ContentType = contentType;
            //SetPostData(request, postData);
            //request.CookieContainer = new CookieContainer();
            //if (cookies != null)
            //{
            //    request.CookieContainer.Add(cookies);
            //}

            //using (response = (HttpWebResponse)request.GetResponse())
            //{
            //    StreamReader reader = new StreamReader(response.GetResponseStream());
            //    sourcePage = reader.ReadToEnd();
            //    cookies = response.Cookies;
            //    responseUrl = response.ResponseUri.AbsoluteUri;

            //    reader.Close();
            //    response.GetResponseStream().Close();
            //    response.Close();
            //}
            string result;
            ExecRequest(request, out result);

            return result;
            //}
            //catch (Exception ex)
            //{
            //    errorString = ex.Message;
            //    return false;
            //}
        }

        public static void SetPostData(HttpWebRequest request, string postData)
        {
            byte[] postDataArray = Encoding.GetEncoding(1251).GetBytes(postData);
            request.ContentLength = postDataArray.Length;
            request.GetRequestStream().Write(postDataArray, 0, postDataArray.Length);
        }

        //public void CreateGetRequest(string url)
        //{
        //    request = CreateGetRequest(url);
        //}

        //public void CreatePostRequest(string url, string postData)
        //{
        //    request = CreatePostRequest(url, postData);
        //}

        //public void ExecRequest()
        //{
        //    using (response = (HttpWebResponse)request.GetResponse())
        //    {
        //        StreamReader reader = new StreamReader(response.GetResponseStream());
        //        responseResult = reader.ReadToEnd();
        //        cookies = response.Cookies;
        //        responseUrl = response.ResponseUri.AbsoluteUri;

        //        reader.Close();
        //        response.GetResponseStream().Close();
        //        response.Close();
        //    }
        //}

        public static void ExecRequest(HttpWebRequest request, out string responseResult)
        {
            //string responseResult;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                responseResult = reader.ReadToEnd();

                reader.Close();
                response.GetResponseStream().Close();
                response.Close();
            }
            //return responseResult;
        }

        public static void ExecRequest(HttpWebRequest request, out byte[] responseResult)
        {
            //byte[] responseResult;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                int length = (int)response.ContentLength;
                responseResult = new byte[length];
                response.GetResponseStream().Read(responseResult, 0, length);

                response.GetResponseStream().Close();
                response.Close();
            }
            //return responseResult;
        }

        public bool PingHost(string hostAddr, out string errorString)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(hostAddr);
                request.Proxy = _proxy;
                request.Credentials = _netCredential;
                using ((HttpWebResponse)request.GetResponse())
                {
                    //    StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1251));
                    //    string str = myStreamReader.ReadToEnd();
                }
                errorString = String.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorString = ex.Message;
                return false;
            }
        }

        #endregion

        #region Temp

        //HttpWebResponse GetPage(string url, CookieCollection cookies)
        //{
        //    result = (HttpWebRequest)HttpWebRequest.Create(url);
        //    result.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/x-shockwave-flash, application/msword, application/vnd.ms-excel, application/vnd.ms-powerpoint, */*";
        //    result.Headers.Add("Accept-Language", "ru-RU");
        //    result.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; Tablet PC 2.0; OfficeLiveConnector.1.4; OfficeLivePatch.1.3)";
        //    result.AllowAutoRedirect = true;
        //    result.Proxy = null;
        //    result.Timeout = 3000;
        //    result.CookieContainer = new CookieContainer();
        //    if (cookies != null)
        //    {
        //        result.CookieContainer.Add(cookies);
        //    }

        //    return (HttpWebResponse)result.GetResponse();
        //}

        //HttpWebResponse PostPage(string url, byte[] postData, CookieCollection cookies)
        //{
        //    result = (HttpWebRequest)HttpWebRequest.Create(url);
        //    result.Method = "POST";
        //    result.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/x-shockwave-flash, application/msword, application/vnd.ms-excel, application/vnd.ms-powerpoint, */*";
        //    result.Headers.Add("Accept-Language", "ru-RU");
        //    result.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; Tablet PC 2.0; OfficeLiveConnector.1.4; OfficeLivePatch.1.3)";
        //    result.ContentType = "application/x-www-form-urlencoded";
        //    result.AllowAutoRedirect = true;
        //    result.ContentLength = postData.Length;
        //    result.Proxy = null;
        //    result.Timeout = 3000;
        //    result.GetRequestStream().Write(postData, 0, postData.Length);
        //    result.CookieContainer = new CookieContainer();
        //    if (cookies != null)
        //    {
        //        result.CookieContainer.Add(cookies);
        //    }
        //    return (HttpWebResponse)result.GetResponse();
        //}

        //HttpWebResponse PostPage(string url, string postString, CookieCollection cookies)
        //{
        //    byte[] byteArr = System.Text.Encoding.GetEncoding(1251).GetBytes(postString);
        //    return PostPage(url, byteArr, cookies);
        //}

        //HttpWebResponse PostPage(string url, string postString)
        //{
        //    return PostPage(url, postString, null);
        //}

        //HttpWebResponse GetPage(string url)
        //{
        //    return GetPage(url, null);
        //}

        //string GetResponseContent(HttpWebResponse response)
        //{
        //    StreamReader reader = new StreamReader(response.GetResponseStream());
        //    string result = reader.ReadToEnd();
        //    reader.Close();
        //    response.GetResponseStream().Close();
        //    response.Close();
        //    response.GetResponseStream().Close();
        //    result.GetResponse().Close();
        //    return result;
        //}

        #endregion
    }
}
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PicACG
{
    public static class Config
    {
        public static readonly string BaseUrl = "http://68.183.234.72/";
        public static readonly string Url = "https://picaapi.picacomic.com/";
        public static readonly string ApiKey = "C69BAF41DA5ABD1FFEDC6D2FEA56B";

        public static readonly string AppChannel = "3";
        public static readonly string Version = "2.2.1.3.3.4";
        public static readonly string BuildVersion = "45";
        public static readonly string Accept = "application/vnd.picacomic.com.v1+json";
        public static readonly string Agent = "okhttp/3.8.1";
        public static readonly string Platform = "android";
        public static readonly string Now = DateTime.Now.ToString("s");
        public static readonly string Nonce = Guid.NewGuid().ToString().Replace("-", string.Empty);

        public static readonly int ThreadNum = 10;
        public static readonly int DownloadThreadNum = 5;
        public static readonly ImageQuality ImageQuality = ImageQuality.original;
        public static readonly string Uuid = "defaultUuid";
        public static readonly string HttpProxy = "";
        public static readonly string SavePath = "";
        public static readonly string SavePathDir = "commies";
        public static readonly int ResetCnt = 5;
        public static readonly int PreLoading = 10;
        public static readonly bool IsLoadingPicture = true;

        public static readonly string UpdateUrl = "";
        public static readonly string UpdateVersion = "v1.0.5";

        public static string GetSignature(Method method)
        {
            return Now + Nonce + method + ApiKey + Version;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ImageQuality
    {
        original,
        low,
        medium,
        high
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Method
    {
        GET,
        POST
    }
}
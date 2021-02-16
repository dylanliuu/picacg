using System;
using System.Security.Cryptography;
using System.Text;
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
        public static readonly string Now = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        public static readonly string Nonce = Guid.NewGuid().ToString().Replace("-", "");
        public static readonly string Host = "picaapi.picacomic.com";

        public static readonly int ThreadNum = 10;
        public static readonly int DownloadThreadNum = 5;
        public static readonly ImageQuality ImageQuality = ImageQuality.original;
        public static readonly string Uuid = "defaultUuid";
        public static readonly string HttpProxy = "http://127.0.0.1:10888";
        public static readonly string SavePath = "";
        public static readonly string SavePathDir = "commies";
        public static readonly int ResetCnt = 5;
        public static readonly int PreLoading = 10;
        public static readonly bool IsLoadingPicture = true;

        public static readonly string UpdateUrl = "";
        public static readonly string UpdateVersion = "v1.0.5";

        private const string SecretKey = "~d}$Q7$eIni=V)9\\RK/P.RM4;9[7|@/CA}b~OW!3?EV`:<>M7pddUBL5n|0/*Cn";

        public static string GetSignature(string uri, Method method)
        {
            return GetSha256Base64(uri + Now + Nonce + method + ApiKey);
        }

        private static string GetSha256Base64(string input)
        {
            var sha256 = new HMACSHA256(Encoding.UTF8.GetBytes(SecretKey));
            if (sha256 is null)
            {
                throw new NullReferenceException();
            }

            var secretBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(secretBytes).Replace("-", "").ToLower();
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
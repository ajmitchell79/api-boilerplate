using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using API_BoilerPlate.BRL.Command;
using API_BoilerPlate.BRL.Common;
using API_BoilerPlate.BRL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API_BoilerPlate.API.Controllers
{
    public class BaseController : Controller
    {
        private const string CLAIM_UNIQUE_NAME = "unique_name";
        private const string CLAIM_NAME_ID = "nameid";
        private const string CLAIM_USER_GUID = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim";

        private string GetUser(string propertyName)
        {
            var authToken = Request.Headers["Authorization"][0].Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(authToken);

            if (token != null) return token.Payload[propertyName].ToString();

            return "No user information";
        }

        private string GetLoginWithoutDomain(string propertyName)
        {
            string userName = GetUser(propertyName);
            return Path.GetFileNameWithoutExtension(userName).ToLower();
        }

        public string UserName => GetUser(CLAIM_UNIQUE_NAME);

        public string UserLogin => GetUser(CLAIM_NAME_ID);

        public string UserGuid => GetUser(CLAIM_USER_GUID);

        public string UserLoginWithoutDomain => GetLoginWithoutDomain(CLAIM_NAME_ID);

        protected void LogError(ILogger<IController> logger, Exception ex, object parameter = null)
        {
            var customData = new List<KeyValuePair<string, object>>();
            customData.Add(new KeyValuePair<string, object>("UserName", GetUser(CLAIM_UNIQUE_NAME)));
            customData.Add(new KeyValuePair<string, object>("UserId", GetUser(CLAIM_NAME_ID)));

            if (parameter != null) customData.Add(new KeyValuePair<string, object>("Parameter", JsonConvert.SerializeObject(parameter)));

            logger.Log(
                LogLevel.Error,
                1,
                customData,
                ex,
                (s, e) => e.Message);

        }

        protected void LogClientSideError(ILogger<IController> logger, LogItem logItem)
        {
            var customData = new List<KeyValuePair<string, object>>();
            customData.Add(new KeyValuePair<string, object>("UserName", GetUser(CLAIM_UNIQUE_NAME)));
            customData.Add(new KeyValuePair<string, object>("UserId", GetUser(CLAIM_NAME_ID)));
            customData.Add(new KeyValuePair<string, object>("Url", logItem.Url));
            //customData.Add(new KeyValuePair<string, object>("Parameter", logItem.Parameter));
            customData.Add(new KeyValuePair<string, object>("StackTrace", logItem.Stack));

            var exception = new ClientSideException(logItem.Stack);
            var level = (LogLevel)Enum.Parse(typeof(LogLevel), logItem.Level);

            logger.Log(
                level,
                1,
                customData,
                exception,
                (s, e) => "Angular Error : " + logItem.Message
            );

        }
    }
}
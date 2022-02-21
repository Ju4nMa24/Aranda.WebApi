using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Aranda.Common.Generics
{
    public static class Resource
    {
        /// <summary>
        /// Method for acquiring reply messages.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private static List<string> GetParameter(string header, string parameter)
        {
            List<string> response = new List<string>();
            try
            {
                Stream resource = Assembly.GetEntryAssembly().GetManifestResourceStream("Aranda.WebApi.Resources.replymessages.json");
                using (StreamReader reader = new StreamReader(resource))
                {
                    dynamic json = JObject.Parse(reader.ReadToEndAsync().Result);
                    JArray message = JsonConvert.DeserializeObject<JArray>(JsonConvert.SerializeObject(json[header][parameter]));
                    response.Add(message.Children().Cast<JObject>().FirstOrDefault().First?.First.ToString());
                    response.Add(message.Children().Cast<JObject>().LastOrDefault().Last?.Last.ToString());
                    return response;
                }
            }
            catch
            {
                response.Add(Constants.DefaultCode);
                response.Add(Constants.DefaultMessage);
                return response;
            }

        }
        /// <summary>
        /// Method for acquiring reply messages by error.
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static dynamic ErrorResponse(Message messages)
        {
            List<string> messageResponse = GetParameter(messages.Header, messages.Parameter);
            messages.ResponseType.InnerContext.Result.ReasonCode = messageResponse.FirstOrDefault();
            messages.ResponseType.InnerContext.Result.ReasonPhrase = messageResponse.LastOrDefault();
            return messages.ResponseType;
        }
        /// <summary>
        /// Method for acquiring reply messages by success.
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static dynamic SuccessMessage(Message messages)
        {
            List<string> messageResponse = GetParameter(messages.Header, messages.Parameter);
            messages.ResponseType.InnerContext.Result.ReasonCode = messageResponse.FirstOrDefault();
            messages.ResponseType.InnerContext.Result.ReasonPhrase = messageResponse.LastOrDefault();
            messages.ResponseType.InnerContext.Result.Success = true;
            return messages.ResponseType;
        }
    }
}

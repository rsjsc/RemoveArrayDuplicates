using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class HttpTrigger1
    {
        [Function("HttpTrigger1")]
        public static HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("HttpTrigger1");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            // Läs inkommande data från request body
            string requestBody = req.ReadString();

            // Omvandla JSON-data till array av strängar
            string[] inputArray = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(requestBody);

            // Sortera arrayen
            string[] sortedArray = StringArraySorter.SortStringArray(inputArray);

            // Returnera den sorterade arrayen som JSON
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteString(Newtonsoft.Json.JsonConvert.SerializeObject(sortedArray));

            return response;
        }
    }
}

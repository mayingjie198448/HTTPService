using HTTPService.Models;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace HTTPService;

public class HTTPClient
{

    private readonly string _apiUrl;

    protected readonly RestClient client;

    public HTTPClient(string apiUrl)
    {
        _apiUrl = apiUrl;
        client = new RestClient();
    }



    public async Task<BaseResponse?> ExecuteAsync(BaseRequest baseRequest)
    {

        var request = new RestRequest(new Uri(_apiUrl + baseRequest.Route), baseRequest.Method);

        request.AddHeader("Content-Type", baseRequest.ContentType);

        if (baseRequest.Parameter != null)
        {
            switch (request.Method)
            {
                case Method.Get:
                    {
                        foreach (var item in baseRequest.Parameter)
                        {
                            request.AddParameter(item.Key, item.Value);
                        }
                        break;
                    }
                case Method.Post:
                    foreach (var item in baseRequest.Parameter)
                    {
                        request.AddQueryParameter(item.Key, item.Value);
                    }
                    break;
                case Method.Put:
                    break;
                case Method.Delete:
                    break;
                case Method.Head:
                    break;
                case Method.Options:
                    break;
                case Method.Patch:
                    break;
                case Method.Merge:
                    break;
                case Method.Copy:
                    break;
                case Method.Search:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        if (baseRequest.Body != null)
        {
            request.AddJsonBody(baseRequest.Body);
        }

        if (baseRequest.Headers != null)
        {
            request.AddHeaders(baseRequest.Headers);
        }

        var response = await client.ExecuteAsync(request);

        if (response.StatusCode != HttpStatusCode.OK)
            return response.ErrorMessage == null
                ? new BaseResponse() { Message = response.StatusDescription, IsSuccessful = false }
                : new BaseResponse() { Message = response.ErrorMessage, IsSuccessful = false };

        if (response.Content != null)
        {
            return new BaseResponse()
            {
                IsSuccessful = true,
                Message = "ok",
                Data = response.Content
            };
        }

        return response.ErrorMessage == null
            ? new BaseResponse() { Message = response.StatusDescription, IsSuccessful = false }
            : new BaseResponse() { Message = response.ErrorMessage, IsSuccessful = false };

    }


    public async Task<BaseResponse<List<T>>?> ExecuteAsync<T>(BaseRequest baseRequest)
    {

        var request = new RestRequest(new Uri(_apiUrl + baseRequest.Route), baseRequest.Method);

        request.AddHeader("Content-Type", baseRequest.ContentType);

        if (baseRequest.Parameter != null)
        {
            switch (request.Method)
            {
                case Method.Get:
                    {
                        foreach (var item in baseRequest.Parameter)
                        {
                            request.AddParameter(item.Key, item.Value);
                        }

                        break;
                    }
                case Method.Post:
                    foreach (var item in baseRequest.Parameter)
                    {
                        request.AddQueryParameter(item.Key, item.Value);
                    }
                    break;
                case Method.Put:
                    break;
                case Method.Delete:
                    break;
                case Method.Head:
                    break;
                case Method.Options:
                    break;
                case Method.Patch:
                    break;
                case Method.Merge:
                    break;
                case Method.Copy:
                    break;
                case Method.Search:
                    break;
                default:
                    throw new ArgumentOutOfRangeException
                    {
                        HelpLink = null,
                        HResult = 0,
                        Source = null
                    };
            }
        }

        if (baseRequest.Body != null)
        {
            request.AddJsonBody(baseRequest.Body);
        }

        var response = await client.ExecuteAsync(request);

        if (response.StatusCode != HttpStatusCode.OK)
            return response.ErrorMessage == null
                ? new BaseResponse<List<T>>() { Message = response.StatusDescription, IsSuccessful = false }
                : new BaseResponse<List<T>>() { Message = response.ErrorMessage, IsSuccessful = false };

        if (response.Content == null)
            return response.ErrorMessage == null
                ? new BaseResponse<List<T>>() {
                    Message = response.StatusDescription, IsSuccessful = false
                }
                : new BaseResponse<List<T>>() {
                    Message = response.ErrorMessage, IsSuccessful = false
                };
        
        var res = JsonConvert.DeserializeObject<BaseResponse>(response.Content);

        var resStr = res?.Data?.ToString();

        if (resStr == null)
            return response.ErrorMessage == null
                ? new BaseResponse<List<T>>() {
                    Message = response.StatusDescription, IsSuccessful = false
                }
                : new BaseResponse<List<T>>() {
                    Message = response.ErrorMessage, IsSuccessful = false
                };
        
        var dataList = JsonConvert.DeserializeObject<List<T>>(resStr);

        if (dataList is List<T> list)
        {
            return new BaseResponse<List<T>>()
            {
                IsSuccessful = true,
                Message = "ok",
                Data = list,
                TotalCount = list.Count
            };
        }

        return response.ErrorMessage == null
            ? new BaseResponse<List<T>>() { Message = response.StatusDescription, IsSuccessful = false }
            : new BaseResponse<List<T>>() { Message = response.ErrorMessage, IsSuccessful = false };
    }

}
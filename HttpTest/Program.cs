// See https://aka.ms/new-console-template for more information

using HTTPService;
using HTTPService.Models;
using HttpTest;
using RestSharp;

var client = new HTTPClient(UrlConst.BaseUrl);

var response =  client.ExecuteAsync<Test>(new BaseRequest
{
    Method = Method.Get,
    Route = UrlConst.get
}).Result;

Console.WriteLine(response?.Message);
Console.WriteLine(response?.Data);


Console.ReadKey();
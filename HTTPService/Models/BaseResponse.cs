namespace HTTPService.Models; 

public class BaseResponse {

    /// <summary>
    /// 结果信息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 结果状态
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// 结果数据
    /// </summary>
    public object? Data { get; set; }
}

public class BaseResponse<T>
{

    /// <summary>
    /// 结果信息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 结果状态
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// 结果数据
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// 请求到的数据总数
    /// </summary>
    public int? TotalCount { get; set; } = 0;

}
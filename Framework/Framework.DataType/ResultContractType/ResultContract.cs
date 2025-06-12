using System.Net.Http.Headers;

namespace Framework.DataType;

public class ResultContract
{
    public bool IsSuccessful { get; set; }
    public ErrorMessage? ErrorMessage { get; private set; }
    public string[]? InformationMessages { get; private set; }
    public string? SuccessMessage { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string? RequestId { get; set; }
    public string? DebugInfo { get; set; }
    public string? CorrelationId { get; set; }

    protected ResultContract(bool isSuccessful)
    {
        Timestamp = DateTime.Now;
        IsSuccessful = isSuccessful;
    }


    public void SetSuccessMessage(string successMessage)
    {
        SuccessMessage = successMessage;
        IsSuccessful = true;
    }

    public void SetErrorMessage(ErrorMessage errorMessage)
    {
        ErrorMessage = errorMessage;
        IsSuccessful = false;
    }

    public void AddInformationMessages(string[] informationMessages)
    {
        InformationMessages = informationMessages;
    }

    public static implicit operator
        ResultContract(bool result) => new ResultContract(result);

    public static implicit operator ResultContract((ErrorType type, string message) result)
    {
        var res = new ResultContract(false);
        res.SetErrorMessage(new ErrorMessage(result.message, result.type));
        return res;
    }

}

public class ResultContract<TData> : ResultContract
{
    protected ResultContract(TData? data) : base(true)
    {
        Data = data;
    }

    public TData? Data { get; set; }

    public static implicit operator
        ResultContract<TData>(TData data) => new ResultContract<TData>(data);

    public static implicit operator 
        ResultContract<TData>((ErrorType type, string message) result)
    {
        var res = new ResultContract<TData>(default!);
        res.SetErrorMessage(new ErrorMessage(result.message, result.type));
        return res;
    }
}
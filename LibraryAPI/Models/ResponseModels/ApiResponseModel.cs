namespace LibraryAPI.Models.ResponseModels;

public class ApiResponseModel<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string ErrorMessage { get; set; }
}

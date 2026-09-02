namespace MinimalApisCrud.Helper
{
    public class GenericResponse<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }

        public static GenericResponse<T> Success(T data, bool isSuccess = true, string message = "Success", int status = 200) =>
            new() { Data = data, IsSuccess = isSuccess, Message = message, Status = status };

        public static GenericResponse<T> Empty(T data, bool isSuccess = true, string message = "Empty", int status = 204) =>
          new() { Data = data, IsSuccess = isSuccess, Message = message, Status = status };

        public static GenericResponse<T> Failure(bool isSuccess = false, string message = "Error", int status = 400) =>
            new() { IsSuccess = isSuccess, Message = message, Status = status };
    }
}

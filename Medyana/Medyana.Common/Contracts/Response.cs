namespace Medyana.Common.Contracts
{
    public class Response<T>
    {
        public T Result { get; set; }
        public bool IsSucceed { get; set; }
        public string ErrorMessage { get; set; }
    }
}
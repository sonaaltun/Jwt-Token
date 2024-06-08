namespace JWT.API.Utilities
{
    public class AccountResult
    {
        public bool IsSucces {  get; set; }
        public string Message { get; set; }
        public string? Token { get; set; }
    }
}

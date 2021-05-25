namespace PublicAPI.DTO.V1.Account
{
    public class LoginResponse
    {
        public string Token { get; set; } = default!;
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;

        public string UserName { get; set; } = default!;
    }
}
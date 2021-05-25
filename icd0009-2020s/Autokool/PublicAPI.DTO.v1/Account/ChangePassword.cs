namespace PublicAPI.DTO.V1.Account
{
    public class ChangePassword
    {
        public string OldPassword { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    }
}
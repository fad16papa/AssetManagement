namespace AssetManagementWeb.Models.ApiResponse
{
    public class LoginResponeModel
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
}
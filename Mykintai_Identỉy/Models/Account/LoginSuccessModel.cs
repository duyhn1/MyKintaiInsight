using System;
namespace Mykintai_Indentỉy.Models.Account
{
    public class LoginSuccessModel
    {
        public LoginSuccessModel()
        {
        }
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public object error { get; set; }
        public object som { get; set; }
        public object sms { get; set; }
        public bool? isError { get; set; }
    }
}

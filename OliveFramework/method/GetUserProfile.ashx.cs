using OliveFramework.Model.Response;
using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// GetUserProfile 的摘要说明
    /// </summary>
    public class GetUserProfile : BasePage
    {
        private UserProfileModel _userProfile;

        protected override void OnRequest()
        {

            _userProfile = new UserProfileModel();

#if FAKEDATA
            _userProfile.Avatar = "http://localhost/OliveFramework/upload/avatar/logo.png";
            _userProfile.Language = "zh-cn";
            _userProfile.Nick = "Wolf";
            _userProfile.UserID = 1;
            _userProfile.Username = "haiyang@awolf.net";
#endif

            WriteSuccess<UserProfileModel>(_userProfile);
        }

    }
}
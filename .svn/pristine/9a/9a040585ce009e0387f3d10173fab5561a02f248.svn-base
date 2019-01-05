using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// GetCurrentVersion 的摘要说明
    /// </summary>
    public class GetCurrentVersion : BasePage
    {
        protected override void OnRequest()
        {

            try
            {
                CurrentVersionModel currentVersion = new CurrentVersionModel();
                string apkFile = SystemConfig.APKFile;
                currentVersion.VersionCode = SystemConfig.VersionCode;
                currentVersion.DownloadURL = tool.Base64.Encode( SystemConfig.WebappURL + SystemConfig.APKFile);
                currentVersion.ForceUpdate = SystemConfig.ForceUpdate;
                currentVersion.VersionName = SystemConfig.VersionName;
                WriteSuccess<CurrentVersionModel>(currentVersion);
            }
            catch (ExceptionMessage ex)
            {
                WriteFail(ex.Code, ex.DisplayMessage);
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }
            finally
            {

            }

        }

        private class CurrentVersionModel
        {
            public uint VersionCode;
            public string VersionName;
            public string DownloadURL;
            public bool ForceUpdate;
        }

    }
}
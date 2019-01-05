using OliveFramework.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OliveFramework.method
{
    /// <summary>
    /// FileUpload 的摘要说明
    /// </summary>
    public class FileUpload : BasePage
    {


        protected override void OnRequest()
        {
            HttpFileCollection httpFileCollection = Request.Files;
            uint attachmentID = 0;
            try
            {
                if (Request.Files.Count <= 0)
                {
                    throw new ExceptionMessage("/language/upload/need_file");
                }

                if (Request.Files.Count >= 2)
                {
                    throw new ExceptionMessage("/language/upload/only_one");
                }

                HttpPostedFile file = Request.Files[0];

                String attachment = new Controller.Attachment().SaveFile(file, "|.jpg|.gif|.bmp|", null);

                WriteSuccess<string>(attachment);
            }
            catch (ExceptionMessage ex)
            {
                WriteFail(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }

        }
    }
}
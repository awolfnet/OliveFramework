﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OliveFramework.Controller
{
    public class Attachment
    {
        public String SaveFile(HttpPostedFile file, string fileType, string uploadkey)
        {
            FileInfo fi = new FileInfo(file.FileName);

            if (!fileType.Contains(fi.Extension))
            {
                throw new ExceptionMessage("/language/upload/file_extension_not_match");
            }

            AttachmentModel attachment = new AttachmentModel();
            attachment.original = fi.Name;
            attachment.filename = String.Format("{0}_{1}_{2}{3}",DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.ToString("ddhhmmssfff") , fi.Extension);
            attachment.filepath = string.Format("{0}\\upload\\ad\\", SystemConfig.AppDomainAppPath);
            attachment.ContentLength = (uint)file.ContentLength;
            attachment.ContentType = file.ContentType;
            attachment.uploadkey = uploadkey;
            attachment.timestamp = DateTime.Now;

            if (!Directory.Exists(attachment.filepath))
            {
                Directory.CreateDirectory(attachment.filepath);
            }

            string attachmentFile = attachment.filepath + attachment.filename;

            file.SaveAs(attachmentFile);

            return attachment.filename;
        }
    }

    public class AttachmentModel
    {
        public uint? id { set; get; }
        public string filename { set; get; }
        public UInt64? ContentLength { set; get; }
        public string ContentType { set; get; }
        public string uploadkey { set; get; }
        public string filepath { set; get; }
        public string original { set; get; }
        public DateTime? timestamp { set; get; }
    }
}
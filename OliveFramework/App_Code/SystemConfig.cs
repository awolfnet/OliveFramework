﻿using OliveFramework.tool;
using System;
using System.Collections.Generic;
using System.Web;

namespace OliveFramework
{
    public class SystemConfig
    {
        private static string _WindowTitle;
        private static string _DefaultLanguage;
        private static string _LanguageFilePath;
        private static int _TokenValidDay;
        private static string _AppDomainAppPath;
        private static int _MessagePullingTimeout;
        private static int _MessagePullingSleep;
        private static string _WebappURL;
        private static string _AdUploadPath;
        private static uint _VersionCode;
        private static string _VersionName;
        private static string _ApkFile;
        private static bool _ForceUpdate;

        /// <summary>
        /// 窗口标题
        /// </summary>
        public static string WindowTitle
        {
            get
            {
                using (Controller.Configure controllerConfig = new Controller.Configure())
                {
                    _WindowTitle = controllerConfig.getString("windowtitle");
                }
                return _WindowTitle;
            }
        }

        /// <summary>
        /// 默认语言
        /// </summary>
        public static string DefaultLanguage
        {
            get
            {
                using (Controller.Configure controllerConfig = new Controller.Configure())
                {
                    _DefaultLanguage = controllerConfig.getString("lang");
                }
                return _DefaultLanguage;
            }
        }

        /// <summary>
        /// 语言文件
        /// </summary>
        public static string LangugeFilePath
        {
            get
            {
                if (WebConfig.isset("langfilepath"))
                {
                    _LanguageFilePath = WebConfig.get("langfilepath");
                }
                else
                {
                    using (Controller.Configure controllerConfig = new Controller.Configure())
                    {
                        _LanguageFilePath = controllerConfig.getString("langfilepath");
                    }
                }

                if (_LanguageFilePath == null)
                {
                    _LanguageFilePath = "/language/zh-cn.xml";
                }
                return _LanguageFilePath;
            }
        }

        /// <summary>
        /// Token有效期
        /// </summary>
        public static int TokenValidDay
        {
            get
            {
                int? validday;
                if (WebConfig.isset("validday"))
                {
                    validday = int.Parse(WebConfig.get("validday"));
                }
                else
                {
                    using (Controller.Configure controllerConfig = new Controller.Configure())
                    {
                        validday = controllerConfig.getInt("validday");
                    }
                }

                if (validday == null)
                {
                    _TokenValidDay = 1;
                }
                else
                {
                    _TokenValidDay = (int)validday;
                }
                return _TokenValidDay;
            }
        }

        /// <summary>
        /// 网站物理路径
        /// </summary>
        public static string AppDomainAppPath
        {
            get
            {
                using (Controller.Configure controllerConfig = new Controller.Configure())
                {
                    if (WebConfig.isset("webpath"))
                    {
                        _AppDomainAppPath = WebConfig.get("webpath");
                    }
                    else if (controllerConfig.isSet("webpath"))
                    {

                        _AppDomainAppPath = controllerConfig.getString("webpath");

                    }
                    else
                    {
                        _AppDomainAppPath = HttpRuntime.AppDomainAppPath;
                    }
                }
                return _AppDomainAppPath;
            }
        }

        /// <summary>
        /// 消息轮询超时（秒）
        /// </summary>
        public static int MessagePullingTimeout
        {
            get
            {
                int? messgePullingTimeout;
                using (Controller.Configure controllerConfig = new Controller.Configure())
                {

                    if (WebConfig.isset("message_pulling_timeout"))
                    {
                        messgePullingTimeout = int.Parse(WebConfig.get("message_pulling_timeout"));
                    }
                    else if (controllerConfig.isSet("message_pulling_timeout"))
                    {
                         messgePullingTimeout = controllerConfig.getInt("message_pulling_timeout");
                    }
                    else
                    {
                        messgePullingTimeout = null;
                    }

                    if (messgePullingTimeout != null)
                    {
                        _MessagePullingTimeout = (int)messgePullingTimeout;
                    }
                    else
                    {
                        _MessagePullingTimeout = 10;
                    }

                }
                return _MessagePullingTimeout;
            }

        }

        /// <summary>
        /// 消息轮询间隔（毫秒）
        /// </summary>
        public static int MessagePullingSleep
        {
            get
            {
                int? messgePullingSleep;
                using (Controller.Configure controllerConfig = new Controller.Configure())
                {
                    if (WebConfig.isset("message_pulling_sleep"))
                    {
                        messgePullingSleep = int.Parse(WebConfig.get("message_pulling_sleep"));
                    }
                    else if (controllerConfig.isSet("message_pulling_sleep"))
                    {
                        messgePullingSleep = controllerConfig.getInt("message_pulling_sleep");
                    }
                    else
                    {
                        messgePullingSleep = null;
                    }

                    if (messgePullingSleep != null)
                    {
                        _MessagePullingSleep = (int)messgePullingSleep;
                    }
                    else
                    {
                        _MessagePullingSleep = 500;
                    }
                }

                return _MessagePullingSleep;
            }
        }

        /// <summary>
        /// 网站程序URL
        /// </summary>
        public static string WebappURL
        {
            get
            {
                string weburl;
                string webalias;

                using (Controller.Configure controllerConfig = new Controller.Configure())
                {
                    if (WebConfig.isset("weburl"))
                    {
                        weburl = WebConfig.get("weburl");
                    }
                    else if (controllerConfig.isSet("weburl"))
                    {
                        weburl = controllerConfig.getString("weburl");
                        if (!weburl.EndsWith("/"))
                        {
                            weburl += "/";
                        }
                    }
                    else
                    {
                        weburl = "http://localhost/";
                    }

                    if (WebConfig.isset("webalias"))
                    {
                        webalias = WebConfig.get("webalias");
                    }
                    else if (controllerConfig.isSet("webalias"))
                    {
                        webalias = controllerConfig.getString("webalias");
                        if (!webalias.EndsWith("/"))
                        {
                            webalias += "/";
                        }
                    }
                    else
                    {
                        webalias = "OliveFramework/";
                    }

                }

                _WebappURL = weburl + webalias;
                return _WebappURL;
            }
        }

        /// <summary>
        /// 广告图片上传路径
        /// </summary>
        public static string AdUploadPath
        {
            get
            {
                using (Controller.Configure controllerConfig = new Controller.Configure())
                {
                    if (controllerConfig.isSet("aduploadpath"))
                    {
                        _AdUploadPath = controllerConfig.getString("aduploadpath");
                        if (!_AdUploadPath.EndsWith("/"))
                        {
                            _AdUploadPath += "/";
                        }
                    }
                }
                return _AdUploadPath;
            }
        }

        public static uint VersionCode
        {
            get
            {
                using (Controller.Configure controllerConfig = new Controller.Configure())
                {
                    if (WebConfig.isset("version_code"))
                    {
                        _VersionCode = uint.Parse(WebConfig.get("version_code"));
                    }else if(controllerConfig.isSet("version_code"))
                    {
                        _VersionCode = (uint)controllerConfig.getUint("version_code");
                    }else
                    {
                        _VersionCode = 0;
                    }

                }

                return _VersionCode;
            }
        }

        public static string VersionName
        {
            get
            {
                using (Controller.Configure controllerConfig = new Controller.Configure())
                {
                    if (WebConfig.isset("version_name"))
                    {
                        _VersionName = WebConfig.get("version_name");
                    }
                    else if (controllerConfig.isSet("version_name"))
                    {
                        _VersionName = controllerConfig.getString("version_name");
                    }
                    else
                    {
                        _VersionName = "";
                    }

                }

                return _VersionName;
            }
        }

        public static string APKFile
        {
            get
            {
                using (Controller.Configure controllerConfig = new Controller.Configure())
                {
                    if (WebConfig.isset("apk_file"))
                    {
                        _ApkFile = WebConfig.get("apk_file");
                    }
                    else if (controllerConfig.isSet("apk_file"))
                    {
                        _ApkFile = controllerConfig.getString("apk_file");
                    }
                    else
                    {
                        _ApkFile = "";
                    }

                }

                return _ApkFile;
            }
        }

        public static bool ForceUpdate
        {
            get
            {
                string keyname = "force_update";
                using (Controller.Configure controllerConfig = new Controller.Configure())
                {
                    if (WebConfig.isset(keyname))
                    {
                        _ForceUpdate =bool.Parse(WebConfig.get(keyname));
                    }
                    else if (controllerConfig.isSet(keyname))
                    {
                        _ForceUpdate = bool.Parse(controllerConfig.getString(keyname));
                    }
                    else
                    {
                        _ForceUpdate = false;
                    }

                }

                return _ForceUpdate;
            }
        }
    }
}
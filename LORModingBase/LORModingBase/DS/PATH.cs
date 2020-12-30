﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LORModingBase.DS
{
    class PATH
    {
        /// <summary>
        /// Export data directory
        /// </summary>
        public const string DIC_EXPORT_DATAS = ".\\exportedModes";

        /// <summary>
        /// Config file
        /// </summary>
        public const string CONFIG = "./config.json";
        /// <summary>
        /// Version info file
        /// </summary>
        public const string VERSION = "./version.txt";

        /// <summary>
        /// Mode resources
        /// </summary>
        public const string RELATIVE_DIC_LOR_MODE_RESOURCES = "LibraryOfRuina_Data\\Managed\\BaseMod";
        /// <summary>
        /// Mode staticInfo resources
        /// </summary>
        public static string RELATIVE_DIC_LOR_MODE_RESOURCES_STATIC_INFO = $"{RELATIVE_DIC_LOR_MODE_RESOURCES}\\StaticInfo";
        /// <summary>
        /// Mode localize resources
        /// </summary>
        public static string RELATIVE_DIC_LOR_MODE_RESOURCES_LOCALIZE = $"{RELATIVE_DIC_LOR_MODE_RESOURCES}\\Localize";
    }
}

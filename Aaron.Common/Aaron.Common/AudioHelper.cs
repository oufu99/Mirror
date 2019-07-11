using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Aaron.Common
{
    public class AudioHelper
    {

        public static void RenameFileInfo()
        {
            string path = @"D:\认知方法论-音频\bf0414.mp3";
            TagLib.File f = TagLib.File.Create(path);
            f.Tag.Album = "认知方法论";

            f.Save();
        }
    }
}
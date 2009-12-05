using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Naspinski.Utilities
{
    public static class FileStreamSave
    {
        /// <summary>
        /// Save a FileStream as a file, defaults to non-overwrite
        /// </summary>
        /// <param name="file">FileStream to be saved</param>
        /// <param name="path">path to save to</param>
        /// <returns>string FileName</returns>
        public static string Save(this FileStream file, string path)
        { return file.Save(path, false); }

        /// <summary>
        /// Save a FileStream as a file with optional overwrite
        /// </summary>
        /// <param name="file">FileStream to be saved</param>
        /// <param name="path">path to save to</param>
        /// <param name="overwrite">true will overwrite if the file already exists, false will append a count to the filename</param>
        /// <returns>string FileName</returns>
        public static string Save(this FileStream file, string path, bool overwrite)
        {
            int count = 1;
            string folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            int fileSize = Convert.ToInt32(file.Length);
            string fileName = Path.GetFileName(file.Name);
            Byte[] bytes = new Byte[fileSize];
            file.Read(bytes, 0, fileSize);
            string root = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path);

            while (!overwrite && File.Exists(path))
                path = root + "[" + count++.ToString() + "]" + Path.GetExtension(path);

            File.WriteAllBytes(path, bytes);
            return Path.GetFileName(path);
        }
    }
}

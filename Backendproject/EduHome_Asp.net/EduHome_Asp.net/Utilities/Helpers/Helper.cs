using System.IO;

namespace EduHome_Asp.net.Utilities.Helpers
{
    public static class Helper
    {
        public static string GetFilePath(string root,string folder ,string fileName)
        {
            return Path.Combine(root, folder, fileName);
        }

        public static void DeleteFile(string Path)
        {
            if (System.IO.File.Exists(Path))
            {
                System.IO.File.Delete(Path);
            }
        }


    }
}

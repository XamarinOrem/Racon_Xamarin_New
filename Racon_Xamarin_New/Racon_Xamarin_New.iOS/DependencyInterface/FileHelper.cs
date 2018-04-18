using System;
using System.IO;
using Racon_Xamarin_New.DependencyInterface;
using Racon_Xamarin_New.iOS.DependencyInterface;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Racon_Xamarin_New.iOS.DependencyInterface
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}

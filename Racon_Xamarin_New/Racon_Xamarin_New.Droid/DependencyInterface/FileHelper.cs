using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Racon_Xamarin_New.DependencyInterface;
using Racon_Xamarin_New.Droid.DependencyInterface;
using Xamarin.Forms;
using System.IO;


[assembly: Dependency(typeof(FileHelper))]
namespace Racon_Xamarin_New.Droid.DependencyInterface
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }

}
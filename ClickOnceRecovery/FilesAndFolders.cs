using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickOnceRecovery
{
    public class FilesAndFolders
    {
        protected ClickOnceRecovery.CallBackDelegate CallBack;
        protected ClickOnceRecovery.StatusDelegate Status;
        protected ApplicationDataClass AppData;


        protected void Copy(string destination, string source)
        {
            Status("Recovery dest : " + destination);
            Status("Recovery source : " + source);
            DirectoryInfo info = new DirectoryInfo(source);
            if (!Directory.Exists(destination))
            {
                Status("Directory in destination does not exists");
                Directory.CreateDirectory(destination);
            }

            foreach (var childDirectoy in info.GetDirectories())
            {
                Status("New folder");
                string destinationChild = Path.Combine(destination, childDirectoy.Name);
                Copy(destinationChild, childDirectoy.FullName);
            }
            Status("Look for files");
            foreach (var files in info.GetFiles())
            {
                string destiniationName = Path.Combine(destination, files.Name);
                string sourceName = files.FullName;
                Status("Dest: " + destiniationName);
                Status("Source: " + sourceName);

                if (!File.Exists(destiniationName))
                    File.Copy(sourceName, destiniationName);

            }

        }
    }
}

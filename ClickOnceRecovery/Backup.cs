using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClickOnceRecovery
{
    public class Backup : FilesAndFolders
    {   
        private const string EXENAME = "ClickOnceRecovery.exe";
        public static void Run(ApplicationDataClass appData, CallBackDelegate callBack, StatusDelegate status)
        {
            Backup process = new Backup();
            process.Status = status;
            process.CallBack = callBack;
            process.AppData = appData;

            Thread th = new Thread(new ThreadStart(process.RunProcess));

            th.Name = "Click once Backup";
            th.IsBackground = true;
            th.Start();
        }



        public void RunProcess()
        {
            DirectoryInfo info = new DirectoryInfo(AppData.ClickOnceLocation);

            List<DirectoryInfo> folders = info.GetDirectories().Where(x => x.Name.StartsWith(AppData.FolderStartsWith)).ToList();
            foreach(var folder in folders)
            {
                string location = Path.Combine(AppData.RecoveryLocation, folder.Name);
                Copy(location, folder.FullName);

            }


            getManifestFiles(info);


            getShortcut();

            CallBack();
        }

        private void getManifestFiles(DirectoryInfo infor)
        {
            List<DirectoryInfo> folders = infor.GetDirectories().Where(x => x.Name.StartsWith("manifests")).ToList();
            foreach (var folder in folders)
            {

                string location = Path.Combine(AppData.RecoveryLocation, folder.Name);
                var files = folder.GetFiles().Where(x => x.Name.StartsWith(AppData.FolderStartsWith)).ToList();
                if (!Directory.Exists(location))
                    Directory.CreateDirectory(location);

                foreach(var file in files)
                {
                    string fileLocation = Path.Combine(location, file.Name);
                    System.IO.File.Copy(file.FullName, fileLocation, true);
                }

                

            }

        }

        private void getShortcut()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DirectoryInfo info = new DirectoryInfo(path);

            DirectoryInfo infoApp = new DirectoryInfo(AppData.RecoveryLocation);
            var file = info.GetFiles().FirstOrDefault(x => x.Name.StartsWith(AppData.ShortCurtStartsWith) );
            if(file !=null)
            {
                string newlocation = Path.Combine(infoApp.Parent.FullName, file.Name);
                System.IO.File.Copy(file.FullName, newlocation, true);

                createShortcut( true);
            }
                
            
        }

        private void createShortcut( bool create)
        {
            if (create)
            {
                try
                {

                    string linkLocation = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), AppData.ShortCurtDisplay + ".lnk");

                    string exeLocation = Path.Combine(System.Windows.Forms.Application.StartupPath, EXENAME);
                    
                    if (System.IO.File.Exists(linkLocation))
                        System.IO.File.Delete(linkLocation);


                    WshShell myShell = new WshShell();
                    WshShortcut myShortcut = (WshShortcut)myShell.CreateShortcut(linkLocation);
                    myShortcut.TargetPath = exeLocation; //The exe file this shortcut executes when double clicked 
                    myShortcut.IconLocation = exeLocation + ",0"; //Sets the icon of the shortcut to the exe`s icon 
                    myShortcut.WorkingDirectory = System.Windows.Forms.Application.StartupPath; //The working directory for the exe 
                    myShortcut.Description = "Click Once Backup";
                    
                    myShortcut.Arguments = ""; //The arguments used when executing the exe 
                    myShortcut.Save(); //Creates the shortcut 
                }
                catch (Exception ex)
                {
                  
                }
            }
            else
            {
                try
                {

                    string linkLocation = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), AppData.ShortCurtDisplay + ".lnk");

                    if (System.IO.File.Exists(linkLocation))
                        System.IO.File.Delete(linkLocation);
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
    }
}

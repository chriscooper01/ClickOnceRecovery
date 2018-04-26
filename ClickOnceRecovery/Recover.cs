using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClickOnceRecovery
{
    public class Recover : FilesAndFolders
    {
      

        public static void Run(ApplicationDataClass appData, CallBackDelegate callBack, StatusDelegate status)
        {
            Recover process = new Recover();
            process.Status = status;
            process.CallBack = callBack;
            process.AppData = appData;
            Thread th = new Thread(new ThreadStart(process.RunProcess));
           
            th.Name = "Click Once Recovery";
            th.IsBackground = true;
            th.Start();
        }

        

        public void RunProcess()
        {
            string requiredFolder = Path.Combine(AppData.ClickOnceLocation, AppData.CurrentFolder);
            if (!Directory.Exists(requiredFolder))
            {

                Copy(AppData.ClickOnceLocation, AppData.RecoveryLocation);

            }
            
            DirectoryInfo info = new DirectoryInfo(AppData.RecoveryLocation);


            var file = info.Parent.GetFiles().FirstOrDefault(x => x.Extension.Equals(".appref-ms"));
            if(file !=null)
            {
                System.Diagnostics.Process.Start(file.FullName);
            }

            

            CallBack();
        }

      
    }
}

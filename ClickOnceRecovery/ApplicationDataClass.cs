using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickOnceRecovery
{
    public class ApplicationDataClass
    {
        public string ClickOnceLocation { get; set; }
        public string RecoveryLocation { get; set; }
        public string CurrentFolder { get; set; }
        /// <summary>
        /// This is the Name for the application exe file, which is used within Click onces
        /// </summary>
        public string ExeName { get; set; }
        /// <summary>
        /// This is what your new short cut will be called on the desktop
        /// </summary>
        public string ShortCurtDisplay { get; set; }

        /// <summary>
        /// This is what the click once short cut on the desktop start with. 
        /// So you can locate the required shortcut
        /// </summary>
        public string ShortCurtStartsWith { get; set; }
        /// <summary>
        /// This is what your Click once folders start within
        /// /// So you can locate the required folders
        /// </summary>
        public string FolderStartsWith { get; set; }
    }
}

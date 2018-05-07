using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;  

namespace MyWindowsLogService
{
    public partial class WindowsLog : ServiceBase
    {
        public WindowsLog()
        {
            InitializeComponent();
            if(!System.Diagnostics.EventLog.SourceExists("ICI2ScriptFolderClean"))
            {
                System.Diagnostics.EventLog.CreateEventSource("ICI2ScriptFolderClean", "CleanFailImageFolder");
            }
            eventLog1.Source = "ICI2ScriptFolderClean";
            eventLog1.Log = "CleanFailImageFolder";

        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("It has started cleaning the ICI2.0 Fail Image Folder!");
            ///folder path
            string strFolderPath = @"D:\IT&V Automation Test\ICI 2.0 Script\02 K226\04 Script Execute Report\Fail Image Folder";
            //string strFolderPath = System.Web.HttpContext.Current.Server.MapPath("C:\\D Disk") + "\\TestFolder\\";
            DirectoryInfo dyInfo = new DirectoryInfo(strFolderPath);
            ///get all files in the path
            foreach(FileInfo feInfo in dyInfo.GetFiles())
            {
                //delete all files 7 days before today
                if (feInfo.CreationTime.DayOfYear < DateTime.Today.DayOfYear - 7)
                    feInfo.Delete();
            }

            string strFolderPath01 = @"D:\IT&V Automation Test\ICI 2.0 Script\02 K226\04 Script Execute Report";
            DirectoryInfo dyInfo01 = new DirectoryInfo(strFolderPath01);
            foreach(FileInfo feInfo01 in dyInfo01.GetFiles())
            {
                if (feInfo01.CreationTime.DayOfYear < DateTime.Today.DayOfYear - 7)
                    feInfo01.Delete();
            }
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Service for Cleaning ICI2.0 Fail Image Folder has stopped!");
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordLedger.Utils
{
    class FS
    {
        string fileLocation;

        public bool isFileExists = false;
        public bool isLastQueryFailed = false;
        public string lastErrorMSG = "";
        public Exception? lastException = null;

        public FS() { }

        public FS(string fileLocation) {
            this.SetFile(fileLocation);
        }

        public string GetFileName() => this.fileLocation;

        public bool SetFile(string path)
        {
            this.fileLocation = path;
            try
            {
                this.isFileExists = File.Exists(path);
            }catch (Exception ex)
            {
                this.SetErrors(ex);
                this.isFileExists = false;
            }
            return this.isFileExists;
        }

        public string ReadFile()
        {
            string rData = "";
            if(isFileExists)
            {
                try
                {
                    rData = File.ReadAllText(this.fileLocation);
                }catch (Exception ex)
                {
                    this.SetErrors(ex);
                }
            }
            return rData;
        }

        private void SetErrors(Exception ex)
        {
            this.isLastQueryFailed = true;
            this.lastErrorMSG = ex.Message;
            this.lastException = ex;
        }

    }
}

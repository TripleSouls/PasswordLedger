using PasswordLedger.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordLedger.Helpers
{
    class FS
    {
        string fileLocation;

        public bool isFileExists = false;
        public bool isLastQueryFailed = false;
        public string lastErrorMSG = "";
        public Exception? lastException = null;

        public FS() {  }

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
            if(!isFileExists) return "";
            string rData = "";

            try
            {
                rData = File.ReadAllText(this.fileLocation);
            }catch (Exception ex)
            {
                this.SetErrors(ex);
            }

            rData = Security.Instance.Decode(rData);

            return rData;
        }

        public void WriteFile(string JSONString)
        {
            JSONString = Security.Instance.Encode(JSONString);


            if(!isFileExists)
            {
                CreateFileIfNotExists(fileLocation, JSONString);
                return;
            }

            FileStream filestream = File.OpenWrite(fileLocation);
            StreamWriter sw = new StreamWriter(filestream);
            sw.Write(JSONString);
            sw.Close();
            filestream.Close();
        }

        private void SetErrors(Exception ex)
        {
            this.isLastQueryFailed = true;
            this.lastErrorMSG = ex.Message;
            this.lastException = ex;
        }

        public static OperationResult CreateFolderIfNotExists(string path)
        {
            try
            {
                if (Directory.Exists(path)) { }
                else
                {
                    Directory.CreateDirectory(path);
                }
                return new OperationResult() { Success = true };
            }catch(Exception ex) {
                return new OperationResult() { Success = false, FailureSource = ex.Source, FailureMessage = ex.Message };
            }
        }

        public static OperationResult CreateFileIfNotExists(string path, string data = "")
        {
            data = Security.Instance.Encode(data);

            try
            {
                if (File.Exists(path)) { }
                else
                {
                    FileStream filestream = File.Create(path);
                    StreamWriter sw = new StreamWriter(filestream);
                    sw.Write(data);
                    sw.Close();
                    filestream.Close();
                }
                return new OperationResult() { Success = true };
            }
            catch (Exception ex)
            {
                return new OperationResult() { Success = false, FailureSource = ex.Source, FailureMessage = ex.Message };
            }
        }

    }
}

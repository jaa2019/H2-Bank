﻿using System;
using System.IO;

namespace H2_Bank.Utilities
{
    static class FileLogger
    {
        static string fileName = "logs.bnk";

        public static void WriteToLog(string logmessage)
        {
            string writeMsg = logmessage + "\n";
            File.AppendAllText(fileName, writeMsg);
        }

        public static string ReadFromLog()
        {
            DirectoryInfo findLog = new DirectoryInfo(Environment.CurrentDirectory);
            foreach (FileInfo filer in findLog.GetFiles())
            {
                if (filer.Extension == ".bnk")
                {
                    string foundFile = File.ReadAllText(filer.FullName);
                }
            }
            return null;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Plasmic_Client
{
    class Logger
    {
        public static void Log(string log)
        {
    File.AppendAllText("Workspace/Logs.txt",
              Environment.NewLine + log);
        }
        public static void Gamertag(string username)
        {
            File.ReadLines("Gamertag:");
        }
    }
    }

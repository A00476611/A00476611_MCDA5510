using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAssignment
{
    internal static class Walker
    {
        public static void walkThen(string path, Action<string> action)
        {
            string[] dirs = Directory.GetDirectories(path);
            foreach (string dirpath in dirs)
            {
                if (Directory.Exists(dirpath))
                {
                    walkThen(dirpath, action);
                }
            }
            string[] files = Directory.GetFiles(path);
            foreach (string filepath in files)
            {
                action(filepath);
            }
        }
    }
}

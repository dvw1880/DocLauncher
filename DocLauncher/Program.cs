using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace DocLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            // Verify input was provided
            if (args.Length == 0)
            {
                MessageBox.Show("Error: No path to launch");
                return;
            }

            // Verify inputs matches correct prefix
            string prefix = "doclaunch:";
            if (!args[0].StartsWith(prefix))
            {
                MessageBox.Show($"Invalid URL path {args[0]}");
                return;
            }

            // Get the path to launch
            string encodedPath = args[0].Substring(prefix.Length);

            // Verify some path was actually provided
            if (String.IsNullOrEmpty(encodedPath))
            {
                MessageBox.Show($"Invalid URL path {args[0]}");
                return;
            }

            // Strip out any additional quotes. Windows run command adds trailing quotes sometimes
            encodedPath = encodedPath.Replace("\"","");

            // Decode the string in case it came from a web browser
            string path = HttpUtility.UrlDecode(encodedPath);

            try
            {
                // Launch the path
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to launch {path} \r\n{ex.Message}");
            }
        }
    }
}
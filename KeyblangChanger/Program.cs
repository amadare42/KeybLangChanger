using System;
using System.Windows.Forms;

namespace KeyblangChanger
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                var action = args[0];

                switch (action)
                {
                    case "--help":
                    case "/?":
                        PrintHelp();
                        break;
                        
                    case "set":
                        if (args.Length < 2)
                        {
                            PrintHelp();
                            break;
                        }

                        Set(args[1]);
                        break;

                    default:
                        PrintHelp();
                        break;
                }
                Application.Exit();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        
        public static void PrintHelp()
        {
            MessageBox.Show(@"Usage:
set <layout id> | set <culture name> - sets layout of foreground window
keyblangchanger.exe set en-us
keyblangchanger.exe set 4090409
");
        }

        public static void Set(string lang)
        {
            if (uint.TryParse(lang, out _))
            {
                Changer.SetByLayout(lang);
            }
            else
            {
                Changer.SetByCulture(lang);
            }
        }
    }
}
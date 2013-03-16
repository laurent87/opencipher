// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Open Cipher">
//   Open Cipher is free software distributed under GPL version 3 license
//   http://www.gnu.org/licenses/gpl-3.0.html
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenCipher
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// The program.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">
        /// The arguments.
        /// </param>
        [STAThread]
        public static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 1)
            {
                String inputFile = args[0];
                Application.Run(new FileEncryptForm(inputFile));
            }
            else
            {
                Application.Run(new MainForm());
            }
        }
    }
}

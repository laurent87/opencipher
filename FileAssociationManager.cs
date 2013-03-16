// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileAssociationManager.cs" company="Open Cipher">
//   Open Cipher is free software distributed under GPL version 3 license
//   http://www.gnu.org/licenses/gpl-3.0.html
// </copyright>
// <summary>
//   The file association manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenCipher
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    using Microsoft.Win32;

    using OpenCipher.Properties;

    /// <summary>
    /// The file association manager.
    /// </summary>
    public class FileAssociationManager
    {
        /// <summary>
        /// Check if file associations are correct
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static Boolean CheckAssociations()
        {
            String openCommand = "\"" + Application.ExecutablePath + "\" \"%1\"";

            List<String> keys = new List<String>
                                    {
                                        "*\\shell\\Open Cipher\\command", 
                                        ".aes\\shell\\decrypt\\command",
                                        ".asc\\shell\\decrypt\\command"
                                    };

            foreach (String key in keys)
            {
                RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(key);
                if (registryKey == null)
                {
                    return false;
                }

                String registryKeyValue = registryKey.GetValue(String.Empty).ToString();
                if (registryKeyValue != openCommand)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// The fix associations.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static Boolean FixAssociations()
        {
            String filePath = Path.GetTempFileName() + @".reg";

            String fileContent = GenerateRegFile(Resources.FileAssociationsRegFile);

            using (StreamWriter file = new StreamWriter(filePath, false, Encoding.Unicode))
            {
                file.WriteLine(fileContent);
                file.Close();
            }

            Process.Start(filePath);

            return true;
        }

        /// <summary>
        /// The generate registry update file.
        /// </summary>
        /// <param name="registryFile">
        /// The registry file.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static String GenerateRegFile(String registryFile)
        {
            String exe = Application.ExecutablePath.Replace("\\", "\\\\");
            String directoryName = Path.GetDirectoryName(Application.ExecutablePath);
            if (directoryName == null)
            {
                throw new Exception("Can't get the application path.");
            }

            String directory = directoryName.Replace("\\", "\\\\");

            registryFile = registryFile.Replace("[[OPEN_CIPHER_EXE]]", exe);
            registryFile = registryFile.Replace("[[OPEN_CIPHER_AES_ICO]]", directory + "\\\\aes.ico");
            registryFile = registryFile.Replace("[[OPEN_CIPHER_ASC_ICO]]", directory + "\\\\asc.ico");

            return registryFile;
        }
    }
}

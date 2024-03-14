using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AppHostPatcher;

internal class Program
{
    private static void Usage()
    {
        Console.WriteLine("apphostpatcher <apphostexe> <origdllpath> <newdllpath>");
        Console.WriteLine("apphostpatcher <apphostexe> <newdllpath>");
        Console.WriteLine("apphostpatcher <apphostexe> -d <newsubdir>");
        Console.WriteLine("example: apphostpatcher my.exe -d bin");
    }

    private const int maxPathBytes = 1024;

    /// <summary>
    ///     Windows apphosts have an .exe extension. Don't call Path.ChangeExtension() unless it's guaranteed
    ///     to have an .exe extension, eg. 'some.file' => 'some.file.dll', not 'some.dll'
    /// </summary>
    /// <param name="apphostExe">App Host EXE</param>
    /// <returns>Path.</returns>
    private static string ChangeExecutableExtension(string apphostExe) =>
        apphostExe.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)
        ? Path.ChangeExtension(apphostExe, ".dll")
        : apphostExe + ".dll";

    private static string GetPathSeparator(string apphostExe) =>
        apphostExe.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)
        ? @"\"
        : "/";

    private static int Main(string[] args)
    {
        try
        {
            string apphostExe, origPath, newPath;
            if (args.Length == 3)
            {
                if (args[1] == "-d")
                {
                    apphostExe = args[0];
                    origPath = Path.GetFileName(ChangeExecutableExtension(apphostExe));
                    newPath = args[2] + GetPathSeparator(apphostExe) + origPath;
                }
                else
                {
                    apphostExe = args[0];
                    origPath = args[1];
                    newPath = args[2];
                }
            }
            else if (args.Length == 2)
            {
                apphostExe = args[0];
                origPath = Path.GetFileName(ChangeExecutableExtension(apphostExe));
                newPath = args[1];
            }
            else
            {
                Usage();
                return 1;
            }

            if (!File.Exists(apphostExe))
            {
                Console.WriteLine($"Apphost '{apphostExe}' does not exist");
                return 1;
            }

            if (origPath == string.Empty)
            {
                Console.WriteLine("Original path is empty");
                return 1;
            }

            var origPathBytes = Encoding.UTF8.GetBytes(origPath + "\0");
            Debug.Assert(origPathBytes.Length > 0);
            var newPathBytes = Encoding.UTF8.GetBytes(newPath + "\0");

            if (origPathBytes.Length > maxPathBytes)
            {
                Console.WriteLine($"Original path is too long");
                return 1;
            }

            if (newPathBytes.Length > maxPathBytes)
            {
                Console.WriteLine($"New path is too long");
                return 1;
            }

            var apphostExeBytes = File.ReadAllBytes(apphostExe);
            int offset = GetOffset(apphostExeBytes, origPathBytes);
            if (offset < 0)
            {
                Console.WriteLine($"Could not find original path '{origPath}'");
                return 1;
            }

            if (offset + newPathBytes.Length > apphostExeBytes.Length)
            {
                Console.WriteLine($"New path is too long: {newPath}");
                return 1;
            }

            for (int i = 0; i < newPathBytes.Length; i++)
                apphostExeBytes[offset + i] = newPathBytes[i];

            File.WriteAllBytes(apphostExe, apphostExeBytes);
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return 1;
        }
    }

    private static int GetOffset(byte[] bytes, byte[] pattern)
    {
        int si = 0;
        var b = pattern[0];

        while (si < bytes.Length)
        {
            si = Array.IndexOf(bytes, b, si);
            if (si < 0)
                break;

            if (Match(bytes, si, pattern))
                return si;

            si++;
        }

        return -1;
    }

    private static bool Match(byte[] bytes, int index, byte[] pattern)
    {
        if (index + pattern.Length > bytes.Length)
            return false;

        for (int i = 0; i < pattern.Length; i++)
        {
            if (bytes[index + i] != pattern[i])
                return false;
        }

        return true;
    }
}

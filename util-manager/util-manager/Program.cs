using System;
using System.IO;
using System.Linq;

namespace util_manager
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string s in args)
            {

                if(s.ToLower().Contains("batchmaker"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Please Enter name of command");
                    ConsoleTools.InvertConsole();
                    string Dataentry1 = Console.ReadLine();
                    ConsoleTools.InvertConsole();
                    Console.Write("Please Enter path of bin file");
                    ConsoleTools.InvertConsole();
                    string Dataentry2 = Console.ReadLine();
                    ConsoleTools.InvertConsole();
                    BatchFactory bf = new BatchFactory();
                    ConsoleTools.InvertConsole();
                    bf.CreateBinaryBatchLinker(Dataentry1, Dataentry2);
                    ConsoleTools.InvertConsole();
                    Environment.Exit(0);
                }
                else if (s.ToLower().Contains("c"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Writing Folders to Hard Disk");
                    FolderCreationTools.CreateFolders();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Opperation Completed");
                    Console.ResetColor();
                }



                //check for arguments here
            }

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Please Enter Data To test:>>");
                ConsoleTools.InvertConsole();
                string Dataentry = Console.ReadLine();
                ConsoleTools.InvertConsole();
                BatchFactory bf = new BatchFactory();
                ConsoleTools.InvertConsole();
                Console.WriteLine(bf.RipDirFromFilepath(Dataentry) + "\n\n");
                Console.WriteLine(bf.RipFileNameFromFilepath(Dataentry) + "\n\n");
                ConsoleTools.InvertConsole();
            }
        }
    }
    static public class ConsoleTools
    {
        static public void InvertConsole()
        {
            ConsoleColor oldFore = Console.ForegroundColor;
            ConsoleColor oldBack = Console.BackgroundColor;
            Console.ForegroundColor = oldBack;
            Console.BackgroundColor = oldFore;
        }
    }
    /// <summary>
    /// Used to create batch files
    /// </summary>
    class BatchFactory
    {
        string ExampleBatchFile = "@echo off\nREM AUTOMATICLY GENERATED BATCH FILE BELOW\n";
        public void CreateExampleBatchFile(string Filepath)
        {
            File.WriteAllText(Filepath, ExampleBatchFile);
        }
        public void CreateBinaryBatchLinker(string commandname,string BinFilepath)
        {
            string BatFilePath = FolderCreationTools.WorkingFolder + "/" + commandname + ".bat";
            if (File.Exists(BatFilePath))
            {
                throw new Exception("command name already implemented Exception");
            }
            else
            {
                File.WriteAllText(BatFilePath, ExampleBatchFile + "REM CreateBinaryBatchLinker() Data Below\n" + "pushd " + RipDirFromFilepath(BinFilepath) + "\n" + RipFileNameFromFilepath(BinFilepath));
            }
        }
        public string RipDirFromFilepath(string filepath)
        {
            if (filepath.Contains('.'))
            {
                char slashmodechar = '/';
                bool forwardslashmode = false;
                bool backslashmode = false;
                if(filepath.Contains('/'))
                {
                    forwardslashmode = true;
                }
                if(filepath.Contains('\\'))
                {
                    backslashmode = true;
                }

                if(backslashmode == forwardslashmode)
                {
                    throw new FormatException();    //bad format
                }
                if(forwardslashmode)
                {
                    slashmodechar = '/';
                }
                else
                {
                    slashmodechar = '\\';
                }

                //good
                int IndexofPeriod = filepath.LastIndexOf('.');
                int b = filepath.LastIndexOf(slashmodechar);
                if(IndexofPeriod>b)
                {
                    //good
                    return filepath.Remove(b);
                }
                else
                {
                    //bad
                    throw new Exception("filepath doesnt contain a fileextension not sure what to do(2)");
                }
            }
            else
            {
                //bad
                throw new Exception("filepath doesnt contain a fileextension not sure what to do");
            }
        }
        public string RipFileNameFromFilepath(string filepath)
        {
            if (filepath.Contains('.'))
            {
                char slashmodechar = '/';
                bool forwardslashmode = false;
                bool backslashmode = false;
                if (filepath.Contains('/'))
                {
                    forwardslashmode = true;
                }
                if (filepath.Contains('\\'))
                {
                    backslashmode = true;
                }

                if (backslashmode == forwardslashmode)
                {
                    throw new FormatException();    //bad format
                }
                if (forwardslashmode)
                {
                    slashmodechar = '/';
                }
                else
                {
                    slashmodechar = '\\';
                }

                //good
                int IndexofPeriod = filepath.LastIndexOf('.');
                int IndexofSlash = filepath.LastIndexOf(slashmodechar);
                if (IndexofPeriod > IndexofSlash)
                {
                    //good
                    return filepath.Substring(IndexofSlash + 1);
                }
                else
                {
                    //bad
                    throw new Exception("filepath doesnt contain a fileextension not sure what to do(2)");
                }
            }
            else
            {
                //bad
                throw new Exception("filepath doesnt contain a fileextension not sure what to do");
            }
        }
    }

    /// <summary>
    /// Tools for creating folder structure
    /// </summary>
    static class FolderCreationTools
    {
        static public string WorkingFolder = "C:/Utils";
        static public void CreateFolders()
        {
            Directory.CreateDirectory("C:/Utils");
            Directory.CreateDirectory("C:/Utils/bin");
            Directory.CreateDirectory("C:/Utils/conf");
            Directory.CreateDirectory("C:/Utils/util-manager");
        }
    }
}

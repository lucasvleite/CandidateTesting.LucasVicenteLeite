using CandidateTesting.LucasVicenteLeite.ConsoleApplication.Model;
using CandidateTesting.LucasVicenteLeite.ConsoleApplication.Services;
using CandidateTesting.LucasVicenteLeite.ConsoleApplication.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace CandidateTesting.LucasVicenteLeite.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Commands: ");
            string readCommands = Console.ReadLine();
            var commands = readCommands?.Split(" ");

            if (commands.Length == 3)
            {
                string urlFile = commands[1];
                string outPathFile = commands[2];
                if (VerifyCommandsEntry(commands[0], urlFile, outPathFile))
                {
                    ConvertToAgoraTemplateService service = new ConvertToAgoraTemplateService();
                    if (service.ConvertMinhaCdnToAgoraModel(urlFile, outPathFile))
                        Console.WriteLine("Successfully generated file");
                    else
                        Console.WriteLine("Error Convert");
                }
            }
            else
            {
                Console.WriteLine("Error Commands");
            }
        }

        private static bool VerifyCommandsEntry(string command, string urlFile, string outPathFile)
        {
            try
            {
                if (command == "convert" && Uri.TryCreate(urlFile, UriKind.RelativeOrAbsolute, out Uri uriResult))
                {
                    var path = outPathFile.Remove(outPathFile.LastIndexOf('/'));
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}

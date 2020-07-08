using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Collections;

namespace projectsummer
{
    
    class Program
    {
        internal static void SaveLogs(ArrayList logs)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Would you like to save logs? Y/N");
            string get = Console.ReadLine();
            if (get.ToLower() == "y")
            {
                Console.WriteLine("Logs are starting to save!");
                string[] myArray = (string[])logs.ToArray(typeof(string));
                File.WriteAllLines("logs.txt", myArray);
                Console.WriteLine("Logs are saved");
            }
            else
                Console.WriteLine("Logs will not saved");
        }
        class Search
        {
            internal static void Search_(string[] link2, string[] extensions2,int length)
            {
                ArrayList list = new ArrayList();
                Console.WriteLine("\n Starting...");

                for (int i = 0; i < link2.Length; i++)
                {
                    for (int z = 0; z <= length; z++)
                    {
                        try
                        {
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link2[i] + extensions2[z]);
                            var response = request.GetResponse();
                            request.Abort();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(" Found! " + link2[i] + extensions2[z]);
                            list.Add(link2[i] + extensions2[z]);
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" Not found! " + link2[i] + extensions2[z]);
                        }
                    }
                    Console.WriteLine("\n");
                }
                Console.WriteLine("Searching succesfully done");
                SaveLogs(list);
            }
        }
        class GetWebsite
        {
           internal static string[] Sorgula()
            {
                Sorgu:
                try
                {
                    Console.WriteLine(" If you want to use for just one site press 1, \n If you will use multiple sites from text file press 2");
                    int a = int.Parse(Console.ReadLine());
                    switch (a)
                    {
                        case 1:
                            return GetOne();
                        case 2:
                            return GetFromFile();
                        default:
                            goto Sorgu;
                    }
                }
                catch 
                {
                    goto Sorgu;
                }
               
            }
            internal static string[] GetOne()
            {
                Console.WriteLine("Please write website to find panel!");
                string[] site = { Console.ReadLine() };
                return site;
            }
            internal static string[] GetFromFile()
            {
                string getpath;
                Path:
                Console.WriteLine(@"Please select your text file path! Example: C:\Users\sites.txt ");
                Console.Write("\n Text file must be like this: \n www.example.com \n www.example2.com \n www.example3.com \n ");
                getpath = Console.ReadLine();
                string[] site;
                try
                {
                    site = File.ReadAllLines(getpath);
                }
                catch 
                {
                    Console.WriteLine("File not found, Please try again");
                    Console.Clear();
                    goto Path;
                }
                return site;
            }
        }
        static void Main(string[] args)
        {
            WebClient client = new WebClient();
            string a = client.DownloadString("https://raw.githubusercontent.com/Nighty-34/Admin_panel_bulucu-tht/master/admin-panel-bulucu/link.txt");
            string[] kes = a.Split('\n');
            string read = File.ReadAllText("panel.txt");
            string[] kes2 = read.Split('\n');
            Basla:
            Console.WriteLine("Which search type do you want to use? \n 1- Simple Search Total 30 Keywords  \n 2- Advanced Search Total "+kes.Length +" Keywords \n 3- Advanced Search-2 Total "+kes2.Length +" Keywords ");
            string al = Console.ReadLine();
            string[] siteler;
           switch (al)
            {
                case "1":
                    siteler = GetWebsite.Sorgula();
                    Search.Search_(siteler,kes,30);
                    break;
                case "2":
                    siteler = GetWebsite.Sorgula();
                    Search.Search_(siteler, kes, kes.Length);
                    break;
                case "3":
                    siteler = GetWebsite.Sorgula();
                    Search.Search_(siteler, kes2, kes2.Length);
                    break;
                default:
                    Console.WriteLine("You wrote wrong number!");
                    Console.Clear();
                    goto Basla;
            }
            Console.ReadKey();
        }
      
    }
}

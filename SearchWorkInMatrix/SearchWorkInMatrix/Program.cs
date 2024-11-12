using Finder.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Finder.ColsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var Work = true;
               
                while (Work)
                {
                    Console.Clear();
               
                    Console.WriteLine("Please provide the path to the file that contains the board:");
                    
                    var pathToFile = Console.ReadLine();
                    
                    if (File.Exists(pathToFile))
                    {
                        // we read all the lines from the document 
                        var lines = File.ReadAllLines(pathToFile);


                        var finder = new WordFinder(lines);

                        Console.WriteLine("Please provide the path to the file that contains the words stream:");
                        pathToFile = Console.ReadLine();
                        if (File.Exists(pathToFile))
                        {
                            lines = File.ReadAllLines(pathToFile);

                            // no need to search for the word many time 
                            var ListWordsToSearchBy = lines[0].Split(' ').Distinct().ToList();
                                                     

                            var results = finder.Find(ListWordsToSearchBy);

                            foreach (var item in results)
                            {
                                Console.WriteLine(item);
                            }
                   
                        }
                        else
                        {
                            Console.WriteLine($"The file does not exists.");
       
                        }
                    }
                    else
                    {
                   
                        Console.WriteLine($"The file does not exists.");
                   
                    }


                    //The user can close the application
                    Console.WriteLine("Close the console (Y/N):");

                    if (Console.ReadLine().ToUpper() == "Y")
                        Work = false;
  
                }

            }
             
            catch (Exception ex)
            {
                Console.WriteLine(" Please try again");
            }
        }
    }
}




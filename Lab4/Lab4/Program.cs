using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;


namespace Lab4
{
    public class Program
    {
        public static void Main(string[] args)
        {
					
            string way = args[0];
			ReadFile(way);
            Regex regex = new Regex(@"(?<name>([A-Z]|[a-z])\w*)(\s*=)\s*(?<value>\w+.*)"); // обработчик регулярных выражений
            List<Pair> results = new List<Pair>();
            if (File.Exists(way))						// Проверка на существование файла Program.cs
            {								 
                    FileToList(way, results, regex);
										
                    Console.WriteLine("1-Добавить");
                    Console.WriteLine("2-Удалить");
                    Console.WriteLine("3-Сортировать по имени по убывания");
                    Console.WriteLine("4-Сортировать по значению по убыванию");
                    Console.WriteLine("5-Сортировать по имени по возрастанию");
                    Console.WriteLine("6-Сортировать по значению по возрастанию");
                    Console.WriteLine("7-Фильтр");
                    Console.WriteLine("8-Выход");
                    
                    bool b = true;
                    while (b ==true)
                    {
                        string a = Console.ReadLine();

                        switch (a)
                        {
                            case "1":
                                Console.WriteLine("Введите пару для добавления");
                                string record = Console.ReadLine();
                                AddToList(record, results, regex);
                                WriteListAnConsole(results);
                                break;
                            case "2":
                                Console.WriteLine("Введите имя для удаления");
                                string namepair = Console.ReadLine();
                                DeleteFromList(namepair, results);
                                WriteListAnConsole(results); 
                                break;
                            case "3":
                                List<Pair> SortedResultsDescName = GetSortedResultsDescName(results);
                                WriteListAnConsole(SortedResultsDescName);
                                break;
                            case "4":
                                List<Pair> SortedResultsDescValue = GetSortedResultsDescValue(results);
                                WriteListAnConsole(SortedResultsDescValue);
                                break;
                            case "5":
                                List<Pair> SortedResultsAsceName = GetSortedResultsAsceName(results);
                                WriteListAnConsole(SortedResultsAsceName);
                                break;
                            case "6":
                                List<Pair> SortedResultsAsceValue = GetSortedResultsAsceValue(results);
                                WriteListAnConsole(SortedResultsAsceValue);
                                break;
                            case "7":
                                Console.WriteLine("Введите значении для фильтрации");
                                string filter = Console.ReadLine();                                
								List<Pair> Fil = FilterList(filter, results, regex);
								WriteListAnConsole(Fil);								                              
                                break;
                            case "8":
                                b = false;
                                break;
                            default:
                                Console.WriteLine("Введите команду");
                                break;                                
                        }
                    }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Файл не существует");
                Console.ReadKey();
            }	
        }

				static void ReadFile(string name)
				{
					// using гарантирует что переменная "StreamReader reader" будет закрыта
					using (StreamReader reader = File.OpenText(name))  // Читать из потока. Открыть файл
					{
						string line = null;
						do
						{
							line = reader.ReadLine(); // Читается строчка из файла
							Console.WriteLine(line);  // Строчка выводится на консоль
						}
						while (line != null);         // Пока файл не закончится
					}
				}



				static void FileToList(string way, List<Pair> results, Regex regex)
				{
					using (StreamReader reader = File.OpenText(way))							// Читать из потока. Открыть файл
					{
						while (reader.EndOfStream == false)								// пока файл не закончится
						{
							string line = reader.ReadLine();						// Читается строка из файла
							MatchCollection matches = regex.Matches(line);
							foreach (Match match in matches)
							{

								string name = match.Groups["name"].Value;
								string value = match.Groups["value"].Value;
								results.Add(new Pair() { Name = name, Value = value });

							}

						}
					}
				}



				static void AddToList(string line, List<Pair> results, Regex regex)
				{

					MatchCollection matches = regex.Matches(line);
					foreach (Match match in matches)
					{

						string name = match.Groups["name"].Value;
						string value = match.Groups["value"].Value;
						results.Add(new Pair() { Name = name, Value = value });

						

					}


				}

                static List<Pair> GetSortedResultsDescName(List<Pair> results)
                {
                    List<Pair> sorted = (from n in results
                                         orderby n.Name descending
                                         select n).ToList();
                    return sorted;

                }

                static List<Pair> GetSortedResultsDescValue(List<Pair> results)
                {
                    List<Pair> sorted = (from n in results
                                         orderby n.Value descending
                                         select n).ToList();
                    return sorted;
                }
                static List<Pair> GetSortedResultsAsceName(List<Pair> results)
                {
                    List<Pair> sorted = (from n in results
                                         orderby n.Name ascending
                                         select n).ToList();
                    return sorted;
                }
                public static List<Pair> GetSortedResultsAsceValue(List<Pair> results)
                {
                    List<Pair> sorted = (from n in results
                                         orderby n.Value ascending
                                         select n).ToList();
                    return sorted;
                }
        

                static void DeleteFromList(string namepair, List<Pair> results)
                {

                    results.RemoveAll(x =>
                    {
						return (String.Compare(namepair, x.Name) == 0);
                    });                    

                }

				public static List<Pair> FilterList(string filter, List<Pair> results, Regex regex)
                {
				   int langthFiltr = filter.Length;
				   List<Pair> pfilt  = results.FindAll(pair =>
				   {
					   return (String.Compare(pair.Name, 0, filter, 0 ,langthFiltr) == 0);
				   }); 

					//foreach (Pair i in results)
					//{
					//	string name = i.Name;
					//	Regex sr = new Regex(filter.ToUpper()); // регулярное выражение
					//	Match fi = sr.Match(name.ToUpper());//ищем символ в слове
					//	if (fi.Success == true)   //если он есть 
					//	{
					//		Console.WriteLine("{0} {1}", i.Name, i.Value);
					//	}
					//}
				   return pfilt;
				   
                }

                static void WriteListAnConsole(List<Pair> results)
                {
                    foreach (Pair i in results)
                    {
                        Console.WriteLine("{0} {1}", i.Name, i.Value);
                    }
                }	     

       


      

        //static void AddToList(string way, string line)
        //{
        //    using (StreamWriter writer = File.AppendText(way))
        //    {
               
        //        writer.WriteLine(line);
               
        //    }
        //}




    }
}

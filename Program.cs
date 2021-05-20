

/** 
    Choose-Media-Display
    Lab 3 for Advanced Programmming in .NET
    Created by Norman Potts
    Goal of Project 
        Promp user by a menu to display the media objects in five different ways.
        Continues to prompt the user until the user selects the exit option.
        Manages error hangling for user input.
        Makes use of interfaces.
        Ways media objects can be listed by:
        1. List all books.
        2. List all Movies.
        3. List all songs.
        4. List all media.
        5. Search all media by title.
        When user enters number six the program will exit.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    ///  Lab 3 for Programming in .NET
    /// </summary>
    class Program
    {
        static Media[] allMedia = new Media[100];
        static int numberOfMedia = 0;

        /// <summary>
        ///  Begins the program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            if (read())
            {
                ShowMedia(ShowMenu());
            }
            else
            {
                Console.Write("Press any key to exit");
                Console.ReadKey();
            }
        }


        /// <summary>
        ///  Reads the data.txt file and creates media objects from it.
        /// </summary>
        /// <returns>true when successful, false when not successful</returns>
        static bool read()
        {
            bool successful = true;
            string fileName = "Data.txt";

            try
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                string[] delimiter = { "-----" };
                string[] mediaData = streamReader.ReadToEnd().Split(delimiter, System.StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < mediaData.Length; i++)
                {
                    string theMedia = mediaData[i].Trim();
                    string identifier = theMedia.Substring(0, 5);

                    int lengthOfProperties;
                    if (identifier.Contains("SONG"))
                    {
                        lengthOfProperties = theMedia.Length;
                    }
                    else
                    {
                        lengthOfProperties = theMedia.IndexOf("\r\n");
                    }

                    string[] theProperties = theMedia.Substring(0, lengthOfProperties).Split('|');
                    string EncryptedSummary = theMedia.Substring(lengthOfProperties).Trim();
                    int year;
                    int.TryParse(theProperties[2], out year);

                    if (identifier.Contains("BOOK"))
                    {
                        Book theBook = new Book(theProperties[1], year, theProperties[3], EncryptedSummary);
                        allMedia[numberOfMedia] = theBook;
                    }
                    else if (identifier.Contains("SONG"))
                    {
                        Song theSong = new Song(theProperties[1], year, theProperties[3], theProperties[4]);
                        allMedia[numberOfMedia] = theSong;
                    }
                    else if (identifier.Contains("MOVIE"))
                    {
                        Movie theMovie = new Movie(theProperties[1], year, theProperties[3], EncryptedSummary);
                        allMedia[numberOfMedia] = theMovie;
                    }

                    numberOfMedia++;
                }
                streamReader.Close();
                fileStream.Close();
            }
            catch (Exception e)
            {
                successful = false;
                if (e.GetType().ToString() == "System.IO.FileNotFoundException")
                {
                    Console.WriteLine(fileName + " not fonud");
                }
                else if (e.GetType().ToString() == "System.IndexOutOfRangeException")
                {
                    Console.WriteLine("Too many records in " + fileName);
                }
                else if (e.GetType().ToString() == "System.ArgumentOutOfRangeException")
                {
                    Console.WriteLine("The records are not well formatted");
                }
                else
                {
                    Console.WriteLine(e.GetType().ToString());
                }
            }
            return successful;
        }


        /// <summary>
        ///  Prompts the user with the menu and the user selects one of six options by providing an integer.
        /// </summary>
        /// <returns></returns>
        static string ShowMenu()
        {
            Console.WriteLine("1. List All Books");
            Console.WriteLine("2. List All Movies");
            Console.WriteLine("3. List All Songs");
            Console.WriteLine("4. List All Media");
            Console.WriteLine("5. Search All Media by Title");
            Console.WriteLine("");
            Console.WriteLine("6. Exit Program");
            Console.WriteLine("");
            Console.Write("Enter choice: ");

            string input = Console.ReadLine();
            return input;
        }


        /// <summary>
        ///  Anaylise the input from the user with a switch case.
        /// </summary>
        /// <param name="type"></param>
        static void ShowMedia(string type)
        {
            Console.WriteLine("");
            switch (type)
            {
                case "1":
                    foreach (var theMedia in allMedia)
                        ShowBook(theMedia, false);
                    break;

                case "2":
                    foreach (var theMedia in allMedia)
                        ShowMovie(theMedia, false);
                    break;

                case "3":
                    foreach (var theMedia in allMedia)
                        ShowSong(theMedia);
                    break;

                case "4":
                    foreach (var theMedia in allMedia)
                    {
                        ShowBook(theMedia, false);
                        ShowMovie(theMedia, false);
                        ShowSong(theMedia);
                    }
                    break;
                case "5":
                    Console.Write("Enter a search string: ");
                    string key = Console.ReadLine();
                    Console.WriteLine();
                    foreach (var theMedia in allMedia)
                    {
                        if (theMedia != null && theMedia.Search(key))
                        {
                            ShowBook(theMedia, true);
                            ShowMovie(theMedia, true);
                            ShowSong(theMedia);
                        }
                    }
                    break;
                case "6":
                    Environment.Exit(0);//Exit the program
                    break;
                default:
                    Console.WriteLine("*** Invalid Choice - Try Again ***");//Input other than 1~6 will be invalid.
                    break;
            }
            Console.WriteLine("");
            Console.Write("Press any key to continue . . .");
            Console.ReadKey();
            Console.Clear();
            ShowMedia(ShowMenu());
        }


        /// <summary>
        ///  Shows a list of all book objects.
        /// </summary>
        /// <param name="theMedia"></param>
        /// <param name="showSummary"></param>
        static void ShowBook(Media theMedia, bool showSummary)
        {
            if (theMedia is Book)
            {
                Book theBook = theMedia as Book;
                Console.WriteLine("Book Title: " + theBook.Title + " (" + theBook.Year + ")");
                Console.WriteLine("Author: " + theBook.Author);
                if (showSummary)
                {
                    Console.WriteLine();
                    Console.WriteLine(theBook.Decrypt());
                }
                Console.WriteLine("--------------------");
            }
        }


        /// <summary>
        ///  Shows a list of all movie objects.
        /// </summary>
        /// <param name="theMedia"></param>
        /// <param name="showSummary"></param>
        static void ShowMovie(Media theMedia, bool showSummary)
        {
            if (theMedia is Movie)
            {
                Movie theMovie = theMedia as Movie;
                Console.WriteLine("Movie Title: " + theMovie.Title + " (" + theMovie.Year + ")");
                Console.WriteLine("Director: " + theMovie.Director);
                if (showSummary)
                {
                    Console.WriteLine();
                    Console.WriteLine(theMovie.Decrypt());
                }
                Console.WriteLine("--------------------");
            }
        }


        /// <summary>
        /// Shows a list of all the song objects
        /// </summary>
        /// <param name="theMedia"></param>
        static void ShowSong(Media theMedia)
        {

            if (theMedia is Song)
            {
                Song theSong = theMedia as Song;
                Console.WriteLine("Song Title: " + theSong.Title + " (" + theSong.Year + ")");
                Console.WriteLine("Album: " + theSong.Album + "  Artist: " + theSong.Artist);
                Console.WriteLine("--------------------");
            }
        }
    }
}

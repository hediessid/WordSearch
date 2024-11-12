using System;
using System.Collections.Generic;
using System.Linq;
using SearchWordInMatrix.WordFinder.Enum;

namespace Finder.Models
{
    public class WordFinder
    {
        private char[][] MyBoard;

        private bool InputIsWrong(IEnumerable<string> matrix)
        {
            return (matrix.Count() > 64 || matrix.Any(x => x.Length > 64));
        }

        public WordFinder(IEnumerable<string> matrix)
        {
            //check if it's null 
            if (matrix == null || matrix.Count() == 0)
                throw new ArgumentNullException("matrix", "The board can't be null or empty");

            //check if it does not exceed the limit 
            if (InputIsWrong(matrix))
                throw new ArgumentException("The value of the board exceed the maximun width or height");


            MyBoard = new char[matrix.Count()][];  // how many line in the document txt

            //line level
            for (int i = 0; i < matrix.Count(); i++)
            {
                
                MyBoard[i] = new char[matrix.ElementAt(i).Length];
                int count = 0;
                foreach (var item in matrix.ElementAt(i))
                {
                    //add one caracter at the time 
                    MyBoard[i][count] = item;
                    count++;
                }
            }
        }


        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var ReturnMessage = new List<string>();
            foreach (var item in wordstream)
            {
                
                if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
                    continue;


                for (int i = 0; i < MyBoard.Length; i++)
                {
                    for (int j = 0; j < MyBoard[i].Length; j++)
                    {
                        if (MyBoard[i][j] == item[0]  )
                            if (FindWordByDirection(i, j, 0, item))
                                ReturnMessage.Add(item);
                    }
                }
            }

            return ReturnMessage.GroupBy(x => x)
                         .OrderByDescending(x => x.Count())
                         .Select(x => x.Key + " occure " + x.Count() + " times.");



        }


        private bool FindWordByDirection(int i, int j, int count, string word, Direction direction = Direction.NONE)
        {
            if (count == word.Length)
                return true;

            if (i < 0 || i >= MyBoard.Length || j < 0 || j >= MyBoard[i].Length || MyBoard[i][j] != word[count])
                return false;

           char tmp = MyBoard[i][j];
            MyBoard[i][j] = ' ';
            var found = false;


            switch (direction)
            {
                case Direction.NONE:
                    found = FindWordByDirection(i + 1, j, count + 1, word, Direction.DOWN)
                         || FindWordByDirection(i - 1, j, count + 1, word, Direction.UP)
                         || FindWordByDirection(i, j + 1, count + 1, word, Direction.RIGHT)
                         || FindWordByDirection(i, j - 1, count + 1, word, Direction.LEFT);
                    break;
                case Direction.UP:
                    found = FindWordByDirection(i - 1, j, count + 1, word, Direction.UP);
                    break;
                case Direction.DOWN:
                    found = FindWordByDirection(i + 1, j, count + 1, word, Direction.DOWN);
                    break;
                case Direction.RIGHT:
                    found = FindWordByDirection(i, j + 1, count + 1, word, Direction.RIGHT);
                    break;
                case Direction.LEFT:
                    found = FindWordByDirection(i, j - 1, count + 1, word, Direction.LEFT);
                    break;

            }

            MyBoard[i][j] = tmp;

            return found;
        }



    }
}

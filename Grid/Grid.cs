using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePreset
{
    public class Grid<T>
    {
        public T[,] Array { get; set; }
        public int HorizontalLength { get; set; }
        public int VerticalLength { get; set; }
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }

        public Grid(string[] lines, string splitter)
        {
            CurrentX = 0;
            CurrentY = 0;
            VerticalLength = lines.Length;

            foreach (string line in lines)
            {
                if (line.Length > HorizontalLength)
                {
                    HorizontalLength = line.Length;
                }
            }

            T[,] array = new T[VerticalLength, HorizontalLength];

            for (int vertical = 0; vertical < VerticalLength; vertical++)
            {
                string line = lines[vertical];
                for (int horizontal = 0; horizontal < HorizontalLength; horizontal++)
                {
                    string valueString = line.Split(splitter)[horizontal];
                    array[vertical, horizontal] = ConvertStringToValue(valueString);
                }
            }

            Array = array;
        }

        public Grid(T[,] array)
        {
            VerticalLength = array.GetLength(0);
            HorizontalLength = array.GetLength(1);
            CurrentX = 0;
            CurrentY = 0;
            Array = new T[VerticalLength, HorizontalLength];

            for (int y = 0; y < VerticalLength; y++)
            {
                for (int x = 0; x < HorizontalLength; x++)
                {
                    Array![y, x] = ConvertStringToValue(array[y, x]!.ToString()!);
                }
            }
        }

        public Grid(string[] lines)
        {
            CurrentX = 0;
            CurrentY = 0;
            VerticalLength = lines.Length;

            foreach (string line in lines)
            {
                if (line.Length > HorizontalLength)
                {
                    HorizontalLength = line.Length;
                }
            }

            T[,] array = new T[VerticalLength, HorizontalLength];

            for (int vertical = 0; vertical < VerticalLength; vertical++)
            {
                string line = lines[vertical];
                for (int horizontal = 0; horizontal < HorizontalLength; horizontal++)
                {
                    string valueString = line[horizontal].ToString();
                    array[vertical, horizontal] = ConvertStringToValue(valueString);
                }
            }

            Array = array;
        }

        public Grid(int horizontalLength, int verticalLength)
        {
            HorizontalLength = horizontalLength;
            VerticalLength = verticalLength;
            Array = new T[horizontalLength, verticalLength];
            CurrentX = 0;
            CurrentY = 0;
        }

        public int AmountOf(T value)
        {
            int counter = 0;

            foreach (T item in Array)
            {
                if (item!.Equals(value))
                {
                    counter++;
                }
            }

            return counter;
        }

        public T Above(int horizontal, int vertical)
        {
            if (horizontal >= 0 && horizontal < HorizontalLength && vertical - 1 < VerticalLength && vertical - 1 >= 0)
            {
                return Array[vertical - 1, horizontal];
            }
            else
            {
                return default!;
            }
        }

        public T Right(int horizontal, int vertical)
        {
            if (horizontal + 1 >= 0 && horizontal + 1 < HorizontalLength && vertical < VerticalLength && vertical >= 0)
            {
                return Array[vertical, horizontal + 1];
            }
            else
            {
                return default!;
            }
        }

        public T Below(int horizontal, int vertical)
        {
            if (horizontal >= 0 && horizontal < HorizontalLength && vertical + 1 < VerticalLength && vertical + 1 >= 0)
            {
                return Array[vertical + 1, horizontal];
            }
            else
            {
                return default!;
            }
        }

        public T Left(int horizontal, int vertical)
        {
            if (horizontal - 1 >= 0 && horizontal - 1 < HorizontalLength && vertical < VerticalLength && vertical >= 0)
            {
                return Array[vertical, horizontal - 1];
            }
            else
            {
                return default!;
            }
        }

        public int GetShortestPath(T startValue, T endValue, Func<T, T, bool> barrier)
        {
            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { -1, 0, 1, 0 };

            int rows = Array.GetLength(0);
            int cols = Array.GetLength(1);

            int[,] distances = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    distances[i, j] = int.MaxValue;
                }
            }

            List<int[,]> distanceList = new List<int[,]>();

            foreach ((int, int) startCoords in GetStartCoords(startValue))
            {
                distanceList.Add(Dijkstra(startCoords.Item1, startCoords.Item2, barrier, distances));
            }

            int endX = -1, endY = -1;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (Array[i, j]!.Equals(endValue))
                    {
                        endX = j;
                        endY = i;
                        break;
                    }
                }
            }

            int[,] distance = distanceList
                            .OrderBy(array => array[endY, endX])
                            .FirstOrDefault()!;

            List<(int, int)> path = ReconstructPath(distance, endX, endY);
            PrintPath(path, startValue, endValue);

            return distance[endY, endX] == int.MaxValue ? -1 : distance[endY, endX];
        }

        private List<(int, int)> GetStartCoords(T startValue)
        {
            int rows = Array.GetLength(0);
            int cols = Array.GetLength(1);

            List<(int, int)> startCoords = new List<(int, int)>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (Array[i, j]!.Equals(startValue))
                    {
                        startCoords.Add((j, i));
                    }
                }
            }

            return startCoords;
        }

        private int[,] Dijkstra(int startX, int startY, Func<T, T, bool> barrier, int[,] distances)
        {
            int rows = Array.GetLength(0);
            int cols = Array.GetLength(1);

            MinHeap<(int, int, int)> minHeap = new MinHeap<(int, int, int)>();


            if (startX != -1 && startY != -1)
            {
                minHeap.Insert((0, startX, startY));
                distances[startY, startX] = 0;
            }

            while (minHeap.Count > 0)
            {
                var (dist, x, y) = minHeap.ExtractMin();
                int[] dx = new int[] { 0, 0, 1, -1 };
                int[] dy = new int[] { 1, -1, 0, 0 };

                for (int dir = 0; dir < 4; dir++)
                {
                    int nextX = x + dx[dir];
                    int nextY = y + dy[dir];

                    if (nextX >= 0 && nextX < cols && nextY >= 0 && nextY < rows && !barrier(Array[y, x], Array[nextY, nextX]))
                    {
                        int newDist = dist + 1;
                        if (newDist < distances[nextY, nextX])
                        {
                            distances[nextY, nextX] = newDist;
                            minHeap.Insert((newDist, nextX, nextY));
                        }
                    }
                }
            }

            return distances;
        }

        private List<(int, int)> ReconstructPath(int[,] distances, int endX, int endY)
        {
            int currentX = endX;
            int currentY = endY;

            List<(int, int)> path = new List<(int, int)>();
            path.Add((currentX, currentY));



            while (distances[currentY, currentX] != 0)
            {
                int[] dx = new int[] { 0, 0, 1, -1 };
                int[] dy = new int[] { 1, -1, 0, 0 };
                for (int dir = 0; dir < 4; dir++)
                {
                    int nextX = currentX + dx[dir];
                    int nextY = currentY + dy[dir];

                    if (nextX >= 0 && nextX < HorizontalLength && nextY >= 0 && nextY < VerticalLength && distances[nextY, nextX] == distances[currentY, currentX] - 1)
                    {
                        path.Add((nextX, nextY));
                        currentX = nextX;
                        currentY = nextY;
                        break;
                    }
                }
            }

            path.Reverse();
            return path;
        }

        private void PrintPath(List<(int, int)> path, T startValue, T endValue)
        {
            char[,] pathGrid = new char[VerticalLength, HorizontalLength];

            // Initialize the path grid with empty cells
            for (int y = 0; y < VerticalLength; y++)
            {
                for (int x = 0; x < HorizontalLength; x++)
                {
                    pathGrid[y, x] = '.';
                }
            }

            // Mark the start and end positions
            pathGrid[path[0].Item2, path[0].Item1] = 'S';
            pathGrid[path[path.Count - 1].Item2, path[path.Count - 1].Item1] = 'E';

            // Mark the path with arrows
            for (int i = 1; i < path.Count - 1; i++)
            {
                var current = path[i];
                var previous = path[i - 1];
                var next = path[i + 1];

                if (previous.Item1 < current.Item1 && previous.Item2 < current.Item2)
                    pathGrid[current.Item2, current.Item1] = '>'; // Down-right
                else if (previous.Item1 > current.Item1 && previous.Item2 < current.Item2)
                    pathGrid[current.Item2, current.Item1] = '<'; // Down-left
                else if (previous.Item1 < current.Item1 && previous.Item2 > current.Item2)
                    pathGrid[current.Item2, current.Item1] = '>'; // Up-right
                else if (previous.Item1 > current.Item1 && previous.Item2 > current.Item2)
                    pathGrid[current.Item2, current.Item1] = '<'; // Up-left
                else if (previous.Item1 < current.Item1 && current.Item1 < next.Item1)
                    pathGrid[current.Item2, current.Item1] = '>'; // Right
                else if (previous.Item1 > current.Item1 && current.Item1 > next.Item1)
                    pathGrid[current.Item2, current.Item1] = '<'; // Left
                else if (previous.Item2 < current.Item2 && current.Item2 < next.Item2)
                    pathGrid[current.Item2, current.Item1] = 'v'; // Down
                else if (previous.Item2 > current.Item2 && current.Item2 > next.Item2)
                    pathGrid[current.Item2, current.Item1] = '^'; // Up
            }

            for (int y = 0; y < VerticalLength; y++)
            {
                for (int x = 0; x < HorizontalLength; x++)
                {
                    Console.Write(pathGrid[y, x]);
                }
                Console.WriteLine();
            }
        }

        private static T ConvertStringToValue(string input)
        {
            Type underlyingType = Nullable.GetUnderlyingType(typeof(T))!;

            if (underlyingType != null)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    return default!;
                }
                return (T)Convert.ChangeType(input, underlyingType);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    return default!;
                }
                return (T)Convert.ChangeType(input, typeof(T));
            }
        }
    }
}
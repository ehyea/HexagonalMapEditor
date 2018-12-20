
using Hexagonal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal
{
    public class Model
    {
        public Board getHexBoard(List<Cluster> clusters,double [][] distanceMatrix)
        {
            Cluster max = null;
            float maxScore = 0, minScore=Int32.MaxValue;
            int index = 0,k=0;
            foreach (Cluster c in clusters)
            {
                if (c.queryScore > maxScore)
                {
                    max = c;
                    maxScore = c.queryScore;
                    index = k;
                }
                if (c.queryScore < minScore)
                {
                    minScore = c.queryScore;
                }
                k++;
            }
            Board board = new Board(5 * 2, 5 * 2, 44, HexOrientation.Flat);
            int n = 5;
            board.Hexes[n, n].minScore = minScore;
            board.Hexes[n, n].maxScore = maxScore;
            board.Hexes[n, n].Cluster = max;
            if (clusters.Count > 1)
            {
                List<Int32> listIndex = new List<Int32>();
                listIndex.Add(index);
                while (listIndex.Count < clusters.Count)
                {
                    double min = 2;
                    int minIndex = 0;
                    for (int i = 0; i < distanceMatrix[index].Length; i++)
                    {
                        if (listIndex.Contains(i))
                        {
                            continue;
                        }
                        if (distanceMatrix[index][i] < min)
                        {
                            minIndex = i;
                            min = distanceMatrix[index][i];
                        }
                    }
                    listIndex.Add(minIndex);
                }
                FillMatrix(board.Hexes, 10, listIndex, clusters,minScore,maxScore);
            }
            

            return board;

            //double[] array = new double[distanceMatrix[index].Length];
            //for (int i = 0; i < distanceMatrix[index].Length; i++)
            //{
            //    array[i] = distanceMatrix[index][i];
            //}
            //Array.Sort(array);
            //int[] indexInOrder = new int[distanceMatrix[index].Length];
            //for (int i = 0; i < indexInOrder.Length; i++)
            //{
            //    double d = array[i];
            //    int l ;
            //    for (l = 0; l < array.Length; l++)
            //    {
            //        if (d == distanceMatrix[index][l])
            //        {
            //            break;
            //        }
            //    }
            //    indexInOrder[i]=l;
            //}

            
        }

        public int getHexIndexFromDistance(double distance)
        {
            if (distance == 1.0d)
            {
                return 5;
            }
            return (int)(distance * 10)/2;
        }

        //original source:http://www.introprogramming.info/tag/spiral-matrix/
        private static void FillMatrix(Hex[,] matrix, int n,List<Int32> indexArray,
            List<Cluster> clusterList,float min, float max)
        {
            int positionX = n / 2; // The middle of the matrix
            int positionY = n % 2 == 0 ? (n / 2) - 1 : (n / 2);

            int direction = 0; // The initial direction is "down"
            int stepsCount = 1; // Perform 1 step in current direction
            int stepPosition = 0; // 0 steps already performed
            int stepChange = 0; // Steps count changes after 2 steps

            for (int i = 0; i <indexArray.Count; i++)
            {
                matrix[positionY, positionX].maxScore = max;
                matrix[positionY, positionX].minScore = min;
                // Fill the current cell with the current value
                matrix[positionY, positionX].Cluster = clusterList.ElementAt(indexArray[i]);
                
                // Check for direction / step changes
                if (stepPosition < stepsCount)
                {
                    stepPosition++;
                }
                else
                {
                    stepPosition = 1;
                    if (stepChange == 1)
                    {
                        stepsCount++;
                    }
                    stepChange = (stepChange + 1) % 2;
                    direction = (direction + 1) % 4;
                }

                // Move to the next cell in the current direction
                switch (direction)
                {
                    case 0:
                        positionY++;
                        break;
                    case 1:
                        positionX--;
                        break;
                    case 2:
                        positionY--;
                        break;
                    case 3:
                        positionX++;
                        break;
                }
            }
        }
    }
}

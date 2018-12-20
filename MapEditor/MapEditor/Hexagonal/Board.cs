using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Hexagonal
{
	/// <summary>
	/// Represents a 2D hexagon board
	/// </summary>
	public class Board
	{
		private Hexagonal.Hex[,] hexes;
		private int width;
		private int height;
		private int xOffset;
		private int yOffset;
		private int side;
		private float pixelWidth;
		private float pixelHeight;
		private Hexagonal.HexOrientation orientation;
		private System.Drawing.Color backgroundColor;
		private Hexagonal.BoardState boardState;

        private int activeRow = 0;
        private int activeCol = 0;
        private int coreX = 6;
        private int coreY = 3;
        /// <param name="width">Board width</param>
        /// <param name="height">Board height</param>
        /// <param name="side">Hexagon side length</param>
        /// <param name="orientation">Orientation of the hexagons</param>
        /// 

        private Hexagonal.MapData m_data = null;


        public Board(int width, int height, int side, Hexagonal.HexOrientation orientation)
		{
			Initialize(width, height, side, orientation, 0, 0);
		}

		/// <param name="width">Board width</param>
		/// <param name="height">Board height</param>
		/// <param name="side">Hexagon side length</param>
		/// <param name="orientation">Orientation of the hexagons</param>
		/// <param name="xOffset">X coordinate offset</param>
		/// <param name="yOffset">Y coordinate offset</param>
		public Board(int width, int height, int side, Hexagonal.HexOrientation orientation, int xOffset, int yOffset)
		{
			Initialize(width, height, side, orientation, xOffset, yOffset);
		}

		#region Properties

        public Hexagonal.MapData MapData
        {
            get
            {
                return m_data;
            }
            set
            {
            }
        }



        public Hexagonal.Hex[,] Hexes
		{
			get
			{
				return hexes;
			}
			set
			{
			}
		}

		public float PixelWidth
		{
			get
			{
				return pixelWidth;
			}
			set
			{
			}
		}

		public float PixelHeight
		{
			get
			{
				return pixelHeight;
			}
			set
			{
			}
		}

		public int XOffset
		{
			get
			{
				return xOffset;
			}
			set
			{
			}
		}

		public int YOffset
		{
			get
			{
				return xOffset;
			}
			set
			{
			}
		}

		public int Width
		{
			get
			{
				return width;
			}
			set
			{
			}
		}

		public int Height
		{
			get
			{
				return height;
			}
			set
			{
			}
		}

		public System.Drawing.Color BackgroundColor
		{
			get
			{
				return backgroundColor;
			}
			set
			{
				backgroundColor = value;
			}
		}

		public Hexagonal.BoardState BoardState
		{
			get
			{
				return boardState;
			}
			set
			{
				throw new System.NotImplementedException();
			}
		}

		#endregion 

		/// <summary>
		/// Sets internal fields and creates board
		/// </summary>
		/// <param name="width">Board width</param>
		/// <param name="height">Board height</param>
		/// <param name="side">Hexagon side length</param>
		/// <param name="orientation">Orientation of the hexagons</param>
		/// <param name="xOffset">X coordinate offset</param>
		/// <param name="yOffset">Y coordinate offset</param>
        /// 
        public void resetArea(int row,int col)
        {
            Console.WriteLine("offset Row is" + row);
            Console.WriteLine("offset Col us" + col);
            Initialize(this.width, this.height, this.side, HexOrientation.Flat, 0, 0, row, col);
        }

        public void setMapData(MapData data)
        {
            this.m_data = data;
            Initialize(this.width, this.height, this.side, HexOrientation.Flat, 0, 0, coreX, coreY);
        }

        public void setSelectHexState(int state)
        {
            Hex selcetHex = BoardState.ActiveHex;
            int row = (int)selcetHex.Row;
            int col = (int)selcetHex.Col;
            m_data.setGridStae(row, col, state);
            switch (state)
            {
                case 0:
                    selcetHex.HexState.setBackgroundColor(System.Drawing.Color.White);
                    break;
                case 1:
                    selcetHex.HexState.setBackgroundColor(System.Drawing.Color.Red);
                    break;
                case 2:

                case 3:

                default:
                    break;
            }
        }

        private void Initialize(int width, int height, int side, Hexagonal.HexOrientation orientation, int xOffset, int yOffset,int row = 0,int col = 0)
		{
			this.width = width;
			this.height = height;
			this.xOffset = xOffset;
			this.yOffset = yOffset;
			this.side = side;
			this.orientation = orientation;
			hexes = new Hex[height, width]; //opposite of what we'd expect
			this.boardState = new BoardState();

			float h = Hexagonal.MathUtil.CalculateH(side); // short side
			float r = Hexagonal.MathUtil.CalculateR(side); // long side

            int offsetRow = row - this.activeRow;
            int offsetCol = col - this.activeCol;

			//
			// Calculate pixel info..remove?
			// because of staggering, need to add an extra r/h
			float hexWidth = 0;
			float hexHeight = 0;
			switch (orientation)
			{
				case HexOrientation.Flat:
					hexWidth = side + h;
					hexHeight = r + r;
					this.pixelWidth = (width * hexWidth) + h;
					this.pixelHeight = (height * hexHeight) + r;
					break;
				case HexOrientation.Pointy:
					hexWidth = r + r;
					hexHeight = side + h;
					this.pixelWidth = (width * hexWidth) + r;
					this.pixelHeight = (height * hexHeight) + h;
					break;
				default:
					break;
			}


			bool inTopRow = false;
			bool inBottomRow = false;
			bool inLeftColumn = false;
			bool inRightColumn = false;
			bool isTopLeft = false;
			bool isTopRight = false;
			bool isBotomLeft = false;
			bool isBottomRight = false;


            // i = y coordinate (rows), j = x coordinate (columns) of the hex tiles 2D plane
           

            for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					// Set position booleans
					#region Position Booleans
						if (i == 0)
						{
							inTopRow = true;
						}
						else
						{
							inTopRow = false;
						}

						if (i == height - 1)
						{
							inBottomRow = true;
						}
						else
						{
							inBottomRow = false;
						}

						if (j == 0)
						{
							inLeftColumn = true;
						}
						else
						{
							inLeftColumn = false;
						}

						if (j == width - 1)
						{
							inRightColumn = true;
						}
						else
						{
							inRightColumn = false;
						}

						if (inTopRow && inLeftColumn)
						{
							isTopLeft = true;
						}
						else
						{
							isTopLeft = false;
						}

						if (inTopRow && inRightColumn)
						{
							isTopRight = true;
						}
						else
						{
							isTopRight = false;
						}

						if (inBottomRow && inLeftColumn)
						{
							isBotomLeft = true;
						}
						else
						{
							isBotomLeft = false;
						}

						if (inBottomRow && inRightColumn)
						{
							isBottomRight = true;
						}
						else
						{
							isBottomRight = false;
						}
						#endregion

					//
					// Calculate Hex positions
					//
					if (isTopLeft)
					{
						//First hex
						switch (orientation)
						{ 
							case HexOrientation.Flat:
								hexes[0, 0] = new Hex(0 + h + xOffset, 0 + yOffset, side, i+ offsetCol, j + offsetRow, orientation);
								break;
							case HexOrientation.Pointy:
								hexes[0, 0] = new Hex(0 + r + xOffset, 0 + yOffset, side, i + offsetCol, j + offsetRow, orientation);
								break;
							default:
								break;
						}

							

					}
					else
					{
						switch (orientation)
						{
							case HexOrientation.Flat:
								if (inLeftColumn)
								{
									// Calculate from hex above
									hexes[i, j] = new Hex(hexes[i - 1, j].Points[(int)Hexagonal.FlatVertice.BottomLeft], side, i + offsetCol, j + offsetRow, orientation);
								}
								else
								{
									// Calculate from Hex to the left and need to stagger the columns
									if (j % 2 == 0)
									{
										// Calculate from Hex to left's Upper Right Vertice plus h and R offset 
										float x = hexes[i, j - 1].Points[(int)Hexagonal.FlatVertice.UpperRight].X;
										float y = hexes[i, j - 1].Points[(int)Hexagonal.FlatVertice.UpperRight].Y;
										x += h;
										y -= r;
										hexes[i, j] = new Hex(x, y, side, i + offsetCol , j + offsetRow , orientation);
									}
									else
									{
										// Calculate from Hex to left's Middle Right Vertice
										hexes[i, j] = new Hex(hexes[i, j - 1].Points[(int)Hexagonal.FlatVertice.MiddleRight], side, i + offsetCol - (offsetRow % 2), j + offsetRow, orientation);
									}
								}
								break;
							case HexOrientation.Pointy:
								if (inLeftColumn)
								{
									// Calculate from hex above and need to stagger the rows
									if (i % 2 == 0)
									{
										hexes[i, j] = new Hex(hexes[i - 1, j].Points[(int)Hexagonal.PointyVertice.BottomLeft], side, i + offsetCol, j + offsetRow, orientation);
									}
									else
									{
										hexes[i, j] = new Hex(hexes[i - 1, j].Points[(int)Hexagonal.PointyVertice.BottomRight], side, i + offsetCol, j + offsetRow, orientation);
									}

								}
								else
								{
									// Calculate from Hex to the left
									float x = hexes[i, j - 1].Points[(int)Hexagonal.PointyVertice.UpperRight].X;
									float y = hexes[i, j - 1].Points[(int)Hexagonal.PointyVertice.UpperRight].Y;
									x += r;
									y -= h;
									hexes[i, j] = new Hex(x, y, side, i + offsetCol, j + offsetRow, orientation);	
								}
								break;
							default:
								break;
                            
						}
                    }

                    if (m_data != null)
                    {
                        Console.WriteLine("not null!");
                        int r1 = (int)hexes[i, j].Row;
                        int c1 = (int)hexes[i, j].Col;
                        if (r1 < m_data.width && r1 >= 0 && c1 < m_data.height && c1 >= 0)
                        {
                            int state = m_data.getGridStae(r1, c1);
                            Console.WriteLine("state is" + state);
                            switch (state)
                            {
                                case 0:
                                    hexes[i, j].HexState.setBackgroundColor(System.Drawing.Color.White);
                                    break;
                                case 1:
                                    hexes[i, j].HexState.setBackgroundColor(System.Drawing.Color.Red);
                                    break;
                                case 2:

                                case 3:

                                default:
                                    break;
                            }
                        }
                    }

                    if (i == coreY && j == coreX)
                    {
                        this.BoardState.ActiveHex = hexes[i, j];
                        this.activeRow = j;
                        this.activeCol = i;
                    }
                    

                }
			}


			
		}

		public bool PointInBoardRectangle(System.Drawing.Point point)
		{
			return PointInBoardRectangle(point.X,point.Y);
		}

		public bool PointInBoardRectangle(int x, int y)
		{
			//
			// Quick check to see if X,Y coordinate is even in the bounding rectangle of the board.
			// Can produce a false positive because of the staggerring effect of hexes around the edge
			// of the board, but can be used to rule out an x,y point.
			//
			int topLeftX = 0 + XOffset;
			int topLeftY = 0 + yOffset;
			float bottomRightX = topLeftX + pixelWidth;
			float bottomRightY = topLeftY + PixelHeight;


			if (x > topLeftX && x < bottomRightX && y > topLeftY && y < bottomRightY)
			{
				return true;
			}
			else 
			{
				return false;
			}

		}

		public Hex FindHexMouseClick(System.Drawing.Point point)
		{
			return FindHexMouseClick(point.X,point.Y);
		}

		public Hex FindHexMouseClick(int x, int y)
		{
			Hex target = null;

			if (PointInBoardRectangle(x, y))
			{
				for (int i = 0; i < hexes.GetLength(0); i++)
				{
					for (int j = 0; j < hexes.GetLength(1); j++)
					{
						if (MathUtil.InsidePolygon(hexes[i, j].Points, 6, new System.Drawing.PointF(x, y)))
						{
							target = hexes[i, j];
							break;
						}
					}

					if (target != null)
					{
						break;
					}
				}

			}
			
			return target;
			
		}

	}
}

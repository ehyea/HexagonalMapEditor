using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.IO;

namespace Hexagonal
{
	public class GraphicsEngine
	{
		private Hexagonal.Board board;
		private float boardPixelWidth;
		private float boardPixelHeight;
		private int boardXOffset;
		private int boardYOffset;

       
	
		public GraphicsEngine(Hexagonal.Board board)
		{
			this.Initialize(board, 0, 0);
		}

		public GraphicsEngine(Hexagonal.Board board, int xOffset, int yOffset)
		{
			this.Initialize(board, xOffset, yOffset);
		}

		public int BoardXOffset
		{
			get
			{
				return boardXOffset;
			}
			set
			{
				throw new System.NotImplementedException();
			}
		}

		public int BoardYOffset
		{
			get
			{
				return boardYOffset;
			}
			set
			{
				throw new System.NotImplementedException();
			}
		}

        public float BoardPixelWidth
        {
            get
            {
                return boardPixelWidth;
            }
            set
            {
                boardPixelWidth = value;
            }
        }

        public float BoardPixelHeight
        {
            get
            {
                return boardPixelHeight;
            }
            set
            {
                boardPixelHeight = value;
            }
        }

        public int Height
        {
            get
            {
                return (int)board.PixelHeight;
            }
        }

        public int Weight
        {
            get
            {
                return (int)board.PixelWidth;
            }
        }
		
		private void Initialize(Hexagonal.Board board, int xOffset, int yOffset)
		{
			this.board = board;
			this.boardXOffset = xOffset;
			this.boardYOffset = yOffset;
		}

        public void Draw(System.Windows.Media.DrawingContext graphics)
		{

			int width =  Convert.ToInt32(System.Math.Ceiling(board.PixelWidth));
			int height = Convert.ToInt32(System.Math.Ceiling(board.PixelHeight));
			// seems to be needed to avoid bottom and right from being chopped off
			width += 1;
			height += 1;

			//
			// Create drawing objects
			//
			Bitmap bitmap = new Bitmap(width, height);
			Graphics bitmapGraphics = Graphics.FromImage(bitmap);
			Pen p = new Pen(Color.Black);
			SolidBrush sb = new SolidBrush(Color.Black);

			
			//
			// Draw Board background
			//
			sb = new SolidBrush(board.BoardState.BackgroundColor);
			bitmapGraphics.FillRectangle(sb, 0, 0, width, height);

			//
			// Draw Hex Background 
			//
			for (int i = 0; i < board.Hexes.GetLength(0); i++)
			{
				for (int j = 0; j < board.Hexes.GetLength(1); j++)
				{
                    //bitmapGraphics.DrawPolygon(p, board.Hexes[i, j].Points);
                    if (board.Hexes[i, j] != null)
                    {
                        bitmapGraphics.FillPolygon(new SolidBrush(board.Hexes[i, j].HexState.BackgroundColor),
                            board.Hexes[i, j].Points);
                    }
				}
			}

			
			//
			// Draw Hex Grid
			//
			p.Color = board.BoardState.GridColor;
			p.Width = board.BoardState.GridPenWidth;
            
			for (int i = 0; i < board.Hexes.GetLength(0); i++)
			{
				for (int j = 0; j < board.Hexes.GetLength(1); j++)
				{
                    if (board.Hexes[i, j] != null)
                    {
                        p.Color = board.Hexes[i, j].HexState.BorderColor;
                        bitmapGraphics.DrawPolygon(p, board.Hexes[i, j].Points);
                        if (board.MapData != null)
                        {
                            Hex hex = board.Hexes[i, j];
                            if (hex.Row >= 0 && hex.Row < board.MapData.width &&  hex.Col >= 0 && hex.Col < board.MapData.height)
                                DrawText(bitmapGraphics, board.Hexes[i, j], board.Hexes[i, j].Points);
                        }
                    }
				}
			}

			//
			// Draw Active Hex, if present
			//
			if (board.BoardState.ActiveHex != null)
			{

                p.Color = board.BoardState.ActiveHexBorderColor;
				p.Width = board.BoardState.ActiveHexBorderWidth;
				bitmapGraphics.DrawPolygon(p, board.BoardState.ActiveHex.Points);
			}

			//
			// Draw internal bitmap to screen
			//
            
            
            graphics.DrawImage(ToBitmapImage2(bitmap),
            new System.Windows.Rect(0, 0, bitmap.Width, bitmap.Height));
			//graphics.DrawImage(bitmap, new Point(this.boardXOffset , this.boardYOffset));
			
			//
			// Release objects
			//
			bitmapGraphics.Dispose();
			bitmap.Dispose();

		}

        const float bts = 10;
        const int sts = 6;

        public void DrawText(Graphics bitmapGraphics,Hex h,PointF [] points)
        {
            //draw the big text
            Font bigFont = new Font(FontFamily.GenericSansSerif, bts, FontStyle.Bold,GraphicsUnit.Point);
            Brush bigBrush = Brushes.Black;

            float ox = (points[0].X + points[4].X + points[5].X) / 3;
            float oy = (points[0].Y + points[4].Y + points[5].Y) / 3;

            //if (h.Row >= 0 && h.Col >= 0)
                //bitmapGraphics.DrawString("nil", bigFont, bigBrush, new PointF(ox, oy));
            //else 
                bitmapGraphics.DrawString(string.Format("{0}-{1}", h.Row, h.Col), bigFont, bigBrush, new PointF(ox,oy));
        }

        public string limit(String input,int size)
        {
            if (input.Length > size)
            {
                return input.Substring(0, size);
            }

            return input;
        }

        public  BitmapImage ToBitmapImage2( Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

	}
}

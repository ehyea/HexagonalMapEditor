using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Hexagonal
{
	public class Hex
	{
		private System.Drawing.PointF[] points;
		private float side;
		private float h;
		private float r;
		private Hexagonal.HexOrientation orientation;
		private float x;
		private float y;
		private HexState hexState;
        private Cluster cluster;
        public float minScore;
        public float maxScore;
        private int col;
        private int row;
        private int id;

		/// <param name="side">length of one side of the hexagon</param>
		public Hex(int x, int y, int side, int col, int row, Hexagonal.HexOrientation orientation)
		{
			Initialize(Hexagonal.MathUtil.ConvertToFloat(x), Hexagonal.MathUtil.ConvertToFloat(y), Hexagonal.MathUtil.ConvertToFloat(side), col, row, orientation);
		}

		public Hex(float x, float y, float side, int col, int row, Hexagonal.HexOrientation orientation)
		{
			Initialize(x, y, side, col, row, orientation);
		}

		public Hex(PointF point, float side, int col, int row, HexOrientation orientation)
		{
			Initialize(point.X, point.Y, side, col, row, orientation);
		}

		public Hex()
		{ }

		/// <summary>
		/// Sets internal fields and calls CalculateVertices()
		/// </summary>
		private void Initialize(float x, float y, float side, int col, int row, Hexagonal.HexOrientation orientation)
		{
            this.id = ((1000000 + ((int)x * 1000)) + (int)y);
            this.x = x;
			this.y = y;
			this.side = side;
            this.col = col;
            this.row = row;
			this.orientation = orientation;
			this.hexState = new HexState();
			CalculateVertices();
		}

		/// <summary>
		/// Calculates the vertices of the hex based on orientation. Assumes that points[0] contains a value.
		/// </summary>
		private void CalculateVertices()
		{
			//  
			//  h = short length (outside)
			//  r = long length (outside)
			//  side = length of a side of the hexagon, all 6 are equal length
			//
			//  h = sin (30 degrees) x side
			//  r = cos (30 degrees) x side
			//
			//		 h
			//	     ---
			//   ----     |r
			//  /    \    |          
			// /      \   |
			// \      /
			//  \____/
			//
			// Flat orientation (scale is off)
			//
	        //     /\
			//    /  \
			//   /    \
			//   |    |
			//   |    |
			//   |    |
			//   \    /
			//    \  /
			//     \/
			// Pointy orientation (scale is off)
         
			h = Hexagonal.MathUtil.CalculateH(side);
			r = Hexagonal.MathUtil.CalculateR(side);

			switch (orientation)
			{ 
				case Hexagonal.HexOrientation.Flat:
					// x,y coordinates are top left point
					points = new System.Drawing.PointF[6];
					points[0] = new PointF(x, y);
					points[1] = new PointF(x + side, y);
					points[2] = new PointF(x + side + h, y + r);
					points[3] = new PointF(x + side, y + r + r);
					points[4] = new PointF(x, y + r + r);
					points[5] = new PointF(x - h, y + r );
					break;
				case Hexagonal.HexOrientation.Pointy:
					//x,y coordinates are top center point
					points = new System.Drawing.PointF[6];
					points[0] = new PointF(x, y);
					points[1] = new PointF(x + r, y + h);
					points[2] = new PointF(x + r, y + side + h);
					points[3] = new PointF(x, y + side + h + h);
					points[4] = new PointF(x - r, y + side + h);
					points[5] = new PointF(x - r, y + h);
					break;
				default:
					throw new Exception("No HexOrientation defined for Hex object.");
			
			}
			
		}

		public Hexagonal.HexOrientation Orientation
		{
			get
			{
				return orientation;
			}
			set
			{
				orientation = value;
			}
		}

		public System.Drawing.PointF[] Points
		{
			get
			{
				return points;
			}
			set
			{
			}
		}

		public float Side
		{
			get
			{
				return side;
			}
			set
			{
			}
		}

		public float H
		{
			get
			{
				return h;
			}
			set
			{
			}
		}

		public float R
		{
			get
			{
				return r;
			}
			set
			{
			}
		}
        public float X
        {
            get
            {
                return x;
            }
        }
        public float Y
        {
            get
            {
                return y;
            }
        }
        public int Col
        {
            get
            {
                return col;
            }
        }
        public float Row
        {
            get
            {
                return row;
            }
        }

        public float ID
        {
            get
            {
                return id;
            }
        }

        public Hexagonal.HexState HexState
		{
			get
			{
				return hexState;
			}
			set
			{
				throw new System.NotImplementedException();
			}
		}

        public Cluster Cluster
        {
            get
            {
                return cluster;
            }
            set
            {
                cluster = value;
                if (cluster != null)
                {
                    hexState = new HexState(cluster.queryScore,minScore,maxScore);
                }
            }
        }

	}
}

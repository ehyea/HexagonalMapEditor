using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Hexagonal
{
	public class BoardState
	{
		private System.Drawing.Color backgroundColor;
		private System.Drawing.Color gridColor;
		private int gridPenWidth;
		private Hexagonal.Hex activeHex;
		private System.Drawing.Color activeHexBorderColor;
		private int activeHexBorderWidth;

		#region Properties

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

		public System.Drawing.Color GridColor
		{
			get
			{
				return gridColor;
			}
			set
			{
				gridColor = value;
			}
		}

		public int GridPenWidth
		{
			get
			{
				return gridPenWidth;
			}
			set
			{
				gridPenWidth = value;
			}
		}

		public Hexagonal.Hex ActiveHex
		{
			get
			{
				return activeHex;
			}
			set
			{
				activeHex = value;
			}
		}

		public System.Drawing.Color ActiveHexBorderColor
		{
			get
			{
				return activeHexBorderColor;
			}
			set
			{
				activeHexBorderColor = value;
			}
		}

		public int ActiveHexBorderWidth
		{
			get
			{
				return activeHexBorderWidth;
			}
			set
			{
				activeHexBorderWidth = value;
			}
		}
		#endregion

		public BoardState()
		{
			backgroundColor = Color.White;
			gridColor = Color.Black;
			gridPenWidth = 1;
			activeHex = null;
			activeHexBorderColor = Color.Blue;
			activeHexBorderWidth = 1;
		}
	}
}

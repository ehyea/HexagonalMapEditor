using System;
using System.Collections.Generic;
using System.Text;

namespace Hexagonal
{
	public class HexState
	{
		private System.Drawing.Color backgroundColor;
        private System.Drawing.Color borderColor;
		

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

        public System.Drawing.Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
            }
        }


		public HexState()
		{
			this.backgroundColor = System.Drawing.Color.White;
            this.borderColor = System.Drawing.Color.LightGray;
		}

        public void setBackgroundColor(System.Drawing.Color col)
        {
          
            this.backgroundColor = col;
        }


        public HexState(float score,float minScore,float maxScore)
        {
            this.borderColor = System.Drawing.Color.LightGray;
            float min = .3f;
            float diff = maxScore - minScore;
            if (diff > 0) 
            { 
                float diffp = score - minScore;
                score = diffp * 100 / diff;
                score = (score / 100) * 0.7f;
                this.backgroundColor =ColorsUtil.HSBtoRGB( 0.1f, min+score, 1);
            }else{
                this.backgroundColor =ColorsUtil.HSBtoRGB( 0.1f, 1f, 1);
            }
        
            //this.backgroundColor = System.Drawing.Color.LightGray;
            //switch (size)
            //{
            //    case 1:
            //        this.backgroundColor = (Color)new ColorConverter().ConvertFromString("#FFF3E0");
            //        break;
            //    case 2:
            //        this.backgroundColor = (Color)new ColorConverter().ConvertFromString("#FFE0B2");
            //        break;
            //    case 3:
            //        this.backgroundColor = (Color)new ColorConverter().ConvertFromString("#FFCC80");
            //        break;
            //    case 4:
            //        this.backgroundColor = (Color)new ColorConverter().ConvertFromString("#FFB74D");
            //        break;
            //    case 5:
            //        this.backgroundColor = (Color)new ColorConverter().ConvertFromString("#FFA726");
            //        break;
            //    case 6:
            //        this.backgroundColor = (Color)new ColorConverter().ConvertFromString("#FF9800");
            //        break;
            //    case 7:
            //        this.backgroundColor = (Color)new ColorConverter().ConvertFromString("#FB8C00");
            //        break;
            //    case 8:
            //        this.backgroundColor = (Color)new ColorConverter().ConvertFromString("#F57C00");
            //        break;
            //    case 9:
            //        this.backgroundColor = (Color)new ColorConverter().ConvertFromString("#EF6C00");
            //        break;
            //    case 10:
            //        this.backgroundColor = (Color)new ColorConverter().ConvertFromString("#E65100");
            //        break;
            //    default:
            //        this.backgroundColor = (Color)new ColorConverter().ConvertFromString("#E65100");
            //        break;
            //}
        }

	}
}

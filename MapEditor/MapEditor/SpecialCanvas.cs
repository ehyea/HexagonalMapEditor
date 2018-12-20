using Hexagonal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
namespace MapEditor
{
    class SpecialCanvas : Canvas
    {
        GraphicsEngine engine;

        public GraphicsEngine Engine
        {
            get
            {
                return engine;
            }
            set
            {
                engine = value;
                Height = engine.Height;
                Width = engine.Weight;
                InvalidateVisual();
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (engine != null)
            {
                engine.Draw(drawingContext);
            }
        }
    }
}
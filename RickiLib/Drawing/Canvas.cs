using System;
using System.Collections.Generic;
using System.Drawing;

namespace RickiLib.Drawing
{

	public class Canvas : RickiLib.Widgets.DrawingAreaGDI
	{
		
		private ShapeCollection shapes;

		public Canvas ()
		{
			shapes = new ShapeCollection ();
		}

		protected override void OnPaint (Graphics g)
		{
			foreach (Shape shape in shapes)
				shape.Draw (g);
		}

		public ShapeCollection Shapes {
			get { return shapes; }
		}
	}
}

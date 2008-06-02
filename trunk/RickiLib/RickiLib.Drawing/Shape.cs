/*
* DEPRECATED *
using System;
using System.Drawing;

namespace RickiLib.Drawing 
{
	public abstract class Shape 
	{
		private Pen pen;
		private Brush background;
		
		public Shape ()
		{
			pen = new Pen (Color.LightBlue);
			background = new SolidBrush (Color.Red);
		}
		
		public abstract void Draw (Graphics g);
		
		public Pen Pen {
			get { return pen; }
			set { pen = value; }
		}
		
		public Brush Background {
			get { return background; }
			set { background = value; }
		}
	}
}
*/
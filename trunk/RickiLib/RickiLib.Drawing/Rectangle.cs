/*
* DEPRECATED *
using System;
using System.Drawing;

namespace RickiLib.Drawing
{
	public class Rectangle : Shape
	{
		private System.Drawing.Rectangle rect;		
		
		public Rectangle (System.Drawing.Rectangle rect)
		{
			this.rect = rect;
		}
		
		public Rectangle (int x, int y, int width, int height) : 
			this (new System.Drawing.Rectangle (x, y, width, height))
		{
		}
		
		public override void Draw (Graphics g)
		{
			g.FillRectangle (
				this.Background,
				rect
			);
			
			g.DrawRectangle (
				this.Pen,
				rect
			);
		}
		
		public  int X {
			get { return rect.X; }
		}
		
		public int Y {
			get { return rect.Y; }
		}
		
		public int Width {
			get { return rect.Width;}
		}
		
		public int Height {
			get { return rect.Height; }
		}		
	}
}
*/
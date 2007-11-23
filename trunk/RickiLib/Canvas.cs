using System;
using System.Drawing;
using RickiLib.Widgets;
using Gtk;

public class Canvas : RickiLib.Widgets.DrawingAreaGDI 
{
	
	protected override void OnPaint (Graphics g)
	{
	//	drawMonth (g, "Enero", X, 100);
	}
	
	
	protected override void OnShown ()
	{
		base.OnShown ();
	//	GLib.Timeout.Add (velocity, timeout);
	}
	
	private bool timeout ()
	{
	//	X += accel;
		base.QueueResize ();
		return true;
	}
	static void Main ()
	{
		Application.Init ();
		CustomWindow win = new CustomWindow ("Drawing Test");
		win.VBox.PackStart (new Canvas ());
		win.ShowAll ();
		Application.Run ();
	}

}


abstract class Shape {
	public abstract void Draw (Graphics g);
}

class RickiCalendar : Shape
{
	private int x;
	private int y;
	
	public RickiCalendar ()
	{
		x = 0; 
		y = 0;
	}
	
	public override void Draw (Graphics g)
	{
		drawMonth (g, string.Empty, x, y);
	}

	private void drawMonth (Graphics g, string name, int x, int y)
	{
		int w = 200, h = 200;
		int border = 10;
		
		g.FillRectangle (Brushes.LightBlue, x, y, w, h);
		g.FillRectangle (Brushes.White, 
			x + border, 
			y + border, 
			w - border * 2, 
			border + 15);
		
		Font font = new Font ("comic sans ms", 10);
		font = new Font (font, FontStyle.Bold);
		
		SizeF size = g.MeasureString (name,font);
		
		int fx = w / 2;
		fx = x + fx - ((int) size.Width /2);
		
		g.DrawString (name, font, Brushes.Brown, fx, y + border + 2);
	}
	int X = 10;
	int accel = 50;
	uint velocity = 500;


}




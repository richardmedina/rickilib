
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Gtk;
using Gdk;
using Gtk.DotNet;

using RickiLib.Types;

namespace RickiLib.Widgets
{
	
	
	public class DrawingAreaGDI : Gtk.DrawingArea
	{
		private System.Drawing.Graphics graphics;
		private Gdk.Pixmap pixmap;
		private System.Drawing.Brush backgroundBrush;
		private System.Drawing.Pen defaultPen;
		
		public event PaintHandler Paint;
		
		public DrawingAreaGDI ()
		{
			base.ModifyBg (StateType.Normal, new Gdk.Color (255, 255, 255));
			backgroundBrush = new SolidBrush (System.Drawing.Color.White);
			defaultPen = new Pen (System.Drawing.Color.Black);
			
			Paint = onPaint;
		}
		
		protected override bool OnConfigureEvent (Gdk.EventConfigure args)
		{
			// Pixmap must be created every time for drawing over
			//compatible drawable..
		
			pixmap = new Gdk.Pixmap (
				this.GdkWindow,
				this.Allocation.Width,
				this.Allocation.Height, 
				-1 // Sets depth from drawable
				);

			graphics = Gtk.DotNet.Graphics.FromDrawable (pixmap);
			
			graphics.FillRectangle (backgroundBrush,
				0, 0,
				Allocation.Width, Allocation.Height);

			Paint (this, new PaintArgs (this.Graphics));
			
			return false;
		}
		
		protected override bool OnExposeEvent (Gdk.EventExpose args)
		{
			
		//	bool ret = base.OnExposeEvent (args);
			
			args.Window.DrawDrawable (
				this.Style.WhiteGC,
				pixmap,
				args.Area.X,
				args.Area.Y,
				args.Area.X,
				args.Area.Y,
				args.Area.Width,
				args.Area.Height
			);
			
			return false;
		}
		
		private void onPaint (object sender, PaintArgs args)
		{
			OnPaint (args.Graphics);
		}
		
		protected virtual void OnPaint (System.Drawing.Graphics graphics)
		{
		}
		
		public System.Drawing.Graphics Graphics {
			get {
				return graphics;
			}
		}
		
		public Gdk.Pixmap Pixmap {
			get {
				return pixmap;
			}
		}
		
		public System.Drawing.Brush BackgroundBrush {
			get {
				return backgroundBrush;
			}
		}
		
		public System.Drawing.Pen DefaultPen {
			get {
				return defaultPen;
			}
		}
		
	}
}


using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class Module : Gtk.EventBox
	{
		private Gdk.Pixbuf pixbuf;
		
		private string title;
		private string path;
		
		private static int instance_counter = 0;
		
		private int instance;
		private int position;
				
		public Module (string title) : 
			this (title, string.Empty)
		{
		}
		
		public Module (string title, string path) : 
			this (null, title, path)
		{
		}
		
		public Module (Gdk.Pixbuf pixbuf, string title) :
			this (pixbuf, title, string.Empty)
		{
		}
		
		public Module (Gdk.Pixbuf buf, string title, string path)
		{
			this.pixbuf = buf;
			this.title = title;
			this.instance =  ++ instance_counter;
			this.path = path;
			this.position = 0;
		}
				
		public Gdk.Pixbuf Pixbuf {
			get {
				return pixbuf;
			}
			set {
				pixbuf = value;
			}
		}
	
		public string Title {
			get {
				return title;
			}
		}
		
		public new string Path {
			get {
				return path;
			}
		}
		
		public int Instance {
			get {
				return instance;
			}
			internal set {
				instance = value;
			}
		}
		
		internal int Position {
			get {
				return position;
			}
			set {
				position = value;
			}
		}
	}
}

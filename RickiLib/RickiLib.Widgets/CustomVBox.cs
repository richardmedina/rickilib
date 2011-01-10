
using System;
using Gtk;

namespace RickiLib.Widgets
{


	public class CustomVBox : Gtk.VBox
	{

		public CustomVBox () : base (false, 0)
		{
			Spacing = 5;
			BorderWidth = 5;
		}
	}
}

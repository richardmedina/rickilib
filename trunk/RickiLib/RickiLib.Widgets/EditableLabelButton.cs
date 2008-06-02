// EditableLabelButton.cs created with MonoDevelop
// User: ricki at 20:47 11/20/2007
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class EditableLabelButton : Gtk.Button
	{
		private EditableLabel editableLabel;
		private HBox hbox;
		
		public EditableLabelButton ()
		{
			base.Relief = ReliefStyle.None;
			editableLabel = new EditableLabel ();
			hbox = new HBox ();
			hbox.PackStart (editableLabel);
			
			base.Child = hbox;
		}
		
		protected override bool OnButtonPressEvent (Gdk.EventButton evnt)
		{
			//Console.WriteLine ("Button Press");
			
			Gdk.Rectangle labelrect =editableLabel.Allocation;
			
			if (Allocation.X + evnt.X >= labelrect.X &&
				Allocation.Y + evnt.Y >= labelrect.Y &&
				Allocation.X + evnt.X <= labelrect.X + labelrect.Width &&
				Allocation.Y + evnt.Y <= labelrect.Y + labelrect.Height) {
				
				editableLabel.ShowHideEditor ();
				return false;
			}
			
			return base.OnButtonPressEvent (evnt);
		}

		
		protected override void OnClicked ()
		{
			base.OnClicked ();
		}

		
		public HBox HBox {
			get { return hbox; }
		}
		
		public EditableLabel EditableLabel {
			get { return editableLabel;}
		}
	}
}

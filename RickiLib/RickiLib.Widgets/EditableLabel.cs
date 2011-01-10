// EditorButton.cs created with MonoDevelop
// User: ricki at 11:06 11/20/2007
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class EditableLabel : Gtk.VBox
	{
		private Gtk.Entry entry;
		private Gtk.Label label;
		
		private string text;
		
		private event EventHandler changed;
		
		public EditableLabel ()
		{
			changed = onChanged;
			this.text = string.Empty;
			
			entry = new Gtk.Entry ();
			entry.WidthRequest = 10;
			entry.Activated += entry_Activated;
			entry.KeyPressEvent += entry_KeyPressEvent;
			entry.FocusOutEvent += delegate {
				ShowHideEditor ();	
			};
			
			label = new Gtk.Label (text);
			label.Ellipsize = Pango.EllipsizeMode.End;
			label.SetAlignment (0.0f, 0.5f);
			label.CanFocus = true;
			
			label.Show ();
			entry.Show ();
			
			this.PackStart (label);
		}
		
		protected override bool OnButtonPressEvent (Gdk.EventButton evnt)
		{
			ShowHideEditor ();
			//base.Active = true;
			
			//base.Active = false;
			//base.OnToggled ();
			
			return base.OnButtonPressEvent (evnt);
		}
		
		private bool editorVisible = false;
		
		private void ActivateEdition ()
		{
			entry.Text = text;
			
			this.Remove (label);
			this.PackStart (entry, false, false, 0);
			entry.GrabFocus ();
			editorVisible = true;
		}
		
		private void DeactivateEdition ()
		{
			this.Remove (entry);
			this.PackStart (label);
			editorVisible = false;
			this.GrabFocus ();
		}
		
		public void ShowHideEditor ()
		{
			if (editorVisible)
				DeactivateEdition ();
			else
				ActivateEdition ();
		}
		
		private void entry_Activated (object sender, EventArgs args)
		{
			if (entry.Text != Text) {
				Text = entry.Text;
				OnChanged ();
			}
			ShowHideEditor ();
		}
		
		private void entry_KeyPressEvent (object sender, KeyPressEventArgs args)
		{
			if (args.Event.Key == Gdk.Key.Escape) {
				ShowHideEditor ();
			}
		}
		
		protected virtual void OnChanged ()
		{
			changed (this, EventArgs.Empty);
		}
		
		private void onChanged (object sender, EventArgs args)
		{
		}
				
		public string Text {
			get { return text; }
			set { 
				text = value;
				label.Text = text;
			}
		}
		
		public event EventHandler Changed {
			add { changed += value; }
			remove { changed -= value; }
		}
	}
}

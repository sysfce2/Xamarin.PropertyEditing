using System.Collections;
using AppKit;
using Xamarin.PropertyEditing.Mac.Resources;
using Xamarin.PropertyEditing.ViewModels;

namespace Xamarin.PropertyEditing.Mac
{
	internal class CharEditorControl : PropertyEditorControl<PropertyViewModel<char>>
	{
		readonly CharTextField editor;

		public CharEditorControl (IHostResourceProvider hostResources)
			: base (hostResources)
		{
			editor = new CharTextField {
				Font = NSFont.FromFontName (DefaultFontName, DefaultFontSize)
			};

			// update the value on keypress
			editor.Changed += (sender, e) => {
				if (editor.StringValue.Length > 0) {
					ViewModel.Value = editor.StringValue[0];
				} else {
					ViewModel.Value = default (char);
				}
			};
			AddSubview (editor);

			this.AddConstraints (new[] {
				NSLayoutConstraint.Create (editor, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this,  NSLayoutAttribute.Top, 1f, 1f),
				NSLayoutConstraint.Create (editor, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this,  NSLayoutAttribute.Width, 1f, -34f),
				NSLayoutConstraint.Create (editor, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1f, DefaultControlHeight - 3),
			});

		}

		public override NSView FirstKeyView => editor;
		public override NSView LastKeyView => editor;

		protected override void UpdateValue ()
		{
			editor.StringValue = ViewModel.Value.ToString ();
		}

		protected override void SetEnabled ()
		{
			editor.Editable = ViewModel.Property.CanWrite;
		}

		protected override void UpdateAccessibilityValues ()
		{
			editor.AccessibilityTitle = string.Format (LocalizationResources.AccessibilityChar, ViewModel.Property.Name);
		}
	}
}

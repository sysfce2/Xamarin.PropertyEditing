using System;
using AppKit;
using CoreGraphics;

namespace Xamarin.PropertyEditing.Mac
{
	internal class FocusablePopUpButton : NSPopUpButton
	{
		public override bool CanBecomeKeyView { get { return Enabled; } }

		public RowProxyResponder ProxyResponder { get; set; }

		public FocusablePopUpButton ()
		{
			Cell.LineBreakMode = NSLineBreakMode.TruncatingMiddle;
		}

		public override void KeyDown (NSEvent theEvent)
		{
			switch (theEvent.KeyCode) {
			case (int)NSKey.Tab:
				if (ProxyResponder != null) {
					if (theEvent.ModifierFlags.HasFlag(NSEventModifierMask.ShiftKeyMask)) {
						ProxyResponder.PreviousResponder ();
					} else {
						ProxyResponder.NextResponder ();
					}
				}
				return;
			}
			base.KeyDown (theEvent);
		}

		public override bool BecomeFirstResponder ()
		{
			var willBecomeFirstResponder = base.BecomeFirstResponder ();
			if (willBecomeFirstResponder) {
				ScrollRectToVisible (Bounds);
			}
			return willBecomeFirstResponder;
		}
	}
}

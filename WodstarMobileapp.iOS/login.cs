﻿
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WodstarMobileapp.iOS
{
	public partial class login : UIViewController
	{
		login view;

		public override void LoadView()
		{
			base.LoadView();
			view = new login();

		}

		public login () : base ("login", null)
		{
			facebook.TouchDown += delegate {
					};

			skipLogin.AllTouchEvents += delegate {

				// mainMenu();

			};
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

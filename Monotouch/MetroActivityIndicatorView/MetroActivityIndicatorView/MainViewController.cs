using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace MetroActivityIndicatorView
{
	public partial class MainViewController : UIViewController
	{
		public MainViewController () : base ("MainViewController", null)
		{
			// Custom initialization
		}

		MetroActivityIndicatorView indicator;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			RectangleF f = new RectangleF (0, 0, 100, 100);

			indicator = new MetroActivityIndicatorView (f);
			View.AddSubview (indicator);
		}

		public override void ViewWillLayoutSubviews()
		{
			base.ViewWillLayoutSubviews ();
			indicator.Center = View.Center;
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear (animated);
			indicator.StartAnimating ();
			//CGPath p = CGPath.EllipseFromRect(new RectangleF(),CGAffineTransform.MakeIdentity());
		}
	}
}

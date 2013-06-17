using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.ObjCRuntime;
using MonoTouch.CoreGraphics;

namespace MetroActivityIndicatorView
{
	[Register("MetroActivityIndicatorView")]
	public class MetroActivityIndicatorView : UIView
	{
		public UIColor Color;

		private UIColor kDefaultColor = UIColor.White;

		private bool isAnimating = false;

		private int circleNumber;

		private int maxCircleNumber = 5; //maximum number of circles

		private float circleSize; //depends on frame.size

		private float radius; //depends on frame.size

		#region Life Cycle

		private void CommonInit(UIColor color)
		{
			isAnimating = false;
			Color = color;
		}

		[Export("initWithRect:")]
		public MetroActivityIndicatorView (RectangleF frame,UIColor color=null) : base(frame)
		{
			if (color==null)
				CommonInit (kDefaultColor);
			else
				CommonInit (color);
		}

		[Export("initWithCoder:")]
		public MetroActivityIndicatorView (NSCoder aDecoder) : base(aDecoder)
		{
			CommonInit (kDefaultColor);
		}

		#endregion

		#region Animation

		public void StartAnimating()
		{    
			if (!isAnimating)
			{
				isAnimating = true;

				circleNumber = 0;

				radius = Frame.Width/2;

				if (Frame.Width > Frame.Height)
				{
					radius = Frame.Height/2;
				}

				circleSize = 15*radius/55;

				AddCircles ();

				StartAnimatingTransaction ();
			}
		}

		public void StopAnimating()
		{
			isAnimating = false;

			RemoveCircles ();
		}

		public bool IsAnimating()
		{
			return isAnimating;
		}

		#endregion

		#region Animation Internals

		[Export("animationDidStop:finished:")]
		public void AnimationDidStop(CAAnimation anim,bool finished)
		{
			if (isAnimating)
			{
				PerformSelector (new Selector("StartAnimatingTransaction"), null, 1.0);
			}
		}

		private void AddCircles()
		{
			CGAffineTransform tnull = CGAffineTransform.MakeIdentity ();
			for (circleNumber=0; circleNumber<maxCircleNumber; circleNumber++)
			{
				RectangleF f = new RectangleF((Frame.Width-circleSize)/2 - 1, Frame.Height-circleSize -1, circleSize +2, circleSize+2);
				CAShapeLayer circleLayer = (CAShapeLayer)CAShapeLayer.Create ();
				circleLayer.Frame = f;
				f.X=0; f.Y=0;
				CGPath p = CGPath.EllipseFromRect(f,tnull); // crash in monotouch! - waiting for fix.
				circleLayer.Path = p;
				circleLayer.FillColor = Color.CGColor;
				Layer.AddSublayer (circleLayer);
			}
		}

		private void RemoveCircles()
		{
			foreach (CALayer layer in Layer.Sublayers)
			{
				layer.RemoveFromSuperLayer ();
			}
			//[[self.layer sublayers] makeObjectsPerformSelector:@selector(removeFromSuperlayer)];
		}

		[Export("StartAnimatingTransaction")]
		private void StartAnimatingTransaction()
		{
			CATransaction.Begin();


			float M_PI_2 = ((float) (Math.PI)) / 2.0f;

			CGAffineTransform tnull = CGAffineTransform.MakeIdentity ();

			CGPath circlePath = new CGPath ();
			circlePath.MoveToPoint(tnull,Frame.Width/2.0f,Frame.Height-circleSize/2.0f);
			circlePath.AddArc(tnull,Frame.Width/2.0f,Frame.Height/2.0f,radius-15/2,M_PI_2, -M_PI_2*3,false);    

			for (int i = 0; i < Layer.Sublayers.Length; i++)
			{
				CALayer circleLayer = Layer.Sublayers[i];					

				CAKeyFrameAnimation circleAnimation = CAKeyFrameAnimation.GetFromKeyPath ("position");
				circleAnimation.BeginTime = CAAnimation.CurrentMediaTime() + 0.2f*i;
				circleAnimation.Duration = 1.5;
				circleAnimation.TimingFunction = CAMediaTimingFunction.FromControlPoints (0.15f, 0.60f, 0.85f, 0.4f);
				circleAnimation.CalculationMode = CAKeyFrameAnimation.AnimationPaced;
				circleAnimation.Path = circlePath;
				circleAnimation.RepeatCount = float.MaxValue;
				if (circleLayer == this.Layer.Sublayers[Layer.Sublayers.Length-1])
				{
					circleAnimation.WeakDelegate = this;
				}
				circleLayer.AddAnimation (circleAnimation, "circleAnimation");
			}
			//CGPathRelease(circlePath);

			CATransaction.Commit();
		}

		#endregion
	}
}

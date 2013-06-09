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

		bool isAnimating = false;

		int circleNumber;

		int maxCircleNumber = 5; //maximum number of circles

		float circleSize; //depends on frame.size

		float radius; //depends on frame.size

		#region Life Cycle

		private void CommonInit(UIColor color=UIColor.White)
		{
			isAnimating = false;
			Color = color;
		}

		[Export("initWithRect:")]
		public MetroActivityIndicatorView (RectangleF frame,UIColor color=UIColor.White) : base(frame)
		{
			CommonInit (color);
		}

		[Export("initWithCoder:")]
		public MetroActivityIndicatorView (NSCoder aDecoder) : base(aDecoder)
		{
			CommonInit ();
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

		public void AnimationDidStop(CAAnimation anim,bool finished)
		{
			if (isAnimating)
			{
				PerformSelector (new Selector("StartAnimatingTransaction"), null, 1.0);
			}
		}

		private void AddCircles()
		{
			for (circleNumber=0; circleNumber<maxCircleNumber; circleNumber++)
			{
				RectangleF f = new RectangleF((Frame.Width-circleSize)/2 - 1, Frame.Height-circleSize -1, circleSize +2, circleSize+2);
				CAShapeLayer circleLayer = CAShapeLayer.Create ();
				circleLayer.Frame = f;
				f.X=0; f.Y=0;
				circleLayer.Path = CGPath.EllipseFromRect(f,CGAffineTransform.MakeIdentity);
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

			CGMutablePathRef circlePath = CGPathCreateMutable();
			CGPathMoveToPoint(circlePath, null, frame.Width/2, frame.Height-circleSize/2);
			CGPathAddArc(circlePath, null, frame.Width/2, frame.Height/2, radius-15/2, M_PI_2, -M_PI_2*3, NO);    

			for (int i = 0; i < Layer.Sublayers.Length; i++)
			{
				CALayer circleLayer = Layer.Sublayers[i];					

				CAKeyframeAnimation

				CAKeyframeAnimation circleAnimation = CAKeyframeAnimation animationWithKeyPath:@"position"];
				[circleAnimation setBeginTime:CACurrentMediaTime()+0.2f*i];
				circleAnimation.duration = 1.5;
				circleAnimation.timingFunction = [CAMediaTimingFunction functionWithControlPoints:0.15f :0.60f :0.85f :0.4f];
				[circleAnimation setCalculationMode:kCAAnimationPaced];
				circleAnimation.path = circlePath;
				circleAnimation.repeatCount = HUGE_VALF;
				if (circleLayer == [self.layer.sublayers lastObject])
				{
					[circleAnimation setDelegate:self];
				}
				[circleLayer addAnimation:circleAnimation forKey:@"circleAnimation"];
			}

			CGPathRelease(circlePath);

			[CATransaction commit];
		}

		public void StopAnimating()
		{
			isAnimating = false;

			RemoveCircles ();
		}

		public bool isAnimating()
		{
			return isAnimating;
		}

		#endregion
	}
}

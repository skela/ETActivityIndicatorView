//
//  ETActivityIndicatorView.m
//  ETActivityIndicatorView
//
//  Created by Eugene Trapeznikov on 5/24/13.
//  Copyright (c) 2013 Eugene Trapeznikov. All rights reserved.
//

#import "MetroActivityIndicatorView.h"

#import <QuartzCore/QuartzCore.h>

@implementation MetroActivityIndicatorView

@synthesize color;

BOOL isAnimating = NO;

UIImageView *circleImage;

int circleNumber;

int maxCircleNumber = 5; //maximum number of circles

float circleSize; //depends on frame.size

float radius; //depends on frame.size

#pragma mark - Life Cycle

- (void)commonInit
{
    isAnimating = NO;
}

- (id)initWithFrame:(CGRect)frame
{
    return [self initWithFrame:frame andColor:[UIColor whiteColor]];
}

- (id)initWithFrame:(CGRect)frame andColor:(UIColor*)theColor
{
    self = [super initWithFrame:frame];
    if (self)
    {
        color = theColor;
        [self commonInit];
    }
    return self;
}

- (id)initWithCoder:(NSCoder *)aDecoder
{
    self = [super initWithCoder:aDecoder];
    if (self)
    {
        [self commonInit];
    }
    return self;
}

#pragma mark - Animation

-(void)startAnimating
{    
    if (!isAnimating)
    {
        isAnimating = YES;
        
        circleNumber = 0;
        
        radius = self.frame.size.width/2;
        
        if (self.frame.size.width > self.frame.size.height)
        {
            radius = self.frame.size.height/2;
        }
        
        circleSize = 15*radius/55;
        
        [self addCircles];
        
        [self startAnimatingTransaction];
    }
}

- (void)animationDidStop:(CAAnimation *)anim finished:(BOOL)flag
{
    if (isAnimating)
    {
        [self performSelector:@selector(startAnimatingTransaction) withObject:nil afterDelay:1.0f];
    }
}

- (void)addCircles
{
    for (circleNumber=0; circleNumber<maxCircleNumber; circleNumber++)
    {        
        CGRect f = CGRectMake((self.frame.size.width-circleSize)/2 - 1, self.frame.size.height-circleSize -1, circleSize +2, circleSize+2);
        CAShapeLayer *circleLayer = [CAShapeLayer layer];
        circleLayer.frame = f;
        f.origin.x=0;
        f.origin.y=0;
        circleLayer.path = CGPathCreateWithEllipseInRect(f,nil);
        circleLayer.fillColor = self.color.CGColor;
        [self.layer addSublayer:circleLayer];
    }
}

- (void)removeCircles
{
    [[self.layer sublayers] makeObjectsPerformSelector:@selector(removeFromSuperlayer)];
}

- (void)startAnimatingTransaction
{
    [CATransaction begin];
    
    CGMutablePathRef circlePath = CGPathCreateMutable();
    CGPathMoveToPoint(circlePath, NULL, self.frame.size.width/2, self.frame.size.height-circleSize/2);
    CGPathAddArc(circlePath, NULL, self.frame.size.width/2, self.frame.size.height/2, radius-15/2, M_PI_2, -M_PI_2*3, NO);    
    
    for (int i = 0; i < self.layer.sublayers.count; i++)
    {
        CALayer *circleLayer = [self.layer.sublayers objectAtIndex:i];        
        
        CAKeyframeAnimation *circleAnimation = [CAKeyframeAnimation animationWithKeyPath:@"position"];
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

-(void)stopAnimating
{
    isAnimating = NO;
    
    [self removeCircles];
}

-(BOOL)isAnimating
{
    return isAnimating;
}

@end

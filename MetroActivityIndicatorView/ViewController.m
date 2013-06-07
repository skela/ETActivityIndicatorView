//
//  ViewController.m
//  ETActivityIndicatorView
//
//  Created by Eugene Trapeznikov on 5/23/13.
//  Copyright (c) 2013 Eugene Trapeznikov. All rights reserved.
//

#import "ViewController.h"

#import "MetroActivityIndicatorView.h"

@interface ViewController ()

@end

@implementation ViewController

- (void)viewDidLoad
{
    [super viewDidLoad];

    //standard UIActivityIndicatorView
    self.activity = [[UIActivityIndicatorView alloc] initWithFrame:CGRectMake(self.view.frame.size.width-110, 150, 20, 20)];
    self.activity.activityIndicatorViewStyle = UIActivityIndicatorViewStyleWhite;
    [self.activity startAnimating];
    [self.view addSubview:self.activity];
    
    //ETActivityIndicatorView
    self.etActivity = [[MetroActivityIndicatorView alloc] initWithFrame:CGRectMake(50, 150, 60, 60)];
    
    //you can set your custom color for ETActivityIndicatorView
    //etActivity.color = [UIColor blueColor];
    
    [self.etActivity startAnimating];
    [self.view addSubview:self.etActivity];
    
    UIButton *startButton = [UIButton buttonWithType:UIButtonTypeRoundedRect];
    [startButton setTitle:@"start" forState:UIControlStateNormal];
    startButton.frame = CGRectMake(50, 50, 100, 50);
    [startButton addTarget:self action:@selector(startAnimation) forControlEvents:UIControlEventTouchUpInside];
    [self.view addSubview:startButton];
    
    UIButton *stopButton = [UIButton buttonWithType:UIButtonTypeRoundedRect];
    [stopButton setTitle:@"stop" forState:UIControlStateNormal];
    stopButton.frame = CGRectMake(self.view.frame.size.width - 50 - 100, 50, 100, 50);
    [stopButton addTarget:self action:@selector(stopAnimation) forControlEvents:UIControlEventTouchUpInside];
    [self.view addSubview:stopButton];
}

-(void)startAnimation{
    [self.activity startAnimating];
    [self.etActivity startAnimating];
}

-(void)stopAnimation{
    [self.activity stopAnimating];
    [self.etActivity stopAnimating];
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

@end

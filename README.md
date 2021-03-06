MetroActivityIndicatorView
=======================

WindowsPhone like activity indicator for iOS. Display it while loading or waiting something.

![example](https://lh5.googleusercontent.com/-WZDKi17rN9A/UZ9cpsbPHEI/AAAAAAAAAIE/2QS3xX2KHqA/s480/iOS%2520Simulator%2520Screen%2520shot%2520May%252024%252C%25202013%25205.58.52%2520PM.png)

Requirements
------------

- Xcode 4.2 or later.
- iOS 5 or later.
- ARC.

How to use
=======================

Include the `MetroActivityIndicatorView.h`, `MetroActivityIndicatorView.m`, in your project.

To display  `MetroActivityIndicatorView`, simply do it like basic `UIActivityIndicatorView`:

    MetroActivityIndicatorView *indicator = [[MetroActivityIndicatorView alloc] initWithFrame:CGRectMake((10, 10, 60, 60)];
    [indicator startAnimating];
    [self.view addSubview:indicator];

Example project included.

License
=======================

The MIT License (MIT)

Copyright (c) 2013 Eugene Trapeznikov

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

Support / Contact / Bugs / Features
-----------------------------------

I can't promise to answer questions about how to use the code.

If you want to submit a feature request or bug report, please use [GitHub's issue tracker for this project](https://github.com/EugeneTrapeznikov/ETActivityIndicatorView/issues).  Or preferably fork the code and implement the feature/fix yourself, then submit a pull request.

Enjoy!

Eugene Trapeznikov

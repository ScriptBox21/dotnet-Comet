﻿using System;
using HotUI.iOS.Controls;
using UIKit;
namespace HotUI.iOS 
{
	public interface iOSViewHandler : IViewHandler 
	{
        event EventHandler<ViewChangedEventArgs> NativeViewChanged;
        event EventHandler RemovedFromView;

        UIView View { get; }

        HUIContainerView ContainerView { get; }
    }
}

using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;

namespace HotUI.iOS.Controls
{
    public class HUIContainerView : UIView
    {
        private UIView _mainView;
        private CAShapeLayer _shadowLayer;
        private CAShapeLayer _maskLayer;
        private CGSize _size;
        private CGSize _intrinsicContentSize;
        
        public HUIContainerView()
        {
            AutosizesSubviews = true;
        }

        public override CGRect Frame
        {
            get => base.Frame;
            set
            {
                base.Frame = value;
                
                if (_shadowLayer != null || _maskLayer != null)
                {
                    if (!_size.Equals(value.Size))
                    {
                        var fx = value.Size.Width / _size.Width;
                        var fy = value.Size.Height / _size.Height;
                        var transform = CGAffineTransform.MakeScale(fx, fy);
                        var path = _shadowLayer?.Path ?? _maskLayer?.Path;
                        var transformedPath = path.CopyByTransformingPath(transform);
                        if (_shadowLayer != null)
                            _shadowLayer.Path = transformedPath;

                        if (_maskLayer != null)
                            _maskLayer.Path = transformedPath;
                    }
                }

                _size = value.Size;
            }
        }

        public UIView MainView
        {
            get => _mainView;
            set
            {
                if (_mainView != null)
                {
                    ShadowLayer = null;
                    MaskLayer = null;
                    _mainView.RemoveFromSuperview();
                }

                _mainView = value;

                if (_mainView != null)
                {
                    base.Frame = _mainView.Bounds;
                    _size = _mainView.Bounds.Size;
                    _mainView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
                    _mainView.Frame = Bounds;
                    AddSubview(_mainView);
                }
            }
        }
        
        public CAShapeLayer ShadowLayer
        {
            get => _shadowLayer;
            set
            {
                _shadowLayer?.RemoveFromSuperLayer();
                _shadowLayer = value;
                if (_shadowLayer != null && _mainView != null)
                    Layer.InsertSublayerBelow(_shadowLayer, _mainView.Layer);
            }
        }

        public override CGSize IntrinsicContentSize => _intrinsicContentSize;

        public override CGSize SizeThatFits(CGSize size)
        {
            if (_mainView != null)
            {
                _intrinsicContentSize = _mainView.SizeThatFits(size);
                return _intrinsicContentSize;
            }

            return base.SizeThatFits(size);
        }

        public override void SizeToFit()
        {
            if (_mainView != null)
            {
                _mainView.SizeToFit();
                _intrinsicContentSize = _mainView.Bounds.Size;
                Frame = _mainView.Bounds;
            }
            else
            {
                base.SizeToFit();
            }
        }

        public CAShapeLayer MaskLayer
        {
            get => _maskLayer;
            set
            {
                if (_maskLayer != null)
                    _mainView.Layer.Mask = null;

                _maskLayer = value;                    
                if (_mainView != null)
                    _mainView.Layer.Mask = value;
            }
        }
    }
}
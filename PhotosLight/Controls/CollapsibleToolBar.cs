//
// Copyright (c) Microsoft Corporation.  All rights reserved.
//


using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace PhotosLight.Controls
{
    public class CollapsibleToolBar : CommandBar
    {
        double? cachedWidthWithLabels;
        private ContentControl leftContent;
        private ContentControl middleContent;
        private ItemsControl primaryAppBar;

        public static readonly DependencyProperty MiddleContentProperty = DependencyProperty.Register(
           "MiddleContent",
           typeof(object),
           typeof(CollapsibleToolBar),
           new PropertyMetadata(null));
        
        public object MiddleContent
        {
            get { return GetValue(MiddleContentProperty); }
            set { SetValue(MiddleContentProperty, value); }
        }
           
        /// <summary>
        /// The more button needs to be accessible as an anchor if 
        /// any secondary commands are trying to show a flyout
        /// </summary>
        public ButtonBase MoreButton { get; private set; }
        
        public bool CollapseLabelsOnResize { get; set; }

        public CollapsibleToolBar()
        {
            CollapseLabelsOnResize = true;            
            this.DefaultLabelPosition = CommandBarDefaultLabelPosition.Right;
        }
        
        #region OnApplyTemplate()
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.MoreButton = (ButtonBase)this.GetTemplateChild("MoreButton");
            leftContent = GetTemplateChild("ContentControl") as ContentControl;
            middleContent = GetTemplateChild("MiddleContentControl") as ContentControl;
            primaryAppBar =  GetTemplateChild("PrimaryItemsControl") as ItemsControl;
           
           this.MoreButton.AllowFocusOnInteraction = false;       
        }
        #endregion


        protected override Size MeasureOverride(Size availableSize)
        {       
            UpdateMiddleContentPosition(availableSize);

            if (!this.CollapseLabelsOnResize)
            {
                return base.MeasureOverride(availableSize);
            }

            // If we haven't measured our width with labels, do so now
            //
            if (this.cachedWidthWithLabels == null)
            {
                this.DefaultLabelPosition = CommandBarDefaultLabelPosition.Right;
                this.primaryAppBar.Measure(new Size(double.PositiveInfinity, availableSize.Height));
                this.cachedWidthWithLabels = this.primaryAppBar.DesiredSize.Width + this.MoreButton.Width;
            }

            double calculatedAvailableWidth = Math.Max(0, availableSize.Width -
                (this.leftContent.Width + this.middleContent.DesiredSize.Width));

            // Set DefaultLabelPosition based on if the labels fit
            //
            bool theLabelsFit = this.cachedWidthWithLabels <= calculatedAvailableWidth;
            if (theLabelsFit)
            {
                this.DefaultLabelPosition = CommandBarDefaultLabelPosition.Right;
            }
            else
            {
                this.DefaultLabelPosition = CommandBarDefaultLabelPosition.Collapsed;
            }

            return base.MeasureOverride(availableSize);
        }

        private void UpdateMiddleContentPosition(Size size)
        {
            if (middleContent == null)
                return;

            middleContent.Measure(size);
            leftContent.Width = Math.Max(0, size.Width / 2 - (middleContent.DesiredSize.Width) / 2);
        }
    }
}

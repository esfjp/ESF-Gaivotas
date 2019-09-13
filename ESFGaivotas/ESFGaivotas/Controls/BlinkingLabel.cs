using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ESFGaivotas.Controls
{
    public class BlinkingLabel : Label
    {

        #region Bindable Properties

        public bool IsBlinking
        {
            get { return (bool)GetValue(IsBlinkingProperty); }
            set { SetValue(IsBlinkingProperty, value); }
        }
        public static readonly BindableProperty IsBlinkingProperty = BindableProperty.Create("IsBlinking", typeof(bool), typeof(BlinkingLabel), default(bool), propertyChanged: OnBlinkingChanged);

        #endregion

        #region Property Changed Events

        private static void OnBlinkingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (BlinkingLabel)bindable;

            if (control.IsBlinking) control.Animate();
            else control.CancelAnimation();
        }

        public void Animate()
        {
            var animation = new Animation();

            var fadeIn = new Animation(callback: o => this.Opacity = o,
                                                      start: 1D,
                                                      end: 0.3D,
                                                      easing: Easing.Linear);

            var fadeOut = new Animation(callback: o => this.Opacity = o,
                                                       start: 0.3D,
                                                       end: 1D,
                                                       easing: Easing.Linear);

            animation.Add(0.0D, 0.5D, fadeIn);
            animation.Add(0.5D, 1.0D, fadeOut);

            animation.Commit(this, nameof(BlinkingLabel), length: 1000, repeat: () => true);
        }

        public void CancelAnimation()
        {
            ViewExtensions.CancelAnimations(this);
        }

        #endregion

    }
}

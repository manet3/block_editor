using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimpleBlockEditor
{
    public struct MouseArguments
    {
        public object Sender;
        public EventArgs Args;
    }

    public interface IGetProperty
    {
        DependencyProperty CommandProperty { get; }
    }

    public class MouseMove: IGetProperty
    {
        public static readonly DependencyProperty MouseCommandProperty = DependencyProperty.RegisterAttached("MouseCommand",
                    typeof(ICommand), 
                    typeof(MouseMove), 
                    new FrameworkPropertyMetadata(new MouseMoveHandler(new MouseMove()).OnCommandChanged));//called before static constructor

        public static ICommand GetMouseCommand(DependencyObject ob) => (ICommand)ob.GetValue(MouseCommandProperty);

        public static void SetMouseCommand(DependencyObject ob, ICommand value) => ob.SetValue(MouseCommandProperty, value);

        public DependencyProperty CommandProperty { get => MouseCommandProperty; }
    }

    public class MouseUp : IGetProperty
    {
        public static readonly DependencyProperty MouseCommandProperty = DependencyProperty.RegisterAttached("MouseCommand",
                    typeof(ICommand), 
                    typeof(MouseUp), 
                    new FrameworkPropertyMetadata(new MouseUpHandler(new MouseUp()).OnCommandChanged));

        public DependencyProperty CommandProperty { get => MouseCommandProperty; }

        public static ICommand GetMouseCommand(DependencyObject ob) => (ICommand)ob.GetValue(MouseCommandProperty);

        public static void SetMouseCommand(DependencyObject ob, ICommand value) => ob.SetValue(MouseCommandProperty, value);

    }

    public class MouseDown: IGetProperty
    {
        public static readonly DependencyProperty MouseCommandProperty = DependencyProperty.RegisterAttached("MouseCommand",
                    typeof(ICommand), 
                    typeof(MouseDown), 
                    new FrameworkPropertyMetadata(new MouseDownHandler(new MouseDown()).OnCommandChanged));

        public DependencyProperty CommandProperty { get => MouseCommandProperty; }

        public static ICommand GetMouseCommand(DependencyObject ob) => (ICommand)ob.GetValue(MouseCommandProperty);

        public static void SetMouseCommand(DependencyObject ob, ICommand value) => ob.SetValue(MouseCommandProperty, value);

    }

    public abstract class MouseHandler
    {      
        private IGetProperty HandledObject;

        public abstract void AddHandler(FrameworkElement e);

        protected MouseHandler(IGetProperty h) => HandledObject = h; 

        public void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement el)
                AddHandler(el);
        }

        protected void MouseHandle(object sender, EventArgs e) => 
            (((DependencyObject)sender).GetValue(HandledObject.CommandProperty) 
            as ICommand)
            ?.Execute(new MouseArguments { Sender = sender, Args = e});
    }

    #region mouseHandler derivatives

    public class MouseUpHandler: MouseHandler
    {
        public MouseUpHandler(IGetProperty h):base(h) { }

        public override void AddHandler(FrameworkElement e)
        {
            e.MouseUp += MouseHandle;
        }
    }

    public class MouseDownHandler : MouseHandler
    {
        public MouseDownHandler(IGetProperty h):base(h) { }

        public override void AddHandler(FrameworkElement e)
        {
            e.MouseDown += MouseHandle;
        }
    }

    public class MouseMoveHandler : MouseHandler
    {
        public MouseMoveHandler(IGetProperty h):base(h) { }

        public override void AddHandler(FrameworkElement e)
        {
            e.MouseMove += MouseHandle;
        }
    }
#endregion
}

using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf.SharpDX;
using Microsoft.Xaml.Behaviors.Core;

namespace CSharpStudy.Render3D.Controls
{
    public partial class Scene3DControl
    {
        private const int AnimationTimeMs = 200;

        public static readonly DependencyProperty ShowCoordinateSystemProperty = Viewport3DX.ShowCoordinateSystemProperty.AddOwner(
            typeof(Scene3DControl), new PropertyMetadata(true));

        public static readonly DependencyProperty AntiRollZProperty = DependencyProperty.Register(
            nameof(AntiRollZ), typeof(bool), typeof(Scene3DControl), new PropertyMetadata(false, OnAntiRollZChanged));

        public static readonly DependencyProperty BackgroundColorProperty = Viewport3DX.BackgroundColorProperty.AddOwner(
            typeof(Scene3DControl), new PropertyMetadata(Colors.Black));

        public Scene3DControl()
        {
            InitializeComponent();

            ViewAllCommand = new ActionCommand(() => Viewport.ZoomExtents());
            SetHomeViewCommand = new ActionCommand(() => Viewport.Camera.CopyTo(Viewport.DefaultCamera));
            ReturnHomeViewCommand = new ActionCommand(() =>
            {
                var homeCamera = Viewport.DefaultCamera;
                Viewport.Camera.AnimateTo(homeCamera.Position, homeCamera.LookDirection, homeCamera.UpDirection, AnimationTimeMs);
            });
            TopViewCommand = new ActionCommand(() => ViewportCommands.TopView.Execute(null, Viewport));
            BottomViewCommand = new ActionCommand(() => ViewportCommands.BottomView.Execute(null, Viewport));
            WestViewCommand = new ActionCommand(() => ViewportCommands.BackView.Execute(null, Viewport));
            EastViewCommand = new ActionCommand(() => ViewportCommands.FrontView.Execute(null, Viewport));
            SouthViewCommand = new ActionCommand(() => ViewportCommands.LeftView.Execute(null, Viewport));
            NorthViewCommand = new ActionCommand(() => ViewportCommands.RightView.Execute(null, Viewport));
        }

        public ICommand ViewAllCommand { get; }

        public ICommand SetHomeViewCommand { get; }

        public ICommand ReturnHomeViewCommand { get; }

        public ICommand TopViewCommand { get; }

        public ICommand BottomViewCommand { get; }

        public ICommand WestViewCommand { get; }

        public ICommand EastViewCommand { get; }

        public ICommand SouthViewCommand { get; }

        public ICommand NorthViewCommand { get; }

        public bool ShowCoordinateSystem
        {
            get => (bool) GetValue(ShowCoordinateSystemProperty);
            set => SetValue(ShowCoordinateSystemProperty, value);
        }

        public bool AntiRollZ
        {
            get => (bool) GetValue(AntiRollZProperty);
            set => SetValue(AntiRollZProperty, value);
        }

        public Color BackgroundColor
        {
            get => (Color) GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        private static void OnAntiRollZChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewport = ((Scene3DControl) d).Viewport;
            var antiRollZ = (bool) e.NewValue;

            if (antiRollZ)
            {
                viewport.CameraRotationMode = CameraRotationMode.Turntable;
                var target = viewport.CameraCore.Target.ToPoint3D();
                var look = viewport.Camera.LookDirection;
                viewport.Camera.LookAt(target, look, new Vector3D(0, 0, 1), AnimationTimeMs);
            }
            else
            {
                viewport.CameraRotationMode = CameraRotationMode.Trackball;
            }
        }
    }
}
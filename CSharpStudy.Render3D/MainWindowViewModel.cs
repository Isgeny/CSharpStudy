using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using CSharpStudy.Render3D.Geometry;
using HelixToolkit.Wpf.SharpDX;
using Prism.Commands;
using Prism.Mvvm;
using SharpDX;
using Camera = HelixToolkit.Wpf.SharpDX.Camera;
using Color = System.Drawing.Color;
using PerspectiveCamera = HelixToolkit.Wpf.SharpDX.PerspectiveCamera;

namespace CSharpStudy.Render3D
{
    public class MainWindowViewModel : BindableBase
    {
        private Camera _homeCamera;
        private Camera _camera;

        private ICommand _setHomeCameraCommand;

        public MainWindowViewModel()
        {
            MeshModels = new ObservableCollection<IMeshViewModel>
            {
                new Marker3DViewModel(Vector3.Zero, 1, Color.Red),
                new Marker3DViewModel(new Vector3(5, 5, 5), 2, Color.Green),
                new Marker3DViewModel(new Vector3(-10, -10, -10), 4, Color.Blue),
            };

            HomeCamera = new PerspectiveCamera
            {
                Position = new Point3D(0, -100, 0),
                LookDirection = new Vector3D(0, 100, 0),
                UpDirection = new Vector3D(0, 0, 1)
            };

            Camera = new PerspectiveCamera();
            HomeCamera.CopyTo(Camera);
        }

        public ICommand SetHomeCameraCommand => _setHomeCameraCommand ??= new DelegateCommand(() => Camera.CopyTo(HomeCamera));

        public ObservableCollection<IMeshViewModel> MeshModels { get; }

        public Camera HomeCamera
        {
            get => _homeCamera;
            set => SetProperty(ref _homeCamera, value);
        }

        public Camera Camera
        {
            get => _camera;
            set => SetProperty(ref _camera, value);
        }
    }
}
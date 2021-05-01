using System.Collections.ObjectModel;
using CSharpStudy.Render3D.Geometry;
using Prism.Mvvm;
using SharpDX;
using Camera = HelixToolkit.Wpf.SharpDX.Camera;
using Color = System.Drawing.Color;

namespace CSharpStudy.Render3D
{
    public class MainWindowViewModel : BindableBase
    {
        private Camera _homeCamera;
        private Camera _camera;

        public MainWindowViewModel()
        {
            MeshModels = new ObservableCollection<IMeshViewModel>
            {
                new Marker3DViewModel(Vector3.Zero, 1, Color.Red),
                new Marker3DViewModel(new Vector3(5, 5, 5), 2, Color.Green),
                new Marker3DViewModel(new Vector3(-10, -10, -10), 4, Color.Blue),
            };
        }

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
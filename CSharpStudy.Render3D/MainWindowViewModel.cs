using System.Collections.ObjectModel;
using CSharpStudy.Render3D.Geometry;
using SharpDX;
using Color = System.Drawing.Color;

namespace CSharpStudy.Render3D
{
    public class MainWindowViewModel : BindableBase
    {
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
    }
}
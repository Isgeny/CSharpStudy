using System.ComponentModel;
using HelixToolkit.Wpf.SharpDX;

namespace CSharpStudy.Render3D.Geometry
{
    public interface IMeshViewModel : INotifyPropertyChanged
    {
        Geometry3D Geometry { get; }

        Material Material { get; }
    }
}
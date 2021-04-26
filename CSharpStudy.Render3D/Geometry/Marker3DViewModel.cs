using CSharpStudy.Render3D.Utils;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using Color = System.Drawing.Color;

namespace CSharpStudy.Render3D.Geometry
{
    public class Marker3DViewModel : BindableBase, IMeshViewModel
    {
        public Marker3DViewModel(Vector3 center, double radius, Color color)
        {
            var meshBuilder = new MeshBuilder();
            meshBuilder.AddSphere(center, radius);
            Geometry = meshBuilder.ToMeshGeometry3D();
            Material = new DiffuseMaterial {DiffuseColor = color.ToSharpDxColor()};
        }

        public Geometry3D Geometry { get; }

        public Material Material { get; }
    }
}
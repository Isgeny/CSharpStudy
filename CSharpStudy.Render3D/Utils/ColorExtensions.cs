using SharpDX;
using Color = System.Drawing.Color;

namespace CSharpStudy.Render3D.Utils
{
    public static class ColorExtensions
    {
        public static Color4 ToSharpDxColor(this Color color)
        {
            return new(color.R, color.G, color.B, color.A);
        }
    }
}
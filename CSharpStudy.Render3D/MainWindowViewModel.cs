using System.ComponentModel;
using System.Runtime.CompilerServices;
using HelixToolkit.Wpf.SharpDX;

namespace CSharpStudy.Render3D
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            Camera = new PerspectiveCamera();
            EffectsManager = new DefaultEffectsManager();
        }

        public Camera Camera { get; }

        public EffectsManager EffectsManager { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
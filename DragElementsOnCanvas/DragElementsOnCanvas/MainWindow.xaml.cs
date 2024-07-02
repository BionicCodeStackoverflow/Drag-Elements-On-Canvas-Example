namespace DragElementsOnCanvas
{
  using System.Text;
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Data;
  using System.Windows.Documents;
  using System.Windows.Input;
  using System.Windows.Media;
  using System.Windows.Media.Imaging;
  using System.Windows.Navigation;
  using System.Windows.Shapes;

  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private Point draggedElementOffset;
    private bool canDrag;

    public MainWindow()
    {
      InitializeComponent();
    }

    private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      this.canDrag = false;

      if (e.OriginalSource is not Shape draggedElement)
      {
        return;
      }

      this.canDrag = true;
      var canvas = (Canvas)sender;
      _ = draggedElement.CaptureMouse();
      var currentElementPosition = new Point(Canvas.GetLeft(draggedElement), Canvas.GetTop(draggedElement));
      Point currentMousePosition = e.GetPosition(canvas);
      this.draggedElementOffset = new Point(currentMousePosition.X - currentElementPosition.X, currentMousePosition.Y - currentElementPosition.Y);
    }

    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
      if (!this.canDrag || e.LeftButton is MouseButtonState.Released)
      {
        return;
      }

      var canvas = (Canvas)sender;
      var draggedElement = (Shape)e.OriginalSource;
      Point currentMousePosition = e.GetPosition(canvas);
      Canvas.SetLeft(draggedElement, currentMousePosition.X - this.draggedElementOffset.X);
      Canvas.SetTop(draggedElement, currentMousePosition.Y - this.draggedElementOffset.Y);
    }

    private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      if (!this.canDrag)
      {
        return;
      }

      var draggedElement = (Shape)e.OriginalSource;
      draggedElement.ReleaseMouseCapture();
    }
  }
}
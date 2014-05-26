using System.Windows;
using Subtlee.ViewModel;

namespace Subtlee
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class OverviewWindow : Window
	{
		/// <summary>
		/// Initializes a new instance of the MainWindow class.
		/// </summary>
		public OverviewWindow()
		{
			InitializeComponent();
			Closing += (_s, _e) => ViewModelLocator.Cleanup();

			var presentationWindow = new SubtitlePresentationWindow();
		}
	}
}
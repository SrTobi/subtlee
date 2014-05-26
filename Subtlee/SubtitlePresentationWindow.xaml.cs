using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Subtlee
{
	public partial class SubtitlePresentationWindow : Window
	{
		public SubtitlePresentationWindow()
		{
			InitializeComponent();
		}

		private void OnLeftMouseDown(object _sender, MouseButtonEventArgs _e)
		{
			this.DragMove();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;

			if (button != null)
			{
				button.ContextMenu.IsEnabled = true;
				button.ContextMenu.PlacementTarget = button;
				button.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
				button.ContextMenu.IsOpen = true;
			}
		}
	}
}

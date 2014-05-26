/*
  In App.xaml:
  <Application.Resources>
	  <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Subtlee.ViewModel"
								   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Subtlee.Model;

namespace Subtlee.ViewModel
{
	public class ViewModelLocator
	{
		private readonly SubtitleOverviewViewModel mOverviewViewModel;
		private readonly SubtitlePresentationViewModel mPresentationViewModel;

		public SubtitleOverviewViewModel Overview
		{
			get { return mOverviewViewModel; }
		}

		public SubtitlePresentationViewModel Presenter
		{
			get { return mPresentationViewModel; }
		}


		public ViewModelLocator()
		{
			mOverviewViewModel = new SubtitleOverviewViewModel(this);
			mPresentationViewModel = new SubtitlePresentationViewModel(this);
		}

		public static void Cleanup()
		{
			
		}
	}
}
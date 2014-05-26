using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Subtlee.Model;
using Subtlee.Parser;

namespace Subtlee.ViewModel
{

	public class SubtitleOverviewViewModel : ViewModelBase
	{
		private readonly IEnumerable<ISubtitleParser> mParsers;  
		private readonly ViewModelLocator mLocator;
		private bool mShowOverview = true;

		public bool ShowOverview
		{
			get { return mShowOverview; }
			set
			{
				mShowOverview = value;
				RaisePropertyChanged(() => ShowOverview);
			}
		}

		private readonly RelayCommand mChangeWindowCommand;
		private readonly RelayCommand mOpenFileCommand;

		public RelayCommand OpenWindowCommand { get { return new RelayCommand(_openWindow); } }
		public RelayCommand ToogleWindowCommand { get { return mChangeWindowCommand; } }
		public RelayCommand OpenFileCommand { get { return mOpenFileCommand; } }

		public SubtitleOverviewViewModel(ViewModelLocator _locator)
		{
			mLocator = _locator;

			mParsers = new ISubtitleParser[]
			{
				new SubRipParser()
			};

			// Commands
			mChangeWindowCommand = new RelayCommand(_toogleWindow);
			mOpenFileCommand = new RelayCommand(_openSubtitlesFromFileDialog); 
		}

		private void _openWindow()
		{
			ISubtitleParser parser = new SubRipParser();
			var fs = new FileStream("subtitle.srt",FileMode.Open);
			ISubtitleData data = parser.ParseSubtitle(fs);

			if (data != null)
			{
				ShowOverview = false;
				mLocator.Presenter.SetSubtitles(data);
			}

		}

		private void _toogleWindow()
		{
			ShowOverview = !ShowOverview;
		}

		private void _openSubtitlesFromFileDialog()
		{
			OpenFileDialog fd = new OpenFileDialog();
			fd.Filter = "SubRip files (*.srt)|*.srt|txt files (*.txt)|*.txt|All files (*.*)|*.*";
			fd.FilterIndex = 0;
			fd.RestoreDirectory = true;
			if (fd.ShowDialog() == DialogResult.OK)
			{
				try
				{
					using(var filestream = fd.OpenFile())
					{
						_openSubtitlesFromFile(filestream);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
		}
		private void _openSubtitlesFromFile(Stream file)
		{
			ISubtitleData data = null;
			foreach (var subtitleParser in mParsers)
			{
				data = subtitleParser.ParseSubtitle(file);

				if (data != null)
					break;
			}

			if (data != null)
			{
				ShowOverview = false;
				mLocator.Presenter.SetSubtitles(data);
			}
		}
	}
}
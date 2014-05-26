using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Subtlee.Model;

namespace Subtlee.ViewModel
{
	public class SubtitlePresentationViewModel : ViewModelBase
	{
		private class DummySubtitle : Model.ISubtitleData
		{
			private readonly TimeSpan mLength = new TimeSpan(1);

			public string Name { get { return "<No Subtitle>"; } }
			public string Format { get { return "None"; } }
			public TimeSpan Length { get { return mLength; } }
			public ISubtitlePassage PassageAt(TimeSpan _time)
			{
				return null;
			}
		}

		private readonly RelayCommand mResetCommand;

		private readonly DispatcherTimer mTimer;
		private ViewModelLocator mLocator;
		private readonly ISubtitleData mDummyData = new DummySubtitle();
		private ISubtitleData mCurrentSubtitle;
		private bool mPlaying = false;
		private ISubtitlePassage mCurrentPassage;
		private DateTime mLastTime;
		private TimeSpan mCurrentPosition = new TimeSpan();
		private bool mShowControls = true;

		public string CurrentText
		{
			get { return mCurrentPassage != null? mCurrentPassage.Text : ""; }
		}
		public ISubtitleData CurrentSubtitle
		{
			get { return mCurrentSubtitle ?? mDummyData; }
		}

		public float Opacity
		{
			get { return 0.7f; }
		}

		public TimeSpan CurrentPosition
		{
			get { return mCurrentPosition; }
			set
			{
				if(mCurrentPosition.Equals(value))
					return;

				mCurrentPosition = mCurrentSubtitle == null ? TimeSpan.Zero : value;

				ISubtitlePassage newPassage = CurrentSubtitle.PassageAt(value);

				if (newPassage != mCurrentPassage)
				{
					mCurrentPassage = newPassage;
					RaisePropertyChanged(() => CurrentText);
				}

				RaisePropertyChanged(() => CurrentPosition);
			}
		}

		public bool Playing
		{
			get { return mPlaying; }
			set
			{
				if (value && mCurrentSubtitle == null)
					return;

				if (mPlaying == value)
					return;

				mPlaying = value;
				
				if (mPlaying)
				{
					mLastTime = DateTime.Now;
					mTimer.Start();
				}
				else
				{
					_updatePosition();
					mTimer.Stop();
				}
				RaisePropertyChanged(() => Playing);
			}
		}

		public RelayCommand ResetCommand
		{
			get { return mResetCommand; }
		}

		public bool ShowControls
		{
			get { return mShowControls || !Properties.Settings.Default.Presenter_HideControls; }
			set
			{
				mShowControls = value;
				RaisePropertyChanged(() => ShowControls);
			}
		}

		public SubtitlePresentationViewModel(ViewModelLocator _locator)
		{
			mLocator = _locator;

			mTimer = new DispatcherTimer();
			mTimer.Tick += new EventHandler(_onTimer);
			mTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);

			// Commands
			mResetCommand = new RelayCommand(_resetSubtitle);
		}

		public void SetSubtitles(ISubtitleData _data)
		{

			mCurrentSubtitle = _data;

			RaisePropertyChanged(() => CurrentSubtitle);

			_resetSubtitle();
		}

		private void _resetSubtitle()
		{
			Playing = false;
			CurrentPosition = TimeSpan.Zero;
		}

		private void _onTimer(object _sender, EventArgs _e)
		{
			_updatePosition();
		}

		private void _updatePosition()
		{
			CurrentPosition += (DateTime.Now - mLastTime);
			mLastTime = DateTime.Now;
		}
	}
}
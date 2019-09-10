using App1.Common;
using Bible_Giant.BusinessModel;
using Bible_Giant.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Bible_Giant
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuizPage : Page
    {
        private readonly NavigationHelper navigationHelper;
        private readonly ObservableClass defaultViewModel = new ObservableClass();
        private static QuestionClass QuestionGroup;
        public QuizPage()
        {
            this.InitializeComponent();
            HideStatusBar();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            this.DataContext = this.DefaultViewModel;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }
        private async void HideStatusBar()
        {
            StatusBar statusBar = StatusBar.GetForCurrentView();
            await statusBar.HideAsync();
        }
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            throw new NotImplementedException();
        }
        public ObservableClass DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }
        async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            QuestionGroup = await BibleGiantDataSource.GetGroupAsync();
            this.DefaultViewModel.Question = QuestionGroup;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        private async void Option_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionGroup == null)
                QuestionGroup = await BibleGiantDataSource.GetGroupAsync();
            var matches = QuestionGroup.Options.Where((option) => option.IsAnswer.Equals("True"));
            if ((sender as Button).Content != null)
            {
                if ((sender as Button).Content.ToString() == matches.First().Option)
                {
                    if (BibleGiantDataSource.currentLevel != Level.Giant)
                    {
                        BibleGiantDataSource.levelDictionary[BibleGiantDataSource.currentLevel]++;
                        BibleGiantDataSource.currentLevel++;
                        QuestionGroup = await BibleGiantDataSource.GetGroupAsync();
                        this.DefaultViewModel.Question = QuestionGroup;
                    }
                }
            }
        }
        private void PhoneAFriendbtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.DefaultViewModel.FafIsActive)
            {
                FoneAFriend faf = new FoneAFriend(QuestionGroup.Options.IndexOf(QuestionGroup.Options.Where(x => x.IsAnswer.Equals("True")).First()));
                this.DefaultViewModel.FoneAFriendOption = faf.FafOption;
                this.DefaultViewModel.LifeLine = LifeLine.FoneAFriend;
                this.DefaultViewModel.FafIsActive = false;
            }
        }

        private void AskdAudience_Click(object sender, RoutedEventArgs e)
        {
            if (this.DefaultViewModel.AsdIsActive)
            {
                Audience askdaudience = new Audience(QuestionGroup.Options.IndexOf(QuestionGroup.Options.Where(x => x.IsAnswer.Equals("True")).First()));
                this.DefaultViewModel.AudienceGrades = new List<int>() { askdaudience.A, askdaudience.B, askdaudience.C, askdaudience.D };
                this.DefaultViewModel.LifeLine = LifeLine.AskDAudience;
                this.DefaultViewModel.AsdIsActive = false;
            }
        }

        private async void FiftyFifty_Click(object sender, RoutedEventArgs e)
        {
            if (this.DefaultViewModel.FffIsActive)
            {
                if (QuestionGroup == null)
                    QuestionGroup = await BibleGiantDataSource.GetGroupAsync();
                var matches = QuestionGroup.Options.Where((option) => !(option.IsAnswer.Equals("True")));
                List<int> nums = new List<int>(3) { 0, 1, 2 };
                nums.Shuffle();
                matches.ElementAt(nums[0]).Option = ""; matches.ElementAt(nums[1]).Option = "";
                this.DefaultViewModel.Question = QuestionGroup;
                this.DefaultViewModel.FffIsActive = false;
            }
        }

        private void foneafriend_ok_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DefaultViewModel.LifeLine = LifeLine.None;
        }

        private void askdaudience_ok_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DefaultViewModel.LifeLine = LifeLine.None;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bible_Giant.DataModel
{
    public class ObservableClass : INotifyPropertyChanged
    {
        public ObservableClass()
        {
            FffIsActive = true;
            FafIsActive = true;
            AsdIsActive = true;
        }
        private QuestionClass question = new QuestionClass();
        public QuestionClass Question
        {
            get
            {
                return this.question;
            }
            set
            {
                if (value != this.question)
                {
                    this.question = value;
                    NotifyPropertyChanged("Question");
                }
            }
        }

        private List<int> audienceGrades = new List<int>() { 0, 0, 0, 0 };
        public List<int> AudienceGrades
        {
            get
            {
                return this.audienceGrades;
            }
            set
            {
                if (value != this.audienceGrades)
                {
                    this.audienceGrades = value;
                    NotifyPropertyChanged("AudienceGrades");
                }
            }
        }

        private LifeLine lifeLine = LifeLine.None;
        public LifeLine LifeLine
        {
            get
            {
                return this.lifeLine;
            }
            set
            {
                if (value != this.lifeLine)
                {
                    this.lifeLine = value;
                    NotifyPropertyChanged("LifeLine");
                }
            }
        }

        private char foneAFriendOption = 'A';
        public char FoneAFriendOption
        {
            get
            {
                return this.foneAFriendOption;
            }
            set
            {
                if (value != this.foneAFriendOption)
                {
                    this.foneAFriendOption = value;
                    NotifyPropertyChanged("FoneAFriendOption");
                }
            }
        }

        private bool fffIsActive = false;
        public bool FffIsActive
        {
            get
            {
                return this.fffIsActive;
            }
            set
            {
                if (value != this.fffIsActive)
                {
                    this.fffIsActive = value;
                    NotifyPropertyChanged("FffIsActive");
                }
            }
        }

        private bool fafIsActive = false;
        public bool FafIsActive
        {
            get
            {
                return this.fafIsActive;
            }
            set
            {
                if (value != this.fafIsActive)
                {
                    this.fafIsActive = value;
                    NotifyPropertyChanged("FafIsActive");
                }
            }
        }

        private bool asdIsActive = false;
        public bool AsdIsActive
        {
            get
            {
                return this.asdIsActive;
            }
            set
            {
                if (value != this.asdIsActive)
                {
                    this.asdIsActive = value;
                    NotifyPropertyChanged("AsdIsActive");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

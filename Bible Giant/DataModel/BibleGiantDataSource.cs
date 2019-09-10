using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace Bible_Giant.DataModel
{
    public class Options : INotifyPropertyChanged

    {
        public Options(String optionId, String isAnswer, String option)
        {
            this.OptionId = optionId;
            this.IsAnswer = isAnswer;
            this.Option = option;
        }
        private string option = string.Empty;

        public string OptionId { get; private set; }
        public string IsAnswer { get; private set; }
        public string Option
        {
            get
            {
                return this.option;
            }
            set
            {
                if (value != this.option)
                {
                    this.option = value;
                    NotifyPropertyChanged("Option");
                }
            }
        }


        public override string ToString()
        {
            return this.Option;
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

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class QuestionClass
    {
        public QuestionClass(String questionId, String level, String question)
        {
            this.QuestionId = questionId;
            this.Level = level;
            this.Question = question;
            this.Options = new ObservableCollection<Options>();
        }

        public QuestionClass()
        {
            
        }

        public string QuestionId { get; private set; }
        public string Level { get; private set; }
        public string Question { get; private set; }
        public ObservableCollection<Options> Options { get; private set; }

        public override string ToString()
        {
            return this.Question;
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    /// 

    public enum Level
    {
        Starter, Amateur, Senior, Enthusiast, Professional, Expert, Leader, Veteran, Master, Giant
    }
    public enum LifeLine
    {
        FiftyFifty, FoneAFriend, AskDAudience, None
    }
    public class LevelClass
    {
        public int LevelQuestionNumber { get; set; }
    }
    public sealed class BibleGiantDataSource
    {
        private static BibleGiantDataSource _bibleGiantDataSource = new BibleGiantDataSource();

        private ObservableCollection<QuestionClass> _questions = new ObservableCollection<QuestionClass>();

        public static Level currentLevel = new Level();

        public static Dictionary<Level,int> levelDictionary = new Dictionary<Level,int>(); 
        public ObservableCollection<QuestionClass> Questions
        {
            get { return this._questions; }
        }

        public static async Task<IEnumerable<QuestionClass>> GetGroupsAsync()
        {
            await _bibleGiantDataSource.GetSampleDataAsync();
            
            return _bibleGiantDataSource.Questions;
        }

        public static async Task<QuestionClass> GetGroupAsync()
        {
            await _bibleGiantDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var level = currentLevel.ToString();
            var matches = _bibleGiantDataSource.Questions.Where((questions) => questions.Level.Equals(level));
            if (matches.Count() > 0)
            {
                try
                {
                    return matches.ToList()[levelDictionary[currentLevel]];
                }
                catch
                {
                    levelDictionary[currentLevel] = 0;
                    return matches.First();
                }
            }
            return null;
        }

        private async Task GetSampleDataAsync()
        {
            if (this._questions.Count != 0)
                return;

            Uri dataUri = new Uri("ms-appx:///DataModel/BibleGiantData.json");
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Questions"].GetArray();

            foreach (JsonValue groupValue in jsonArray)
            {
                JsonObject groupObject = groupValue.GetObject();
                QuestionClass questions = new QuestionClass(groupObject["QuestionId"].GetString(),
                                                            groupObject["Level"].GetString(),
                                                            groupObject["Question"].GetString());

                foreach (JsonValue option in groupObject["Options"].GetArray())
                {
                    JsonObject itemObject = option.GetObject();
                    questions.Options.Add(new Options(itemObject["OptionId"].GetString(),
                                                       itemObject["IsAnswer"].GetString(),
                                                       itemObject["Option"].GetString()));
                }
                questions.Options.Shuffle();

                this.Questions.Add(questions);
                
            }
            this.Questions.Shuffle();
        }
    }
}

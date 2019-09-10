using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bible_Giant.BusinessModel
{
    public class FoneAFriend
    {
        List<int> fRandom;
        private int CorrectIndex;
        private List<char> options = new List<char> { 'A', 'B', 'C', 'D' };
        public char FafOption { get; set; }
        public FoneAFriend(int correctIndex)
        {
            this.CorrectIndex = correctIndex;
            fRandom = new List<int>(9) { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            AskFriend(FriendIsCorrect());
        }
        private bool FriendIsCorrect()
        {
            fRandom.Shuffle();
            if (fRandom[0] == 9)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void AskFriend(bool isCorrect)
        {
            char correctAnswer = options[CorrectIndex];
            if (isCorrect)
            {
                FafOption = correctAnswer;
            }
            else
            {
                do 
                {
                    options.Shuffle();
                }
                while (options[0] == correctAnswer);
                FafOption = options[0];
            }
        }
    }
}

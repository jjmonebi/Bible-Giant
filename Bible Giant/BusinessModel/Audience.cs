using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bible_Giant.BusinessModel
{
    public class Audience
    {
        private List<int> audienceRandom;
        private int CorrectIndex;
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }
        public Audience(int correctIndex)
        {
            this.CorrectIndex = correctIndex;
            audienceRandom = new List<int>(9) { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ProcessAudience(AudienceIsCorrect());
            A = bars[0]; B = bars[1]; C = bars[2]; D = bars[3];
        }

        private bool AudienceIsCorrect()
        {
            audienceRandom.Shuffle();
            if (audienceRandom[0] == 9)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private int[] bars = new int[4] { 0, 0, 0, 0 };
        void ProcessAudience(bool IsCorrect)
        {
            int total = 0;
            bool correctTest;
            do
            {
                for (int i = 0; i < bars.Length; i++)
                {
                    bars[i] = ThreadSafeRandom.ThisThreadsRandom.Next(1, 100);
                    //total += bars[i];
                }
                total = bars.Sum();
            }
            while (total != 100);
            do
            {
                bars.Shuffle();
                correctTest = IsCorrect ? (bars[CorrectIndex] == bars.Max()) : (bars[CorrectIndex] != bars.Max());
            } 
            while (correctTest == false);
        }
    }
}

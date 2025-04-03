using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProdAvanz_Semana1
{
    internal class Riddle : GameEntity
    {
        public string[] Options { get; private set; }
        public int CorrectAnswerIndex { get; private set; }
        public Key? Reward { get; private set; }
        public bool IsSolved { get; private set; }

        public Riddle(string name, string description, string[] options, int correctAnswerIndex, Key? reward)
            : base(name, description)
        {
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
            Reward = reward;
            IsSolved = false;
        }

        public override string Interact()
        {
            if (IsSolved)
            {
                return "Este acertijo ya fue resuelto.";
            }
            return Description;
        }

        public bool CheckAnswer(int answerIndex)
        {
            bool isCorrect = answerIndex == CorrectAnswerIndex;
            if (isCorrect)
            {
                IsSolved = true;
            }
            return isCorrect;
        }
    }
}

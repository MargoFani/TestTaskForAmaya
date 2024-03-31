using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "New GameLevelData", menuName = "Game Level Data", order = 10)]
    public class GameLevelData : ScriptableObject
    {
        [SerializeField]
        private int _identifier;
        [SerializeField]
        private int _answerVariantsCountInRow;
        [SerializeField]
        private int _answerVariantsCountInColumn;
        public int Identifier => _identifier;
        public int RowCount => _answerVariantsCountInRow;
        public int ColumnCount => _answerVariantsCountInColumn;
    }
}

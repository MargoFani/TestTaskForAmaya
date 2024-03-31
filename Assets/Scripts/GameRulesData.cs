using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "New GameRulesData", menuName = "Game Rules Data", order = 10)]
    public class GameRulesData : ScriptableObject
    {
        [SerializeField]
        private GameLevelData[] _levelsData;

        public GameLevelData[] GameLevelsData => _levelsData;
    }
}

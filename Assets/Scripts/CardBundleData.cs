using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName  = "New CardBundleData", menuName = "Card Bundle Data", order = 10)]
    public class CardBundleData : ScriptableObject
    {
        [SerializeField]
        private List<CardData> _cardData;

        public List<CardData> CardData => _cardData;
    }
}

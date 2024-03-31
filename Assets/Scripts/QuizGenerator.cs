using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Profiling;
using UnityEngine;
using VContainer;
using Random = System.Random;

namespace Assets.Scripts
{
    public class QuizGenerator
    {
        private List<CardData> _cardsOrderForTasks;
        private List<CardData> _cardsOrderForAnswerVariants;
        private CardBundleData _cardBundleData;
        private GameRulesData _gameRulesData;
        private List<CardData> _levelAnswerCards;
        private int _rightAnswerId;
        public List<CardData> AllAnswerVariantCards => _cardsOrderForAnswerVariants;
        public List<CardData> LevelAnswerCards => _levelAnswerCards;        
        public int RightAnswerId => _rightAnswerId;
        public QuizGenerator(CardBundleData cardBundleData, GameRulesData gameRulesData)
        {
            _cardBundleData = cardBundleData;
            _gameRulesData = gameRulesData;
            Init();
        }
        public void Init()
        {
            _cardsOrderForTasks = new List<CardData>();
            _cardsOrderForAnswerVariants = new List<CardData>();
            CopyCardBundleDataTo(_cardsOrderForTasks);

            CopyCardBundleDataTo(_cardsOrderForAnswerVariants);

            GenerateCardsOrderForTasks();
        }
        private void GenerateCardsOrderForTasks()
        {
            Shuffle(_cardsOrderForTasks);
            Debug.Log("GenerateCardsOrderForTasks");
            foreach(CardData data in _cardsOrderForTasks)
            {
                Debug.Log("cardData " + data.Identifier);
            }
        }

        public string GetRightAnswerName(int levelId)
        {
            return _cardsOrderForTasks[levelId].Identifier;
        }

        public void GenerateAnswers(int levelId)
        {
            _levelAnswerCards = new List<CardData>();
            int answerVariantsCount = _gameRulesData.GameLevelsData[levelId].ColumnCount * _gameRulesData.GameLevelsData[levelId].RowCount;
            Debug.Log("answerVariantsCount " + answerVariantsCount);
            Shuffle(_cardsOrderForAnswerVariants);

            for(int i = 0; i < answerVariantsCount; i++)
            {
                int n = 0;
                if (_cardsOrderForAnswerVariants[i + n] != _cardsOrderForTasks[levelId])
                {
                    _levelAnswerCards.Add(_cardsOrderForAnswerVariants[i + n]);
                }
                else
                {
                    n++;
                    _levelAnswerCards.Add(_cardsOrderForAnswerVariants[i + n]);
                }
            }

            GenerateRightAnswerIdInAnswerCards(answerVariantsCount);
            _levelAnswerCards[_rightAnswerId] = _cardsOrderForTasks[levelId];

        }
        private void GenerateRightAnswerIdInAnswerCards(int variantsCount)
        {
            Random random = new Random();
            int rightAnsweIndex = random.Next(variantsCount);
            _rightAnswerId = rightAnsweIndex;
        }
        private List<CardData> Shuffle(List<CardData> list)
        {
            Random random = new Random();
            for (int i = list.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                CardData tmp = list[j];
                list[j] = list[i];
                list[i] = tmp;
            }

            return list;
        }
        private List<CardData> CopyCardBundleDataTo(List<CardData> cards)
        {
            foreach (var element in _cardBundleData.CardData)
            {
                cards.Add(element);
            }
            return cards;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CardGridCreator : MonoBehaviour
    {
        [SerializeField]
        private Card _elementPrefab;
        [SerializeField]
        private GridLayoutGroup _gridLayoutGroup;
        [SerializeField]
        private Text _taskText;
        [SerializeField]
        private UiAnimationManager _uiAnimationManager;
        private GameRulesData _gameRulesData;
        private List<Card> _cards;

        public UnityEvent OnRightAnswerSelected;

        public void InitGrid(GameRulesData gameRulesData)
        {
            _gameRulesData = gameRulesData;
            _uiAnimationManager.OnDotTweenComplete.AddListener(OnRightAnswerAfterAnimation);
        }

        public void SetUpCards(List<CardData> cards, int levelId, string task, int rightAnswerId)
        {
            ClearGridFromCards();
            SetTaskText(task);

            _cards = new List<Card>();
            _gridLayoutGroup.constraintCount = _gameRulesData.GameLevelsData[levelId].ColumnCount;
  
            Debug.Log("cards.Count " + cards.Count);

            for (int i = 0; i < cards.Count; i++)
            {
                var newElement = Instantiate(_elementPrefab, transform);
                _cards.Add(newElement);
                if (rightAnswerId == i)
                {
                    newElement.Init(cards[i], OnRightAnswer);
                }
                else
                    newElement.Init(cards[i], OnWrongAnswer);
            }

        }
        public void PlayStartAnimation()
        {
            _uiAnimationManager.TextFadeIn(_taskText);
            _uiAnimationManager.StartCardsAnimation(_gridLayoutGroup);
        }
        private void SetTaskText(string text)
        {
            _taskText.text = "Find " + text;
        }

        public void OnRightAnswer(Card card)
        {
            _uiAnimationManager.CardRightAnswerAnimation(card);
        }
        public void OnWrongAnswer(Card card)
        {
            _uiAnimationManager.CardWrongAnswerAnimation(card);
        }

        public void OnRightAnswerAfterAnimation()
        {
            OnRightAnswerSelected?.Invoke();
        }

        private void ClearGridFromCards()
        {
            foreach (Transform child in _gridLayoutGroup.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}

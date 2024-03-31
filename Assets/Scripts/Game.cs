using Assets.Scripts;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

public class Game : MonoBehaviour
{
    [SerializeField]
    private CardGridCreator _gridCreator;
    [SerializeField]
    private CardBundleData _cardBundleData;
    [SerializeField]
    private GameRulesData _gameRulesData;
    [SerializeField]
    private CanvasGroup _onEndMenu;

    private QuizGenerator _quizGenerator;
    private int level;
    void Awake()
    {
        _gridCreator.OnRightAnswerSelected.AddListener(OnRightAnswerSelected);
    }
    void Start()
    {
        level = 0;
        _gridCreator.InitGrid(_gameRulesData);
        SetUpLevel(level);
        _gridCreator.PlayStartAnimation();
    }

    private void OnRightAnswerSelected()
    {
        Debug.Log("_gameRulesData.GameLevelsData.Length " + _gameRulesData.GameLevelsData.Length);
        if (level < _gameRulesData.GameLevelsData.Length - 1)
        {
            level++;
            SetUpLevel(level);
        }
        else {
            _onEndMenu.gameObject.SetActive(true);
            _onEndMenu.DOFade(1f, 1f);
            Debug.Log("конец игры");
        }
        //почистить грид
        //сгенерировать новые ответы
        
    }

    private void SetUpLevel(int level)
    {
        _quizGenerator = new QuizGenerator(_cardBundleData, _gameRulesData);
        _quizGenerator.GenerateAnswers(level);

        Debug.Log("_rightAnswerId " + _quizGenerator.RightAnswerId);
        Debug.Log("answerCards: " + _quizGenerator.LevelAnswerCards.Count);

        foreach (var ans in _quizGenerator.LevelAnswerCards)
        {
            Debug.Log("card " + ans.Identifier);
        }        
        _gridCreator.SetUpCards(_quizGenerator.LevelAnswerCards, level, _quizGenerator.GetRightAnswerName(level), _quizGenerator.RightAnswerId);
    }

    public void OnStartAgain()
    {
        _onEndMenu.DOFade(0f, 1f);
        RestartGame();
    }

    public void RestartGame()
    {
        _onEndMenu.gameObject.SetActive(false);
        level = 0;
        _quizGenerator = new QuizGenerator(_cardBundleData, _gameRulesData);

        SetUpLevel(level);
    }
}


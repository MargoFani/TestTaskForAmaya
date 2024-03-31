using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    private CardData _cardData;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Image _corretAnswerImage;
    public Image CorretAnswerImage => _corretAnswerImage;
    private Action<Card> _onClickAction;
    public void Init(CardData cardData, Action<Card> action)
    {
        if (_image) _image.sprite = cardData.Sprite;
        _cardData = cardData;
        _onClickAction = action;
    }
    public void OnMouseUp()
    {
        _onClickAction.Invoke(this);
    }


}

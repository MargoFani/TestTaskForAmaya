using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace Assets.Scripts
{
    public class UiAnimationManager : MonoBehaviour
    {
        private GridLayoutGroup _gridLayoutGroup;
        private float fadeTime = 1f;

        public UnityEvent OnDotTweenComplete;
        public void TextFadeIn(UnityEngine.UI.Text text)
        {
            text.rectTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
            text.rectTransform.DOAnchorPos(new Vector2(0f, -116f), fadeTime, false).SetEase(Ease.OutElastic);
            text.DOFade(1, fadeTime);
        }
        IEnumerator ItemsAnimation()
        {
            foreach (Transform child in _gridLayoutGroup.transform)
            {
                child.localScale = Vector3.zero;
                Debug.Log("child.localScale");
            }
            foreach (Transform child in _gridLayoutGroup.transform)
            {
                child.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
                yield return new WaitForSeconds(0.25f);
            }
        }

        public void StartCardsAnimation(GridLayoutGroup gridLayoutGroup)
        {
            _gridLayoutGroup = gridLayoutGroup;
            StartCoroutine("ItemsAnimation");
        }

        public void CardRightAnswerAnimation(Card card)
        {
            card.transform.localScale = Vector3.zero;
            card.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce).OnComplete(()=> CardCorrectAnswerImageAnimation(card));
        }
        public void CardCorrectAnswerImageAnimation(Card card)
        {
            card.CorretAnswerImage.gameObject.SetActive(true);
            card.CorretAnswerImage.transform.localScale = Vector3.zero;
            card.CorretAnswerImage.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce).OnComplete(() => OnDotTweenComplete?.Invoke());
        }
        public void CardWrongAnswerAnimation(Card card)
        {
            card.transform.DOMoveX(gameObject.transform.position.x + 3f, 0.3f).SetLoops(4, LoopType.Yoyo).SetEase(Ease.InOutSine);

        }


    }
}

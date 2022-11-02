using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SpeedometerDemo.Visual
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] Image frontFillImg = null;
        [SerializeField] Image backFillImg = null;

        [SerializeField] float barMovingSpeed = 0.05f;

        Sequence sequnce;
        RectTransform rect;


        public void Init()
        {
            rect = GetComponent<RectTransform>();
        }
        public void ResetHp()
        {
            frontFillImg.DOKill();
            backFillImg.DOKill();
            sequnce.Kill();

            var time = Mathf.Abs(1 - frontFillImg.fillAmount) / barMovingSpeed;
            backFillImg.color = Color.green;

            backFillImg.DOFillAmount(1, time * 0.1f);
            frontFillImg.DOFillAmount(1, time * 0.5f);
        }
        public void TakenDamage(float value)
        {
            rect.DOKill();
            rect.DOScale(1.0f, 0.0f);

            sequnce.Kill();
            frontFillImg.DOKill();
            backFillImg.DOKill();

            var time = Mathf.Abs(frontFillImg.fillAmount - value) / barMovingSpeed;
            backFillImg.color = Color.red;

            sequnce = DOTween.Sequence();
            sequnce.AppendCallback(() => rect.DOScale(1.1f, 0.2f));
            sequnce.Append(frontFillImg.DOFillAmount(value, time * 0.3f));
            sequnce.AppendInterval(0.5f);
            sequnce.Append(backFillImg.DOFillAmount(value, time));
            sequnce.AppendCallback(() => rect.DOScale(1.0f, 0.2f));
        }
    }
}


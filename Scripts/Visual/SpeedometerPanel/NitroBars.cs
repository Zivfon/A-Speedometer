using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SpeedometerDemo.Visual
{
    public class NitroBars : MonoBehaviour
    {
        [SerializeField] Image fillBarImg = null;
        [SerializeField] Image fillBottleImg = null;

        [SerializeField] Color fillColor = Color.blue;
        [SerializeField] int damping = 20;

        readonly float initBarValue = 0.125f;
        readonly float endBarValue = 0.875f;
        readonly float initBottleValue = 0.0f;
        readonly float endBottleValue = 1.0f;


        private void OnDestroy()
        {
            fillBarImg.DOKill();
        }
        private void Update()
        {
            if (GameController.instance.CurrentCar.onNitro
                || GameController.instance.CurrentCar.runningData.nitro == GameController.instance.CurrentCar.infoData.nitroCapacity)
            {
                fillBarImg.gameObject.SetActive(true);
                fillBottleImg.color = fillColor;

                UpdateBar();
            }
            else
            {
                fillBarImg.gameObject.SetActive(false);
                fillBottleImg.color = fillColor;

                UpdateBar();
            }
        }

        public void Init()
        {
            fillBarImg.DOKill();
            fillBarImg.color = Color.white;
            fillBarImg.DOColor(fillColor, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }

        private void UpdateBar()
        {
            float targetVallue = (((float)GameController.instance.CurrentCar.runningData.nitro
                / GameController.instance.CurrentCar.infoData.nitroCapacity) * (endBarValue - initBarValue) / 1) + initBarValue;
            fillBarImg.fillAmount = Mathf.Lerp(fillBarImg.fillAmount, targetVallue, Time.deltaTime * damping);
            fillBarImg.fillAmount = Mathf.Clamp(fillBarImg.fillAmount, initBarValue, endBarValue);

            float targetBottleVallue = (((float)GameController.instance.CurrentCar.runningData.nitro
                / GameController.instance.CurrentCar.infoData.nitroCapacity) * (endBottleValue - initBottleValue) / 1) + initBottleValue;

            fillBottleImg.fillAmount = Mathf.Lerp(fillBottleImg.fillAmount, targetBottleVallue, Time.deltaTime * damping);
            fillBottleImg.fillAmount = Mathf.Clamp(fillBottleImg.fillAmount, initBottleValue, endBottleValue);
        }
    }
}

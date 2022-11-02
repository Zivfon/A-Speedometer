using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpeedometerDemo.Visual
{
    public class RpmBar : MonoBehaviour
    {
        [SerializeField] Image pointerImg = null;
        [SerializeField] Image rpmBarImg = null;
        [SerializeField] Image limitBarImg = null;

        [SerializeField] int damping = 20;

        readonly float initValue = 0.125f;
        readonly float endValue = 0.875f;
        readonly float initAngle = -133;
        readonly float endAngle = 133;


        public void SetRpmBar(int minDangerRpm)
        {
            limitBarImg.fillAmount = (((float)(GameController.instance.CurrentCar.infoData.maxRpm - minDangerRpm)
                / GameController.instance.CurrentCar.infoData.maxRpm) * (endValue - initValue) / 1) + initValue;
            limitBarImg.fillAmount = Mathf.Clamp(limitBarImg.fillAmount, initValue, endValue);
        }
        private void Update()
        {
            if (GameController.instance.CurrentCar != null)
            {
                float targetVallue = (((float)GameController.instance.CurrentCar.runningData.rpm
                    / GameController.instance.CurrentCar.infoData.maxRpm) * (endValue - initValue) / 1) + initValue;
                rpmBarImg.fillAmount = Mathf.Lerp(rpmBarImg.fillAmount, targetVallue, Time.deltaTime * damping);
                rpmBarImg.fillAmount = Mathf.Clamp(rpmBarImg.fillAmount, initValue, endValue);

                float angle = (((float)GameController.instance.CurrentCar.runningData.rpm
                                    / GameController.instance.CurrentCar.infoData.maxRpm) * (endAngle - initAngle)) + initAngle;

                angle = (angle < 0) ? angle + 360 : angle;
                pointerImg.rectTransform.eulerAngles = new Vector3(pointerImg.transform.eulerAngles.x, pointerImg.transform.eulerAngles.y, angle);
            }
        }
    }
}

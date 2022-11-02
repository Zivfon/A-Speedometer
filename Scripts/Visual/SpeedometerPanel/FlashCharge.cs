using System.Collections;
using System.Collections.Generic;
using SpeedometerDemo.Data;
using UnityEngine;
using UnityEngine.UI;

namespace SpeedometerDemo.Visual
{
    public class FlashCharge : MonoBehaviour
    {
        [SerializeField] Text flashChargesTxt = null;
        [SerializeField] Image flashChargeProgressImg = null;

        [SerializeField] int damping = 20;


        private void Update()
        {
            flashChargesTxt.text = $"{GameController.instance.CurrentCar.runningData.flashCharges}";

            float targetVallue = (float)GameController.instance.CurrentCar.runningData.flashChargeProgress
                / CarInfoData.FlashChargeCapacity;
            flashChargeProgressImg.fillAmount = Mathf.Lerp(flashChargeProgressImg.fillAmount, targetVallue, Time.deltaTime * damping);
        }
    }
}

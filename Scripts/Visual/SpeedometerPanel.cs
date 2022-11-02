using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using SpeedometerDemo.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace SpeedometerDemo.Visual
{
    public class SpeedometerPanel : MonoBehaviour
    {
        [SerializeField] HpBar hpBar = null;
        [SerializeField] NitroBars nitroBars = null;
        [SerializeField] RpmBar rpmBar = null;
        [SerializeField] Text carInfoTxt = null;
        [SerializeField] Text gearTxt = null;
        [SerializeField] Text mphTxt = null;
        [SerializeField] RectTransform contentArea = null;


        #region Unity Functions
        private void OnDestroy()
        {
            GameController.instance.carFactory.UnRegisterToCarBuilt(OnNewCarBuilt);
        }
        private void Update()
        {
            var angle = Mathf.Clamp(GameController.instance.CurrentCar.runningData.mph / 10, 0, 30);
            contentArea.eulerAngles = Vector3.Lerp(contentArea.eulerAngles, new Vector3(angle, contentArea.eulerAngles.y, contentArea.eulerAngles.z), Time.deltaTime);

            mphTxt.text = $"{GameController.instance.CurrentCar.runningData.mph}";
            contentArea.anchoredPosition = Vector2.Lerp(contentArea.anchoredPosition, new Vector2(GameController.instance.playerUtil_InputAxis.x, 0) * 20, Time.deltaTime * 10);
        }
        #endregion

        #region Public Functions
        public void Init()
        {
            GameController.instance.carFactory.RegisterToCarBuilt(OnNewCarBuilt);

            hpBar.Init();
            nitroBars.Init();
        }
        #endregion

        #region Private Functions
        private void OnNewCarBuilt(CarController newCar)
        {
            newCar.RegisterToDamageUpdates(OnDamageUpdated);
            newCar.RegisterToGearUpdates(OnGearUpdated);

            var infoBuilder = new StringBuilder();
            infoBuilder.AppendLine($"[{newCar.infoData.carName}]");
            infoBuilder.AppendLine($"- Max Rpm {newCar.infoData.maxRpm}");
            infoBuilder.AppendLine($"- Rpm Limit {newCar.infoData.minDangerRpm}");
            infoBuilder.AppendLine($"- Max Mph {newCar.infoData.maxMph}");
            infoBuilder.AppendLine($"- Max Gear {newCar.infoData.maxGear - 1}");
            infoBuilder.AppendLine($"- Max Hp {newCar.infoData.maxHp}");
            infoBuilder.AppendLine($"- Nitro {newCar.infoData.nitroCapacity}ml");
            infoBuilder.AppendLine($"- Flash {newCar.infoData.maxFlashCharges}");
            carInfoTxt.text = infoBuilder.ToString();

            hpBar.ResetHp();

            gearTxt.text = ((Data.CarGears)newCar.runningData.gear).ToString();

            rpmBar.SetRpmBar(newCar.infoData.minDangerRpm);
        }
        private void OnGearUpdated(int gear)
        {
            gearTxt.text = ((Data.CarGears)gear).ToString();
        }
        private void OnDamageUpdated(int hp)
        {
            float value = (float)hp / GameController.instance.CurrentCar.infoData.maxHp;

            hpBar.TakenDamage(value);
            contentArea.DOKill();
            contentArea.DOShakeAnchorPos(0.2f, 15, 50, 50);
        }
        #endregion
    }
}

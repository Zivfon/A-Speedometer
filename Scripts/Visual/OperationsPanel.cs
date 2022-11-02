using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpeedometerDemo.Visual
{
    public class OperationsPanel : MonoBehaviour
    {
        [SerializeField] Button buildNewCarBtn = null;
        [SerializeField] Button damageBtn = null;
        [SerializeField] Button gearUpBtn = null;
        [SerializeField] Button gearDownBtn = null;
        [SerializeField] Button nitrousBtn = null;
        [SerializeField] Button flashChargeBtn = null;


        private void Update()
        {
            if (Input.GetKey(KeyCode.W))
                GameController.instance.playerUtil_InputAxis.y = 1;
            if (Input.GetKeyUp(KeyCode.W))
                GameController.instance.playerUtil_InputAxis.y = 0;

            if (Input.GetKey(KeyCode.A))
                GameController.instance.playerUtil_InputAxis.x = -1;
            if (Input.GetKeyUp(KeyCode.A))
                GameController.instance.playerUtil_InputAxis.x = 0;

            if (Input.GetKey(KeyCode.S))
                GameController.instance.playerUtil_InputAxis.y = -1;
            if (Input.GetKeyUp(KeyCode.S))
                GameController.instance.playerUtil_InputAxis.y = 0;

            if (Input.GetKey(KeyCode.D))
                GameController.instance.playerUtil_InputAxis.x = 1;
            if (Input.GetKeyUp(KeyCode.D))
                GameController.instance.playerUtil_InputAxis.x = 0;

            if (Input.GetKeyDown(KeyCode.Q))
                GameController.instance.PlayerUtil_GearUp();
            if (Input.GetKeyDown(KeyCode.E))
                GameController.instance.PlayerUtil_GearDown();
            if (Input.GetKeyDown(KeyCode.Space))
                GameController.instance.PlayerUtil_UseNitro();
            if (Input.GetKeyDown(KeyCode.F))
                GameController.instance.PlayerUtil_FlashChargeNitro();
        }

        public void Init()
        {
            buildNewCarBtn.onClick.AddListener(GameController.instance.PlayerUtil_BuildNewCar);
            damageBtn.onClick.AddListener(GameController.instance.PlayerUtil_TakenDamage);
            gearUpBtn.onClick.AddListener(GameController.instance.PlayerUtil_GearUp);
            gearDownBtn.onClick.AddListener(GameController.instance.PlayerUtil_GearDown);
            nitrousBtn.onClick.AddListener(GameController.instance.PlayerUtil_UseNitro);
            flashChargeBtn.onClick.AddListener(GameController.instance.PlayerUtil_FlashChargeNitro);
        }
    }
}

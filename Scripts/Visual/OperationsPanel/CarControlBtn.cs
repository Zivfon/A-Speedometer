using System;
using System.Collections;
using System.Collections.Generic;
using SpeedometerDemo.Visual;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpeedometerDemo.Visual
{
    public class CarControlBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] Text buttonTxt = null;

        [SerializeField] Color pressedColor = Color.white;
        [SerializeField] int axisValue;
        [SerializeField] bool isVertical;


        public void OnPointerDown(PointerEventData eventData)
        {
            if (isVertical)
                GameController.instance.playerUtil_InputAxis.y = axisValue;
            else
                GameController.instance.playerUtil_InputAxis.x = axisValue;

            if (buttonTxt != null)
                buttonTxt.color = pressedColor;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (isVertical)
                GameController.instance.playerUtil_InputAxis.y = 0;
            else
                GameController.instance.playerUtil_InputAxis.x = 0;

            if (buttonTxt != null)
                buttonTxt.color = Color.black;
        }
    }
}

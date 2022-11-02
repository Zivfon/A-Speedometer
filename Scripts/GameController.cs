using System.Collections;
using System.Collections.Generic;
using SpeedometerDemo.Logic;
using SpeedometerDemo.Visual;
using UnityEngine;

namespace SpeedometerDemo
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] SpeedometerPanel speedometerPanel = null;
        [SerializeField] OperationsPanel operationsPanel = null;

        public static GameController instance;
        public Vector2 playerUtil_InputAxis = Vector2.zero;
        public CarFactory carFactory;
        public CarController CurrentCar { get; set; }


        #region Unity Functions
        private void Awake()
        {
            instance = this;
            carFactory = new CarFactory();

            //To avoid null
            carFactory.BuildNewCar();
        }
        void Start()
        {
            operationsPanel.Init();
            speedometerPanel.Init();

            PlayerUtil_BuildNewCar();
        }
        void Update()
        {
            if (CurrentCar != null)
                CurrentCar.Update(Time.deltaTime);
        }
        #endregion

        #region Public Function
        public void PlayerUtil_BuildNewCar()
        {
            CurrentCar.Dispose();
            carFactory.BuildNewCar();
        }
        public void PlayerUtil_TakenDamage() { CurrentCar.TakenDamage(); }
        public void PlayerUtil_GearUp() { CurrentCar.SetGear(true); }
        public void PlayerUtil_GearDown() { CurrentCar.SetGear(false); }
        public void PlayerUtil_UseNitro() { CurrentCar.UseNitro(); }
        public void PlayerUtil_FlashChargeNitro() { CurrentCar.FlashChargeNitro(); }
        #endregion

        #region Debug Functions
        public void Debug_LogForMe(string content)
        {
            Debug.Log($"GameController: {content}");
        }
        #endregion
    }

    public static class MyRandom
    {
        static System.Random random = new System.Random();

        public static bool RollTheDice(int rateX10)
        {
            return Range(1, 11) <= rateX10;
        }
        public static int Range(int minInclusive, int maxExclusive)
        {
            return random.Next(minInclusive, maxExclusive);
        }
        public static int RangInclusive(int minInclusive, int maxInclusive)
        {
            return random.Next(minInclusive, maxInclusive + 1);
        }
        public static int Range(int maxExclusive)
        {
            return random.Next(0, maxExclusive);
        }
        public static bool Chance(int chanceX10)
        {
            return chanceX10 >= random.Next(1, 11);
        }
    }
}

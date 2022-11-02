using System;
using System.Collections;
using System.Collections.Generic;
using SpeedometerDemo.Data;

namespace SpeedometerDemo.Logic
{
    public class CarController
    {
        public CarController(CarInfoData data)
        {
            infoData = data;
            InitCarStatus();
        }

        public CarInfoData infoData;
        public CarRunningData runningData;
        public bool onNitro = false;
        Action<int> onDamageUpdated;
        Action<int> onGearUpdated;
        Action<int> onFlashChargeUpdated;


        #region Public Functions
        public void RegisterToDamageUpdates(Action<int> callback) { onDamageUpdated += callback; }
        public void RegisterToGearUpdates(Action<int> callback) { onGearUpdated += callback; }
        public void UseNitro()
        {
            if (runningData.nitro >= infoData.nitroCapacity)
                onNitro = true;
        }
        public void FlashChargeNitro()
        {
            if (runningData.flashCharges > 0 && runningData.flashChargeProgress >= CarInfoData.FlashChargeCapacity)
            {
                runningData.flashCharges--;
                runningData.flashChargeProgress = 0;
                runningData.nitro = infoData.nitroCapacity;
            }
        }
        public void TakenDamage()
        {
            runningData.hp -= CarInfoData.TestDamageTaken;

            runningData.mph -= 30;
            runningData.mph = runningData.mph <= 0 ? 0 : runningData.mph;
            var targetL = runningData.gear == 0 ? 1 : runningData.gear;
            runningData.rpm = runningData.mph * 100 / targetL;

            if (onDamageUpdated != null)
                onDamageUpdated(runningData.hp);
        }
        public void SetGear(bool isUp)
        {
            if (isUp)
            {
                runningData.gear++;
                if (runningData.gear >= infoData.maxGear - 1)
                    runningData.gear = infoData.maxGear - 1;

                var targetL = runningData.gear == 0 ? 1 : runningData.gear;
                runningData.rpm = runningData.mph * 100 / targetL;
            }
            else
            {
                runningData.gear--;
                if (runningData.gear <= 0)
                    runningData.gear = 0;
            }

            if (onGearUpdated != null)
                onGearUpdated(runningData.gear);
        }
        public void Dispose()
        {
            onDamageUpdated = null;
            onGearUpdated = null;
        }
        public void Update(float deltaTime)
        {
            //For convenience, all data are left as integer, with no deltaTime applied
            EngineProcessor(deltaTime);
            NitrousProcessor(deltaTime);
            FlashChargeProcessor(deltaTime);
        }
        #endregion

        #region Private Functions
        private void InitCarStatus()
        {
            runningData.hp = infoData.maxHp;
            runningData.gear = 1;
            runningData.flashChargeProgress = CarInfoData.FlashChargeCapacity;
            runningData.mph = 0;
            runningData.nitro = infoData.nitroCapacity;
            runningData.rpm = 0;
            runningData.flashCharges = infoData.maxFlashCharges;
        }
        private void EngineProcessor(float deltaTime)
        {
            var targetL = runningData.gear == 0 ? 1 : runningData.gear;
            if (GameController.instance.playerUtil_InputAxis.y > 0.5f)
            {
                var nitro = onNitro ? CarInfoData.NitroSpeedMultiplier : 1;
                if (runningData.rpm < infoData.minDangerRpm)
                    runningData.rpm += CarInfoData.EngineAcceleration / targetL * nitro;
            }
            else if (GameController.instance.playerUtil_InputAxis.y < -0.5f)
            {
                if (runningData.rpm > 0)
                    runningData.rpm -= CarInfoData.EngineBraking;
                else if (runningData.rpm < 0)
                    runningData.rpm = 0;
            }
            else
            {
                if (runningData.rpm > 0)
                    runningData.rpm -= CarInfoData.EngineDeceleration;
                else if (runningData.rpm < 0)
                    runningData.rpm = 0;
            }
            runningData.mph = runningData.rpm / 100 * targetL;
        }
        private void NitrousProcessor(float deltaTime)
        {
            if (!onNitro)
            {
                if (runningData.nitro < infoData.nitroCapacity)
                    runningData.nitro += CarInfoData.NitroChargingSpeed;
            }
            else
            {
                if (runningData.nitro > 0)
                    runningData.nitro -= CarInfoData.NitroUsingSpeed;
                else
                {
                    runningData.nitro = 0;
                    onNitro = false;
                }
            }
        }
        private void FlashChargeProcessor(float deltaTime)
        {
            if (runningData.flashChargeProgress < CarInfoData.FlashChargeCapacity)
                runningData.flashChargeProgress += CarInfoData.FlashChargingSpeed;
            else
                runningData.flashChargeProgress = CarInfoData.FlashChargeCapacity;
        }
        #endregion
    }
}

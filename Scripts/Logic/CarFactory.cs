using System;
using SpeedometerDemo.Data;

namespace SpeedometerDemo.Logic
{
    public class CarFactory
    {
        CarConfigData configData = new CarConfigData();
        Action<CarController> onCarBuilt;


        public void RegisterToCarBuilt(Action<CarController> callback) { onCarBuilt += callback; }
        public void UnRegisterToCarBuilt(Action<CarController> callback) { onCarBuilt -= callback; }
        public CarController BuildNewCar()
        {
            var newCar = new CarController(GenerateCarData());
            GameController.instance.CurrentCar = newCar;
            if (onCarBuilt != null)
                onCarBuilt(newCar);
            return newCar;
        }

        private CarInfoData GenerateCarData()
        {
            return new CarInfoData(configData.brands[MyRandom.Range(configData.brands.Length)] + " - " + MyRandom.Range(1000, 10000),
                MyRandom.RangInclusive(configData.minRpm, configData.maxRpm),
                MyRandom.RangInclusive(configData.mindangerRpmRange, configData.maxdangerRpmRange),
                MyRandom.RangInclusive(configData.minGear, configData.maxGear),
                MyRandom.RangInclusive(configData.minNitro, configData.maxNitro),
                MyRandom.RangInclusive(configData.minHp, configData.maxHp),
                MyRandom.RangInclusive(configData.minFlashCharges, configData.maxFlashCharges)
            );
        }
    }
}

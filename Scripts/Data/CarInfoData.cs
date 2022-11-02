namespace SpeedometerDemo.Data
{
    public struct CarInfoData
    {
        public string carName;
        public int maxRpm;
        public int minDangerRpm;
        public int maxMph;
        public int maxGear;
        public int nitroCapacity;
        public int maxHp;
        public int maxFlashCharges;

        public const int SpeedAcceleration = 1;
        public const int EngineAcceleration = 23;
        public const int EngineDeceleration = 11;
        public const int EngineBraking = 52;
        public const int NitroSpeedMultiplier = 3;
        public const int NitroUsingSpeed = 12;
        public const int NitroChargingSpeed = 1;
        public const int FlashChargingSpeed = 1;
        public const int FlashChargeCapacity = 1000;
        public const int TestDamageTaken = 40;

        public CarInfoData(string carName, int maxRpm, int minDangerRpm, int maxGear, int nitroCapacity, int maxHp, int maxFlashCharges) : this()
        {
            this.carName = carName;
            this.maxRpm = maxRpm;
            this.minDangerRpm = maxRpm - minDangerRpm;
            this.maxMph = this.minDangerRpm / 100 * (maxGear - 1);
            this.maxGear = maxGear;
            this.nitroCapacity = nitroCapacity;
            this.maxHp = maxHp;
            this.maxFlashCharges = maxFlashCharges;
        }
    }
}

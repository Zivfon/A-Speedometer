namespace SpeedometerDemo.Data
{
    public enum CarGears
    {
        N, L1, L2, L3, L4, L5, L6, L7, L8,
    }
    public class CarConfigData
    {
        public readonly string[] brands = new string[] { "BMW", "Audi", "Tesla", "Volvo", "Toyota", "Jeep", };
        public readonly int maxRpm = 10000;
        public readonly int minRpm = 10000;
        public readonly int maxdangerRpmRange = 3000;
        public readonly int mindangerRpmRange = 1000;
        public readonly int maxGear = 8;
        public readonly int minGear = 4;
        public readonly int maxNitro = 5000;
        public readonly int minNitro = 1000;
        public readonly int maxHp = 500;
        public readonly int minHp = 100;
        public readonly int maxFlashCharges = 5;
        public readonly int minFlashCharges = 1;
    }
}

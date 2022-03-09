using UnityEngine;

namespace BombsTest.Npc
{
    public class Health
    {
        public float MaxValue { get; }
        public float MinValue { get; }
        public float Value { get; private set; } = 100;

        public Health(float value, float minValue, float maxValue)
        {
            Value = value;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public Health(float minValue, float maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public void Add(float amount)
        {
            Value = Mathf.Clamp(Value + amount, MinValue, MaxValue);
        }
        
        public void Subtract(float amount)
        {
            Value = Mathf.Clamp(Value - amount, MinValue, MaxValue);
        }
    }
}
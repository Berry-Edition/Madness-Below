[System.Serializable]
public struct Vital : IVital {
    public int Value;
    public int Min, Max;

    public void AddValue(int count){
        if (Value + count <= Max)
        {
            Value += count;
        }
        else
        {
            Value = Max;
        }
    }

    public void DecreaseValue(int count){
        if (Value - count >= Min)
        {
            Value -= count;
        }
        else
        {
            Value = Min;
        }
    }
}
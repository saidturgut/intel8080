namespace i8080_emulator.Executing;
using Signaling;

public partial class DataPath
{
    public void AddressBuffer()
    {
        if(signals.AddressDriver == Register.NONE)
            return;

        if (RegisterPairs.TryGetValue(signals.AddressDriver, out var registerPair))
        {
            ABUS_L.Set(registerPair[0].Get());
            ABUS_H.Set(registerPair[1].Get());
        }
    }
}
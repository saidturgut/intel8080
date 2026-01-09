namespace i8080_emulator.Executing;
using Signaling;

public partial class DataPath
{
    public void AddressBuffer()
    {
        if(signals.AddressDriver == Register.NONE && 
           signals.SideEffect != SideEffect.JMP)
            return;

        if (signals.SideEffect == SideEffect.SP_NXT)
            Decrement(Registers[Register.SP_L], Registers[Register.SP_H]);
        
        if (RegisterPairs.TryGetValue(signals.AddressDriver, out var addressPair))
        {
            if(PcOverriders.TryGetValue(signals.SideEffect, out var overrider))
            {
                Registers[Register.PC_L].Set(overrider[0].Get());
                Registers[Register.PC_H].Set(overrider[1].Get());
                addressPair = overrider;
            }
            
            ABUS_L.Set(addressPair[0].GetTemp());
            ABUS_H.Set(addressPair[1].GetTemp());

            /*if (signals.TakeSnapshot)
            {
                ABUS_L.SetSnapshot();
                ABUS_H.SetSnapshot();
            }*/
        }
    }
}
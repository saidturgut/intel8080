namespace i8080_emulator.Executing;
using Signaling;

public partial class DataPath
{
    public void AddressBuffer()
    {
        if(signals.AddressDriver == Register.NONE)
            return;
        
        // PROGRAM COUNTER
        if (signals.AddressDriver == Register.PC_L)
        {
            ABUS_L.Set(Registers[Register.PC_L].Get());
            ABUS_H.Set(Registers[Register.PC_H].Get());
            return;
        }
        
        if (signals.AddressDriver == Register.C)
        {
            ABUS_L.Set(Registers[Register.C].Get());
            ABUS_H.Set(Registers[Register.B].Get());
            return;
        }
        
        if (signals.AddressDriver == Register.E)
        {
            ABUS_L.Set(Registers[Register.E].Get());
            ABUS_H.Set(Registers[Register.D].Get());
            return;
        }
        
        // TEMP ADDRESS REGISTER
        if (signals.AddressDriver == Register.HL_L)
        {
            ABUS_L.Set(Registers[Register.HL_L].Get());
            ABUS_H.Set(Registers[Register.HL_H].Get());
            return;
        }
    }
}
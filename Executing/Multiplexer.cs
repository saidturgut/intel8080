namespace i8080_emulator.Executing;
using Signaling;

public partial class DataPath
{
    public byte GetIR() => Registers[Register.IR].Get();
    
    public void MultiplexerDrive()
    {        
        if(signals.DataDriver == Register.NONE)
            return;
        
        // RANDOM ACCESS MEMORY
        if (signals.DataDriver == Register.RAM)
        {
            RAM.Read(ABUS_H, ABUS_L, DBUS);
            return;
        }

        if (DataDrivers.ContainsKey(signals.DataDriver))
        {
            DBUS.Set(DataDrivers[signals.DataDriver].Get());
        }
    }
    
    public void MultiplexerLatch()
    {        
        if(signals.DataLatcher == Register.NONE)
            return;
        
        // RANDOM ACCESS MEMORY
        if (signals.DataLatcher == Register.RAM)
        {
            RAM.Write(ABUS_H, ABUS_L, DBUS);
            return;
        }

        if (DataLatchers.ContainsKey(signals.DataLatcher))
        {
            DataLatchers[signals.DataLatcher].Set(DBUS.Get());
        }
    }
}
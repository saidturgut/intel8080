namespace i8080_emulator.Executing;
using Components;
using Signaling;

public partial class DataPath
{
    private readonly ClockedRegister[] Registers = new ClockedRegister[16];
    
    public void DataDrive()
    {
        if(signals.DataDriver is Register.NONE)
            return;

        if (signals.DataDriver is Register.RAM)
            Ram.Read(AbusL, AbusH, Dbus);
        else
            Dbus.Set(Registers[(byte)signals.DataLatcher].Get());
    }
    
    public void DataLatch()
    {
        if(signals.DataLatcher is Register.NONE)
            return;

        if (signals.DataLatcher is Register.RAM)
            Ram.Write(AbusL, AbusH, Dbus);
        else
            Registers[(byte)signals.DataLatcher].Set(Dbus.Get());
    }
}
namespace i8080_emulator.Executing;
using Signaling;
using Components;
using Computing;

public partial class DataPath : DataPathRom
{
    private readonly Ram Ram = new ();
    
    private readonly TriStateBus Dbus = new ();
    private readonly TriStateBus AbusL = new ();
    private readonly TriStateBus AbusH = new (); 

    private SignalSet signals = new ();

    public void Init()
    {
        Ram.Init(true);
        
        for (int i = 0; i < Registers.Length; i++)
        {
            Registers[i] = new ClockedRegister();
        }
        
        DebugInit();
    }
    
    public void Clear()
    {
        Dbus.Clear();
        AbusL.Clear();
        AbusH.Clear();
    }

    public void Receive(SignalSet input) =>      
        signals = input;

    public byte GetIr()
        => Registers[(byte)Register.IR].Get();

    private bool GetFlag(PswFlag flag) 
        => (Registers[(byte)Register.PSW].Get() & (byte)flag) != 0;

    public void Commit()
    {
        foreach (ClockedRegister register in Registers)
        {
            register.Commit();
        }
    }
}

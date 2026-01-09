namespace i8080_emulator;
using Executing;
using Signaling;

public class CPU
{    
    private readonly DataPath DataPath = new ();
    private readonly ControlUnit ControlUnit = new ();
    
    public void PowerOn() => Clock();
    
    private void Clock()
    {
        DataPath.Init();
        
        while (!DataPath.HALT)
        {
            Thread.Sleep(50);
            
            Tick();
        }
        
        DataPath.MemoryDump();
    }

    private void Tick()
    {
        DataPath.Commit();
        DataPath.Debug();

        DataPath.Set(
        ControlUnit.Emit());
        
        if(DataPath.HALT) return;
        
        DataPath.Clear();

        DataPath.ControlALU();
        DataPath.AddressBuffer();
        DataPath.MultiplexerDrive();
        
        DataPath.MultiplexerLatch();
        DataPath.Incrementer();
        
        ControlUnit.Decode(
        DataPath.GetIR());
        
        ControlUnit.Advance();
    }
}
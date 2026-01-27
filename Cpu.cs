namespace i8080_emulator;
using Executing;
using Signaling;

public class Cpu
{    
    private readonly DataPath DataPath = new ();
    private readonly MicroUnit MicroUnit = new ();
    
    public void PowerOn() => Clock();
    
    private void Clock()
    {
        DataPath.Init();
        MicroUnit.Init();
        
        while (MicroUnit.State is not State.HALT)
        {
            Tick();
        }
    }

    private void Tick()
    {
        DataPath.Clear();
        DataPath.Receive(
        MicroUnit.Emit());
        
        DataPath.AddressDrive();
        DataPath.DataDrive();
        DataPath.AluAction();
        DataPath.DataLatch();

        DataPath.Commit();
        DataPath.Debug();
        
        MicroUnit.Advance(DataPath.GetIr());
    }
}
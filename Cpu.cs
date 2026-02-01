namespace intel8080;
using Executing;
using Signaling;

public class Cpu
{
    private readonly DataPath DataPath = new();
    private readonly MicroUnit MicroUnit = new();
    
    private const bool DEBUG_MODE = true;
    
    public void PowerOn() => Clock();

    private void Clock()
    {
        DataPath.Init(DEBUG_MODE);
        MicroUnit.Init(DataPath.Psw);
        
        while (DataPath.signals.MicroStep is not MicroStep.HALT)
        {
            Tick();
            
            Thread.Sleep(25);
        }
    }

    private void Tick()
    {
        DataPath.Receive(
        MicroUnit.Emit());

        DataPath.Execute();
        
        MicroUnit.Advance(DataPath.GetIr());
        
        if(!MicroUnit.BOUNDARY) return;
        DataPath.Debug(MicroUnit.DEBUG_NAME);
        MicroUnit.BOUNDARY = false;
    }
}
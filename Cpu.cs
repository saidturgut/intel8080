namespace intel8080;
using Executing;
using Signaling;

public class Cpu
{
    private readonly DataPath DataPath = new();
    private readonly MicroUnit MicroUnit = new();
    
    private const bool DEBUG_MODE = false;
    
    public void PowerOn() => Clock();

    private void Clock()
    {
        DataPath.Init(DEBUG_MODE);
        MicroUnit.Init(DataPath.Psw);
        
        while (DataPath.signals.MicroStep is not MicroStep.HALT)
            Tick();
    }

    private void Tick()
    {
        DataPath.Receive(MicroUnit.Emit());

        DataPath.Execute();
        
        MicroUnit.Advance(DataPath.GetIr());
        
        if(!MicroUnit.BOUNDARY) Boundary();
    }

    private void Boundary()
    {
        DataPath.Debug(MicroUnit.DEBUG_NAME);
        DataPath.Tty.HostInput();
    }
}
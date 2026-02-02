namespace intel8080;
using Executing;
using Signaling;

public class Cpu
{
    private readonly DataPath DataPath = new();
    private readonly MicroUnit MicroUnit = new();
    
    private const bool DEBUG_MODE = true;
    private const bool STEP_MODE = false;
    
    public void PowerOn() => Clock();

    private void Clock()
    {
        DataPath.Init(DEBUG_MODE);
        MicroUnit.Init(DataPath.Psw);
        
        while (DataPath.signals.MicroStep is not MicroStep.HALT)
        {
            Tick();

            if (DEBUG_MODE && !STEP_MODE)
                Thread.Sleep(1);
        }
    }

    private void Tick()
    {
        DataPath.Receive(
        MicroUnit.Emit());

        DataPath.Execute(MicroUnit.DEBUG_NAME);
        
        MicroUnit.Advance(DataPath.GetIr());
        
        if(!MicroUnit.BOUNDARY) return;
        DataPath.Debug();
        MicroUnit.BOUNDARY = false;

        if (Console.KeyAvailable)
        {
            if (Console.ReadKey().Key == ConsoleKey.H)
            {
                DataPath.MemoryDump();
            }
        }
        
        if(!STEP_MODE) return;
        if (Console.ReadKey().Key != ConsoleKey.Enter)
        {
            DataPath.MemoryDump();
        }
    }
}
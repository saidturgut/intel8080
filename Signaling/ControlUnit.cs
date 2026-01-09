namespace i8080_emulator.Signaling;
using Decoding;
using Cycles;

public class ControlUnit : ControlUnitROM
{        
    private readonly Decoder Decoder = new ();
    private readonly Sequencer Sequencer = new ();
    
    private MachineCycle currentCycle = MachineCycle.FETCH;
    
    public SignalSet Emit() 
        => MachineCyclesMethod[currentCycle]();

    public void Decode(byte IR)
    {
        Console.WriteLine("CURRENT CYCLE : "  + currentCycle);
        
        if (currentCycle == MachineCycle.FETCH)
        {
            decoded = Decoder.Decode(IR);
        }
    }

    public void Advance()
    {
        Sequencer.Advance((byte)(decoded.Cycles.Count - 1));
        currentCycle = decoded.Cycles[Sequencer.mState];
    }
}
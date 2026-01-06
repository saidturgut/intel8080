namespace i8080_emulator.Signaling;
using Decoding;
using Cycles;

public class ControlUnit : ControlUnitROM
{        
    private readonly Decoder Decoder = new ();
    private readonly Sequencer Sequencer = new ();
    
    private MachineCycle currentCycle = MachineCycle.FETCH;
    
    public bool HALT;

    public SignalSet Emit() 
        => MachineCyclesMethod[currentCycle]();

    public void Decode(byte IR)
    {
        if (IR == 0x76) HALT = true; // HLT
        
        if (currentCycle == MachineCycle.FETCH)
        {
            decoded = Decoder.Decode(IR);
        }
    }

    public void Advance()
    {
        Console.WriteLine(currentCycle);
        
        Sequencer.Advance((byte)(decoded.Cycles.Count - 1));
        currentCycle = decoded.Cycles[Sequencer.mState];
    }
}
namespace i8080_emulator.Signaling;
using Decoding;

public partial class ControlUnit
{        
    private readonly Decoder Decoder = new ();
    private readonly Sequencer Sequencer = new ();
    
    private MachineCycle currentCycle;
    
    public bool HALT;
    
    public SignalSet Emit(byte IR)
    {
        if (IR == 0x76) HALT = true; // HLT
        
        currentCycle = decoded.Table[Sequencer.mState];

        SignalSet signals = MachineCyclesMethod[currentCycle](Sequencer.tState);
        
        if (signals.SideEffect == SideEffect.DECODE)
            decoded = Decoder.Decode(IR);
        
        return signals;
    }
    
    public void Advance()
        => Sequencer.Advance((byte)
            (decoded.Table.Count - 1), 
            MachineCyclesLength[currentCycle]);
}
namespace i8080_emulator.Signaling;
using Decoding.Multiplexer;
using Decoding;
using Cycles;

public class MicroUnit : MicroUnitRom
{
    private readonly Decoder Decoder = new();

    private SignalSet Signals = new();
    
    public State State = State.FETCH;
    
    private byte currentCycle;
    
    public void Init()
    {
        decoded = DecoderMux.FETCH();
    }
    
    public SignalSet Emit()
    {
        Console.WriteLine($"CYCLE: \"{decoded.MicroCycles[currentCycle]}\"");

        Signals = MicroCycles[(byte)decoded.MicroCycles[currentCycle]]();
        State = Signals.State;
        return Signals;
    }

    public void Advance(byte ir)
    {
        if (Signals.Index) pairIndex++;
        
        if (currentCycle != decoded.MicroCycles.Count - 1)
        {
            currentCycle++;
        }
        else
        {
            switch (State)
            {
                case State.HALT: break;
                case State.FETCH: throw new Exception("ILLEGAL");
                case State.DECODE: decoded = Decoder.Decode(ir); break;
                case State.EXECUTE: decoded = DecoderMux.FETCH(); break;
            }
            
            currentCycle = 0;
            pairIndex = 0;
        }
    }
}
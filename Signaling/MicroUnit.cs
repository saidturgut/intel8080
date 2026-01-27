using i8080_emulator.Decoding.Multiplexer;

namespace i8080_emulator.Signaling;
using Decoding;
using Cycles;

public class MicroUnit : MicroUnitRom
{
    private readonly Decoder Decoder = new();
    
    private byte currentCycle;
    public bool HALT { get; private set; }

    public void Init()
    {
        decoded = DecoderMux.FETCH();
    }
    
    public SignalSet Emit(byte ir)
    {
        if (decoded.MicroCycles[currentCycle] is MicroCycle.DECODE)
        {
            decoded = Decoder.Decode(ir);
            Clear();
        }
        
        return MicroCycles[(byte)decoded.MicroCycles[currentCycle]]();
    }

    public void Advance()
    {
        HALT = decoded.MicroCycles[currentCycle] is MicroCycle.HALT;

        if (currentCycle != decoded.MicroCycles.Count - 1)
        {
            currentCycle++;
        }
        else
        {
            decoded = DecoderMux.FETCH();
            Clear();
        }
    }

    private void Clear() 
        => currentCycle = 0;

}
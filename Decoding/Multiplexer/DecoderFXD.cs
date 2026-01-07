namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Cycles;

public partial class DecoderMultiplexer
{
    protected Decoded FamilyFXD(MachineCycle machineCycle)
    {
        Decoded decoded = new Decoded();

        if (machineCycle != MachineCycle.EMPTY) 
            decoded.Cycles.Add(machineCycle);
        
        return decoded;
    }
}
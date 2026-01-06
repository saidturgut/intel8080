namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Cycles;
using Signaling;
using Executing;

public partial class DecoderMultiplexer
{
    // 01
    protected Decoded FamilyMOV(byte opcode)
    {
        Decoded decoded = new Decoded
        {            
            DataDriver = EncodedRegisters[BB_BBB_XXX(opcode)],
            DataLatcher = EncodedRegisters[BB_XXX_BBB(opcode)],
        };

        if (decoded.DataLatcher == Register.RAM)
        {
            decoded.Cycles.Add(MachineCycle.TMP_LATCH);
            decoded.Cycles.Add(MachineCycle.RAM_WRITE);
        }
        else
        {
            if (decoded.DataDriver == Register.RAM)
                decoded.Cycles.Add(MachineCycle.RAM_READ);
            
            decoded.Cycles.Add(MachineCycle.INTERNAL_LATCH);
        }
        
        return decoded; // 01 110 110 (0x76) is already HLT
    }
}
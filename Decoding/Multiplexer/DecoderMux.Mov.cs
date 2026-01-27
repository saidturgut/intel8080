namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected static Decoded MOV(byte ir)
    {
        Decoded decoded = new()
        {
            AddressDriver = Register.PC_L,
            DataDriver = EncodedRegisters[ir & 0x7],
            DataLatcher = EncodedRegisters[(ir >> 3) & 0x7],
        };
        
        if(decoded.DataDriver is Register.RAM)
            decoded.MicroCycles.Add(MicroCycle.ADDR_INC);
        
        decoded.MicroCycles.Add(MicroCycle.REG_TO_REG);
        
        return decoded;
    }
}
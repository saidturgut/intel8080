namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Cycles;
using Signaling;
using Executing;

public partial class DecoderMultiplexer : DecoderModel
{
    // 00
    protected Decoded FamilyMSC(byte opcode)
    {
        Decoded decoded = new Decoded
        {
            DataLatcher = EncodedRegisters[BB_XXX_BBB(opcode)],
        };
        
        decoded.Cycles.Add(MachineCycle.RAM_READ_IMM);

        if (decoded.DataLatcher == Register.RAM)
            decoded.Cycles.Add(MachineCycle.RAM_WRITE);
        else
        {
            decoded.DataDriver = Register.TMP;
            decoded.Cycles.Add(MachineCycle.INTERNAL_LATCH);
        }
        
        return decoded;
    }

    protected Decoded INX_DCX(byte opcode)
    {
        Decoded decoded = new() { SideEffect = 
            IncrementOpcodes[GetRegisterPair(opcode)], };
        
        decoded.Cycles.Add(MachineCycle.INX_DCX);
        return decoded;
    }

    protected Decoded LXI(byte opcode)
    {
        Decoded decoded = new() { RegisterPairs = 
            RegisterPairs[GetRegisterPair(opcode)] };
        
        decoded.Cycles.Add(MachineCycle.LXI_LOW);
        decoded.Cycles.Add(MachineCycle.LXI_HIGH);
        
        return decoded;
    }
}
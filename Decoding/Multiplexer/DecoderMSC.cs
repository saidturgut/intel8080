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
            AddressDriver = Register.HL_L,
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

    protected Decoded LDAX_STAX(byte opcode)
    {
        if (opcode >> 4 > 1) 
            throw new Exception("ILLEGAL REGISTER PAIR");
        
        Decoded decoded = new()
        {
            AddressDriver = RegisterPairs[GetRegisterPairNormal(opcode)][0],
            DataDriver = Register.TMP,
            DataLatcher = Register.A,
        };
        
        if (BB_BBX_BBB(opcode) == 0)
        {// A -> TMP -> RAM[ADDR_DRV]
            decoded.DataDriver = Register.A;
            decoded.Cycles.Add(MachineCycle.TMP_LATCH);
            decoded.Cycles.Add(MachineCycle.RAM_WRITE);
        }
        else
        {// RAM[ADDR_DRV] -> TMP -> A
            decoded.Cycles.Add(MachineCycle.RAM_READ);
            decoded.Cycles.Add(MachineCycle.INTERNAL_LATCH);
        }
        
        return decoded;
    }
}
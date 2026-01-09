using i8080_emulator.Executing;
using i8080_emulator.Signaling;

namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Cycles;

public partial class DecoderMultiplexer
{
    protected Decoded FamilyFXD(FixedOpcode opcode)
    {
        Decoded decoded = new Decoded { SideEffect = opcode.SideEffect };

        decoded.Cycles.Add(opcode.MachineCycle);
        
        return decoded;
    }

    protected Decoded RST()
    {
        Decoded decoded = new Decoded
        {
            DataDriver = Register.IR,
            DataLatcher = Register.WZ_L,
            SideEffect = SideEffect.JMP,
        };
        decoded.Cycles.Add(MachineCycle.INTERNAL_LATCH);
        
        decoded.Cycles.Add(MachineCycle.CALL_HIGH);
        decoded.Cycles.Add(MachineCycle.CALL_LOW);
        
        decoded.Cycles.Add(MachineCycle.MICRO_CYCLE);
        return decoded;
    }
    
    protected Decoded CALL()
    {
        Decoded decoded = new Decoded
        {
            LatchPairs = EncodedRegisterPairs[5],// WZ
            SideEffect = SideEffect.JMP,
        };
        
        decoded.Cycles.Add(MachineCycle.RAM_READ_IMM_LOW);
        decoded.Cycles.Add(MachineCycle.RAM_READ_IMM_HIGH);
        
        decoded.Cycles.Add(MachineCycle.CALL_HIGH);
        decoded.Cycles.Add(MachineCycle.CALL_LOW);

        decoded.Cycles.Remove(MachineCycle.MICRO_CYCLE);
        decoded.Cycles.Add(MachineCycle.MICRO_CYCLE);

        return decoded;
    }
    
    protected Decoded RET()
    {
        Decoded decoded = new Decoded
        {
            LatchPairs = EncodedRegisterPairs[5],// WZ
            SideEffect = SideEffect.JMP,
        };
        decoded.Cycles.Add(MachineCycle.RET_LOW);
        decoded.Cycles.Add(MachineCycle.RET_HIGH);
        decoded.Cycles.Add(MachineCycle.MICRO_CYCLE);
        return decoded;
    }

    protected Decoded JMP()
    {
        Decoded decoded = new Decoded
        {
            LatchPairs = EncodedRegisterPairs[5],// WZ
            SideEffect = SideEffect.JMP,
        };
        
        decoded.Cycles.Add(MachineCycle.RAM_READ_IMM_LOW);
        decoded.Cycles.Add(MachineCycle.RAM_READ_IMM_HIGH);
        
        decoded.Cycles.Add(MachineCycle.MICRO_CYCLE);
        return decoded;
    }

    protected Decoded COPY_HL(byte pairIndex)
    {
        Decoded decoded = new Decoded
        {
            DrivePairs = EncodedRegisterPairs[2],// HL
            LatchPairs = EncodedRegisterPairs[pairIndex],
        };
        
        if (pairIndex == 1) decoded.SideEffect = SideEffect.SWAP;
        
        decoded.Cycles.Add(MachineCycle.COPY_RP_LOW);
        decoded.Cycles.Add(MachineCycle.COPY_RP_HIGH);
        return decoded;
    }
}
namespace i8080_emulator.Decoding;
using Multiplexer;

public class Decoder : DecoderMultiplexer
{
    public Decoded Decode(byte opcode)
    {
        // CHECK FIXED OPCODES
        if (FixedOpcodes.TryGetValue(opcode, out var value))
            return FamilyFXD(value);
        
        // CHECK INSTRUCTION FAMILY
        switch ((opcode & 0b1100_0000) >> 6)
        {
            case 0b00:
                switch (BB_BBB_XXX(opcode))
                {
                    case 0b100:
                        
                    case 0b101: return FamilyALU(opcode, false);
                    
                    case 0b011: return INX_DCX(opcode);
                    
                    case 0b001:
                    {
                        if((opcode & 0b00_001_000) >> 3 == 1)
                            return DAD(opcode);
                        else
                            return LXI(opcode);
                    }
                }
                
                return FamilyMSC(opcode);
            case 0b01:
                return FamilyMOV(opcode);
            case 0b10:
                return FamilyALU(opcode, true);
            case 0b11:
                throw new Exception("INVALID OPCODE");
        }

        return new Decoded();
    }
}
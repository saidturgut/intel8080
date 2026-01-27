namespace i8080_emulator.Decoding;
using Signaling.Cycles;
using Multiplexer;

public class Decoder : DecoderMux
{
    public Decoded Decode(byte ir)
    {
        opcode = ir;
        
        if (FixedOpcodes.ContainsKey(opcode))
            return FIXED();
        
        switch (opcode)
        {
            case 0x22: return LHLD_SHLD(false);
            case 0x2A: return LHLD_SHLD(true);
            case 0x32: return LDA_STA(false);
            case 0x3A: return LDA_STA(true);
            case 0xEB: return XCHG();
        }
        
        switch (opcode >> 6)
        {
            case 0x0:
            {
                switch (zz_zzx_xxx())
                {
                    case 0x1: return LXI();
                    case 0x2: return LDAX_STAX(false);
                    case 0xA: return LDAX_STAX(true);
                }
                
                switch (zz_zzz_xxx())
                {
                    case 0x6: return MVI();
                }
                break;
            }
            case 0x1:
                return MOV();
        }

        throw new Exception($"ILLEGAL OPCODE \"{opcode}\"");
    }
}
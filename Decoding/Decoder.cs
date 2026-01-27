namespace i8080_emulator.Decoding;
using Signaling.Cycles;
using Multiplexer;

public class Decoder : DecoderMux
{
    public Decoded Decode(byte ir)
    {
        if (FixedOpcodes.ContainsKey(ir))
            return FIXED(ir);
        
        switch (ir >> 6)
        {
            case 0b01:
                return MOV(ir);
        }

        throw new Exception($"ILLEGAL OPCODE \"{ir}\"");
    }
}
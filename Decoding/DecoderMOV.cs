namespace i8080_emulator.Decoding;
using Signaling;

public partial class DecoderMultiplexer
{
    // 01
    protected Decoded FamilyMOV(byte opcode)
    {
        Decoded decoded = new Decoded
        {            
            DataDriver = DataDrivers[BB_BBB_XXX(opcode)],
            DataLatcher = DataLatchers[BB_XXX_BBB(opcode)],
        };

        if (decoded.DataLatcher == DataLatcher.RAM)
        {
            decoded.Table.Add(MachineCycle.TMP_LATCH);
            decoded.Table.Add(MachineCycle.RAM_WRITE);
        }
        else
        {
            if (decoded.DataDriver == DataDriver.RAM)
                decoded.Table.Add(MachineCycle.RAM_READ);
            
            decoded.Table.Add(MachineCycle.BUS_LATCH);
        }
        
        return decoded; // 01 110 110 (0x76) is already HLT
    }
}
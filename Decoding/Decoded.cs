namespace i8080_emulator.Decoding;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public struct Decoded()
{
    public Register AddressDriver = Register.NONE;
    public Register DataDriver = Register.NONE;
    public AluAction? AluAction = null;
    public Register DataLatcher = Register.NONE;
    
    public Register[] Queue = [];
    
    public List<MicroCycle> MicroCycles = [];
}
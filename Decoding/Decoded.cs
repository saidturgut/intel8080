namespace i8080_emulator.Decoding;
using Signaling.Cycles;
using Signaling;

public struct Decoded()
{
    public Register AddressDriver = Register.NONE;
    public Register DataDriver = Register.NONE;
    public Register DataLatcher = Register.NONE;
    
    public List<MicroCycle> MicroCycles = [];
}
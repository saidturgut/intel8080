using i8080_emulator.Executing;

namespace i8080_emulator.Decoding;
using Executing;
using Executing.Computing;
using Signaling.Multiplexer;
using Signaling;

public struct Decoded()
{
    public Register AddressDriver = Register.NONE;
    public IncAction IncAction = IncAction.NONE;
    public Register DataDriver = Register.NONE;
    public AluAction? AluAction = null;
    public IoAction IoAction = IoAction.NONE;
    public Register DataLatcher = Register.NONE;
    
    public byte EncodeLatch = 0;
    
    public Register[] Queue = [];
    
    public List<MicroCycle> MicroCycles = [];
}
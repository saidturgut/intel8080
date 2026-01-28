namespace i8080_emulator.Executing.Computing;

public partial class AluRom
{
    protected static readonly Func<AluInput, AluOutput>[] Operations =
    [
        NONE, ADD ,SUB, AND, XOR, OR,
    ];
}

public enum Operation
{
    NONE, ADD, SUB, AND, XOR, OR,
}

public struct AluInput
{
    public Operation Operation;
    public byte A;
    public byte B;
    public byte C;
}

public struct AluOutput
{
    public byte Result;
    public byte Flags;
}

[Flags]
public enum PswFlag
{
    None = 0,
    Sign = 1 << 7,
    Zero = 1 << 6,
    //000000 = 1 <<  5,
    Auxiliary = 1 <<  4,
    //000000 = 1 <<  3,
    Parity = 1 <<  2,
    //111111 = 1 <<  1,
    Carry = 1 <<  0,
}

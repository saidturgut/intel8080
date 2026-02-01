namespace intel8080.Signaling.Microcodes;
using Executing.Computing;
using Signaling;

public partial class Microcode
{
    protected SignalSet[] BIT =>
    [
        ALU_COMPUTE(new AluAction
        {
            Operation = EncodedBitOperations[zz_xxx_zzz],
            FlagMask = zz_xxx_zzz switch
            {
                0x4 => FlagMasks[(byte)FlagMask.SZAPC], // DAA
                0x5 => Flag.None, // CMA
                _ => Flag.Carry
            },
            UseCarry = true,
        }, Register.A),
        
        REG_MOVE(Register.TMP, Register.A),
    ];

    protected SignalSet[] DAD =>
    [
        REG_MOVE(decodedPair[0], Register.TMP),
        
        ALU_COMPUTE(new AluAction
        {
            Operation = Operation.ADD,
            FlagMask = Flag.Carry,
            UseCarry = false,
        }, Register.HL_L),

        REG_MOVE(decodedPair[1], Register.TMP),
        
        ALU_COMPUTE(new AluAction
        {
            Operation = Operation.ADD,
            FlagMask = Flag.Carry,
            UseCarry = true,
        }, Register.HL_H),
    ];
}
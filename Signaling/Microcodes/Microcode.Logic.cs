namespace intel8080.Signaling.Microcodes;
using Executing.Computing;
using Signaling;

public partial class Microcode
{
    protected SignalSet[] ALU(bool native) =>
    [
        ..native ? EA_READ(decodedSource) : EA_IMM,
        
        ALU_COMPUTE(new AluAction
        {
            Operation = EncodedCoreOperations[zz_xxx_zzz],
            FlagMask = FlagMasks[(byte)FlagMask.SZAPC],
            UseCarry = zz_xxx_zzz is 0x1 or 3, // ADC, SBB, ADI, SUI
        }, Register.A),
        
        ..zz_xxx_zzz != 0x7 ? [REG_MOVE(Register.TMP, Register.A)] : NONE,
    ];
    
    protected SignalSet[] INR_DCR(bool inr) =>
    [
        ..EA_READ(decodedOperand),
        
        ALU_COMPUTE(new AluAction
        {
            Operation = inr ? Operation.INC : Operation.DEC,
            FlagMask = FlagMasks[(byte)FlagMask.SZAP],
            UseCarry = false,
        }, Register.TMP),
        
        ..EA_WRITE(decodedOperand),
    ];

    protected SignalSet[] INX_DCX(bool inx) =>
    [
        inx ? PAIR_INC(decodedPair[0]) : PAIR_DEC(decodedPair[0]),
    ];
}
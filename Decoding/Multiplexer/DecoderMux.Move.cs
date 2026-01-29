namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Cycles;
using Signaling;

// 00b AND 01b INSTRUCTIONS
public partial class DecoderMux
{
    protected static Decoded MOV() => new()
    {
        AddressDriver = Register.HL_L,
        DataDriver = EncodedRegisters[zz_zzz_xxx()],
        DataLatcher = EncodedRegisters[zz_xxx_zzz()],
        MicroCycles = [MicroCycle.MOVE_LOAD]
    };

    protected static Decoded MVI() => new()
    {
        AddressDriver = Register.HL_L,
        DataDriver = Register.TMP,
        DataLatcher = EncodedRegisters[zz_xxx_zzz()],
        MicroCycles = [MicroCycle.MOVE_IMM, MicroCycle.MOVE_LOAD,]
    };
    
    protected static Decoded LXI() => new()
    {
        DataDriver = Register.RAM,
        Queue = EncodedPairsSp[zz_xxz_zzz()],
        MicroCycles = [..MovePairImm]
    };

    protected static Decoded LDA_STA(bool lda) => new()
    {
        AddressDriver = Register.WZ_L,
        DataDriver = Register.RAM,
        DataLatcher = Register.A,
        Queue = [Register.WZ_L, Register.WZ_H],
        MicroCycles =
        [
            ..MovePairImm,
            lda ? MicroCycle.MOVE_LOAD : MicroCycle.MOVE_STORE,
        ]
    };

    protected static Decoded LDAX_STAX(bool ldax) => new()
    {
        AddressDriver = EncodedPairsSp[zz_xxz_zzz()][0],
        DataDriver = Register.RAM,
        DataLatcher = Register.A,
        MicroCycles = [ldax ? MicroCycle.MOVE_LOAD : MicroCycle.MOVE_STORE],
    };
    
    protected static Decoded LHLD_SHLD(bool lhld) => new()
    {
        AddressDriver = Register.WZ_L,
        DataDriver = Register.RAM,
        IncAction = IncAction.INC,
        Queue = [Register.WZ_L, Register.WZ_H, Register.HL_L, Register.HL_H],
        MicroCycles =
        [
            ..MovePairImm,
            ..lhld ? MovePairLoad : MovePairStore,
        ]
    };
    
    private static readonly MicroCycle[] MovePairImm = [MicroCycle.MOVE_PAIR_IMM, MicroCycle.MOVE_PAIR_IMM,];
    private static readonly MicroCycle[] MovePairLoad = [MicroCycle.MOVE_PAIR_LOAD, MicroCycle.MOVE_PAIR_LOAD,];
    private static readonly MicroCycle[] MovePairStore = [MicroCycle.MOVE_PAIR_STORE, MicroCycle.MOVE_PAIR_STORE,];
}
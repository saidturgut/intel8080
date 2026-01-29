namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Multiplexer;
using Signaling;

// 00b AND 01b INSTRUCTIONS
public partial class DecoderMux
{
    protected static Decoded MOV() => new()
    {
        AddressDriver = Register.HL_L,
        DataDriver = EncodedRegisters[zz_zzz_xxx()],
        DataLatcher = EncodedRegisters[zz_xxx_zzz()],
        MicroCycles = [MicroCycle.STORE_REG]
    };

    protected static Decoded MVI() => new()
    {
        AddressDriver = Register.HL_L,
        DataDriver = Register.TMP,
        DataLatcher = EncodedRegisters[zz_xxx_zzz()],
        MicroCycles = [MicroCycle.IMM_TMP, MicroCycle.STORE_REG,]
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
            lda ? MicroCycle.STORE_REG : MicroCycle.LOAD_REG,
        ]
    };

    protected static Decoded LDAX_STAX(bool ldax) => new()
    {
        AddressDriver = EncodedPairsSp[zz_xxz_zzz()][0],
        DataDriver = Register.RAM,
        DataLatcher = Register.A,
        MicroCycles = [ldax ? MicroCycle.STORE_REG : MicroCycle.LOAD_REG],
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
    
    private static readonly MicroCycle[] MovePairImm = [MicroCycle.IMM_PAIR, MicroCycle.IMM_PAIR,];
    private static readonly MicroCycle[] MovePairLoad = [MicroCycle.STORE_PAIR, MicroCycle.STORE_PAIR,];
    private static readonly MicroCycle[] MovePairStore = [MicroCycle.LOAD_PAIR, MicroCycle.LOAD_PAIR,];
}
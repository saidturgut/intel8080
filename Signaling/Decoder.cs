namespace intel8080.Signaling;
using Executing.Components;
using Microcodes;
using Signaling;

public class Decoder : Microcode
{
    public Psw Psw;

    public string DEBUG_NAME;
    
    public SignalSet[] Decode(byte ir)
    {
        if (StateInstructions.TryGetValue(ir, out var state))
        {
            DEBUG_NAME = state.ToString();
            return [CHANGE_STATE(state)];
        }
        
        UpdateOpcode(ir);
        
        switch (opcode >> 6)
        {
            case 0x0:
            {
                switch (opcode)
                {
                    case 0x22: DEBUG_NAME = "SHLD"; return LHLD_SHLD(false);
                    case 0x2A: DEBUG_NAME = "LHLD"; return LHLD_SHLD(true);
                    case 0x32: DEBUG_NAME = "STA"; return LDA_STA(false);
                    case 0x3A: DEBUG_NAME = "LDA"; return LDA_STA(true);
                }
                switch (zz_zzx_xxx)
                {
                    case 0x1: DEBUG_NAME = $"LXI {(RegisterPair)zz_xxz_zzz}"; return LXI;
                    case 0x2: DEBUG_NAME = $"STAX {(RegisterPair)zz_xxz_zzz}"; return LDAX_STAX(false);
                    case 0xA: DEBUG_NAME = $"LDAX {(RegisterPair)zz_xxz_zzz}"; return LDAX_STAX(true);
                    case 0x3: DEBUG_NAME = $"INX {(RegisterPair)zz_xxz_zzz}"; return INX_DCX(true);
                    case 0xB: DEBUG_NAME = $"DCX {(RegisterPair)zz_xxz_zzz}"; return INX_DCX(false);
                    case 0x9: DEBUG_NAME = $"DAD {(RegisterPair)zz_xxz_zzz}"; return DAD;
                }
                switch (zz_zzz_xxx)
                {
                    case 0x4: DEBUG_NAME = $"INR {decodedOperand}"; return INR_DCR(true);
                    case 0x5: DEBUG_NAME = $"DCR {decodedOperand}"; return INR_DCR(false);
                    case 0x6: DEBUG_NAME = $"MVI {decodedOperand}"; return MVI();
                    case 0x7: DEBUG_NAME = EncodedBitOperations[zz_xxx_zzz].ToString(); return BIT;
                }
                break;
            }
            case 0x1: DEBUG_NAME = $"MOV {decodedOperand}, {decodedSource}"; return MOV();
            case 0x2: DEBUG_NAME = $"{(AluOpcodes)zz_xxx_zzz} {decodedSource}"; return ALU(true);
            case 0x3:
            {
                switch (opcode)
                {
                    case 0xC3: DEBUG_NAME = "JMP"; return JMP(true);
                    case 0xCD: DEBUG_NAME = "CALL"; return CALL(true);
                    case 0xC9: DEBUG_NAME = "RET"; return RET(true);
                    case 0xDB: DEBUG_NAME = "IN"; return IO(true);
                    case 0xD3: DEBUG_NAME = "OUT"; return IO(false);
                    case 0xE3: DEBUG_NAME = "XTHL"; return XTHL;
                    case 0xE9: DEBUG_NAME = "PCHL"; return PCHL;
                    case 0xEB: DEBUG_NAME = "XCHG"; return XCHG;
                    case 0xF9: DEBUG_NAME = "SPHL"; return SPHL;
                }
                switch (zz_zzx_xxx)
                {
                    case 0x1: DEBUG_NAME = $"POP {(RegisterPair)zz_xxz_zzz}"; return POP(false);
                    case 0x5: DEBUG_NAME = $"PUSH {(RegisterPair)zz_xxz_zzz}"; return PUSH(false);
                }
                switch (zz_zzz_xxx)
                {
                    case 0x2: DEBUG_NAME = $"J{decodedCondition}"; return COND(JMP); 
                    case 0x4: DEBUG_NAME = $"C{decodedCondition}"; return COND(CALL);
                    case 0x0: DEBUG_NAME = $"R{decodedCondition}"; return COND(RET);
                    case 0x6: DEBUG_NAME = $"{((AluOpcodes)zz_xxx_zzz)}I"; return ALU(false);
                    case 0x7: DEBUG_NAME = $"RST {zz_xxx_zzz}"; return RST;
                }
                break;
            }
        }
        
        throw new Exception($"ILLEGAL OPCODE \"{opcode}\"");
    }

    private SignalSet[] COND(Func<bool, SignalSet[]> method) =>
        method(Psw.CheckCondition((Condition)zz_xxx_zzz));
}
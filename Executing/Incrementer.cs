using i8080_emulator.Executing.Computing;

namespace i8080_emulator.Executing;
using Signaling;

public partial class DataPath
{
    public void Incrementer()
    {
        if(signals.SideEffect is SideEffect.NONE or SideEffect.DECODE)
            return;
        
        // PROGRAM COUNTER
        if (signals.SideEffect == SideEffect.PC_INC)
        {
            PC_L++;
            if (PC_L == 0)
                PC_H++;
        }
        
        if (signals.SideEffect == SideEffect.SP_INC)
        {
            SP_L++;
            if (SP_L == 0)
                SP_H++;
        }
        if (signals.SideEffect == SideEffect.SP_DCR)
        {
            SP_L--;
            if (SP_L == 0xFF)
                SP_H--;
        }
        
        if (signals.SideEffect == SideEffect.BC_INC)
        {
            C++;
            if (C == 0)
                B++;
        }
        if (signals.SideEffect == SideEffect.BC_DCR)
        {
            C--;
            if (C == 0xFF)
                B--;
        }
        if (signals.SideEffect == SideEffect.DE_INC)
        {
            E++;
            if (E == 0)
                D++;
        }
        if (signals.SideEffect == SideEffect.DE_DCR)
        {
            E--;
            if (E == 0xFF)
                D--;
        }
        if (signals.SideEffect == SideEffect.HL_INC)
        {
            L++;
            if (L == 0)
                H++;
        }
        if (signals.SideEffect == SideEffect.HL_DCR)
        {
            L--;
            if (L == 0xFF)
                H--;
        }

        // CARRY FLAG CONTROLS
        if (signals.SideEffect == SideEffect.STC)
            FLAGS |= (byte)ALUFlags.Carry;
        if (signals.SideEffect == SideEffect.CMC)
            FLAGS ^= (byte)ALUFlags.Carry;
    }
}
namespace i8080_emulator.Signaling.Cycles;
using Decoding;

public partial class ControlUnitROM
{
    protected static Decoded decoded = new ();
    
    protected static readonly Dictionary<MachineCycle, Func<SignalSet>> MachineCyclesMethod = new()
    {
        { MachineCycle.FETCH, FETCH },
        
        { MachineCycle.RAM_READ_TMP, RAM_READ_TMP },
        { MachineCycle.RAM_WRITE_TMP, RAM_WRITE_TMP },
        { MachineCycle.RAM_READ_EXE, RAM_READ_EXE },
        { MachineCycle.RAM_WRITE_EXE, RAM_WRITE_EXE },

        { MachineCycle.RAM_READ_IMM, RAM_READ_IMM },
        { MachineCycle.RAM_READ_IMM_LOW, RAM_READ_IMM_LOW },
        { MachineCycle.RAM_READ_IMM_HIGH, RAM_READ_IMM_HIGH },

        { MachineCycle.INTERNAL_LATCH, INTERNAL_LATCH },
        { MachineCycle.TMP_LATCH, TMP_LATCH },

        { MachineCycle.ALU_EXECUTE, ALU_EXECUTE },

        { MachineCycle.MICRO_CYCLE, MICRO_CYCLE },
        
        { MachineCycle.CMA, CMA },
        { MachineCycle.INX_DCX, INX_DCX },

        { MachineCycle.RAM_READ_H, RAM_READ_H },
        { MachineCycle.RAM_WRITE_H, RAM_WRITE_H },
        
        { MachineCycle.COPY_RP_LOW, COPY_RP_LOW },
        { MachineCycle.COPY_RP_HIGH, COPY_RP_HIGH },
        
        { MachineCycle.CALL_LOW, CALL_LOW },
        { MachineCycle.CALL_HIGH, CALL_HIGH },
        
        { MachineCycle.RET_LOW, RET_LOW },
        { MachineCycle.RET_HIGH, RET_HIGH },
    };
}

public enum MachineCycle
{
    FETCH,
    RAM_READ_TMP, RAM_WRITE_TMP, 
    RAM_READ_EXE, RAM_WRITE_EXE, 

    RAM_READ_IMM,
    RAM_READ_IMM_LOW, RAM_READ_IMM_HIGH, 

    INTERNAL_LATCH,
    TMP_LATCH,
    
    ALU_EXECUTE,

    EMPTY, //FIXED INSTRUCTIONS
    
    MICRO_CYCLE,
    
    CMA,
    INX_DCX, 
    
    RAM_READ_H, RAM_WRITE_H,
    
    COPY_RP_LOW, COPY_RP_HIGH,
    
    CALL_LOW, CALL_HIGH,
    RET_LOW, RET_HIGH,
}
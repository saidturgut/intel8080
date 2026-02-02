using intel8080.Executing.Components;
using intel8080.Signaling;

namespace intel8080.Executing;

public partial class DataPath
{
    private readonly Reg[] Registers = new Reg[17];
    
    private readonly Rom Rom = new ();
    private readonly Ram Ram = new ();
    
    public SignalSet signals;
    
    private bool DEBUG_MODE;

    public void Init(bool debug)
    {
        DEBUG_MODE = debug;
        
        Rom.Init(Ram);
        Ram.Init();
        Tty.Init();
        Disk.Init();

        for (int i = 0; i < Registers.Length; i++)
            Registers[i] = new Reg();
        
        DebugInit();
        
        Reg(Register.PC_H).Set(Rom.JUMP);
        
        Psw.Update(Reg(Register.PSW).Get());
    }

    public void MemoryDump() 
        => Ram.MemoryDump();

    public void Receive(SignalSet input)
        => signals = input;

    public void Execute(string debugName)
    {
        DEBUG_NAME = debugName;
        switch (signals.MicroStep)
        {
            case MicroStep.REG_MOVE: RegisterMove(); break;
            case MicroStep.RAM_READ: RamRead(); break;
            case MicroStep.RAM_WRITE: RamWrite(); break;
            case MicroStep.PAIR_INC: Increment(); break;
            case MicroStep.PAIR_DEC: Decrement(); break;
            case MicroStep.ALU_COMPUTE: AluCompute(); break;
            case MicroStep.IO_READ: Input(); break;
            case MicroStep.IO_WRITE: Output(); break;
        }
        Psw.Update(Reg(Register.PSW).Get());
    }
    
    private Reg Reg(Register register) 
        => Registers[(byte)register];
    
    public byte GetIr() 
        => Reg(Register.IR).Get();
    
    private void PairSet(Register register, ushort value)
    {
        Reg(register).Set((byte)(value & 0xFF));
        Reg(register + 1).Set((byte)(value >> 8));
    }
    private ushort PairGet(Register register)
        => (ushort)(Reg(register).Get() + (Reg(register + 1).Get() << 8));
}
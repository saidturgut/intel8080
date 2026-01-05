namespace i8080_emulator.Executing;
using Computing;
using Signaling;

// ALU RESOLVER, ADDRESS BUFFER, MULTIPLEXER, INCREMENTER
public partial class DataPath
{
    private readonly RAM RAM = new ();
    private readonly ALU ALU = new ();
    
    private readonly Bus DBUS = new (); // DATA BUS 
    private readonly Bus ABUS_H = new (); 
    private readonly Bus ABUS_L = new (); 
    
    private byte PC_H, PC_L; // PROGRAM COUNTER
    private byte SP_H, SP_L; // STACK POINTER
    
    public byte IR; // INSTRUCTION REGISTER
    
    private byte A, // ACCUMULATOR
        B, C, D, E, // GENERAL REGISTERS
        H, L; // MEMORY ADDRESS REGISTERS

    private byte TMP; // BRIDGE BETWEEN RAM AND REGISTERS

    private byte FLAGS = 0x2;
    
    private SignalSet signals = new ();
    
    public void Init()
    {
        D = 0x12;
        E = 0x34;
        
        RAM.Init();
    }
    
    public void Clear()
    {        
        DBUS.Clear();
        ABUS_H.Clear();
        ABUS_L.Clear();
    }

    public void Set(SignalSet input)
    {
        signals = input;
    }

    public void Debug()
    {
        Console.WriteLine($"PROGRAM COUNTER : {(ushort)((PC_H << 8) + PC_L)}");
        Console.WriteLine($"IR : {IR}");
        Console.WriteLine($"B : {B}");
        Console.WriteLine($"C : {C}");
        Console.WriteLine($"D : {D}");
        Console.WriteLine($"E : {E}");
        Console.WriteLine($"H : {H}");
        Console.WriteLine($"L : {L}");
        Console.WriteLine($"A : {A}");
        Console.WriteLine($"HL : {(ushort)((H << 8) + L)}");
        Console.WriteLine($"SP : {(ushort)((SP_H << 8) + SP_L)}");
        Console.WriteLine(
            $"FLAGS : S={(FLAGS >> 7) & 1} Z={(FLAGS >> 6) & 1} AC={(FLAGS >> 4) & 1} P={(FLAGS >> 2) & 1} CY={(FLAGS >> 0) & 1}");
    }

    public void MemoryDump()
    {
        Console.WriteLine();
        foreach (var slot in RAM.MemoryDump)
        {
            Console.WriteLine($"MEMORY[{slot.Key}] : {slot.Value}");
        }
    }
}
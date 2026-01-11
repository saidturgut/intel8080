namespace i8080_emulator.External.Firmware;

public partial class BIOS
{
    // 0xE130
    private static readonly byte[] SELDSK =
    [
        0x21, 0x00, 0x00, // LXI H,0000
        0xC9, // RET
    ];
    
    // 0xE136
    private static readonly byte[] SETDMA =
    [
        0x60, // MOV H,B
        0x69, // MOV L,C
        0x22, 0x50, 0xE1, // SHLD DMAADDR
        0xC9, // RET
    ];
    
    // 0xE13B
    private static readonly byte[] READ =
    [
        0x3E, 0x01, // MVI A,01  (error)
        0xC9,       // RET
    ];
    
    // 0xE13E
    private static readonly byte[] WRITE =
    [
        0x3E, 0x01, // MVI A,01 (error)
        0xC9, // RET
    ];

    // 0xE144
    private static readonly byte[] SECTRAN =
    [
        0x60, // MOV H,B
        0x69, // MOV L,C
        0xC9, // RET
    ];
    
    // 0xE150
    private static readonly byte[] DMAADDR =
    [
        0x80, 0x00 // DMAADDR = 0080
    ];
}
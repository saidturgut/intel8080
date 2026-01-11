namespace i8080_emulator.External.Firmware;

public partial class BIOS
{
    // 0xE110 ** CHECK INPUT BUFFER EMPTY OR NOT
    private static readonly byte[] CONST =
    [
        0xDB, 0x00, // IN 00
        0xE6, 0x01, // ANI 01
        0xC9 // RET
    ];

    // 0xE118 ** ADD DATA TO INPUT BUFFER
    private static readonly byte[] CONIN =
    [
        0xDB, 0x00,        // IN 00
        0xE6, 0x01,        // ANI 01
        0xCA, 0x18, 0xE1,  // JZ E118
        0xDB, 0x01,        // IN 01
        0xC9               // RET
    ];

    // 0xE125 ** REMOVE DATA FROM INPUT BUFFER
    private static readonly byte[] CONOUT =
    {
        0x79, // MOV A,C
        0xD3, 0x02, // OUT 02
        0xC9 // RET
    };
}
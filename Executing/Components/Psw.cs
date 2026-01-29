namespace i8080_emulator.Executing.Components;
using Computing;

public class Psw
{
    public static bool Sign;
    public static bool Zero;
    public static bool Auxiliary;
    public static bool Parity;
    public static bool Carry;

    public void Update(byte psw)
    {
        Sign = (psw & (byte)PswFlag.Sign) != 0;
        Zero = (psw & (byte)PswFlag.Zero) != 0;
        Auxiliary = (psw & (byte)PswFlag.Auxiliary) != 0;
        Parity = (psw & (byte)PswFlag.Parity) != 0;
        Carry = (psw & (byte)PswFlag.Carry) != 0;
    }

    public bool Condition(byte type) => type switch
    {
        0b000 => !Zero,
        0b001 => Zero,
        0b010 => !Carry,
        0b011 => Carry,
        0b100 => !Parity,
        0b101 => Parity,
        0b110 => !Sign,
        0b111 => Sign,
        _ => false
    };
}
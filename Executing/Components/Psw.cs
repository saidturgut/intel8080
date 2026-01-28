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
}
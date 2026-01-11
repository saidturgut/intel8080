namespace i8080_emulator.External;

internal static class Run
{
    private static readonly CPU Cpu = new ();

    private static void Main() => Cpu.PowerOn();
}
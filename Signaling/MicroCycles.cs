namespace i8080_emulator.Signaling;
public partial class ControlUnit
{
    private static SignalSet STC(byte tState) => new () { SideEffect = SideEffect.STC };
    private static SignalSet CMC(byte tState) => new () { SideEffect = SideEffect.CMC };
    private static SignalSet INX_DCX(byte tState) => new () { SideEffect = decoded.SideEffect };
}
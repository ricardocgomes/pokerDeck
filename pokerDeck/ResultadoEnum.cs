using System.ComponentModel;

namespace pokerDeck
{
    public enum ResultadoEnum
    {
        None = 0,
        [Description("Black wins")]
        Black = 1,
        [Description("White wins")]
        White = 2,
        [Description("Tie")]
        Tie = 3,
    }
}

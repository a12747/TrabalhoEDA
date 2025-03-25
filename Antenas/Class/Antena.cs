namespace Antenas.Class;

/// <summary>
/// Classe Antena
/// </summary>
public class Antena
{
    public char Frequencia { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }

    /// <summary>
    /// Construtor Antena
    /// </summary>
    /// <param name="Frequencia">Frequência</param>
    /// <param name="PosX">Posição X</param>
    /// <param name="PosY">Posição Y</param>
    public Antena(char Frequencia, int PosX, int PosY)
    {
        this.Frequencia = Frequencia;
        this.PosX = PosX;
        this.PosY = PosY;
    }
}
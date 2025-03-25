namespace Antenas.Classes;

/// <summary>
/// Classe de lista de antenas
/// </summary>
public class Lista
{
    private LinkedList<Antena> lista = new();
    private LinkedList<(int, int)> listaErros = new();

    /// <summary>
    /// Adicionar uma nova antena
    /// </summary>
    /// <param name="antena">Nova Antena</param>
    public void Adicionar(Antena antena)
    {
        lista.AddLast(antena);
    }

    /// <summary>
    /// Remover uma antena
    /// </summary>
    /// <param name="antena">Antena a remover</param>
    public void Remover(int x, int y)
    {
        int count = 0;
        if (lista.Any())
        {
            var node = lista.First;
            
            while (node != null)
            {
                if (node.Value.PosX == x && node.Value.PosY == y)
                {
                    lista.Remove(node);
                    count++;
                    break;
                }
                node = node.Next;
            }

            if (count == 0)
            {
                Console.WriteLine("Nenhuma antena para remover!");
            }
            else
            {
                Console.WriteLine("Antena removida!");
            }
        }
        else
        {
            Console.WriteLine("Nenhuma antena para remover!");
        }
    }

    /// <summary>
    /// Mostrar Antenas
    /// </summary>
    public void Mostrar()
    {
        if (lista.Any())
        {
            foreach (var antena in lista)
            {
                Console.WriteLine($"Frequência: {antena.Frequencia}, Posição: ({antena.PosX}, {antena.PosY})");
            }
        }
        else
        {
            Console.WriteLine("Nenhuma antena adicionada!");
        }
    }
    
    /// <summary>
    /// Carregar ficheiro de Antenas
    /// </summary>
    /// <param name="caminho">Caminho do ficheiro</param>
    public void CarregarFicheiro(string caminho)
    {
        if (!File.Exists(caminho))
        {
            Console.WriteLine("Ficheiro não encontrado.");
            return;
        }
        
        string[] linhas = File.ReadAllLines(caminho);
        for (int i = 0; i < linhas.Length; i++)
        {
            for (int j = 0; j < linhas[i].Length; j++)
            {
                if (char.IsLetterOrDigit(linhas[i][j]))
                {
                    Antena novaAntena = new Antena(linhas[i][j], j, i);
                    Adicionar(novaAntena);
                }
            }
        }
        
        ValidarErros();
    }
    
    /// <summary>
    /// Validar Erros de Efeitos Nefastos
    /// </summary>
    public void ValidarErros()
    {
        if (lista.Any())
        {
            listaErros.Clear();
            var antenasArray = lista.ToList();

            // Calcular limites máximos com base nas antenas existentes
            int largura = antenasArray.Any() ? antenasArray.Max(a => a.PosX) + 1 : 0;
            int altura = antenasArray.Any() ? antenasArray.Max(a => a.PosY) + 1 : 0;

            for (int i = 0; i < antenasArray.Count; i++)
            {
                for (int j = i + 1; j < antenasArray.Count; j++)
                {
                    var a1 = antenasArray[i];
                    var a2 = antenasArray[j];

                    if (a1.Frequencia != a2.Frequencia) continue;

                    int dx = a2.PosX - a1.PosX;
                    int dy = a2.PosY - a1.PosY;

                    if (dx % 2 != 0 || dy % 2 != 0) continue;

                    int px1 = a1.PosX - dx;
                    int py1 = a1.PosY - dy;
                    if (Limites(px1, py1, largura, altura))
                        listaErros.AddLast((px1, py1));

                    int px2 = a2.PosX + dx;
                    int py2 = a2.PosY + dy;
                    if (Limites(px2, py2, largura, altura))
                        listaErros.AddLast((px2, py2));
                }
            }
        }
        else
        {
            Console.WriteLine("Nenhuma antena adicionada!");
        }
    }
    
    /// <summary>
    /// Calcular limites da listagem
    /// </summary>
    /// <param name="x">X</param>
    /// <param name="y">Y</param>
    /// <param name="largura">Largura da lista</param>
    /// <param name="altura">Altura da lista</param>
    /// <returns></returns>
    private bool Limites(int x, int y, int largura, int altura)
    {
        return x >= 0 && y >= 0 && x < largura && y < altura;
    }
    
    /// <summary>
    /// Mostrar listagem de erros nefastos
    /// </summary>
    public void MostrarErros()
    {
        if (listaErros.Any())
        {
            Console.WriteLine("Localizações com efeito nefasto:");
            foreach (var localizacao in listaErros)
            {
                Console.WriteLine($"Coordenadas: ({localizacao.Item1}, {localizacao.Item2})");
            }
        }
        else
        {
            Console.WriteLine("Nenhuma antena adicionada!");
        }
    }
}
using Antenas.Classes;

class Program
{
    static void Main()
    {
        Lista lista = new Lista();
        
        string caminho = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "Teste2.txt");
        lista.CarregarFicheiro(caminho);
        
        while (true)
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("1 - Mostrar antenas");
            Console.WriteLine("2 - Mostrar efeitos nefastos");
            Console.WriteLine("3 - Adicionar antena");
            Console.WriteLine("4 - Remover antena");
            Console.WriteLine("5 - Sair");
            Console.Write("Opção: ");
            string opcao = Console.ReadLine();
            
            switch (opcao)
            {
                case "1":
                    lista.Mostrar();
                    break;
                case "2":
                    lista.MostrarErros();
                    break;
                case "3":
                    Console.Write("Introduza a frequência da antena: ");
                    char freq = Console.ReadKey().KeyChar;
                    Console.Write("Introduza a posição X: ");
                    int x = int.Parse(Console.ReadLine());
                    Console.Write("Introduza a posição Y: ");
                    int y = int.Parse(Console.ReadLine());
                    lista.Adicionar(new Antena(freq, x, y));
                    lista.ValidarErros();
                    break;
                case "4":
                    Console.Write("Introduza a posição X: ");
                    int xr = int.Parse(Console.ReadLine());
                    Console.Write("Introduza a posição Y: ");
                    int yr = int.Parse(Console.ReadLine());
                    lista.Remover(xr, yr);
                    lista.ValidarErros();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Opção inválida, tente novamente.");
                    break;
            }
        }
    }
}
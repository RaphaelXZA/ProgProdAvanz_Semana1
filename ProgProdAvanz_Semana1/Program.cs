using ProgProdAvanz_Semana1;

internal class Program
{
    private static void Main(string[] args)
    {
        IGame game = new Game();
        game.Execute();
    }
}
using System;
using System.Threading;

// ============================================
// CLASSE BASE - MÁQUINA
// ============================================
public abstract class Maquina
{
    protected bool ligada;

    public bool Ligada
    {
        get { return ligada; }
    }

    public string Nome { get; protected set; }

    public Maquina(string nome)
    {
        Nome = nome;
        ligada = false;
    }

    public virtual void Ligar()
    {
        if (!ligada)
        {
            ligada = true;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Nome} ligado(a).");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine($"{Nome} já está ligado(a).");
        }
    }

    public virtual void Desligar()
    {
        if (ligada)
        {
            ligada = false;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Nome} desligado(a).");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine($"{Nome} já está desligado(a).");
        }
    }

    public override string ToString()
    {
        return $"{Nome}: {(ligada ? "LIGADO" : "DESLIGADO")}";
    }
}

// ============================================
// CLASSE FORNO INDUSTRIAL
// ============================================
public class FornoIndustrial : Maquina
{
    public FornoIndustrial() : base("Forno Industrial") { }
}

// ============================================
// CLASSE ESTEIRA
// ============================================
public class Esteira : Maquina
{
    public Esteira() : base("Esteira Transportadora") { }
}

// ============================================
// CLASSE PRENSA HIDRÁULICA
// ============================================
public class PrensaHidraulica : Maquina
{
    public PrensaHidraulica() : base("Prensa Hidráulica") { }
}

// ============================================
// FÁBRICA DE MÁQUINAS
// ============================================
public static class FabricaMaquinas
{
    public static Maquina CriarMaquina(string tipo)
    {
        switch (tipo.ToLower())
        {
            case "forno":
                return new FornoIndustrial();
            case "esteira":
                return new Esteira();
            case "prensa":
                return new PrensaHidraulica();
            default:
                throw new ArgumentException("Tipo de máquina desconhecido");
        }
    }
}

// ============================================
// SISTEMA DE CONTROLE
// ============================================
public class SistemaControle
{
    private Maquina[] maquinas;

    public SistemaControle()
    {
        maquinas = new Maquina[]
        {
            new FornoIndustrial(),
            new Esteira(),
            new PrensaHidraulica()
        };
    }

    public void ExibirMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n===================== MENU =====================");
        Console.ResetColor();

        Console.WriteLine("1 - Ligar Forno Industrial");
        Console.WriteLine("2 - Desligar Forno Industrial");
        Console.WriteLine("3 - Ligar Esteira Transportadora");
        Console.WriteLine("4 - Desligar Esteira Transportadora");
        Console.WriteLine("5 - Ligar Prensa Hidráulica");
        Console.WriteLine("6 - Desligar Prensa Hidráulica");
        Console.WriteLine("7 - Ver Estado de Todas as Máquinas");
        Console.WriteLine("0 - Encerrar Sistema");
    }

    public void Executar()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("==================================================");
        Console.WriteLine("      SISTEMA DE CONTROLE DE MÁQUINAS - POO");
        Console.WriteLine("==================================================");
        Console.ResetColor();

        bool executando = true;

        while (executando)
        {
            ExibirMenu();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nEscolha uma opção: ");
            Console.ResetColor();

            string opcao = Console.ReadLine();
            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    maquinas[0].Ligar();
                    break;

                case "2":
                    maquinas[0].Desligar();
                    break;

                case "3":
                    maquinas[1].Ligar();
                    break;

                case "4":
                    maquinas[1].Desligar();
                    break;

                case "5":
                    maquinas[2].Ligar();
                    break;

                case "6":
                    maquinas[2].Desligar();
                    break;

                case "7":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("===== ESTADO DAS MÁQUINAS =====");
                    Console.ResetColor();
                    foreach (var maquina in maquinas)
                    {
                        Console.WriteLine(maquina);
                    }
                    break;

                case "0":
                    executando = false;
                    Console.WriteLine("Encerrando sistema...");
                    Thread.Sleep(300);
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nSistema encerrado com sucesso.");
        Console.ResetColor();
    }
}

// ============================================
// PROGRAMA PRINCIPAL
// ============================================
public class SmartTechPrograma
{
    public static void Main(string[] args)
    {
        SistemaControle sistema = new SistemaControle();
        sistema.Executar();
    }
}
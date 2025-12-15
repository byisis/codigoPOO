using System;
using System.Drawing;
using System.Windows.Forms;

// ============================================
// CLASSE BASE - MÁQUINA
// ============================================
public abstract class Maquina
{
    protected bool ligada;
    public string Nome { get; protected set; }
    public bool Ligada => ligada;

    public Maquina(string nome)
    {
        Nome = nome;
        ligada = false;
    }

    public virtual string Ligar(out Color cor)
    {
        if (!ligada)
        {
            ligada = true;
            cor = Color.Green;
            return $"{Nome} ligado(a).";
        }
        cor = Color.Orange;
        return $"{Nome} já estava ligado(a).";
    }

    public virtual string Desligar(out Color cor)
    {
        if (ligada)
        {
            ligada = false;
            cor = Color.Red;
            return $"{Nome} desligado(a).";
        }
        cor = Color.Orange;
        return $"{Nome} já estava desligado(a).";
    }

    public override string ToString()
    {
        return $"{Nome}: {(ligada ? "LIGADO" : "DESLIGADO")}";
    }
}

// ============================================
// MÁQUINAS / DISPOSITIVOS IoT
// ============================================
public class FornoIndustrial : Maquina
{
    public FornoIndustrial() : base("Forno Industrial") { }
}

public class Esteira : Maquina
{
    public Esteira() : base("Esteira Transportadora") { }
}

public class PrensaHidraulica : Maquina
{
    public PrensaHidraulica() : base("Prensa Hidráulica") { }
}

public class RoboIndustrial : Maquina
{
    public RoboIndustrial() : base("Robô Industrial") { }
}

public class CompressorAr : Maquina
{
    public CompressorAr() : base("Compressor de Ar") { }
}

public class SistemaRefrigeracao : Maquina
{
    public SistemaRefrigeracao() : base("Sistema de Refrigeração") { }
}

public class SensorTemperatura : Maquina
{
    public SensorTemperatura() : base("Sensor de Temperatura") { }
}

public class SensorVibracao : Maquina
{
    public SensorVibracao() : base("Sensor de Vibração") { }
}

// ============================================
// FRONTEND - INTERFACE GRÁFICA
// ============================================
public class TelaPrincipal : Form
{
    Maquina[] maquinas;
    Label lblStatus;
    Label lblMensagem;

    public TelaPrincipal()
    {
        Text = "SmartTech IoT - Controle Industrial";
        Size = new Size(650, 550);

        maquinas = new Maquina[]
        {
            new FornoIndustrial(),
            new Esteira(),
            new PrensaHidraulica(),
            new RoboIndustrial(),
            new CompressorAr(),
            new SistemaRefrigeracao(),
            new SensorTemperatura(),
            new SensorVibracao()
        };

        int y = 20;

        foreach (var maquina in maquinas)
        {
            Button btnOn = new Button
            {
                Text = $"Ligar {maquina.Nome}",
                Location = new Point(20, y),
                Width = 230
            };

            Button btnOff = new Button
            {
                Text = $"Desligar {maquina.Nome}",
                Location = new Point(270, y),
                Width = 230
            };

            btnOn.Click += (s, e) =>
            {
                Color cor;
                string msg = maquina.Ligar(out cor);
                MostrarMensagem(msg, cor);
            };

            btnOff.Click += (s, e) =>
            {
                Color cor;
                string msg = maquina.Desligar(out cor);
                MostrarMensagem(msg, cor);
            };

            Controls.Add(btnOn);
            Controls.Add(btnOff);

            y += 35;
        }

        // BOTÃO DESLIGAR TUDO
        Button btnDesligarTudo = new Button
        {
            Text = "DESLIGAR TODAS AS MÁQUINAS",
            Location = new Point(20, y + 10),
            Width = 480,
            Height = 40,
            BackColor = Color.DarkRed,
            ForeColor = Color.White
        };

        btnDesligarTudo.Click += (s, e) =>
        {
            foreach (var m in maquinas)
                m.Desligar(out _);

            MostrarMensagem("⚠ Todas as máquinas foram desligadas.", Color.Red);
        };

        lblStatus = new Label
        {
            Location = new Point(20, y + 60),
            Size = new Size(600, 170),
            BorderStyle = BorderStyle.FixedSingle
        };

        lblMensagem = new Label
        {
            Location = new Point(20, y + 240),
            Size = new Size(600, 45),
            BorderStyle = BorderStyle.FixedSingle,
            Font = new Font("Arial", 10, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.LightGray,
            ForeColor = Color.White
        };

        Controls.Add(btnDesligarTudo);
        Controls.Add(lblStatus);
        Controls.Add(lblMensagem);

        AtualizarStatus();
    }

    void MostrarMensagem(string texto, Color cor)
    {
        lblMensagem.Text = texto;
        lblMensagem.BackColor = cor;
        AtualizarStatus();
    }

    void AtualizarStatus()
    {
        lblStatus.Text = "";
        foreach (var m in maquinas)
            lblStatus.Text += m + Environment.NewLine;
    }
}

// ============================================
// MAIN
// ============================================
static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new TelaPrincipal());
    }
}




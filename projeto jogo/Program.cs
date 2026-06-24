namespace projeto_jogo;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        MessageBox.Show("Entrando no quadrante inimigo. A nave capitânia adversária NYRA detectou sua presença. Prepare os escudos e acione os lasers!", "ALERTA DE COMBATE");

        Application.Run(new Form2());

        MessageBox.Show("Transmissão cósmica encerrada. Retornando para a base aliada hiperespaço.", "STATUS DA MISSÃO");
    }
}
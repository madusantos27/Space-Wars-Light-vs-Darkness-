using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Timer = System.Windows.Forms.Timer;

namespace projeto_jogo;

public partial class Form2 : Form
{
   
    PictureBox fundo = new PictureBox();
    PictureBox jogador = new PictureBox();
    PictureBox nyra = new PictureBox();

    Label lblVidaJogador = new Label();
    Label lblVidaNyra = new Label();
    Label lblPontos = new Label();
    Label lblFase = new Label();

    
    Panel barraHPJogador = new Panel();
    Panel barraHPNyra = new Panel();
    Panel fundoHPJogador = new Panel();
    Panel fundoHPNyra = new Panel();

    Timer gameTimer = new Timer();

    
    int hpJogador = 100;
    int hpNyra = 100; 
    int pontos = 0;

    
    bool cima;
    bool baixo;
    bool esquerda;
    bool direita;

   
    int direcaoNyra = 5;
    int cooldownNyra = 0;
    
  
    int cooldownJogador = 0; 

    List<PictureBox> tirosJogador = new List<PictureBox>();
    List<PictureBox> tirosNyra = new List<PictureBox>();

    public Form2()
    {
      
        Width = 1200;
        Height = 700;
        Text = "Space Wars - Fase 2";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        KeyPreview = true;
        BackColor = Color.Black;

     
        CriarFundo(); 
        CriarJogador();
        CriarNyra();
        CriarHUD();

        gameTimer.Interval = 20;
        gameTimer.Tick += GameLoop;
        gameTimer.Start();

      
        KeyDown += Form2_KeyDown;
        KeyUp += Form2_KeyUp;
    }

    private Image CarregarImagem(string nome)
    {
        string caminho = Path.Combine(Application.StartupPath, "img", nome);
        return Image.FromFile(caminho);
    }

    private void CriarFundo()
    {
        try
        {
            fundo.Image = CarregarImagem("fundo2.png");
        }
        catch
        {
            fundo.BackColor = Color.FromArgb(10, 10, 25);
        }

        fundo.SizeMode = PictureBoxSizeMode.StretchImage;
        fundo.Width = 1200;
        fundo.Height = 700;
        fundo.Left = 0;
        fundo.Top = 0;

        Controls.Add(fundo);
    }

    private void CriarJogador()
    {
        try
        {
            jogador.Image = CarregarImagem("nave_jogador.png");
        }
        catch
        {
            jogador.BackColor = Color.Blue;
        }

        jogador.SizeMode = PictureBoxSizeMode.StretchImage;
        jogador.Width = 90;
        jogador.Height = 90;
        jogador.Left = 60;
        jogador.Top = 300;

        jogador.Parent = fundo; 
        jogador.BackColor = Color.Transparent;
    }

    private void CriarNyra()
    {
        try
        {
            nyra.Image = CarregarImagem("nave_inimiga_2.png");
        }
        catch
        {
            nyra.BackColor = Color.Red;
        }

        nyra.SizeMode = PictureBoxSizeMode.StretchImage;
        nyra.Width = 160;
        nyra.Height = 160;
        nyra.Left = 950;
        nyra.Top = 250;

        nyra.Parent = fundo;
        nyra.BackColor = Color.Transparent;
    }

    private void CriarHUD()
    {
  
        lblFase.Text = "FASE 2: CONFRONTO COM NYRA";
        lblFase.ForeColor = Color.Cyan;
        lblFase.BackColor = Color.Transparent;
        lblFase.Font = new Font("Calibri", 14, FontStyle.Bold);
        lblFase.AutoSize = true;
        fundo.Controls.Add(lblFase);
        lblFase.Left = (Width / 2) - (lblFase.PreferredWidth / 2);
        lblFase.Top = 15;

       
        lblPontos.ForeColor = Color.Yellow;
        lblPontos.BackColor = Color.Transparent;
        lblPontos.Font = new Font("Calibri", 12, FontStyle.Bold);
        lblPontos.AutoSize = true;
        fundo.Controls.Add(lblPontos);
        lblPontos.Text = "PONTOS: 0000";
        lblPontos.Left = (Width / 2) - (lblPontos.PreferredWidth / 2);
        lblPontos.Top = 45;


        lblVidaJogador.Text = "Nave Aliada (PLAYER)";
        lblVidaJogador.ForeColor = Color.White;
        lblVidaJogador.BackColor = Color.Transparent;
        lblVidaJogador.Font = new Font("Calibri", 12, FontStyle.Bold);
        lblVidaJogador.Left = 25;
        lblVidaJogador.Top = 15;
        lblVidaJogador.AutoSize = true;
        fundo.Controls.Add(lblVidaJogador);

        fundoHPJogador.BackColor = Color.FromArgb(60, 15, 15);
        fundoHPJogador.Width = 200;
        fundoHPJogador.Height = 14;
        fundoHPJogador.Left = 25;
        fundoHPJogador.Top = 42;
        fundo.Controls.Add(fundoHPJogador);

        barraHPJogador.BackColor = Color.SpringGreen;
        barraHPJogador.Width = 200;
        barraHPJogador.Height = 14;
        barraHPJogador.Left = 25;
        barraHPJogador.Top = 42;
        fundo.Controls.Add(barraHPJogador);
        barraHPJogador.BringToFront(); 

      
        lblVidaNyra.Text = "Nave Capitânia (NYRA)";
        lblVidaNyra.ForeColor = Color.Crimson;
        lblVidaNyra.BackColor = Color.Transparent;
        lblVidaNyra.Font = new Font("Calibri", 12, FontStyle.Bold);
        lblVidaNyra.AutoSize = true;
        fundo.Controls.Add(lblVidaNyra);
        lblVidaNyra.Left = Width - 250;
        lblVidaNyra.Top = 15;

        fundoHPNyra.BackColor = Color.FromArgb(60, 15, 15);
        fundoHPNyra.Width = 200;
        fundoHPNyra.Height = 14;
        fundoHPNyra.Left = Width - 250;
        fundoHPNyra.Top = 42;
        fundo.Controls.Add(fundoHPNyra);

        barraHPNyra.BackColor = Color.Red;
        barraHPNyra.Width = 200;
        barraHPNyra.Height = 14;
        barraHPNyra.Left = Width - 250;
        barraHPNyra.Top = 42;
        fundo.Controls.Add(barraHPNyra);
        barraHPNyra.BringToFront(); 
    }

    private void GameLoop(object sender, EventArgs e)
    {
        // Movimento do jogador
        if (esquerda) jogador.Left -= 8;
        if (direita) jogador.Left += 8;
        if (cima) jogador.Top -= 8;
        if (baixo) jogador.Top += 8;

 
        if (jogador.Left < 0) jogador.Left = 0;
        if (jogador.Top < 80) jogador.Top = 80; 
        if (jogador.Right > fundo.Width)
            jogador.Left = fundo.Width - jogador.Width;
        if (jogador.Bottom > fundo.Height)
            jogador.Top = fundo.Height - jogador.Height;

        nyra.Top += direcaoNyra;
        if (nyra.Top < 100 || nyra.Top > 500)
            direcaoNyra *= -1;

        if (cooldownJogador > 0) cooldownJogador--;

     
        for (int i = tirosJogador.Count - 1; i >= 0; i--)
        {
            tirosJogador[i].Left += 16;

            if (tirosJogador[i].Bounds.IntersectsWith(nyra.Bounds))
            {
                hpNyra -= 10;
                pontos += 15;

                fundo.Controls.Remove(tirosJogador[i]);
                tirosJogador.RemoveAt(i);
                continue;
            }

            if (tirosJogador[i].Left > fundo.Width)
            {
                fundo.Controls.Remove(tirosJogador[i]);
                tirosJogador.RemoveAt(i);
            }
        }

        
        AtirarNyra();

        for (int i = tirosNyra.Count - 1; i >= 0; i--)
        {
            tirosNyra[i].Left -= 13;

            if (tirosNyra[i].Bounds.IntersectsWith(jogador.Bounds))
            {
                hpJogador -= 10;

                fundo.Controls.Remove(tirosNyra[i]);
                tirosNyra.RemoveAt(i);
                continue;
            }

            if (tirosNyra[i].Left < 0)
            {
                fundo.Controls.Remove(tirosNyra[i]);
                tirosNyra.RemoveAt(i);
            }
        }

       
        lblVidaJogador.Text = $"Nave Aliada: {hpJogador}%";
        lblVidaNyra.Text = $"Nyra Boss: {hpNyra}%";
        lblPontos.Text = $"PONTOS: {pontos:D4}";

        
        barraHPJogador.Width = Math.Max(0, hpJogador * 2);
        barraHPNyra.Width = Math.Max(0, hpNyra * 2);

        // Regra de fim de jogo
        if (hpJogador <= 0)
        {
            gameTimer.Stop();
            MessageBox.Show("Você foi reduzido a poeira cósmica pela Nyra!");
            Close();
        }

        if (hpNyra <= 0)
        {
            pontos += 1500;
            gameTimer.Stop();
            MessageBox.Show($"MISSÃO CUMPRIDA!\nNyra foi derrotada!\nPontuação Final: {pontos}");
            Close();
        }
    }

    private void AtirarNyra()
    {
        cooldownNyra++;

        if (cooldownNyra > 35) 
        {
            PictureBox tiro = new PictureBox();
            tiro.BackColor = Color.Fuchsia; 
            tiro.Width = 14;
            tiro.Height = 4;

            tiro.Left = nyra.Left;
            tiro.Top = nyra.Top + (nyra.Height / 2) - 2;

            tirosNyra.Add(tiro);
            fundo.Controls.Add(tiro); 

            cooldownNyra = 0;
        }
    }

    private void Form2_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Left) esquerda = true;
        if (e.KeyCode == Keys.Right) direita = true;
        if (e.KeyCode == Keys.Up) cima = true;
        if (e.KeyCode == Keys.Down) baixo = true;

        if (e.KeyCode == Keys.Space && cooldownJogador == 0)
        {
            PictureBox tiro = new PictureBox();
            tiro.BackColor = Color.Cyan; 
            tiro.Width = 14;
            tiro.Height = 4;

            tiro.Left = jogador.Right;
            tiro.Top = jogador.Top + (jogador.Height / 2) - 2;

            tirosJogador.Add(tiro);
            fundo.Controls.Add(tiro); 

            cooldownJogador = 10; 
        }
    }

    private void Form2_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Left) esquerda = false;
        if (e.KeyCode == Keys.Right) direita = false;
        if (e.KeyCode == Keys.Up) cima = false;
        if (e.KeyCode == Keys.Down) baixo = false;
    }
}
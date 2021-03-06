﻿using NextteamBr.PainelAdministraivo;
using System;
using System.Windows.Forms;

namespace NextteamBr
{
    public partial class Frm_Escolha : Form
    {
        int IdUsuario;
        int IDEmpresa;
        bool Admin;

        public Frm_Escolha(int p_id, bool admin, int IDempresa)
        {
            InitializeComponent();
            Admin = admin;
            IdUsuario = p_id;
            IDEmpresa = IDempresa;
        }

        private void Btm_LogBook_Click(object sender, EventArgs e)
        {
            var frm_Principal = new Frm_Principal(IdUsuario, IDEmpresa);

            frm_Principal.Show();
        }

        private void Btm_JobSync_Click(object sender, EventArgs e)
        {
            var frm_informacoes = new Frm_InformacoesPessoais(IdUsuario);

            frm_informacoes.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm_rankings = new Frm_RankingMotoristas(IDEmpresa);

            frm_rankings.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Admin)
            {
                var frm_ADM = new Frm_MDI();

                frm_ADM.Show();
            }
            else
            {
                MessageBox.Show("Somente administradores podem ter acesso a essa funcionalidade!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_Plugin_Click(object sender, EventArgs e)
        {
            try
            {
                if (Plugin.VerificarPastaETS())//Verfica se a pasta do ETS esta padrão
                {
                    Plugin.Copiar($@"{Application.StartupPath}\Plugins\", Plugin.DiretorioPadraoETS);

                    MessageBox.Show("Os plugins foram instalads com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Não foi possível encontrar o diretório do Eurotruck 2 automaticamente. Por favor indique o direório do seu jogo para que os plugins possam ser instalados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    if (folderBrowserDialogets.ShowDialog() == DialogResult.OK)
                    {
                        Plugin.Copiar($@"{Application.StartupPath}\Plugins\", folderBrowserDialogets.SelectedPath);

                        MessageBox.Show("Os plugins foram instalads com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (MessageBox.Show("Continua com problemas no APP? Instale o Visual C++.", "Visual C++", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("https://www.microsoft.com/en-gb/download/details.aspx?id=48145");
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Não foi possível efetuar a instalação dos plugins! Execute o software como administrador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var frm_Empresa = new Frm_Empresas();

            frm_Empresa.Show();
        }
    }
}
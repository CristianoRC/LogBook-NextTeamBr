﻿using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace NextteamBr
{
    public partial class Frm_Login : Form
    {
        public Frm_Login()
        {
            InitializeComponent();
        }

        private void logar()
        {
            if (!String.IsNullOrWhiteSpace(Txt_Login.Text) && !String.IsNullOrWhiteSpace(Txt_Senha.Text))
            {
                var usuario = new Motorista(Txt_Login.Text, Txt_Senha.Text);

                var resultado = usuario.Logar();

                if (resultado == "1")
                {
                    Motorista.SalvarUltimoLogin(Txt_Login.Text);

                    var usuarioTemp = MotoristaService.ObterInformacoes(Txt_Login.Text);

                    ChamarFormularioDeEscolha(usuarioTemp.ID, usuarioTemp.Admin, usuarioTemp.IDEmpresa);
                }
                else
                {
                    MessageBox.Show("Login ou senha inválidos!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Insira seu login e senha!", "Alterta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Btm_Sair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Btm_Logar_Click(object sender, EventArgs e)
        {
            logar();
        }

        private void ChamarFormularioDeEscolha(int p_ID, bool admin, int empresa)
        {
            var frm_Principal = new Frm_Escolha(p_ID, admin, empresa);
            this.Visible = false;
            frm_Principal.ShowDialog();
            this.Dispose();
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {
            if(MessageBox.Show("Aplicativo desabilitado, utilize o novo app!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                var link = "http://zerohoravirtual.com/index.php?p=app";
                Process.Start(link);
                Application.Exit();
            }
            var randon = new Random();

            var img = randon.Next(1, 5);

            switch (img)
            {
                case 1:
                    this.BackgroundImage = Properties.Resources._1;
                    break;
                case 2:
                    this.BackgroundImage = Properties.Resources._2;
                    break;
                case 3:
                    this.BackgroundImage = Properties.Resources._3;
                    break;
                case 4:
                    this.BackgroundImage = Properties.Resources._4;
                    break;
                case 5:
                    this.BackgroundImage = Properties.Resources._5;
                    break;
            }





            Lbl_Versao.Text = "Versão: " + Application.ProductVersion;


            var Resultado = ControleVersao.VerificarAtualizacoes();

            if (Resultado.Contains("Erro ao tentar fazer a requisição:"))
            {
                MessageBox.Show("Verifique a sua conexão, para poder continuar utilizando o software", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Btm_Logar.Enabled = false;
            }
            else if (Resultado != String.Empty && !Resultado.Contains("Erro ao tentar fazer a requisição:"))
            {
                MessageBox.Show("Atualize o seu Software para poder continuar Utilizando!", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Btm_Logar.Enabled = false;
                Process.Start(Resultado);
            }

            Txt_Login.Text = Motorista.ObterUltimoLogin();
        }

        private void btn_Cadastrar_Click(object sender, EventArgs e)
        {
            var frm_Cadastro = new Frm_Cadastro();

            frm_Cadastro.ShowDialog();
        }

        private void Txt_Senha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                logar();
        }

        private void Txt_Login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                logar();    
        }
    }
}

using SistemaCadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SistemaCadastro
{
    public partial class Form1 : Form
    {
        List<Pessoa> pessoas;
        public Form1()
        {
            InitializeComponent();

            pessoas = new List<Pessoa>();

            string[] opcoesCombo = { "Solteiro(a)", "Casado(a)","Viuvo(a)","Separado(a)" };
            comboEC.Items.AddRange(opcoesCombo);
            comboEC.SelectedIndex = 0;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
        
            var idPessoa = Convert.ToInt32(txtIdPessoa.Text);

            var pessoaExists = pessoas.FirstOrDefault(a => a.Id == idPessoa);
       
            if (!verificarCamposPreenchidos()) return;

            char sexo;

            if (radioM.Checked)
            {
                sexo = 'M';
            }
            else if (radioF.Checked)
            {
                sexo = 'F';
            }
            else
            {
                sexo = 'O';
            }

           

            if (pessoaExists == null)
            {
                Pessoa p = new Pessoa()
                {
                    Nome = txtNome.Text,
                    DataNascimento = txtData.Value,
                    Telefone = txtTelefone.Text,
                    CasaPropria = checkCasa.Checked,
                    Veiculo = checkVeiculo.Checked,
                    EstadoCivil = comboEC.SelectedItem.ToString(),
                    Sexo = sexo
                };
                p.Id = pessoas.Count + 1;
                pessoas.Add(p);
                MessageBox.Show("Cadastro Criado!", "Sucesso");
            }
            else
            {
                pessoaExists.Nome = txtNome.Text;
                pessoaExists.DataNascimento = txtData.Value;
                pessoaExists.Telefone = txtTelefone.Text;
                pessoaExists.CasaPropria = checkCasa.Checked;
                pessoaExists.Veiculo = checkVeiculo.Checked;
                pessoaExists.EstadoCivil = comboEC.SelectedItem.ToString();
                pessoaExists.Sexo = sexo;
                MessageBox.Show("Cadastro Alterado!", "Sucesso");
            }            
          
            btnLimpar_Click(btnLimpar, EventArgs.Empty);
      
            ListarPessoas();
        }

        private bool verificarCamposPreenchidos()
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Preencha o campo nome", "Ops..");
                txtNome.Focus();
                return false;
            }

            if (!txtTelefone.MaskCompleted)
            {
                MessageBox.Show("Preencha o campo telefone", "Ops..");
                txtTelefone.Focus();
                return false;
            }

            var sexoSelecionado = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);
            if (sexoSelecionado == null)
            {
                MessageBox.Show("Selecione o sexo", "Ops..");
                return false;
            }

            return true;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int indice = lista.SelectedIndex;
            if (indice < 0) {
                MessageBox.Show("Selecione um cadastro para excluir", "Ops..");
                return;
            } 
            pessoas.RemoveAt(indice);
            ListarPessoas();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtIdPessoa.Text = "0";
            txtNome.Text = "";
            txtData.Text = "";
            txtTelefone.Text = "";
            comboEC.SelectedIndex = 0;
            checkCasa.Checked = false;
            checkVeiculo.Checked = false;

            radioM.Checked = false;
            radioF.Checked = false;
            radioO.Checked = false;

            txtNome.Focus();
        }

        private void ListarPessoas()
        {
            lista.Items.Clear();

            foreach (var p in pessoas)
            {
                lista.Items.Add($"{p.Nome} - Sexo: {(p.Sexo == 'M' ? "Masculino" : p.Sexo == 'F' ? "Feminino" : "Outros")} - Estado Civil: {p.EstadoCivil} - Data de Nascimento: {p.DataNascimento.ToShortDateString()}");
            }
        }

        private void lista_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int indice = lista.SelectedIndex;
            if (indice < 0) return;
            Pessoa p = pessoas[indice];

            txtNome.Text = p.Nome;
            txtData.Value = p.DataNascimento;
            txtTelefone.Text = p.Telefone;
            comboEC.SelectedIndex = comboEC.Items.IndexOf(p.EstadoCivil);
            checkCasa.Checked = p.CasaPropria;
            checkVeiculo.Checked = p.Veiculo;
            txtIdPessoa.Text = p.Id.ToString();

            radioM.Checked = p.Sexo == 'M' ? true : false;
            radioF.Checked = p.Sexo == 'F' ? true : false;
            radioO.Checked = p.Sexo == 'O' ? true : false;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            var filtro = txtFiltro.Text.ToUpper();
            var pessoasFiltradas = pessoas.FindAll(a => a.Nome.ToUpper().Contains(filtro));            
            lista.Items.Clear();
            foreach (var p in pessoasFiltradas)
            {
                lista.Items.Add($"{p.Nome} - Sexo: {(p.Sexo == 'M' ? "Masculino" : p.Sexo == 'F' ? "Feminino" : "Outros")} - Estado Civil: {p.EstadoCivil} - Data de Nascimento: {p.DataNascimento.ToShortDateString()}");
            }
        }
    }
}

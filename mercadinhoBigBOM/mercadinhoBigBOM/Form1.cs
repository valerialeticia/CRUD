using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace mercadinhoBigBOM
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        //botao inserir
        private void button1_Click(object sender, EventArgs e)
        {
            //conexao banco de dados
            try
            {
                //passa a string de conexao
                MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;User Id=root;database=mercadinho;password=@Solemar157");
                //abre o banco de dados
                objcon.Open();
                //comando mysql para inserir dados na tabela
                MySqlCommand objCmd = new MySqlCommand("insert into tb_dados(txt_id, txt_nome, txt_endereco, txt_cidade) values(null, ?, ?, ?)", objcon);
                //parametros de comando
                objCmd.Parameters.Add("@txt_id", MySqlDbType.VarChar, 60).Value = txtNome.Text;
                objCmd.Parameters.Add("@txt_endereco", MySqlDbType.VarChar, 50).Value = txtEndereco.Text;
                objCmd.Parameters.Add("@txt_cidade", MySqlDbType.VarChar, 30).Value = txtCidade.Text;
                //comando para executar o query
                objCmd.ExecuteNonQuery();

                MessageBox.Show("Cadastrado com sucesso!!");
                
                //fecha o banco de dados
                objcon.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Nao conectado:" + erro);
            }
        }
        //botao do datagrid
        private void button1_Click_1(object sender, EventArgs e)
        {
            //passa a string de conexao novamente
            MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;User Id=root;database=mercadinho;password=@Solemar157");
            //cria um objeto data table 
            DataTable dt = new DataTable();
            //a funcao data adapter adapta os dados do bd para o vs
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tb_dados", objcon);
            objcon.Open();
            //adiciona ou atualiza os dados que ficam na memoria ou seja no dataset
            da.Fill(dt);
            //preenche o data grid
            DG_mercadinho.DataSource = dt;
            objcon.Close();


        }

        //botao deletar
        private void btn_deletar_Click(object sender, EventArgs e)
        {
            try
            {
               
                MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;User Id=root;database=mercadinho;password=@Solemar157");
                objcon.Open();
                //comando mysql com seus devidos parametros para deletar dados da tabela
                MySqlCommand objCmd = new MySqlCommand("delete from tb_dados where txt_id  = ?", objcon);
                //da um clear para evitar problemas
                objCmd.Parameters.Clear();
                //adiconar parametros
                objCmd.Parameters.Add("@txt_id", MySqlDbType.Int32).Value = txtId.Text;
                
                //para saber qual tipo de comando ira usar e executa o comando
                objCmd.CommandType = CommandType.Text;
                objCmd.ExecuteNonQuery();
                //fecha a conexao
                objcon.Close();
                MessageBox.Show("Registro deletado com sucesso!!");
                


            }
            catch (Exception erro)
            {
                MessageBox.Show("Nao foi possivel deletar:" + erro);
            }
        }

        //botao de buscar
        private void button1_Click_2(object sender, EventArgs e)
        {
            try
            {
                //passa a string de conexao
                MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;User Id=root;database=mercadinho;password=@Solemar157");
                //abre o banco de dados
                objcon.Open();
                MySqlCommand objCmd = new MySqlCommand("select txt_nome, txt_endereco, txt_cidade from tb_dados where txt_id = ?", objcon);
                //da um clear para evitar problemas
                objCmd.Parameters.Clear();
                //adiconar parametros
                objCmd.Parameters.Add("@txt_id", MySqlDbType.Int32).Value = txtId.Text;
                //para saber qual tipo de comando ira usar e executa o comando
                objCmd.CommandType = CommandType.Text;
                //recebe o conteudo que vem do banco
                MySqlDataReader dr;
                dr = objCmd.ExecuteReader();
                //faz a leitura para o que veio para o dr e tambem insere as informacoes recebidas pelo banco
                dr.Read();

                txtNome.Text = dr.GetString(0);
                txtEndereco.Text = dr.GetString(1);
                txtCidade.Text = dr.GetString(2);


                //fecha a conexao
                objcon.Close();

            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao buscar o registro!" + erro);
            }
        }

        //botao de alterar
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //passa a string de conexao
                MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;User Id=root;database=mercadinho;password=@Solemar157");
                //abre o banco de dados
                objcon.Open();
                MySqlCommand objCmd = new MySqlCommand("update tb_dados set txt_nome = ?, txt_endereco = ?, txt_cidade = ? where txt_id = ?", objcon);
                //da um clear para evitar problemas
                objCmd.Parameters.Clear();
                //passando os parametros
                objCmd.Parameters.Add("@txt_nome", MySqlDbType.VarChar, 60).Value = txtNome.Text;
                objCmd.Parameters.Add("@txt_endereco", MySqlDbType.VarChar, 50).Value = txtEndereco.Text;
                objCmd.Parameters.Add("@txt_cidade", MySqlDbType.VarChar, 30).Value = txtCidade.Text;
                objCmd.Parameters.Add("@txt_id", MySqlDbType.Int32).Value = txtId.Text;

                //executando o comando
                objCmd.CommandType = CommandType.Text;
                objCmd.ExecuteNonQuery();
                
                //fecha a conexao
                objcon.Close();

                MessageBox.Show("Alteracao feita com sucesso!!");

            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível alterar o registro" + erro);
            }
        }
    }
}

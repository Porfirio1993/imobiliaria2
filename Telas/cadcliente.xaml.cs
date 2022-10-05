using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using imobiliaria;
namespace imobiliaria
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public string operacao;
        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {

            using (imobiliariaEntities1 ctx = new imobiliariaEntities1())
            {
                this.AlterarBotao(3);
                cliente c;
                if (operacao == "alterar")
                {

                    //txtcodigo.IsEnabled = true;
                    //txtCPF.IsEnabled = true;
                    //txtNome.IsEnabled = true;
                    //txtsenha.IsEnabled = true;
                    //txtusuario.IsEnabled = true;
                    //radiativo.IsEnabled = true;
                    //radidesativo.IsEnabled = true;
                    //radifem.IsEnabled = true;
                    //radimasc.IsEnabled = true;
                    c = ctx.cliente.Find(Convert.ToInt32(txtcodigo.Text)); // indo no banco e buscando o cliente.
                    MessageBox.Show("Alterado com sucesso");
                }
                else
                {
                    c = new cliente(); // criando novo cliente
                    MessageBox.Show("Salvo com sucesso");


                }

                c.nome_completo = txtNome.Text;
                c.cpf = txtCPF.Text; // preciso colocar mascara no cpf
                c.data_nascimento = dtanascimento.SelectedDate.Value.Date;
                c.sexo = radimasc.IsChecked == true ? "M" : "F";
                if (rbCliente.IsChecked == true)
                {
                    c.tipo_de_pessoa = "C";
                }
                else
                {
                    c.tipo_de_pessoa = "F";
                }

                c.login = txtusuario.Text;
                c.senha = txtsenha.Text;
                if(c.id == 0)
                {
                    ctx.cliente.Add(c);
                }

                if (radiativo.IsChecked == true)
                {
                    c.status = true;
                }
                else
                {
                    c.status = false;
                }
                ctx.SaveChanges();
                
                this.LimpaCampos();
                this.ListarClientes();
            }

           
                    
        }  private void LimpaCampos() // vai limpar o que esta na tela.
        {
            txtcodigo.Clear();
            txtcodigo.Clear();
            txtNome.Clear();
            txtCPF.Clear();   
            txtusuario.Clear();
            txtsenha.Clear();

        } 
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Item da linha

            cliente c = new cliente();

            txtNome.Text = c.nome_completo;

            if (c.tipo_de_pessoa == "C")
            {
                rbCliente.IsChecked = true;
                rbFunc.IsChecked = false;
            }
            else
            {
                rbCliente.IsChecked = false;
                rbFunc.IsChecked = true;
            } 
            if(c.status == true)
            {
                radidesativo.IsChecked = false;
                radiativo.IsChecked = true;
            }
            else
            {
                radiativo.IsChecked = false;
                radidesativo.IsChecked = true;
            }
            
        }

        private void radiativo_Checked(object sender, RoutedEventArgs e)
        {
            var opcaoEscolhida = (RadioButton)sender;

        }

        
        private void btnalterar_Click(object sender, RoutedEventArgs e)
        {
            //this.AlterarBotao(2);
            operacao = "alterar";
                             
             
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        //chamando a lista de cliente para aparecer na gridlist
        {
            
            this.ListarClientes();
        }
        private void ListarClientes()//metodo lista cliente criado para sempre que tiver atualização (salvar, alterar, excluir) ele vai ser chamado.
        {
            using (imobiliariaEntities1 ctx = new imobiliariaEntities1())
            //convertendo a variavel consulta que é db para exibir em lista
            {
                var consulta = ctx.cliente.ToList();
                gridlist.ItemsSource = consulta;
            }
            //importante: Quando colocar variavel var ela assumi o tipo quando recebe os dados.
        }
        private void AlterarBotao(int op)
        {
            btnalterar.IsEnabled = false;
            if (op == 1)
            {
                btnalterar.IsEnabled = true;
            }
            if (op == 2) // Habilita opção alterar e excluir
            {
                btnalterar.IsEnabled = true;
                btnexcluir.IsEnabled = true;
                btnGravar.IsEnabled = true;
                btnlocalizar.IsEnabled = false;
                btncancelar.IsEnabled = false;
            }
            if(op == 3)//habilita a função cancelar e gravar
            {
                btncancelar.IsEnabled = true;
                btnGravar.IsEnabled = true;
                txtcodigo.IsEnabled = false;
                btnalterar.IsEnabled=false;
                btnexcluir.IsEnabled=false;
            }
            if(op == 4)
            {
                txtcodigo.IsEnabled = false;
                txtCPF.IsEnabled = false;
                txtNome.IsEnabled = false;
                txtsenha.IsEnabled = false;
                txtusuario.IsEnabled = false;
                radiativo.IsEnabled = false;
                radidesativo.IsEnabled = false;
                radifem.IsEnabled = false;
                radimasc.IsEnabled = false;
            }

        }

        private void btnexcluir_Click(object sender, RoutedEventArgs e)
        {
            using (imobiliariaEntities1 ctx = new imobiliariaEntities1())
            //excluindo o cadastro
            {
                cliente c = ctx.cliente.Find(Convert.ToInt32(txtcodigo.Text));
                if (c != null)
                {

                    ctx.cliente.Remove(c);
                    ctx.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Favor selecionar um cliente!");
                }
                this.ListarClientes();
                this.LimpaCampos();
                this.AlterarBotao(3);
                
      
            }


        }

        private void btnlocalizar_Click(object sender, RoutedEventArgs e)
        {
            

            if (txtNome.Text.Trim().Count() > 0)//Para pesquisar com nome se for maior que zero caracter entra no if
            {

                try
                {
                    using (imobiliariaEntities1 ctx = new imobiliariaEntities1())
                    {
                        var consulta = from c in ctx.cliente
                                       where c.nome_completo.Contains(txtNome.Text)// cria a variavel consulta e olha "c" um contato  onde tiver na tabela cliente "ctx.cliente" o nome_completo igual ao txtnome.text digitado.
                                       select c;// ao selecionar dentro de "c" joga dentro da variavel consulta.
                        gridlist.ItemsSource = consulta.ToList();
                    }


                }
                catch { }
               
                
            }
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            this.LimpaCampos();//quando clicar em cancelar vai limpar os campos
            Close();
        }

        private void gridlist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            

            if (gridlist.SelectedIndex >= 0)//Se dentro da minha grid tiver algo selecionado sera maior que zero e entra no if
            {
                cliente c = (cliente)gridlist.Items[gridlist.SelectedIndex];
                //cliente c = (cliente)gridlist.SelectedItem;//selecionando com selecteditem uma linha e exibindo para alterar na tela
                txtcodigo.Text = c.id.ToString(); //aqui estou levando os dados da linha para cada item da tela
                txtNome.Text = c.nome_completo;
                txtCPF.Text = c.cpf;
                txtusuario.Text = c.login;
                txtsenha.Text = c.senha;
                dtanascimento.Text = c.data_nascimento.ToString();
                if(c.status == true)
                {
                    radiativo.IsChecked = true;
                    radidesativo.IsChecked = false;
                }
                else
                {
                    radidesativo.IsChecked = true;
                    radiativo.IsChecked = false;

                }

                //problema para exibir se esta ativo e se é masc ou fem
                this.AlterarBotao(2);
            }
            //this.AlterarBotao(4);
        }

        void MaskedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txtbox  = sender as TextBox;
            txtbox.CaretIndex = txtbox.Text.Length;
            var txt = txtbox.Text.Replace(".","");
            txt = txt.Replace("-","");

            if (txtbox != null && txtbox.Text.Length > 0)
            {
                txtbox.Text = Convert.ToUInt64(txt).ToString(@"000\.000\.000\-00").PadLeft(11, '0');
            }
        }
    }
}

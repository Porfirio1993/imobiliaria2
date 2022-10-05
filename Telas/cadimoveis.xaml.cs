using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace imobiliaria
{
    /// <summary>
    /// Lógica interna para cadimoveis.xaml
    /// </summary>
    public partial class cadimoveis : Window
    {
        public string operacao;
        public cadimoveis()
        {
            InitializeComponent();

        }

        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            using (imobiliariaEntities1 ctx = new imobiliariaEntities1())
            {


                imoveis i;
                if (operacao == "alterar")
                {
                    i = ctx.imoveis.Find(Convert.ToInt32(txtcodigo.Text));//indo no banco e buscando o cliente.
                    MessageBox.Show("Alterado com sucesso");
                    this.Listarimoveis();
                }
                else
                {
                    i = new imoveis();
                    MessageBox.Show("Salvo com sucesso");
                    this.Listarimoveis();
                }
                i.rua = txtEnd.Text;
                i.numero = Convert.ToInt32(txtnum.Text);//convertendo uma string em numero
                i.bairro = txtbairro.Text.Trim();
                i.cep = txtcep.Text;
                i.valor_aluguel = Convert.ToDouble(txtvaloraluguel.Text);
                i.valor_de_venda = Convert.ToDouble(txtValorvenda.Text);
                i.fone = txtfone.Text.Trim();
                i.cidade = txtcidade.Text.Trim();
                i.descricao_imovel = Convert.ToString(txtdescricao);

                if (i.id == 0)
                {
                    ctx.imoveis.Add(i);//adicionando novo imovel.
                }
                ctx.SaveChanges();
                this.Listarimoveis();

            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            imoveis i = new imoveis();
            txtEnd.Text = i.rua;
            txtbairro.Text = i.bairro;
            txtcep.Text = i.cep;
            txtfone.Text = i.fone;
            txtnum.Text = i.numero.ToString();
            txtdescricao.Text = i.descricao_imovel;
            txtvaloraluguel.Text = Convert.ToString(i.valor_aluguel);
            txtValorvenda.Text = Convert.ToString(i.valor_de_venda); //preciso converter o que esta double no bd para receber no txtbox em string.
            
        }

        private void btnalterar_Click(object sender, RoutedEventArgs e)
        {
            this.operacao = "alterar";
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.Listarimoveis();
        }
        private void Listarimoveis()
        {
            using (imobiliariaEntities1 ctx = new imobiliariaEntities1())
            {
                var consulta = ctx.imoveis;
                gridlist.ItemsSource = consulta.ToList();
            }
        }

        private void btnexcluir_Click(object sender, RoutedEventArgs e)
        {
            using (imobiliariaEntities1 ctx = new imobiliariaEntities1())
            {
                imoveis i = ctx.imoveis.Find(Convert.ToInt32(txtcodigo.Text));
                if (i != null)
                {
                    ctx.imoveis.Remove(i);
                    ctx.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Favor selecionar imovel");
                }
                this.Listarimoveis();
            }
        }

        private void btnlocalizar_Click(object sender, RoutedEventArgs e)
        {
            if (txtEnd.Text.Trim().Count() > 0)
            {
                try
                {
                    using (imobiliariaEntities1 ctx = new imobiliariaEntities1())
                    {
                        var consulta = from i in ctx.imoveis
                                       where i.rua.Contains(txtEnd.Text)
                                       select i;
                        gridlist.ItemsSource = consulta.ToList();

                    }
                }
                catch { }
            }
        }

        

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void gridlist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            

            if (gridlist.SelectedIndex >= 0)//Se dentro da minha grid tiver algo selecionado sera maior que zero e entra no if
            {
                imoveis i = (imoveis)gridlist.Items[gridlist.SelectedIndex];
                //cliente c = (cliente)gridlist.SelectedItem;//selecionando com selecteditem uma linha e exibindo para alterar na tela
                txtcodigo.Text = i.id.ToString(); //aqui estou levando os dados da linha para cada item da tela
                txtEnd.Text = i.rua;
                txtbairro.Text = i.bairro;
                txtnum.Text = i.numero.ToString();
                txtcep.Text = i.cep;
                txtvaloraluguel.Text = Convert.ToString(i.valor_aluguel);
                txtValorvenda.Text = Convert.ToString(i.valor_de_venda);
                txtfone.Text = i.fone;
                txtdescricao.Text = i.descricao_imovel;
                txtcidade.Text = i.cidade;
                



            }
        }
    }
}

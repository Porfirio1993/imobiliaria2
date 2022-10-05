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
    /// Lógica interna para Cadastro.xaml
    /// </summary>
    public partial class Cadastro : Window
    {
        public Cadastro()
        {
            InitializeComponent();
        }

        private void btnimoveis_Click(object sender, RoutedEventArgs e)
        {
            cadimoveis cadastroimoveis = new cadimoveis();
            cadastroimoveis.ShowDialog();
        }

        private void btncliente_Click(object sender, RoutedEventArgs e)
        {
            MainWindow cadastrocliente = new MainWindow();
            cadastrocliente.ShowDialog();
        }
    }
}

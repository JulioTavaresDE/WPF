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

namespace AppWPF
{
    /// <summary>
    /// Lógica interna para FormAlunos.xaml
    /// </summary>
    public partial class FormAlunos : Window
    {
        public FormAlunos()
        {
            InitializeComponent();
            EstadoCivilComboBox.ItemsSource = Enum.GetValues(typeof(Model.EstadoCivil)).Cast<Model.EstadoCivil>();
            StatusMatriculaComboBox.ItemsSource = Enum.GetValues(typeof(Model.StatusMatricula)).Cast<Model.StatusMatricula>();
            SexoComboBox.ItemsSource = Enum.GetValues(typeof(Model.Sexo)).Cast<Model.Sexo>();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}

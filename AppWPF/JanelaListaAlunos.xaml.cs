using System.Windows;


namespace AppWPF
{
    /// <summary>
    /// Interação lógica para JanelaListaAlunos.xam
    /// </summary>
    public partial class JanelaListaAlunos : Window
    {
        public JanelaListaAlunos()
        {
            InitializeComponent();
            DataContext = new ViewModel.AlunosViewModel();
        }
    }
}

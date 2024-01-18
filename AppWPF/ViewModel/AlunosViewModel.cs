using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using AppWPF.Repositories;
using Dapper;

namespace AppWPF.ViewModel
{
    public class AlunosViewModel : BaseNotifyPropertyChanged
    {
        public System.Collections.ObjectModel.ObservableCollection<Model.Aluno> Alunos { get; private set; }

        private Model.Aluno _AlunoSelecionado;
        public Model.Aluno AlunoSelecionado
        {
            get { return _AlunoSelecionado; }
            set
            {
                SetField(ref _AlunoSelecionado, value);
                Deletar.RaiseCanExecuteChanged();
                Editar.RaiseCanExecuteChanged();
            }
        }
        public AlunosViewModel()
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    string sql = @" select * from Alunos
                        ";
                    Alunos = new System.Collections.ObjectModel.ObservableCollection<Model.Aluno>();
                    dynamic param = new ExpandoObject();
                    List<Model.Aluno> list = Repository.GetList<Model.Aluno>(sql, param, conn, tran);
                    foreach (var dto in list)
                    {
                        Alunos.Add(dto);
                    }
                }
            }
        }

        public DeletarCommand Deletar { get; private set; } = new DeletarCommand();

        public class DeletarCommand : BaseCommand
        {
            public override bool CanExecute(object parameter)
            {
                var viewModel = parameter as AlunosViewModel;
                return viewModel != null && viewModel.AlunoSelecionado != null;
            }

            public override void Execute(object parameter)
            {
                var viewModel = (AlunosViewModel)parameter;
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        string sql = @" Delete alunos where Id = @Id
                        ";
                        dynamic param = new ExpandoObject();
                        param.Id = viewModel.AlunoSelecionado.Id;
                        Repository.Execute(sql, param, conn, tran);
                        tran.Commit();
                    }
                }
                
                viewModel.Alunos.Remove(viewModel.AlunoSelecionado);
                viewModel.AlunoSelecionado = viewModel.Alunos.FirstOrDefault();

            }
        }

        public EditarCommand Editar { get; private set; } = new EditarCommand();

        public class EditarCommand : BaseCommand
        {
            public override bool CanExecute(object parameter)
            {
                var viewModel = parameter as AlunosViewModel;
                return viewModel != null && viewModel.AlunoSelecionado != null;
            }

            public override void Execute(object parameter)
            {
                var viewModel = (AlunosViewModel)parameter;
                var cloneAlunos = (Model.Aluno)viewModel.AlunoSelecionado.Clone();
                var aw = new FormAlunos();
                aw.DataContext = cloneAlunos;
                aw.ShowDialog();

                if (aw.DialogResult.HasValue && aw.DialogResult.Value)
                {
                    viewModel.AlunoSelecionado.DataMatricula = cloneAlunos.DataMatricula;
                    viewModel.AlunoSelecionado.DataNascimento = cloneAlunos.DataNascimento;
                    viewModel.AlunoSelecionado.EstadoCivil = cloneAlunos.EstadoCivil;
                    viewModel.AlunoSelecionado.Nome = cloneAlunos.Nome;
                    viewModel.AlunoSelecionado.Sexo = cloneAlunos.Sexo;
                    viewModel.AlunoSelecionado.StatusMatricula = cloneAlunos.StatusMatricula;

                    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                    {
                        conn.Open();
                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            string sql = @" update Alunos set
                                
                                Nome = @Nome,
                                DataNascimento = @DataNascimento,
                                Sexo = @Sexo,
                                DataMatricula = @DataMatricula,
                                EstadoCivil = @EstadoCivil,
                                StatusMatricula = @StatusMatricula
                            
where Id = @Id
                        ";
                            /*dynamic param = new ExpandoObject();
                            param.Id = viewModel.AlunoSelecionado.Id;*/
                            Repository.Execute(sql, viewModel.AlunoSelecionado, conn, tran);
                            tran.Commit();
                        }
                    }


                }

            }
        }

        public NovoCommand Novo { get; private set; } = new NovoCommand();

        public class NovoCommand : BaseCommand
        {
            public override bool CanExecute(object parameter)
            {
                return parameter is AlunosViewModel;
            }

            public override void Execute(object parameter)
            {
                var viewModel = (AlunosViewModel)parameter;
                var aluno = new Model.Aluno();
                aluno.DataMatricula = DateTime.Now;
                aluno.DataNascimento = DateTime.Now;
                var maxID = 0;
                if (viewModel.Alunos.Any())
                {
                    maxID = viewModel.Alunos.Max(a => a.Id);
                }
                aluno.Id = maxID + 1;

                var aw = new FormAlunos();
                aw.DataContext = aluno;
                aw.ShowDialog();

                if (aw.DialogResult.HasValue && aw.DialogResult.Value)
                {
                    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                    {
                        conn.Open();
                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            string sql = @" Insert into Alunos Values
                                (@Id,
                                @Nome,
                                @DataNascimento,
                                @Sexo,
                                @DataMatricula,
                                @EstadoCivil,
                                @StatusMatricula )
                        ";

                            Repository.Execute(sql, aluno, conn, tran);
                            tran.Commit();
                        }
                    }

                    viewModel.Alunos.Add(aluno);
                    viewModel.AlunoSelecionado = aluno;
                }


            }

        }
    }
}

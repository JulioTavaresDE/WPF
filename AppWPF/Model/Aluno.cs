using System;

namespace AppWPF.Model
{
    public class Aluno:BaseNotifyPropertyChanged, ICloneable
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetField(ref _id, value); }
        }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { SetField(ref _nome, value); }
        }

        private DateTime _dataNascimento;
        public DateTime DataNascimento
        {
            get { return _dataNascimento; }
            set { SetField(ref _dataNascimento, value); }
        }

        private string _sexo;
        public string Sexo
        {
            get { return _sexo; }
            set { SetField(ref _sexo, value); }
        }

        private DateTime _dataMatricula;
        public DateTime DataMatricula
        {
            get { return _dataMatricula; }
            set { SetField(ref _dataMatricula, value); }
        }

        private EstadoCivil _estadoCivil;
        public EstadoCivil EstadoCivil
        {
            get { return _estadoCivil; }
            set {  SetField(ref _estadoCivil, value); }
        }

        private StatusMatricula _statusMatricula;
        public StatusMatricula StatusMatricula
        {
            get { return _statusMatricula; }
            set { SetField(ref _statusMatricula, value); }
        }

        public object Clone() {
            return this.MemberwiseClone();
        }


    }
}

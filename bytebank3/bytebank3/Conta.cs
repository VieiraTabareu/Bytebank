namespace ByteBank
{
    internal class Conta
    {
        private string? nome;
        private string? cpf;
        private string? senha;
        private double saldo;
        private int numConta;

        public Conta(string? nome, string? cpf, string? senha, double saldo, int numConta)
        {
            this.nome = nome;
            this.cpf = cpf;
            this.senha = senha;
            this.saldo = saldo;
            this.numConta = numConta;
        }

        public int NumConta { get; internal set; }
        public object Titular { get; internal set; }
        public double Saldo { get; internal set; }
        public string? Senha { get; internal set; }

        internal void Deposito(double valor)
        {
            throw new NotImplementedException();
        }

        internal void Saque(double valor)
        {
            throw new NotImplementedException();
        }

        internal void Transferencia(Conta contaOrigem, Conta contaDestino, double valorTransferencia)
        {
            throw new NotImplementedException();
        }
    }
}
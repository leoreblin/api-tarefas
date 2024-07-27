namespace Thunders.Tasks.Core.Errors
{
    public class Error
    {
        public string Mensagem { get; }

        public Error(string mensagem) => Mensagem = mensagem;

        public static readonly Error None = new(string.Empty);
    }
}

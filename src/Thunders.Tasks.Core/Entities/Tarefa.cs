namespace Thunders.Tasks.Domain.Entities
{
    public class Tarefa : BaseEntity
    {
        public string Nome { get; } = default!;
        public string? Descricao { get; }
        public bool EstaCompleta { get; private set; }
        public DateTime DataCriacao { get; }
        public DateTime? DataCompletou { get; private set; }

        private Tarefa(
            string nome,
            string? descricao,
            bool estaCompleta,
            DateTime dataCriacao) : base(Guid.NewGuid())
        {
            Nome = nome;
            Descricao = descricao;
            EstaCompleta = estaCompleta;
            DataCriacao = dataCriacao;
        }

        public static Tarefa Criar(string nome, string? descricao = "")
        {
            var dataCriacao = DateTime.Now;
            return new Tarefa(nome, descricao, estaCompleta: false, dataCriacao);
        }

        public void Completar()
        {
            if (EstaCompleta)
            {
                return;
            }

            EstaCompleta = true;
            DataCompletou = DateTime.Now;
        }
    }
}

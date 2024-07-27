namespace Thunders.Tasks.Core.Errors
{
    public static class TarefaError
    {
        public static readonly Error NaoEncontrada = new("Tarefa não encontrada");

        public static readonly Error ListaVazia = new("Nenhuma tarefa encontrada");

        public static readonly Error NomeObrigatorio = new("Nome da tarefa é obrigatório");
    }
}

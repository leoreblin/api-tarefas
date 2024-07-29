using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.Tasks.Domain.Entities;

namespace Thunders.Tasks.Tests.Core
{
    public class TarefaTests
    {
        [Fact]
        public void Criar_DeveRetornarTarefaCriada()
        {
            // Arrange
            var nome = "Tarefa Fake";
            var descricao = "Descrição Tarefa Fake";

            // Act
            var tarefaCriada = Tarefa.Criar(nome, descricao);

            // Assert
            tarefaCriada.Nome.Should().Be(nome);
            tarefaCriada.Descricao.Should().Be(descricao);
        }

        [Fact]
        public void Completar_DeveCompletarTarefa()
        {
            // Arrange
            var nome = "Tarefa Fake";
            var descricao = "Descrição Tarefa Fake";
            var tarefaCriada = Tarefa.Criar(nome, descricao);

            // Act
            tarefaCriada.Completar();

            // Assert
            tarefaCriada.EstaCompleta.Should().BeTrue();

            tarefaCriada.DataCompletou.Should().BeCloseTo(
                DateTime.Now,
                precision: TimeSpan.FromSeconds(3));
        }

        [Fact]
        public void Completar_DeveRetornarQuandoTarefaJaEstiverCompleta()
        {
            // Arrange
            var nome = "Tarefa Fake";
            var descricao = "Descrição Tarefa Fake";
            var tarefaCriada = Tarefa.Criar(nome, descricao);
            tarefaCriada.Completar();
            Thread.Sleep(TimeSpan.FromSeconds(8));

            // Act
            tarefaCriada.Completar();

            // Assert
            tarefaCriada.EstaCompleta.Should().BeTrue();

            tarefaCriada.DataCompletou.Should().BeCloseTo(
                DateTime.Now.AddSeconds(-8), TimeSpan.FromSeconds(1));
        }
    }
}

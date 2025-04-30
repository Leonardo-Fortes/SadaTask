using Sada.Core.Entities;

namespace Sada.Tests;

[TestClass]

    public sealed class TaskSadaTests
    {
        [TestMethod]
        public void Deve_retornar_erro_quando_expirationDate_for_menor_ou_igual_ao_presente()
        {
          
            var dataExpirada = DateTime.UtcNow.AddMinutes(-10);

            
            var tarefa = new TaskSada("Tarefa Expirada", "Descrição", dataExpirada, 0);

            Assert.IsFalse(tarefa.IsValid, "A tarefa deveria ser inválida");
            Assert.IsTrue(tarefa.Notifications.Any(n => n.Key == "ExpirationDate"), "Deveria haver uma notificação sobre a data de expiração");
    }

        [TestMethod]
        public void Deve_retornar_valido_quando_data_for_no_futuro()
        {
       
            var dataValida = DateTime.UtcNow.AddMinutes(10);

            var tarefa = new TaskSada("Tarefa Válida", "Descrição", dataValida, 0);

            Assert.IsTrue(tarefa.IsValid, "A tarefa deveria ser válida");
            Assert.AreEqual(0, tarefa.Notifications.Count);
        }
    }

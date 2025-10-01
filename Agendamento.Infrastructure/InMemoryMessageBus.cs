using Agendamento.Domain.Interfaces;
namespace Agendamento.Infrastructure;

    public class InMemoryMessageBus : IMessageBus
    {
        public event Action<object> OnEventPublished;
        public void Publish<T>(T @evento)
        {
            OnEventPublished?.Invoke(@evento);
        }

    }


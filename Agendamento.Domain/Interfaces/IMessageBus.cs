namespace Agendamento.Domain.Interfaces;

    public interface IMessageBus
    {
        event Action<object> OnEventPublished;
        void Publish<T>(T @event);
    }


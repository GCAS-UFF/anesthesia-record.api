using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.EventTypes;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Service.Mappers;

namespace UFF.FichaAnestesica.Service.Services
{
    public class EventTypeService : IEventTypeService
    {
        private readonly IEventTypeRepository _eventTypeRepository;

        public EventTypeService(IEventTypeRepository eventTypeRepository)
        {
            _eventTypeRepository = eventTypeRepository;
        }

        public async Task<CommandResult> GetPagedForAdminAsync(string? term, int page, int size)
        {
            var (items, totalItems) = await _eventTypeRepository.GetPagedAsync(term, page, size);

            return CommandResult.Success(new PagedResponse<EventTypeResponse>
            {
                Data = EventTypeResponseMapper.Map(items),
                TotalItems = totalItems,
                Page = page,
                PageSize = size,
                HasNext = page * size < totalItems
            });
        }

        public async Task<CommandResult> GetActiveAsync()
        {
            var eventTypes = await _eventTypeRepository.GetActiveAsync();
            return CommandResult.Success(EventTypeResponseMapper.Map(eventTypes));
        }

        public async Task<CommandResult> CreateAsync(CreateEventTypeCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
                return CommandResult.Fail("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(command.Description))
                return CommandResult.Fail("Descrição é obrigatória");

            if (await _eventTypeRepository.ExistsByNameAsync(command.Name))
                return CommandResult.Fail("Já existe um evento com esse nome");

            var eventType = Domain.Entities.EventType.Create(command.Name, command.Description);
            await _eventTypeRepository.AddAsync(eventType);
            await _eventTypeRepository.SaveChangesAsync();

            return CommandResult.Success(EventTypeResponseMapper.Map(eventType));
        }

        public async Task<CommandResult> UpdateAsync(int id, UpdateEventTypeCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
                return CommandResult.Fail("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(command.Description))
                return CommandResult.Fail("Descrição é obrigatória");

            var eventType = await _eventTypeRepository.GetByIdAsync(id);

            if (eventType == null)
                return CommandResult.Fail("Evento não encontrado");

            if (await _eventTypeRepository.ExistsByNameAsync(command.Name, id))
                return CommandResult.Fail("Já existe um evento com esse nome");

            eventType.Update(command.Name, command.Description);

            if (command.Active)
                eventType.Enable();
            else
                eventType.Disable();

            _eventTypeRepository.Update(eventType);
            await _eventTypeRepository.SaveChangesAsync();

            return CommandResult.Success(EventTypeResponseMapper.Map(eventType));
        }
    }
}

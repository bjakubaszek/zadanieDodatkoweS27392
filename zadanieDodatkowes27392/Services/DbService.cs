// Services/IDbService.cs

using Microsoft.EntityFrameworkCore;
using zadanieDodatkowes27392.Data;
using zadanieDodatkowes27392.Exceptions;
using zadanieDodatkowes27392.Models;

public interface IDbService
{
    Task<EventGetDto> CreateEventAsync(EventCreateDto dto);
    Task AddSpeakerToEventAsync(int eventId, int speakerId);
    Task<RegistrationResultDto> RegisterParticipantAsync(int eventId, int participantId);
    Task CancelRegistrationAsync(int eventId, int participantId);
    Task<ICollection<EventGetDto>> GetUpcomingEventsAsync();
    Task<ICollection<ParticipantEventReportDto>> GetParticipantReportAsync(int participantId);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<EventGetDto> CreateEventAsync(EventCreateDto dto)
    {
        
        // 1. Nie mozna do tylu
        // 2. profit
        
        if (dto.Date <= DateTime.UtcNow)
            throw new BadRequestException("Date must be in the future.");
            
        var entity = new Event
        {
            Title = dto.Title,
            Description = dto.Description,
            Date = dto.Date,
            MaxParticipants = dto.MaxParticipants,
        };
        await data.Events.AddAsync(entity);
        await data.SaveChangesAsync();

        return new EventGetDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Date = entity.Date,
            MaxParticipants = entity.MaxParticipants,
            ParticipantCount = 0,
            Speakers = new List<SpeakerShortDto>()
        };
    }

    public async Task AddSpeakerToEventAsync(int eventId, int speakerId)
    {
        
        // 1. Event istnieje
        // 2. speaker istnieje
        // 3. nie ma konfliku
        // 4. profit
        
        
        var ev = await data.Events.FindAsync(eventId) ?? throw new NotFoundException($"Event {eventId} not found");
        var speaker = await data.Speakers.FindAsync(speakerId) ?? throw new NotFoundException($"Speaker {speakerId} not found");

        bool hasConflict = await data.EventSpeakers
            .Include(es => es.Event)
            .AnyAsync(es => es.SpeakerId == speakerId && es.Event.Date == ev.Date);
        if (hasConflict) 
            throw new BadRequestException($"Speaker {speakerId} is already assigned to another event at the same date");

        data.EventSpeakers.Add(new EventSpeaker { EventId = eventId, SpeakerId = speakerId });
        await data.SaveChangesAsync();
    }

    public async Task<RegistrationResultDto> RegisterParticipantAsync(int eventId, int participantId)
    {
        
        // 1. event istnieje
        // 2. Liczba uczestnikow sie zgadza
        // 3. Czy on juz jest zarejestrowany
        // 4. profit
        
        var ev = await data.Events.Include(x => x.EventParticipants).FirstOrDefaultAsync(x => x.Id == eventId) ?? 
            throw new NotFoundException($"Event {eventId} not found");

        if (ev.EventParticipants.Count >= ev.MaxParticipants)
            throw new BadRequestException($"Event {eventId} has reached maximum number of participants");

        bool alreadyRegistered = ev.EventParticipants.Any(ep => ep.ParticipantId == participantId);
        if (alreadyRegistered)
            throw new BadRequestException($"Participant {participantId} already registered for Event {eventId}");

        data.EventParticipants.Add(new EventParticipant
        {
            EventId = eventId,
            ParticipantId = participantId
        });
        await data.SaveChangesAsync();

        return new RegistrationResultDto
        {
            EventId = eventId,
            ParticipantId = participantId,
            Status = "Registered"
        };
    }

    public async Task CancelRegistrationAsync(int eventId, int participantId)
    {
        
        // 1. Event istnieje
        // 2. MOzna cancelowac tlyko 24h przed
        // 3. gosc nie jest zapisany
        // 4. profit
        
        var ev = await data.Events.FindAsync(eventId) ?? 
            throw new NotFoundException($"Event {eventId} not found");

        if (ev.Date <= DateTime.UtcNow.AddHours(24))
            throw new BadRequestException($"Cancellation is only allowed until 24h before the event");

        var entry = await data.EventParticipants.FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.ParticipantId == participantId);
        if (entry is null) 
            throw new NotFoundException($"Participant {participantId} is not registered for Event {eventId}");

        data.EventParticipants.Remove(entry);
        await data.SaveChangesAsync();
    }

    public async Task<ICollection<EventGetDto>> GetUpcomingEventsAsync()
    {
        return await data.Events
            .Where(ev => ev.Date > DateTime.UtcNow)
            .Select(ev => new EventGetDto
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                Date = ev.Date,
                MaxParticipants = ev.MaxParticipants,
                ParticipantCount = ev.EventParticipants.Count,
                Speakers = ev.EventSpeakers.Select(es => new SpeakerShortDto
                {
                    Id = es.SpeakerId,
                    FirstName = es.Speaker.FirstName,
                    LastName = es.Speaker.LastName
                }).ToList()
            })
            .ToListAsync();
    }


    public async Task<ICollection<ParticipantEventReportDto>> GetParticipantReportAsync(int participantId)
    {
        return await data.EventParticipants
            .Where(ep => ep.ParticipantId == participantId)
            .Select(ep => new ParticipantEventReportDto
            {
                EventId = ep.EventId,
                EventTitle = ep.Event.Title,
                Date = ep.Event.Date,
                Speakers = ep.Event.EventSpeakers.Select(es => new SpeakerShortDto
                {
                    Id = es.SpeakerId,
                    FirstName = es.Speaker.FirstName,
                    LastName = es.Speaker.LastName
                }).ToList()
            }).ToListAsync();
    }
}

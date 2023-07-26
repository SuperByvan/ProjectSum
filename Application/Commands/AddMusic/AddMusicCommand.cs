using Application.Common.Models.Bl;
using MediatR;

namespace Application.Commands.AddMusic;

public class AddMusicCommand : IRequest<TrackBl>
{
    public TrackBl TrackBl { get; set; }

    public AddMusicCommand(TrackBl trackBl)
    {
        TrackBl = trackBl;
    }
}
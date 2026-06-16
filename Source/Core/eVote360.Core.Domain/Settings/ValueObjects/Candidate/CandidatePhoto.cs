using eVote360.Core.Domain.Common.CodeErrors;
using System;
using System.IO;
using System.Linq;

namespace eVote360.Core.Domain.Settings.ValueObjects.Candidate
{
    public sealed record CandidatePhoto
    {
        public string? PhotoUrl { get; init; }

        private CandidatePhoto() { }

        public CandidatePhoto(string photoUrl)
        {
            if (string.IsNullOrWhiteSpace(photoUrl))
                throw new ArgumentException(CandidatesError.PhotoInvalid.Description);


        
            string extension = Path.GetExtension(photoUrl).ToLower();
            string[] validExtensions = { ".jpg", ".jpeg", ".png" };


            if (!validExtensions.Contains(extension))
                throw new ArgumentException(CandidatesError.PhotoInvalid.Description);


            PhotoUrl = photoUrl;

        }
    }
}

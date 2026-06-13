using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eVote360.Core.Application.DTOs.Candidates
{
    public record CreateCandidateDto
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public IFormFile? PhotoUrl { get; set; }

    }
}

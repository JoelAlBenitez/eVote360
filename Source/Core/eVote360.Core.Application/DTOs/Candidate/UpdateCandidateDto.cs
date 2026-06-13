using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.DTOs.Candidates
{
    
    public record UpdateCandidateDto 
    {

        public int Id { get; init; }
        public required string Name { get; set; }

        public required string LastName { get; set; }

         public IFormFile? PhotoUrl { get; set; }
    }
}

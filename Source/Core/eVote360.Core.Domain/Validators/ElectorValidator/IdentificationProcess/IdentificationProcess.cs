using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.ServiceValidates.Citizens;
using eVote360.Core.Domain.Contracts.ServiceValidates.Election;
using eVote360.Core.Domain.Contracts.ServiceValidates.Elector.Votes;
using eVote360.Core.Domain.Common.CodeErrors;
using System.Text.RegularExpressions;

namespace eVote360.Core.Domain.Validators.ElectorValidator.IdentificationProcess
{
    public class IdentificationProcess : IIdentificationProcess
    {
     
        private readonly ICitizensServiceValidate _citizensServiceValidate;
        private readonly IVotesValidate _votesValidate;
        private readonly IElectionDomainService _electionDomainService;
        public  List<Error> _errors = new List<Error>();

        public IdentificationProcess (ICitizensServiceValidate citizensServiceValidate,
            IVotesValidate votesValidate,
            IElectionDomainService electionDomainService
            )
        {
            _citizensServiceValidate = citizensServiceValidate;
            _votesValidate = votesValidate;
            _electionDomainService = electionDomainService;
        }

        public async Task<ValidationResult> ValidateComparadIdentificationByImg(string IdentificationImg,
            string IdentificationEntered)
        {
            var errors = new List<Error>();
            if (IdentificationImg == null && IdentificationEntered == null)
            {
                errors.Add(new Error("Identificación no valida", "La indentificación ingresada y procesada no es valida, favor intentelo de nuevo"));
                return ValidationResult.Failure(errors);
            }

            // OCR devuelve todo el texto de la cédula; extraer solo el número (formato DRD: 3-7-1 dígitos)
            var match = Regex.Match(IdentificationImg!, @"\d{3}[-\s]?\d{7}[-\s]?\d{1}");
            if (!match.Success)
            {
                errors.Add(new Error("No se pudo leer la cédula", "No se encontró un número de cédula válido en la imagen. Intente con una imagen más clara."));
                return ValidationResult.Failure(errors);
            }

            // Normalizar igual que el value object: quitar guiones y espacios
            var extractedId = Regex.Replace(match.Value, @"[-\s]", "").Trim();
            var normalizedEntered = Regex.Replace(IdentificationEntered, @"[-\s]", "").Trim();

            var citizenExist = await _citizensServiceValidate.ExistCitizensByIdentification(extractedId);
            if (!citizenExist) errors.Add(CitizenErrors.NoExtisCitizen);

            if (extractedId != normalizedEntered)
                errors.Add(new Error("La identificación de la imagen no es valida", "La identificación de la imagen no coincide con la ingresada anteriormente."));

            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateEnteredIdentification(string Identification)
        {
            var errors = new List<Error>();
            var electivActive = await _electionDomainService.ExistActiveElection();
            if(!electivActive)
            {
                errors.Add(VotesError.ElectoralProcessNoValid);
                return ValidationResult.Failure(errors);
            }

            var citizenExits = await _citizensServiceValidate.ExistCitizensByIdentification(Identification);
            if (!citizenExits) errors.Add(CitizenErrors.NoExtisCitizen);
            
            if (citizenExits)
            {
                var citizenState = await _citizensServiceValidate.CurrentStateOfTheCitizen(null, Identification);
                if (!citizenState) errors.Add(CitizenErrors.CitizentNoActiveOfVote);

                var citizenParticiped = await _votesValidate.ExistVoteByCitizen(Identification);
                if (citizenParticiped) errors.Add(VotesError.ExistVotes);
            }
            
            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }
    }
}

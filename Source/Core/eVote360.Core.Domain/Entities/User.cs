using eVote360.Core.Domain.Enums;
using eVote360.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Entities
{
    public class User
    {
        public int UserId { get; private set; }

        public string UserFirstName { get; private set; }

        public string UserLastName { get; private set; }

        public UserEmail UserEmail { get; private set; }

        public Username Username { get; private set; }

        public UserPassword UserPassword { get; private set; }

        public UserRole UserRole { get; private set; }

        public bool UserState { get; private set; }

        private User() { }

        public User(string userFirstName, string userLastName, UserEmail userEmail, Username username, UserPassword userPassword, UserRole userRole)
        {
            UserFirstName = userFirstName.Trim();
            UserLastName = userLastName.Trim();
            UserEmail = userEmail;
            Username = username;
            UserPassword = userPassword;
            UserRole = userRole;


            if (!Enum.IsDefined(typeof(UserRole), userRole))
                throw new ArgumentException("El rol de usuario no es válido");

            UserState = true;
        }

        public void Deactivate(bool isOnlyActiveAdmin, bool isSelfDeactivating)
        {
            if (isSelfDeactivating)
                throw new InvalidOperationException("No puede desactivar su propio usuario mientras está autenticado.");


            if (UserRole == UserRole.Admin && isOnlyActiveAdmin)
                throw new InvalidOperationException("No se puede desactivar este usuario porque es el único administrador activo del sistema.");

            UserState = false;

        }
    }
}

using FluentValidation;
using Iks.WebApi.Dominio.DTOs;

namespace Iks.WebApi.Aplicacion.Validadores;

public class IksDtoValidador : AbstractValidator<IksDto>
{
    public IksDtoValidador()
    {
        // Validaciones generales
        RuleFor(u => u.IdCliente)
            .NotEmpty().WithMessage("El IdCliente no puede estar vacio.")
            .NotNull().WithMessage("El IdCliente no puede ser nulo.")
            .Must(SoloNumeros).WithMessage("El IdCliente solo puede contener números.");

        RuleFor(u => u.CodigoDeLlave)
            .NotEmpty().WithMessage("El CodigoDeLlave no puede estar vacio.")
            .NotNull().WithMessage("El CodigoDeLlave no puede ser nulo.");

        // Validaciones de registro
        When(u => u.UsuarioQueRegistra != null, () =>
        {
            RuleFor(u => u.UsuarioQueRegistra)
                .NotEmpty().WithMessage("El usuario que registra es obligatorio.")
                .NotNull().WithMessage("El usuario que registra no puede ser nulo.")
                .MaximumLength(80).WithMessage("El usuario que registra no puede tener más de 80 caracteres.")
                .EmailAddress().WithMessage("El correo debe tener un formato válido. (ejemplo@dominio.com)");

            RuleFor(u => u.IpDeRegistro)
                .NotEmpty().WithMessage("La IP de registro es obligatoria.")
                .NotNull().WithMessage("La IP de registro no puede ser nula.")
                .Matches(@"^\d{1,3}(\.\d{1,3}){3}$").WithMessage("La IP de registro debe tener un formato válido.");
        });

        // Validaciones de actualización
        When(u => u.UsuarioQueActualiza != null, () =>
        {
            RuleFor(u => u.UsuarioQueActualiza)
                .NotEmpty().WithMessage("El usuario que actualiza es obligatorio.")
                .NotNull().WithMessage("El usuario que actualiza no puede ser nulo.")
                .MaximumLength(80).WithMessage("El usuario que actualiza no puede tener más de 80 caracteres.")
                .EmailAddress().WithMessage("El correo debe tener un formato válido. (ejemplo@dominio.com)");

            RuleFor(u => u.FechaDeActualizado)
                .NotNull().WithMessage("La fecha de actualización no puede ser nula.")
                .NotEmpty().WithMessage("La fecha de actualización es obligatorio.");

            RuleFor(u => u.HoraDeActualizado)
                .NotNull().WithMessage("La hora de actualización no puede ser nula.")
                .NotEmpty().WithMessage("La fecha de actualización es obligatorio.");

            RuleFor(u => u.IpDeActualizado)
                .NotEmpty().WithMessage("La IP de actualización es obligatoria.")
                .NotNull().WithMessage("La IP de actualización no puede ser nula.")
                .Matches(@"^\d{1,3}(\.\d{1,3}){3}$").WithMessage("La IP de actualización debe tener un formato válido.");
        });
    }

    private bool SoloNumeros(long? telefono)
    {
        return telefono?.ToString().All(char.IsDigit) ?? false;
    }
}
